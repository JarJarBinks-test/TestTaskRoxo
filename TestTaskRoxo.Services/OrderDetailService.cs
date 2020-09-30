using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestTaskRoxo.BaseClasses;
using TestTaskRoxo.BaseClasses.Interaces;
using TestTaskRoxo.Services.Interfaces;

namespace TestTaskRoxo.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        readonly ILogger<IOrderDetailService> logger;
        readonly IOrderDetailDataService orderDetailDataService;
        public OrderDetailService(IOrderDetailDataService orderDetailDataService, ILogger<IOrderDetailService> logger)
        {
            this.orderDetailDataService = orderDetailDataService ?? throw new ArgumentNullException(nameof(orderDetailDataService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Add(OrderDetail orderDetail)
        {
            logger.LogInformation($"{nameof(Add)} {orderDetail}.");
            throw new NotImplementedException($"{nameof(Add)} not implemented");
        }

        public void Remove(Int32 orderDetailId)
        {
            logger.LogInformation($"{nameof(Remove)} {orderDetailId}.");
            throw new NotImplementedException($"{nameof(Remove)} not implemented");
        }

        public void Update(OrderDetail orderDetail)
        {
            logger.LogInformation($"{nameof(Update)} {orderDetail}.");
            throw new NotImplementedException($"{nameof(Update)} not implemented");
        }

        public async Task<List<OrderDetail>> GetOrderDetails(int orderId)
        {
            logger.LogInformation($"{nameof(GetOrderDetails)}.");
            return await orderDetailDataService.GetOrderDetails(orderId);
        }
    }
}
