using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace FxEcis.WebApi.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Headers.ContainsKey("Authorization"))
            {
                var authorize = httpContext.Request.Headers["Authorization"].ToString();
                    //如果是Basic 身份认证
                if (authorize.Contains("Basic"))
                {
                    var info = authorize.Replace("Basic ", string.Empty);
                    var bytes = Convert.FromBase64String(info);
                    var contents = Encoding.Default.GetString(bytes);
                    var token = contents.Split(":");
                    var userName = token[0];
                    var userPwd = token[1];
                    if (userName == "test" && userPwd == "123456")
                    {
                        await httpContext.Response.WriteAsync("验证通过").ConfigureAwait(true);
                        await _next(httpContext);
                    }
                }
            }
            httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            await httpContext.Response.WriteAsync("Unauthorized!");

        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class AuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthenticationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthenticationMiddleware>();
        }
    }
}
