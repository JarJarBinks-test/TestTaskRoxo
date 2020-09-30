using System.Collections.Generic;
using System.Threading.Tasks;
using TestTaskRoxo.BaseClasses;

namespace TestTaskRoxo.Services.Interfaces
{
    public interface IStatusService
    {
        public Task<List<OrderStatus>> GetOrderStatuses();
    }
}
