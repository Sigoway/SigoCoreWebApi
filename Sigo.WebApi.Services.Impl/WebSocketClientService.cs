using Sigo.WebApi.DataEntities;
using log4net;
using System;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sigo.WebApi.Services.Impl
{
    /// <summary>
    /// 提供WebSocket通信相关的功能
    /// </summary>
    public class WebSocketClientService : IWebSocketClientService, IDisposable
    {
        /// <summary>
        /// log对象
        /// </summary>
        private readonly ILog _log;

        /// <summary>
        /// WebSocket客户端
        /// </summary>
        public ConcurrentDictionary<string, WebSocket> Clients { get; } = new ConcurrentDictionary<string, WebSocket>();

        /// <summary>
        /// 构造<see cref="WebSocketClientService"/>对象
        /// </summary>
        /// <param name="log">log对象</param>
        public WebSocketClientService(ILog log)
        {
            _log = log;
        }

        /// <summary>
        /// 添加WebSocket客户端
        /// </summary>
        /// <param name="clientID">客户端ID</param>
        /// <param name="webSocket"><see cref="WebSocket"/>连接实例</param>
        public void AddClient(string clientID, WebSocket webSocket)
        {
            Clients[clientID] = webSocket;
        }

        /// <summary>
        /// 移除WebSocket客户端
        /// </summary>
        /// <param name="clientID">客户端ID</param>
        public bool RemoveClient(string clientID)
        {
            return Clients.TryRemove(clientID, out _);
        }

        /// <summary>
        /// 广播消息
        /// </summary>
        /// <param name="message"><see cref="WebSocketMessageEntity"/>消息实体</param>
        public void Broadcast(WebSocketMessageEntity message)
        {
            var jsonData = message.Message;
            var buffer = Encoding.UTF8.GetBytes(jsonData);
            var data = new ArraySegment<byte>(buffer, 0, buffer.Length);
            Parallel.ForEach(Clients,
            (item) =>
            {
                if (item.Key != message.ClientID && !item.Value.CloseStatus.HasValue)
                {
                    item.Value.SendAsync(data, WebSocketMessageType.Text, true, CancellationToken.None);
                    _log.Info($"WebSocketClientService-Send message[{jsonData}] to client[{item.Key}]");
                }
            });
        }

        /// <summary>
        /// 释放未关闭的WebSocket
        /// </summary>
        public void Dispose()
        {
            Parallel.ForEach(Clients,
            (item) =>
            {
                if (!item.Value.CloseStatus.HasValue)
                {
                    item.Value.CloseAsync(WebSocketCloseStatus.NormalClosure, "Application Quit", CancellationToken.None);
                    _log.Info($"WebSocketClient[ClientID={item.Key}] is disconnected, Reason:App Stop.");
                }
            });
        }
    }
}
