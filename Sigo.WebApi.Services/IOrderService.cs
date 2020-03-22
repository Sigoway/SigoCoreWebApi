using Sigo.WebApi.DataEntities;
using System;
using System.Collections.Generic;

namespace Sigo.WebApi.Services
{
    /// <summary>
    /// 提供与订单信息交互的相关功能
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>OrderEntity集合</returns>
        public IList<OrderEntity> GetOrderList(DateTime beginTime, DateTime endTime);
    }
}
