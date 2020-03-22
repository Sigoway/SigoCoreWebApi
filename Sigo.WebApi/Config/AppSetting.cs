namespace Sigo.WebApi.Config
{
    /// <summary>
    /// 应用配置实体类
    /// </summary>
    public class AppSetting
    {
        /// <summary>
        /// <see cref="WebSocketSetting"/>
        /// </summary>
        public WebSocketSetting WebSocketSetting { get; set; }

        /// <summary>
        /// <see cref="SignalRSetting"/>
        /// </summary>
        public SignalRSetting SignalRSetting { get; set; }
    }
}
