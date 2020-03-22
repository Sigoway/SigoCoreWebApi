namespace Sigo.WebApi.Config
{
    /// <summary>
    /// WebSocket配置实体类
    /// </summary>
    public class WebSocketSetting
    {
        /// <summary>
        /// 接收消息缓存单位（KB）
        /// </summary>
        public int ReceiveBufferSize { get; set; } = 4;

        /// <summary>
        /// 心跳检测间隔时间（分钟）
        /// </summary>
        public int KeepAliveInterval { get; set; } = 2;

        /// <summary>
        /// Pipeline名称
        /// </summary>
        public string ChatPipelineName { get; set; }
    }
}
