using Sigo.WebApi.DataEntities;
using Sigo.WebApi.Hubs;
using Sigo.WebApi.Services;
using log4net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sigo.WebApi.Middlewares
{
    /// <summary>
    /// WebSocket中间件
    /// </summary>
    public class WebSocketMiddleware
    {
        /// <summary>
        /// <see cref="IWebSocketClientService"/>WebSocket服务
        /// </summary>
        private readonly IWebSocketClientService _webSocketClientService;

        /// <summary>
        /// HubContext
        /// </summary>
        private readonly IHubContext<ExecOrdersHub> _hubContext;

        /// <summary>
        /// log对象
        /// </summary>
        private readonly ILog _log;

        /// <summary>
        /// 请求委托
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// 医嘱执行WebSocket管道名称
        /// </summary>
        public readonly string _executeOrdersPipelineName;

        /// <summary>
        /// WebSocket接收消息缓存大小
        /// </summary>
        private readonly int _receiveBufferSize;

        /// <summary>
        /// 构造<see cref="WebSocketMiddleware"/>对象
        /// </summary>
        /// <param name="next"><see cref="RequestDelegate"/></param>
        /// <param name="configration"><see cref="IConfiguration"/></param>
        /// <param name="log">log对象</param>
        /// <param name="webSocketClientService"><see cref="IWebSocketClientService"/></param>
        /// <param name="hubContext"><see cref="IHubContext{THub}"/></param>
        public WebSocketMiddleware(RequestDelegate next, IConfiguration configration, ILog log, IWebSocketClientService webSocketClientService, IHubContext<ExecOrdersHub> hubContext)
        {
            _next = next;
            _log = log;
            _hubContext = hubContext;
            _webSocketClientService = webSocketClientService;
            _executeOrdersPipelineName = configration.GetValue<string>("WebSocket:PipelineNames:ExecOrders", "/wsOrder");
            _receiveBufferSize = configration.GetValue("WebSocket:Options:ReceiveBufferSize", 4) * 1024;
        }

        /// <summary>
        /// 处理Http请求
        /// async is important
        /// </summary>
        /// <param name="httpContext"><see cref="HttpContext"/></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext)
        {
            //医嘱执行WebSocket消息
            if (httpContext.Request.Path == _executeOrdersPipelineName)
            {
                if (httpContext.WebSockets.IsWebSocketRequest)
                {
                    try
                    {
                        var webSocket = await httpContext.WebSockets.AcceptWebSocketAsync();
                        var clientId = httpContext.Connection.Id;
                        _webSocketClientService.AddClient(clientId, webSocket);
                        await ListeningAsync(clientId, httpContext, webSocket);
                    }
                    catch (Exception ex)
                    {
                        var errMsg = $"WebSocket[{_executeOrdersPipelineName}] 通信发生异常，异常信息：[{ex.Message}]";
                        _log.Error(errMsg + $"\r\n Trace:{ex}");
                    }
                }
                else
                {
                    httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                }
            }
            else
            {
                await _next(httpContext);
            }
        }

        /// <summary>
        /// 监听WebSocket客户端
        /// </summary>
        /// <param name="fromClientId">WebSocket客户端ID</param>
        /// <param name="context"><see cref="HttpContext"/></param>
        /// <param name="webSocket"><see cref="WebSocket"/>客户端管道</param>
        /// <returns></returns>
        private async Task ListeningAsync(string fromClientId, HttpContext context, WebSocket webSocket)
        {
            _log.Info($"WebSocket client is connected[ClientID={fromClientId} IP={context.Connection.RemoteIpAddress}:{context.Connection.RemotePort}].");

            var buffer = new byte[_receiveBufferSize];
            //等待接收消息
            var result = await webSocket.ReceiveAsync(buffer, CancellationToken.None);
            while (!result.CloseStatus.HasValue)
            {
                var msg = Encoding.UTF8.GetString(buffer, 0, result.Count);
                _log.Info($"Receive message[{msg}] from WebSocket client[ID={context.Connection.Id} IP={context.Connection.RemoteIpAddress}:{context.Connection.RemotePort}].");

                //将消息发送至WebSocket客户端
                BroadcastToWebSocket(new WebSocketMessageEntity() { ClientID = fromClientId, Message = msg });
                //将消息发送至Signalr客户端
                BroadcastToSignalR(new WebSocketMessageEntity() { ClientID = fromClientId, Message = msg });

                //等待接收消息
                result = await webSocket.ReceiveAsync(buffer, CancellationToken.None);
            }

            //WebSocket断开连接
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
            if (_webSocketClientService.RemoveClient(fromClientId))
            {
                _log.Info($"WebSocket client is disconnected[ClientID={fromClientId} IP={context.Connection.RemoteIpAddress}:{context.Connection.RemotePort}].");
            }
        }

        /// <summary>
        /// 广播消息至WebSocket客户端
        /// </summary>
        /// <param name="messageEntity">WebSocket消息实体</param>
        private void BroadcastToWebSocket(WebSocketMessageEntity messageEntity)
        {
            _webSocketClientService.Broadcast(messageEntity);
        }

        /// <summary>
        /// 广播消息至Signalr客户端
        /// </summary>
        /// <param name="messageEntity">WebSocket消息实体</param>
        private void BroadcastToSignalR(WebSocketMessageEntity messageEntity)
        {
            _hubContext.Clients.All.SendAsync("ReceiveMessage", messageEntity.Message);
        }
    }

    /// <summary>
    /// <see cref="WebSocketMiddleware"/>扩展类，用于注册WebSocket中间件
    /// </summary>
    public static class WebSocketMiddlewareExtension
    {
        /// <summary>
        /// 将<see cref="WebSocketMiddleware"/>注册至Http请求管道中
        /// </summary>
        /// <param name="app"><see cref="IApplicationBuilder"/></param>
        /// <returns></returns>
        public static IApplicationBuilder UseWebSocketMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<WebSocketMiddleware>();
        }
    }
}