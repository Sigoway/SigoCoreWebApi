using log4net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Sigo.WebApi.Middlewares
{
    /// <summary>
    /// 异常处理中间件
    /// </summary>
    public class ExceptionMiddleware
    {
        /// <summary>
        /// log对象
        /// </summary>
        private readonly ILog _log;

        /// <summary>
        /// 请求委托
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// 构造<see cref="ExceptionMiddleware"/>对象
        /// </summary>
        /// <param name="next"><see cref="RequestDelegate"/></param>
        /// <param name="log">log对象</param>
        public ExceptionMiddleware(RequestDelegate next, ILog log)
        {
            _next = next;
            _log = log;
        }

        /// <summary>
        /// 处理Http请求
        /// async is important
        /// </summary>
        /// <param name="httpContext"><see cref="HttpContext"/></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                var errMsg = $"Exception was catched by [{nameof(ExceptionMiddleware)}]-请求处理[{httpContext.Request.Method}:{httpContext.Request.Path}]发生错误，异常信息：[{ex.Message}]";
                _log.Error(errMsg + $"\r\n Trace:{ex}");

                httpContext.Response.StatusCode = 500;
                httpContext.Response.ContentType = "text/text;charset=utf-8;";
                await httpContext.Response.WriteAsync(errMsg);
            }
        }
    }

    /// <summary>
    /// <see cref="ExceptionMiddleware"/>扩展类，用于注册异常中间件
    /// </summary>
    public static class ExceptionMiddlewareExtensions
    {
        /// <summary>
        /// 注册异常中间件至Http请求管道中
        /// </summary>
        /// <param name="builder"><see cref="IApplicationBuilder"/></param>
        /// <returns></returns>
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
