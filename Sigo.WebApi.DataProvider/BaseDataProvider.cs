namespace Sigo.WebApi.DataProvider
{
    /// <summary>
    /// 数据驱动基类
    /// </summary>
    public class BaseDataProvider
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        protected readonly string _dbConnectionString;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dbConnectionString">数据库连接字符串</param>
        public BaseDataProvider(string dbConnectionString)
        {
            _dbConnectionString = dbConnectionString;
        }
    }
}
