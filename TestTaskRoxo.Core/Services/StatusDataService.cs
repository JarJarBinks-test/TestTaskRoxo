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
    public class StatusDataService : IStatusDataService
    {
        ITestTaskDbContext context;
        ILogger<IStatusDataService> logger;

        public StatusDataService(ITestTaskDbContext dbContext, ILogger<IStatusDataService> logger)
        {
            context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<List<OrderStatus>> GetStatuses()
        {
            logger.LogInformation($"{nameof(GetStatuses)}.");
            var result = await context.DoWithResult<DbOrderStatus, List<DbOrderStatus>>(x => x.ToListAsync());
            return result.Select(x => x.To<OrderStatus>()).ToList();
        }
    }
}
