using Sigo.WebApi.Services.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace Sigo.WebApi.Services
{
    /// <summary>
    /// 业务Service扩展类，提供注册业务相关的Service
    /// </summary>
    public static class BizServicesExtensions
    {
        /// <summary>
        /// 注册业务相关的Service
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/></param>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddBizServices(this IServiceCollection services)
        {
            return services
                .AddScoped<IAccountService, AccountService>()
                .AddSingleton<IPatientService, PatientService>()
                .AddScoped<IOrderService, OrderService>();
        }

        /// <summary>
        /// 注册资源相关的Service
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/></param>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddResourceServices(this IServiceCollection services)
        {
            return services.AddScoped<IResourceService, ResourceService>();
        }

        /// <summary>
        /// 注册WebSocket客户端管理服务
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/></param>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddWebSocketClientManagerServices(this IServiceCollection services)
        {
            return services.AddSingleton<IWebSocketClientService, WebSocketClientService>();
        }
    }
}
