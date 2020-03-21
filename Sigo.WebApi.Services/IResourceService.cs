using Sigo.WebApi.DataEntities.React;
using System.Collections.Generic;

namespace Sigo.WebApi.Services
{
    /// <summary>
    /// 提供资源相关的功能接口
    /// </summary>
    public interface IResourceService
    {
        /// <summary>
        /// 加载前端React插件的配置信息
        /// </summary>
        /// <returns>前端React插件的配置信息</returns>
        IList<ReactPluginConfig> GetReactPluginConfigs();
    }
}
