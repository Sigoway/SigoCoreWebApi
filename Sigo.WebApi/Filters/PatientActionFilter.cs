using Sigo.WebApi.DataEntities;
using Sigo.WebApi.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;

namespace Sigo.WebApi.Filters
{
    /// <summary>
    /// 患者相关请求筛选器
    /// </summary>
    public class PatientActionFilter : BaseActionFilter
    {
        /// <summary>
        /// 请求执行后处理
        /// </summary>
        /// <param name="context"><see cref="ActionExecutedContext"/></param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception == null && context.Result is ObjectResult result)
            {
                result.DeclaredType = typeof(ActionResultGenerator);
                if (result.Value == null)
                {
                    result.Value = ActionResultGenerator.CreateEmptyDataResult();
                }
                else if (result.Value is IEnumerable<PatientEntity> patientList
                     && (patientList == null || patientList.Count() == 0))
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
