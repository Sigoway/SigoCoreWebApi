namespace Sigo.WebApi.Config
{
    /// <summary>
    /// SignalR配置实体类
    /// </summary>
    public class SignalRSetting
    {
        /// <summary>
        /// 跨域允许访问源列表
        /// </summary>
        public string[] Origins { get; set; }

        /// <summary>
        /// Hub名称
        /// </summary>
        public string ChatHubName { get; set; } = "/chatHub";

        /// <summary>
        /// Hub接收消息方法名
        /// </summary>
        public string ChatHubReceiveMsgMethodName { get; set; } = "ReceiveMessage";
    }
}
