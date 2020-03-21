using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sigo.WebApi.DataEntities.React;

namespace Sigo.WebApi.Services.Impl
{
    /// <summary>
    /// 提供资源相关的功能
    /// </summary>
    public class ResourceService : IResourceService
    {
        //前端React插件
        private const string ReactPluginsPath = "wwwroot\\plugins";

        //IHostEnvironment
        private readonly IHostEnvironment _hostEnvironment;

        /// <summary>
        /// 构造<see cref="ResourceService"/>对象
        /// </summary>
        /// <param name="hostEnvironment"><see cref="IHostEnvironment"/></param>
        public ResourceService(IHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        /// <summary>
        /// 加载前端React插件的配置信息
        /// 返回<see cref="ReactPluginConfig"/>对象
        /// </summary>
        /// <returns>前端React插件的配置信息</returns>
        public IList<ReactPluginConfig> GetReactPluginConfigs()
        {
            var pluginConfigs = new List<ReactPluginConfig>();
            //插件目录
            var pluginPath = Path.Combine(_hostEnvironment.ContentRootPath, ReactPluginsPath);
            if (!Directory.Exists(pluginPath))
            {
                return pluginConfigs;
            }

            //获取插件配置文件
            var plugins = Directory.GetFiles(pluginPath, "config.json", SearchOption.AllDirectories);
            foreach (var pluginConfig in plugins)
            {
                try
                {
                    var directoryName = Directory.GetParent(pluginConfig).Name;
                    if (!directoryName.StartsWith('.'))
                    {
                        using (var jsonReader = new JsonTextReader(new StreamReader(pluginConfig)))
                        {
                            var jobject = JObject.Load(jsonReader);
                            jobject.Add("pluginDir", directoryName);
                            pluginConfigs.Add(jobject.ToObject<ReactPluginConfig>());
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{pluginConfig} 加载失败！异常信息:{ex.Message}");
                }
            }

            return pluginConfigs;
        }
    }
}
