using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTaskRoxo.BaseClasses;
using TestTaskRoxo.BaseClasses.Interaces;
using TestTaskRoxo.Core.Tables;

namespace TestTaskRoxo.Core
{
    public class OrderDataService : IOrderDataService
    {
        ITestTaskDbContext context;
        ILogger<IOrderDataService> logger;

        public OrderDataService(ITestTaskDbContext dbContext, ILogger<IOrderDataService> logger)
        {
            context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<List<Order>> GetOrders()
        {
            logger.LogInformation($"{nameof(GetOrders)}.");
            var result = await context.DoWithResult<DbOrder, List<DbOrder>>(x => x.ToListAsync());
            return result.Select(x => x.To<Order>()).ToList();
        }
    }
}
