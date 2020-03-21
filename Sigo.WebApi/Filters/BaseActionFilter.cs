using Sigo.WebApi.Utils;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Sigo.WebApi.Filters
{
    /// <summary>
    /// 筛选器基类
    /// </summary>
    public class BaseActionFilter : ActionFilterAttribute
    {
        /// <summary>
        /// log对象
        /// </summary>
        protected readonly ILog _log;

        /// <summary>
        /// 构造<see cref="PatientActionFilter"/>对象
        /// </summary>
        public BaseActionFilter()
        {
            _log = LogManager.GetLogger(SigoConst.RepositoryName, SigoConst.LogName);
        }

        /// <summary>
        /// 请求执行前处理
        /// </summary>
        /// <param name="context"><see cref="ActionExecutingContext"/></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _log.Debug($"Begin-{context.HttpContext.Request.Method}: {context.HttpContext.Request.Path}");
        }

        /// <summary>
        /// 请求执行后处理
        /// </summary>
        /// <param name="context"><see cref="ActionExecutedContext"/></param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                context.Result = new ObjectResult(ActionResultGenerator.CreateErrorDataResult(context.Exception.Message));
                context.ExceptionHandled = true;
                _log.Error($"Exception was catched by [{nameof(BaseActionFilter)}]-[{context.Exception.Message}]\r\n Trace:{context.Exception}");
            }

            _log.Debug($"End-{context.HttpContext.Request.Method}: {context.HttpContext.Request.Path} StatusCode:{context.HttpContext.Response.StatusCode}");
        }
    }
}
