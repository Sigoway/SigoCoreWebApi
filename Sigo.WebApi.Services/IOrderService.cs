using Sigo.WebApi.DataEntities;
using System;
using System.Collections.Generic;

namespace Sigo.WebApi.Services
{
    /// <summary>
    /// 提供与医嘱信息交互的相关功能
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// 获取医嘱执行列表
        /// </summary>
        /// <param name="pvid">就诊唯一标识</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>OrderEntity集合</returns>
        public IList<OrderEntity> GetOrderList(string pvid, DateTime beginTime, DateTime endTime);
    }
}
