namespace Sigo.WebApi.DataEntities
{
    /// <summary>
    /// WebSocket消息实体类
    /// </summary>
    public class WebSocketMessageEntity
    {
        public string ClientID { get; set; }

        public string Message { get; set; }
    }
}
