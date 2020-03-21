using Sigo.WebApi.DataEntities;
using System.Collections.Generic;

namespace Sigo.WebApi.Services
{
    /// <summary>
    /// 提供与患者信息交互的相关功能
    /// </summary>
    public interface IPatientService
    {
        /// <summary>
        /// 根据<paramref name="patientId"/>获取患者信息
        /// </summary>
        /// <param name="patientId">患者ID</param>
        /// <returns></returns>
        PatientEntity GetPatientInfo(string patientId);

        /// <summary>
        /// 根据<paramref name="wardArea"/>获取患者列表
        /// </summary>
        /// <param name="wardArea">病区</param>
        /// <returns></returns>
        IList<PatientEntity> GetPatientList(string wardArea);

        /// <summary>
        /// 根据<paramref name="status"/>获取患者列表
        /// </summary>
        /// <param name="status">状态</param>
        /// <returns></returns>
        IList<PatientEntity> GetPatientList(IList<string> status);
    }
}
