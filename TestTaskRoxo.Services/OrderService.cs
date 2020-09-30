using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestTaskRoxo.BaseClasses;
using TestTaskRoxo.BaseClasses.Interaces;
using TestTaskRoxo.Services.Interfaces;

namespace TestTaskRoxo.Services
{
    public class OrderService : IOrderService
    {
        readonly ILogger<IOrderService> logger;
        readonly IOrderDataService orderDataService;
        public OrderService(IOrderDataService orderDataService, ILogger<IOrderService> logger)
        {
            this.orderDataService = orderDataService ?? throw new ArgumentNullException(nameof(orderDataService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Add(Order order)
        {
            logger.LogInformation($"{nameof(Add)} {order}.");
            throw new NotImplementedException($"{nameof(Add)} not implemented");
        }

        public void Remove(Int32 orderId)
        {
            logger.LogInformation($"{nameof(Remove)} {orderId}.");
            throw new NotImplementedException($"{nameof(Remove)} not implemented");
        }

        public void Update(Order order)
        {
            logger.LogInformation($"{nameof(Update)} {order}.");
            throw new NotImplementedException($"{nameof(Update)} not implemented");
        }

        public async Task<List<Order>> GetOrders()
        {
            logger.LogInformation($"{nameof(GetOrders)}.");
            return await orderDataService.GetOrders();
        }
    }
}
