using Sigo.WebApi.DataEntities;
using Sigo.WebApi.DataProvider;
using System;
using System.Collections.Generic;

namespace Sigo.WebApi.Services.Impl
{
    /// <summary>
    /// 提供与订单信息交互的相关功能
    /// </summary>
    public class OrderService: IOrderService
    {
        /// <summary>
        /// <see cref="IDataProvider"/>数据驱动对象
        /// </summary>
        private IDataProvider _dataProvider;

        /// <summary>
        /// 构造<see cref="PatientService"/>对象
        /// </summary>
        /// <param name="dataProvider">数据驱动</param>
        public OrderService(ISqlServerDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>OrderEntity集合</returns>
        public IList<OrderEntity> GetOrderList(DateTime beginTime, DateTime endTime)
        {
            throw new NotImplementedException();
        }
    }
}
