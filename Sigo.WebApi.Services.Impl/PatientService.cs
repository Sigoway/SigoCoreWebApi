using Sigo.WebApi.DataEntities;
using Sigo.WebApi.DataProvider;
using System.Collections.Generic;
using System.Linq;

namespace Sigo.WebApi.Services.Impl
{
    /// <summary>
    /// 提供与患者信息交互的相关功能
    /// </summary>
    public class PatientService : IPatientService
    {
        /// <summary>
        /// <see cref="IDataProvider"/>数据驱动对象
        /// </summary>
        private IDataProvider _dataProvider;

        /// <summary>
        /// 构造<see cref="PatientService"/>对象
        /// </summary>
        /// <param name="dataProvider">数据驱动</param>
        public PatientService(ISqlServerDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        /// <summary>
        /// 根据<paramref name="wardArea"/>获取患者列表
        /// </summary>
        /// <param name="wardArea">病区</param>
        /// <returns></returns>
        public IList<PatientEntity> GetPatientList(string wardArea)
        {
            //TODO 请替换实际的业务SQL
            return string.IsNullOrEmpty(wardArea)
                ? null
                : _dataProvider.Query<PatientEntity>("Select * From dbo.View_PatientInfo Where WardArea=@WardArea",
                    new { WardArea = wardArea });
        }

        /// <summary>
        /// 根据<paramref name="status"/>获取患者列表
        /// </summary>
        /// <param name="status">状态</param>
        /// <returns></returns>
        public IList<PatientEntity> GetPatientList(IList<string> status)
        {
            if (status == null || status.Count == 0)
            {
                return null;
            }

            //TODO 请替换实际的业务SQL
            var strStatus = string.Join(',', status.Select(t => $"'{t}'"));
            return _dataProvider.Query<PatientEntity>($"EXEC Proc_PatientList_GetPatientList \" And Status IN ({strStatus})\"");
        }

        /// <summary>
        /// 根据<paramref name="patientId"/>获取患者信息
        /// </summary>
        /// <param name="patientId">患者ID</param>
        /// <returns></returns>
        public PatientEntity GetPatientInfo(string patientId)
        {
            //TODO 请替换实际的业务SQL
            return string.IsNullOrEmpty(patientId)
                ? null
                : _dataProvider.QueryFirstOrDefault<PatientEntity>("Select * From dbo.View_PatientInfo Where PatientID=@PatientID",
                    new { PatientID = patientId });
        }
    }
}
