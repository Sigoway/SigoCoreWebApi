using Sigo.WebApi.DataEntities;
using Sigo.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Sigo.WebApi.Controllers
{
    /// <summary>
    /// 提供医嘱相关信息的控制器
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        /// <summary>
        /// <see cref="IOrderService"/>
        /// </summary>
        private readonly IOrderService _orderService;

        /// <summary>
        /// <see cref="IConfiguration"/>
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// 构造<see cref="PatientController"/>
        /// </summary>
        /// <param name="orderService"><see cref="IOrderService"/></param>
        /// <param name="configuration">配置信息</param>
        public OrderController(IOrderService orderService, IConfiguration configuration)
        {
            this._orderService = orderService;
            this._configuration = configuration;
        }

        /// <summary>
        /// 获取医嘱执行列表
        /// </summary>
        /// <param name="pvid">就诊唯一标识</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>OrderEntity集合</returns>
        /// <example>
        /// <code>
        /// GET http://localhost:8080/api/Order/list?pvid={pvid}&amp;beginTime={beginTime}&amp;endTime={endTime}
        /// </code>
        /// </example>
        [HttpGet("list")]
        public IList<OrderEntity> GetOrderList(string pvid, DateTime beginTime, DateTime endTime)
        {
            return _orderService.GetOrderList(pvid, beginTime, endTime);
        }
    }
}