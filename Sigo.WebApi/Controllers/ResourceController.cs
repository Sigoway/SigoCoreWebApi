using Sigo.WebApi.DataEntities.React;
using Sigo.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Sigo.WebApi.Controllers
{
    /// <summary>
    /// 资源控制器
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        //资源服务对象
        private readonly IResourceService _resourceService;

        /// <summary>
        /// 构造<see cref="ResourceController"/>对象
        /// </summary>
        /// <param name="resourceService"><see cref="IResourceService"/>对象</param>
        public ResourceController(IResourceService resourceService)
        {
            _resourceService = resourceService;
        }

        /// <summary>
        /// 加载前端React插件的配置信息
        /// </summary>
        /// <returns></returns>
#if DEBUG
        [AllowAnonymous]
#endif
        [HttpGet("plugins")]
        public IList<ReactPluginConfig> Plugins()
        {
            return _resourceService.GetReactPluginConfigs();
        }
    }
}