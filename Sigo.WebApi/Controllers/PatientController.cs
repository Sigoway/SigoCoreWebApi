using Sigo.WebApi.DataEntities;
using Sigo.WebApi.Filters;
using Sigo.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Sigo.WebApi.Controllers
{
    /// <summary>
    /// 提供患者相关信息的控制器
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [PatientActionFilter]
    public class PatientController : ControllerBase
    {
        /// <summary>
        /// <see cref="IPatientService"/>
        /// </summary>
        private readonly IPatientService _patientService;

        /// <summary>
        /// 构造<see cref="PatientController"/>
        /// </summary>
        /// <param name="patientService"><see cref="IPatientService"/></param>
        public PatientController(IPatientService patientService)
        {
            this._patientService = patientService;
        }

        /// <summary>
        /// 获取指定<paramref name="patientId"/>的患者基本信息
        /// </summary>
        /// <param name="patientId">患者ID</param>
        /// <returns>PatientEntity</returns>
        /// <example>
        /// <code>
        /// GET: http://localhost:8080/api/Patient/20200217
        /// </code>
        /// </example>
        [HttpGet("{patientId:int}", Name = "Get")]
        public PatientEntity Get(string patientId)
        {
            return _patientService.GetPatientInfo(patientId);
        }

        /// <summary>
        /// 获取指定病区的患者列表
        /// </summary>
        /// <param name="wardArea">病区</param>
        /// <returns>PatientEntity集合</returns>
        ///<example>
        /// <code>
        /// GET: http://localhost:8080/api/Patient/list/{wardArea}
        /// </code>
        /// </example>
        [HttpGet("list/{wardArea}")]
        public IList<PatientEntity> GetPatientList(string wardArea)
        {
            return _patientService.GetPatientList(wardArea);
        }

        /// <summary>
        /// 获取未入科的患者列表
        /// </summary>
        /// <returns>PatientEntity集合</returns>
        ///<example>
        /// <code>
        /// GET: http://localhost:8080/api/Patient/unIndept
        /// </code>
        /// </example>
        [HttpGet("list/unIndept")]
        public IList<PatientEntity> GetUnIndept()
        {
            return _patientService.GetPatientList(new List<string>() { "100" });
        }

        /// <summary>
        /// 获取已入科的患者列表
        /// </summary>
        /// <returns>PatientEntity集合</returns>
        ///<example>
        /// <code>
        /// GET: http://localhost:8080/api/Patient/indept
        /// </code>
        /// </example>
        [HttpGet("list/indept")]
        public IList<PatientEntity> GetIndept()
        {
            return _patientService.GetPatientList(new List<string>() { "0", "1", "3" });
        }
    }
}
