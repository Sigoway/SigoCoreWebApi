using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Sigo.WebApi.DataProvider
{
    /// <summary>
    /// 提供与数据库库交互的异步接口定义
    /// </summary>
    public partial interface IDataProvider
    {
        #region Query
        /// <summary>
        /// 使用Task执行异步查询，返回一个类型 <typeparamref name="T"/> 的集合<see cref="IList{T}"/>
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="command"><see cref="SqlCommandDefinition"/></param>
        /// <returns>类型 <typeparamref name="T"/> 的集合</returns>
        public Task<IList<T>> QueryAsync<T>(SqlCommandDefinition command);

        /// <summary>
        /// 使用Task执行异步查询，返回一个类型 <typeparamref name="T"/> 的集合<see cref="IList{T}"/>
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="sqlText">查询SQL语句或存储过程</param>
        /// <param name="param">与<paramref name="sqlText"/>相对应的参数，非必须项，推荐匿名对象形式，如：<code>new { Param1 = 1, Param2 = "test" }</code></param>
        /// <param name="commandTimeout">执行<paramref name="sqlText"/>的超时时间</param>
        /// <param name="commandType"><paramref name="sqlText"/>的类型</param>
        /// <returns>类型 <typeparamref name="T"/> 的集合</returns>
        public Task<IList<T>> QueryAsync<T>(string sql, object param = null, CommandType? commandType = null, int? commandTimeout = null);

        /// <summary>
        /// 使用Task执行异步查询，返回一个类型 <typeparamref name="T"/> 的对象
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="command"><see cref="SqlCommandDefinition"/></param>
        /// <returns>类型 <typeparamref name="T"/> 的对象</returns>
        public Task<T> QueryFirstOrDefaultAsync<T>(SqlCommandDefinition command);

        /// <summary>
        /// 使用Task执行异步查询，返回一个类型 <typeparamref name="T"/> 的对象
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="sqlText">查询SQL语句或存储过程</param>
        /// <param name="param">与<paramref name="sqlText"/>相对应的参数，非必须项，推荐匿名对象形式，如：<code>new { Param1 = 1, Param2 = "test" }</code></param>
        /// <param name="commandTimeout">执行<paramref name="sqlText"/>的超时时间</param>
        /// <param name="commandType"><paramref name="sqlText"/>的类型</param>
        /// <returns>类型 <typeparamref name="T"/> 的对象</returns>
        public Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, CommandType? commandType = null, int? commandTimeout = null);
        #endregion
    }
}
