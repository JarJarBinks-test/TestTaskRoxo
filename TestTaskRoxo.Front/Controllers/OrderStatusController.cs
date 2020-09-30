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
    public class OrderStatusController : ControllerBase
    {
        readonly IStatusService statusService;
        readonly ILogger<OrderStatusController> logger;

        public OrderStatusController(IStatusService statusService, ILogger<OrderStatusController> logger)
        {
            this.statusService = statusService ?? throw new ArgumentNullException(nameof(statusService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public Task<List<OrderStatus>> Get()
        {
            return statusService.GetOrderStatuses();
        }
    }
}
