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
    public class OrderDetailController : ControllerBase
    {
        readonly IOrderDetailService orderDetailService;
        readonly ILogger<OrderDetailController> logger;

        public OrderDetailController(IOrderDetailService orderDetailService, ILogger<OrderDetailController> logger)
        {
            this.orderDetailService = orderDetailService ?? throw new ArgumentNullException(nameof(orderDetailService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("{id}")]
        public Task<List<OrderDetail>> Get(Int32 id)
        {
            return orderDetailService.GetOrderDetails(id);
        }
    }
}
