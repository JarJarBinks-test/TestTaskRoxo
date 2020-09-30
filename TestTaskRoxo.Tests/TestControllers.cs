using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTaskRoxo.BaseClasses;
using TestTaskRoxo.BaseClasses.Interaces;
using TestTaskRoxo.Front.Controllers;
using TestTaskRoxo.Services.Interfaces;

namespace TestTaskRoxo.Tests
{
    [TestClass]
    public class TestControllers
    {
        Mock<IServiceProvider> mockServiceProvider;
        Mock<IOrderDataService> mockSourceService;

        Mock<IOrderService> mockOrderService;
        Mock<IStatusService> mockStatusService;
        Mock<IOrderDetailService> mockOrderDetailService;
        Mock<ILoggerFactory> mockILoggerFactory;


        [TestInitialize]
        public void Setup()
        {
            mockILoggerFactory = new Mock<ILoggerFactory>();
            mockILoggerFactory
                .Setup(x => x.CreateLogger(It.IsAny<String>()))
                .Returns(new Mock<ILogger>().Object);

            mockSourceService = new Mock<IOrderDataService>();
            mockOrderService = new Mock<IOrderService>();
            mockStatusService = new Mock<IStatusService>();
            mockOrderDetailService = new Mock<IOrderDetailService>();

            mockServiceProvider = new Mock<IServiceProvider>();
            mockServiceProvider
                .Setup(x => x.GetService(typeof(ILoggerFactory)))
                .Returns(mockILoggerFactory.Object);

            mockServiceProvider
                .Setup(x => x.GetService(typeof(IOrderDataService)))
                .Returns(mockSourceService.Object);

            mockServiceProvider
                .Setup(x => x.GetService(typeof(IOrderService)))
                .Returns(mockOrderService.Object);
            mockServiceProvider
               .Setup(x => x.GetService(typeof(IStatusService)))
               .Returns(mockStatusService.Object);
            mockServiceProvider
               .Setup(x => x.GetService(typeof(IOrderDetailService)))
               .Returns(mockOrderDetailService.Object);

            var serviceScope = new Mock<IServiceScope>();
            serviceScope.Setup(x => x.ServiceProvider).Returns(mockServiceProvider.Object);

            var serviceScopeFactory = new Mock<IServiceScopeFactory>();
            serviceScopeFactory
                .Setup(x => x.CreateScope())
                .Returns(serviceScope.Object);

            mockServiceProvider
                .Setup(x => x.GetService(typeof(IServiceScopeFactory)))
                .Returns(serviceScopeFactory.Object);
        }

        [TestCleanup]
        public void CleanUp()
        {
        }

        [TestMethod]
        public void Controller_Get_Order_No_Error()
        {
            // Arrange
            var orderController = new OrderController(mockOrderService.Object, mockILoggerFactory.Object.CreateLogger<OrderController>());
            var ord = new Order()
            {
                OrderId = -1,
                DateCreated = DateTime.Now,
                OrderStatusId = 2
            };
            mockOrderService.Setup(nt => nt.GetOrders()).Returns(Task.Run(() => new List<Order>() { ord }));

            // Action
            var ar = orderController.Get().Result;

            // Assert
            mockOrderService.Verify(x => x.GetOrders(), Times.Once);
            Assert.IsInstanceOfType(ar, typeof(List<Order>));
        }

        [TestMethod]
        public void Controller_Get_Status_No_Error()
        {
            // Arrange
            var orderController = new OrderStatusController(mockStatusService.Object, mockILoggerFactory.Object.CreateLogger<OrderStatusController>());
            var ord = new OrderStatus()
            {
                OrderStatusId = -1,
                Name = "TestName"
            };
            mockStatusService.Setup(nt => nt.GetOrderStatuses()).Returns(Task.Run(() => new List<OrderStatus>() { ord }));

            // Action
            var ar = orderController.Get().Result;

            // Assert
            mockStatusService.Verify(x => x.GetOrderStatuses(), Times.Once);
            Assert.IsInstanceOfType(ar, typeof(List<OrderStatus>));
        }

        [TestMethod]
        public void Controller_Get_Orders_Return_Orders()
        {
            // Arrange
            var orderDetailController = new OrderDetailController(mockOrderDetailService.Object, mockILoggerFactory.Object.CreateLogger<OrderDetailController>());

            var rnd = new Random();
            var ordersDetailsQty = 49;
            var currDate = DateTime.UtcNow;
            var orders = Enumerable.Range(1, ordersDetailsQty).Select(x => new OrderDetail()
            {
                OrderDetailId = x,
                Price = rnd.Next(x * 1000) * 3.1414M + new Random().Next(x) / 100,
                Quantity = rnd.Next(x * 1000),
                ProductId = rnd.Next(5),
                OrderId = x/10 + 1
            }).ToList();
            var idForCheck = orders[new Random().Next(ordersDetailsQty)].OrderId;
            var badIdForCheck = -10000;

            var firstNum = rnd.Next(ordersDetailsQty - 1);

            Int32 orderid = 0;
            var ordersResults = new List<List<OrderDetail>>();
            mockOrderDetailService.Setup(nt => nt.GetOrderDetails(It.IsAny<Int32>())).Callback<Int32>((x) => {
                orderid = x;
            }).ReturnsAsync(() => {
                var res = orders.Where(x => x.OrderId == orderid).ToList();
                ordersResults.Add(res);
                return res;
            });

            // Action
            var ordersIds = orders.Select(x => x.OrderId).Distinct().ToList();
            var result = orderDetailController.Get(ordersIds[0]).Result;
            var resultId = orderDetailController.Get(idForCheck).Result;
            var resultbadId = orderDetailController.Get(badIdForCheck).Result;

            // Assert
            mockOrderDetailService.Verify(x => x.GetOrderDetails(ordersIds[0]), Times.Once);
            mockOrderDetailService.Verify(x => x.GetOrderDetails(idForCheck), Times.Once);
            mockOrderDetailService.Verify(x => x.GetOrderDetails(badIdForCheck), Times.Once);

            Assert.AreEqual(3, ordersResults.Count);

            var resultType = typeof(List<OrderDetail>);
            Assert.IsInstanceOfType(result, resultType);
            Assert.IsInstanceOfType(resultId, resultType);
            Assert.IsInstanceOfType(resultbadId, resultType);
        }
    }
}
