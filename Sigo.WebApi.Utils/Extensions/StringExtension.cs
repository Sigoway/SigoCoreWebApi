using System;
using System.Collections.Generic;
using System.Text;

namespace Sigo.WebApi.Utils.Extensions
{
    /// <summary>
    /// 字符串扩展类
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// 如果<paramref name="condition"/>为真，返回<paramref name="value"/>,否则返回<see cref="string.Empty"/>
        /// </summary>
        /// <param name="value">字符串值</param>
        /// <param name="condition">匹配条件</param>
        /// <returns></returns>
        public static string If(this string value, bool condition)
        {
            return condition ? value : string.Empty;
        }
    }
}
