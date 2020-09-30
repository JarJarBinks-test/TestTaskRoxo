using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestTaskRoxo.BaseClasses;
using TestTaskRoxo.BaseClasses.Interaces;
using TestTaskRoxo.Services.Interfaces;

namespace TestTaskRoxo.Services
{
    public class StatusService : IStatusService
    {
        readonly ILogger<IStatusService> logger;
        readonly IStatusDataService statusDataService;
        public StatusService(IStatusDataService statusDataService, ILogger<IStatusService> logger)
        {
            this.statusDataService = statusDataService ?? throw new ArgumentNullException(nameof(statusDataService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Add(OrderStatus orderStatus)
        {
            logger.LogInformation($"{nameof(Add)} {orderStatus}.");
            throw new NotImplementedException($"{nameof(Add)} not implemented");
        }

        public void Remove(Int32 orderStatusId)
        {
            logger.LogInformation($"{nameof(Remove)} {orderStatusId}.");
            throw new NotImplementedException($"{nameof(Remove)} not implemented");
        }

        public void Update(OrderStatus orderStatus)
        {
            logger.LogInformation($"{nameof(Update)} {orderStatus}.");
            throw new NotImplementedException($"{nameof(Update)} not implemented");
        }

        public async Task<List<OrderStatus>> GetOrderStatuses()
        {
            logger.LogInformation($"{nameof(GetOrderStatuses)}.");
            return await statusDataService.GetStatuses();
        }
    }
}
