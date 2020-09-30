using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestTaskRoxo.BaseClasses.Interaces
{
    public interface IStatusDataService
    {
        public Task<List<OrderStatus>> GetStatuses();
    }
}
