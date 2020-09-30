using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestTaskRoxo.BaseClasses;

namespace TestTaskRoxo.Services.Interfaces
{
    public interface IOrderDetailService
    {
        public Task Add(OrderDetail orderDetail);
        public void Remove(Int32 orderDetailId);
        public void Update(OrderDetail orderDetail);
        public Task<List<OrderDetail>> GetOrderDetails(Int32 orderId);
    }
}
