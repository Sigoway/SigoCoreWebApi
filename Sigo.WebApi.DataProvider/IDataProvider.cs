using System.Collections.Generic;
using System.Data;

namespace Sigo.WebApi.DataProvider
{
    /// <summary>
    /// 提供与数据库库交互的同步接口定义
    /// </summary>
    public partial interface IDataProvider
    {
        #region Query

        /// <summary>
        /// 执行查询，返回一个类型 <typeparamref name="T"/> 的对象
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="command"><see cref="SqlCommandDefinition"/></param>
        /// <returns>类型 <typeparamref name="T"/> 的对象</returns>
        public T QueryFirstOrDefault<T>(SqlCommandDefinition command);

        /// <summary>
        /// 执行查询，返回一个类型 <typeparamref name="T"/> 的对象
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="sqlText">查询SQL语句或存储过程</param>
        /// <param name="param">与<paramref name="sqlText"/>相对应的参数，非必须项，推荐匿名对象形式，如：<code>new { Param1 = 1, Param2 = "test" }</code></param>
        /// <param name="commandTimeout">执行<paramref name="sqlText"/>的超时时间</param>
        /// <param name="commandType"><paramref name="sqlText"/>的类型</param>
        /// <returns>类型 <typeparamref name="T"/> 的对象</returns>
        public T QueryFirstOrDefault<T>(string sqlText, object param = null, CommandType? commandType = null, int? commandTimeout = null);

        /// <summary>
        /// 执行查询，返回一个类型 <typeparamref name="T"/> 的集合<see cref="IList{T}"/>
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="command"><see cref="SqlCommandDefinition"/></param>
        /// <returns>类型 <typeparamref name="T"/> 的集合</returns>
        public IList<T> Query<T>(SqlCommandDefinition command);

        /// <summary>
        /// 执行查询，返回一个类型 <typeparamref name="T"/> 的集合<see cref="IList{T}"/>
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="sqlText">查询SQL语句或存储过程</param>
        /// <param name="param">与<paramref name="sqlText"/>相对应的参数，非必须项，推荐匿名对象形式，如：<code>new { Param1 = 1, Param2 = "test" }</code></param>
        /// <param name="commandTimeout">执行<paramref name="sqlText"/>的超时时间</param>
        /// <param name="commandType"><paramref name="sqlText"/>的类型</param>
        /// <returns>类型 <typeparamref name="T"/> 的集合</returns>
        public IList<T> Query<T>(string sqlText, object param = null, CommandType? commandType = null, int? commandTimeout = null);

        /// <summary>
        /// 执行查询，返回单值，类型为<see cref="object"/>
        /// </summary>
        /// <param name="sqlText">查询SQL语句或存储过程</param>
        /// <param name="param">与<paramref name="sqlText"/>相对应的参数，非必须项，推荐匿名对象形式，如：<code>new { Param1 = 1, Param2 = "test" }</code></param>
        /// <param name="transaction">执行SQL语句的事务</param>
        /// <param name="commandTimeout">执行<paramref name="sqlText"/>的超时时间</param>
        /// <param name="commandType"><paramref name="sqlText"/>的类型</param>
        /// <returns>单值</returns>
        public object ExecuteScalar(string sqlText, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// 执行查询，返回单值，类型为<typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="sqlText">查询SQL语句或存储过程</param>
        /// <param name="param">与<paramref name="sqlText"/>相对应的参数，非必须项，推荐匿名对象形式，如：<code>new { Param1 = 1, Param2 = "test" }</code></param>
        /// <param name="transaction">执行SQL语句的事务</param>
        /// <param name="commandTimeout">执行<paramref name="sqlText"/>的超时时间</param>
        /// <param name="commandType"><paramref name="sqlText"/>的类型</param>
        /// <returns>单值，类型为<typeparamref name="T"/></returns>
        public T ExecuteScalar<T>(string sqlText, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        ///<summary>
        /// 执行查询，返回单值，类型为<see cref="object"/>
        /// </summary>
        /// <param name="command"><see cref="SqlCommandDefinition"/></param>
        /// <returns>单值，类型为<see cref="object"/></returns>
        public object ExecuteScalar(SqlCommandDefinition command);

        ///<summary>
        /// 执行查询，返回单值，类型为<typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="command"><see cref="SqlCommandDefinition"/></param>
        /// <returns>单值，类型为<typeparamref name="T"/></returns>
        public T ExecuteScalar<T>(SqlCommandDefinition command);

        #endregion

        #region Execute

        /// <summary>
        /// 执行增删改SQL语句
        /// </summary>
        /// <param name="sqlText">存储过程名或增删改SQL语句</param>
        /// <param name="param">与<paramref name="sqlText"/>相对应的参数，非必须项，推荐匿名对象形式，如：<code>new { Param1 = 1, Param2 = "test" }</code></param>
        /// <param name="transaction">执行SQL语句的事务</param>
        /// <param name="commandTimeout">执行<paramref name="sqlText"/>的超时时间</param>
        /// <param name="commandType"><paramref name="sqlText"/>的类型</param>
        /// <returns>影响的行数</returns>
        public int Execute(string sqlText, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// 执行增删改SQL语句
        /// </summary>
        /// <param name="command"><see cref="SqlCommandDefinition"/></param>
        /// <returns>影响的行数</returns>
        public int Execute(SqlCommandDefinition command);

        #endregion
    }
}
