using Microsoft.Extensions.DependencyInjection;

namespace Sigo.WebApi.DataProvider
{
    /// <summary>
    /// 数据驱动扩展类
    /// </summary>
    public static class DataProviderExtensions
    {
        /// <summary>
        /// 注册ISqlServerDataProvider单例服务
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>对象</param>
        /// <param name="dbConnectionString">SqlServer数据库连接字符串</param>
        /// <returns></returns>
        public static IServiceCollection AddSqlServerDataProvider(this IServiceCollection services, string dbConnectionString)
        {
            return services.AddSingleton<ISqlServerDataProvider>(c => new SqlServerDataProvider(dbConnectionString));
        }
    }
}
