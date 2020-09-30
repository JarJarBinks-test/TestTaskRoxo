using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestTaskRoxo.BaseClasses;
using TestTaskRoxo.Services.Interfaces;

namespace TestTaskRoxo.Front.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        readonly IOrderService orderService;
        readonly ILogger<OrderController> logger;

        public OrderController(IOrderService orderService, ILogger<OrderController> logger)
        {
            this.orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public Task<List<Order>> Get()
        {
            return orderService.GetOrders();
        }
    }
}
