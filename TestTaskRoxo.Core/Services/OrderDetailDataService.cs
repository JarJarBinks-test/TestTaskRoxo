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
    public class OrderDetailDataService: IOrderDetailDataService
    {
        ITestTaskDbContext context;
        ILogger<IOrderDetailDataService> logger;

        public OrderDetailDataService(ITestTaskDbContext dbContext, ILogger<IOrderDetailDataService> logger)
        {
            context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<List<OrderDetail>> GetOrderDetails(int orderId)
        {
            logger.LogInformation($"{nameof(GetOrderDetails)}. {nameof(orderId)}:{orderId}");
            var result = await context.DoWithResult<DbOrderDetail, List<DbOrderDetail>>(x => x.Where(c=>c.OrderId == orderId).ToListAsync());
            return result.Select(x => x.To<OrderDetail>()).ToList();
        }
    }
}
