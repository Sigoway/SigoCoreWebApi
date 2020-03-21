using Dapper;
using System.Data;

namespace Sigo.WebApi.DataProvider
{
    /// <summary>
    /// SqlCommand相关参数定义
    /// </summary>
    public struct SqlCommandDefinition
    {
        /// <summary>
        /// SQL语句或存储过程
        /// </summary>
        public string CommandText { get; }

        /// <summary>
        /// 与<paramref name="commandText"/>相对应的参数，非必须项，推荐匿名对象形式。
        /// 如：
        /// <code>
        ///     new { Param1 = 1, Param2 = "test" }
        /// </code>
        /// </summary>
        public object Parameters { get; }

        /// <summary>
        /// 执行<paramref name="CommandText"/>的超时时间
        /// </summary>
        public int? CommandTimeout { get; }

        /// <summary>
        /// <paramref name="CommandText"/>的类型
        /// </summary>
        public CommandType? CommandType { get; }

        /// <summary>
        /// 构造<see cref="SqlCommandDefinition"/>
        /// </summary>
        /// <param name="commandText">SQL语句或存储过程</param>
        /// <param name="parameters">与<paramref name="commandText"/>相对应的参数，非必须项，推荐匿名对象形式，如：<code>new { Param1 = 1, Param2 = "test" }</code></param>
        /// <param name="commandType"><paramref name="commandText"/>的类型</param>
        /// <param name="commandTimeout">执行<paramref name="commandText"/>的超时时间</param>
        public SqlCommandDefinition(string commandText, object parameters = null, CommandType? commandType = null, int? commandTimeout = null)
        {
            CommandText = commandText;
            Parameters = parameters;
            CommandTimeout = commandTimeout;
            CommandType = commandType;
        }

        /// <summary>
        /// 转换为<see cref="CommandDefinition"/>
        /// </summary>
        /// <returns>Dapper.CommandDefinition</returns>
        public CommandDefinition AsCommandDefinition()
        {
            return new CommandDefinition(CommandText, Parameters, commandType: CommandType, commandTimeout: CommandTimeout);
        }
    }
}
