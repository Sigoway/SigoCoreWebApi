using Sigo.WebApi.DataEntities;
using System.Net.WebSockets;

namespace Sigo.WebApi.Services
{
    /// <summary>
    /// 提供WebSocket通信相关的功能接口
    /// </summary>
    public interface IWebSocketClientService
    {
        /// <summary>
        /// 添加WebSocket客户端
        /// </summary>
        /// <param name="clientID">客户端ID</param>
        /// <param name="webSocket"><see cref="WebSocket"/>连接实例</param>
        void AddClient(string clientID, WebSocket webSocket);

        /// <summary>
        /// 移除WebSocket客户端
        /// </summary>
        /// <param name="clientID">客户端ID</param>
        bool RemoveClient(string clientID);

        /// <summary>
        /// 广播消息
        /// </summary>
        /// <param name="message"><see cref="WebSocketMessageEntity"/>消息实体</param>
        void Broadcast(WebSocketMessageEntity message);
    }
}
