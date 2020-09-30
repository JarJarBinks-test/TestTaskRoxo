using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestTaskRoxo.BaseClasses.Interaces
{
    public interface IOrderDetailDataService
    {
        public Task<List<OrderDetail>> GetOrderDetails(Int32 orderId);
    }
}
