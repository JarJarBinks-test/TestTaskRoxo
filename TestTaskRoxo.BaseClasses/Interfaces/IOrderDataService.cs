using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestTaskRoxo.BaseClasses.Interaces
{
    public interface IOrderDataService
    {
        public Task<List<Order>> GetOrders();
    }
}
