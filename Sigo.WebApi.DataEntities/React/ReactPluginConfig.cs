namespace Sigo.WebApi.DataEntities.React
{
    /// <summary>
    /// React前端插件配置实体
    /// </summary>
    public struct ReactPluginConfig
    {
        public string Name { get; set; }

        public string Version { get; set; }

        public string Route { get; set; }

        public string Color { get; set; }

        public string AuthName { get; set; }

        public string Description { get; set; }

        public int Sort { get; set; }

        public string PluginDir { get; set; }
    }
}
