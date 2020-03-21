using Sigo.WebApi.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Sigo.WebApi.Filters
{
    /// <summary>
    /// 登录验证请求相关筛选器
    /// </summary>
    public class AccountActionFilter : BaseActionFilter
    {
        /// <summary>
        /// 请求执行后处理
        /// </summary>
        /// <param name="context"><see cref="ActionExecutedContext"/></param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is ApplicationException appex)
            {
                //拦截应用内部定义异常
                context.Result = new ObjectResult(ActionResultGenerator.CreateErrorDataResult(appex.Message));
                context.ExceptionHandled = true;
            }
            else if (context.Result is ObjectResult result)
            {
                result.DeclaredType = typeof(ActionResultGenerator);
                if (result.Value == null)
                {
                    result.Value = ActionResultGenerator.CreateEmptyDataResult();
                }
                else
                {
                    result.Value = ActionResultGenerator.CreateDataResult(result.Value);
                }
            }

            base.OnActionExecuted(context);
        }
    }
}
