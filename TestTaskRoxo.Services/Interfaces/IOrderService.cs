using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestTaskRoxo.BaseClasses;

namespace TestTaskRoxo.Services.Interfaces
{
    public interface IOrderService
    {
        public Task Add(Order order);
        public void Remove(Int32 orderId);
        public void Update(Order order);
        public Task<List<Order>> GetOrders();
    }
}
