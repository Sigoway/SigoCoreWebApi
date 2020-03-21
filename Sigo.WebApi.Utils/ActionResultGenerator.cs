using Sigo.WebApi.DataEntities;

namespace Sigo.WebApi.Utils
{
    /// <summary>
    /// ActionResult生成类
    /// </summary>
    public class ActionResultGenerator
    {

        public static ActionResultEntity CreateEmptyDataResult()
        {
            return new ActionResultEntity() { Data = null, ErrorMessage = "No Data" };
        }

        public static ActionResultEntity CreateDataResult(object value)
        {
            return new ActionResultEntity() { Data = value, ErrorMessage = string.Empty };
        }

        public static ActionResultEntity CreateErrorDataResult(string errorMsg)
        {
            return new ActionResultEntity() { Data = null, ErrorMessage = errorMsg };
        }
    }
}
