using Sigo.WebApi.DataEntities;
using Sigo.WebApi.Services;
using log4net;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Sigo.WebApi.Hubs
{
    /// <summary>
    /// 医嘱执行集线器
    /// </summary>
    public class ExecOrdersHub : Hub
    {
        /// <summary>
        /// log对象
        /// </summary>
        private readonly ILog _log;

        /// <summary>
        /// WebSocket客户端管理服务
        /// </summary>
        private readonly IWebSocketClientService _webSocketClientService;

        /// <summary>
        /// 客户端接收消息的方法名
        /// </summary>
        private readonly string _receiveMsgMethodName;

        /// <summary>
        /// 构造<see cref="ExecOrdersHub"/>对象
        /// </summary>
        /// <param name="configuration">配置信息</param>
        /// <param name="log">日志对象</param>
        /// <param name="webSocketClientService">WebSocket客户端管理服务</param>
        public ExecOrdersHub(IConfiguration configuration, ILog log, IWebSocketClientService webSocketClientService)
        {
            _log = log;
            _webSocketClientService = webSocketClientService;
            _receiveMsgMethodName = configuration.GetValue("SignalR:Hubs:ExecOrders:ReceiveMsgMethodName", "ReceiveMessage");
        }

        /// <summary>
        /// 有新客户端与集线器建立连接
        /// </summary>
        /// <returns></returns>
        public async override Task OnConnectedAsync()
        {
            _log.Info($"SignalR Client[{Context.ConnectionId }] is connecting.");
            await base.OnConnectedAsync();
        }

        /// <summary>
        /// 客户端与集线器断开连接
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public async override Task OnDisconnectedAsync(Exception exception)
        {
            _log.Info($"SignalR Client[{Context.ConnectionId }] was disconnected.");
            await base.OnDisconnectedAsync(exception);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="data">消息体</param>
        /// <returns></returns>
        public async Task SendMessage(string data)
        {
            _log.Info($"SignalR Client[{Context.ConnectionId }] send message: [{data}]");

            _webSocketClientService.Broadcast(new WebSocketMessageEntity() { ClientID = Context.ConnectionId, Message = data });

            //给自身客户端以外的客户端发信息
            await Clients.AllExcept(Context.ConnectionId).SendAsync(_receiveMsgMethodName, data);
        }
    }
}