using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Sigo.WebApi.DataProvider
{
    /// <summary>
    /// 提供与SqlServer数据库异步交互的数据驱动类
    /// </summary>
    public partial class SqlServerDataProvider
    {
        #region Query

        /// <summary>
        /// 使用Task执行异步查询，返回一个类型 <typeparamref name="T"/> 的集合<see cref="IList{T}"/>
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="command"><see cref="SqlCommandDefinition"/></param>
        /// <returns>类型 <typeparamref name="T"/> 的集合</returns>
        public Task<IList<T>> QueryAsync<T>(SqlCommandDefinition command)
        {
            using (var sqlConn = new SqlConnection(_dbConnectionString))
            {
                return sqlConn.QueryAsync<T>(command.AsCommandDefinition()) as Task<IList<T>>;
            }
        }

        /// <summary>
        /// 使用Task执行异步查询，返回一个类型 <typeparamref name="T"/> 的集合<see cref="IList{T}"/>
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="sqlText">查询SQL语句或存储过程</param>
        /// <param name="param">与<paramref name="sqlText"/>相对应的参数，非必须项，推荐匿名对象形式，如：<code>new { Param1 = 1, Param2 = "test" }</code></param>
        /// <param name="commandTimeout">执行<paramref name="sqlText"/>的超时时间</param>
        /// <param name="commandType"><paramref name="sqlText"/>的类型</param>
        /// <returns>类型 <typeparamref name="T"/> 的集合</returns>
        public Task<IList<T>> QueryAsync<T>(string sqlText, object param = null, CommandType? commandType = null, int? commandTimeout = null)
        {
            using (var sqlConn = new SqlConnection(_dbConnectionString))
            {
                return sqlConn.QueryAsync<T>(sqlText, param, commandTimeout: commandTimeout, commandType: commandType) as Task<IList<T>>;
            }
        }

        /// <summary>
        /// 使用Task执行异步查询，返回一个类型 <typeparamref name="T"/> 的对象
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="command"><see cref="SqlCommandDefinition"/></param>
        /// <returns>类型 <typeparamref name="T"/> 的对象</returns>
        public Task<T> QueryFirstOrDefaultAsync<T>(SqlCommandDefinition command)
        {
            using (var sqlConn = new SqlConnection(_dbConnectionString))
            {
                return sqlConn.QueryFirstOrDefaultAsync<T>(command.AsCommandDefinition());
            }
        }

        /// <summary>
        /// 使用Task执行异步查询，返回一个类型 <typeparamref name="T"/> 的对象
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="sqlText">查询SQL语句或存储过程</param>
        /// <param name="param">与<paramref name="sqlText"/>相对应的参数，非必须项，推荐匿名对象形式，如：<code>new { Param1 = 1, Param2 = "test" }</code></param>
        /// <param name="commandTimeout">执行<paramref name="sqlText"/>的超时时间</param>
        /// <param name="commandType"><paramref name="sqlText"/>的类型</param>
        /// <returns>类型 <typeparamref name="T"/> 的对象</returns>
        public Task<T> QueryFirstOrDefaultAsync<T>(string sqlText, object param = null, CommandType? commandType = null, int? commandTimeout = null)
        {
            using (var sqlConn = new SqlConnection(_dbConnectionString))
            {
                return sqlConn.QueryFirstOrDefaultAsync<T>(sqlText, param, commandTimeout: commandTimeout, commandType: commandType);
            }
        }

        #endregion
    }
}
