using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Sigo.WebApi.DataProvider
{
    /// <summary>
    /// 提供与SqlServer数据库同步交互的数据驱动类
    /// </summary>
    public partial class SqlServerDataProvider : BaseDataProvider, IEcisPlatform5DataProvider, IDataProvider
    {
        #region 构造方法
        /// <summary>
        /// 构造<see cref="SqlServerDataProvider"/>对象
        /// </summary>
        /// <param name="dbConnectionString">SqlServer数据库连接字符串</param>
        public SqlServerDataProvider(string dbConnectionString) : base(dbConnectionString)
        {
        }
        #endregion

        #region Query

        /// <summary>
        /// 执行查询，返回一个类型 <typeparamref name="T"/> 的集合<see cref="IList{T}"/>
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="command"><see cref="SqlCommandDefinition"/></param>
        /// <returns>类型 <typeparamref name="T"/> 的集合</returns>
        public IList<T> Query<T>(SqlCommandDefinition command)
        {
            using (var sqlConn = new SqlConnection(_dbConnectionString))
            {
                return sqlConn.Query<T>(command.AsCommandDefinition())?.AsList();
            }
        }

        /// <summary>
        /// 执行查询，返回一个类型 <typeparamref name="T"/> 的集合<see cref="IList{T}"/>
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="sqlText">查询SQL语句或存储过程</param>
        /// <param name="param">与<paramref name="sqlText"/>相对应的参数，非必须项，推荐匿名对象形式，如：<code>new { Param1 = 1, Param2 = "test" }</code></param>
        /// <param name="commandTimeout">执行<paramref name="sqlText"/>的超时时间</param>
        /// <param name="commandType"><paramref name="sqlText"/>的类型</param>
        /// <returns>类型 <typeparamref name="T"/> 的集合</returns>
        public IList<T> Query<T>(string sqlText, object param = null, CommandType? commandType = null, int? commandTimeout = null)
        {
            using (var sqlConn = new SqlConnection(_dbConnectionString))
            {
                return sqlConn.Query<T>(sqlText, param, commandTimeout: commandTimeout, commandType: commandType)?.AsList();
            }
        }

        /// <summary>
        /// 执行查询，返回一个类型 <typeparamref name="T"/> 的对象
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="command"><see cref="SqlCommandDefinition"/></param>
        /// <returns>类型 <typeparamref name="T"/> 的对象</returns>
        public T QueryFirstOrDefault<T>(SqlCommandDefinition command)
        {
            using (var sqlConn = new SqlConnection(_dbConnectionString))
            {
                return sqlConn.QueryFirstOrDefault<T>(command.AsCommandDefinition());
            }
        }

        /// <summary>
        /// 执行查询，返回一个类型 <typeparamref name="T"/> 的对象
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="sqlText">查询SQL语句或存储过程</param>
        /// <param name="param">与<paramref name="sqlText"/>相对应的参数，非必须项，推荐匿名对象形式，如：<code>new { Param1 = 1, Param2 = "test" }</code></param>
        /// <param name="commandTimeout">执行<paramref name="sqlText"/>的超时时间</param>
        /// <param name="commandType"><paramref name="sqlText"/>的类型</param>
        /// <returns>类型 <typeparamref name="T"/> 的对象</returns>
        public T QueryFirstOrDefault<T>(string sqlText, object param = null, CommandType? commandType = null, int? commandTimeout = null)
        {
            using (var sqlConn = new SqlConnection(_dbConnectionString))
            {
                return sqlConn.QueryFirstOrDefault<T>(sqlText, param, commandTimeout: commandTimeout, commandType: commandType);
            }
        }

        /// <summary>
        /// 执行查询，返回单值，类型为<see cref="object"/>
        /// </summary>
        /// <param name="sqlText">查询SQL语句或存储过程</param>
        /// <param name="param">与<paramref name="sqlText"/>相对应的参数，非必须项，推荐匿名对象形式，如：<code>new { Param1 = 1, Param2 = "test" }</code></param>
        /// <param name="transaction">执行SQL语句的事务</param>
        /// <param name="commandTimeout">执行<paramref name="sqlText"/>的超时时间</param>
        /// <param name="commandType"><paramref name="sqlText"/>的类型</param>
        /// <returns>单值</returns>
        public object ExecuteScalar(string sqlText, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (var sqlConn = new SqlConnection(_dbConnectionString))
            {
                return sqlConn.ExecuteScalar(sqlText, param, transaction, commandTimeout, commandType);
            }
        }

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
        public T ExecuteScalar<T>(string sqlText, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (var sqlConn = new SqlConnection(_dbConnectionString))
            {
                return sqlConn.ExecuteScalar<T>(sqlText, param, transaction, commandTimeout, commandType);
            }
        }

        ///<summary>
        /// 执行查询，返回单值，类型为<see cref="object"/>
        /// </summary>
        /// <param name="command"><see cref="SqlCommandDefinition"/></param>
        /// <returns>单值，类型为<see cref="object"/></returns>
        public object ExecuteScalar(SqlCommandDefinition command)
        {
            using (var sqlConn = new SqlConnection(_dbConnectionString))
            {
                return sqlConn.ExecuteScalar(command.AsCommandDefinition());
            }
        }

        ///<summary>
        /// 执行查询，返回单值，类型为<typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="command"><see cref="SqlCommandDefinition"/></param>
        /// <returns>单值，类型为<typeparamref name="T"/></returns>
        public T ExecuteScalar<T>(SqlCommandDefinition command)
        {
            using (var sqlConn = new SqlConnection(_dbConnectionString))
            {
                return sqlConn.ExecuteScalar<T>(command.AsCommandDefinition());
            }
        }
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
        public int Execute(string sqlText, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (var sqlConn = new SqlConnection(_dbConnectionString))
            {
                return sqlConn.Execute(sqlText, param, transaction, commandTimeout, commandType);
            }
        }

        /// <summary>
        /// 执行增删改SQL语句
        /// </summary>
        /// <param name="command"><see cref="SqlCommandDefinition"/></param>
        /// <returns>影响的行数</returns>
        public int Execute(SqlCommandDefinition command)
        {
            using (var sqlConn = new SqlConnection(_dbConnectionString))
            {
                return sqlConn.Execute(command.AsCommandDefinition());
            }
        }

        #endregion
    }
}
