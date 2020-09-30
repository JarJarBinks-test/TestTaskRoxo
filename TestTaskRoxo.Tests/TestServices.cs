using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using TestTaskRoxo.BaseClasses;
using TestTaskRoxo.BaseClasses.Interaces;
using TestTaskRoxo.Services;
using TestTaskRoxo.Services.Interfaces;

namespace TestTaskRoxo.Tests
{
    [TestClass]
    public class TestServices
    {
        Mock<IServiceProvider> mockServiceProvider;
        Mock<IOrderDataService> mockSourceService;
        Mock<IStatusDataService> mockSourceStatusService;
        Mock<IOrderDetailDataService> mockSourceOrderDetailService; 
        Mock<ILoggerFactory> mockILoggerFactory;


        [TestInitialize]
        public void Setup()
        {
            mockILoggerFactory = new Mock<ILoggerFactory>();
            mockILoggerFactory
                .Setup(x => x.CreateLogger(It.IsAny<String>()))
                .Returns(new Mock<ILogger>().Object);

            mockSourceService = new Mock<IOrderDataService>();
            mockSourceStatusService = new Mock<IStatusDataService>();
            mockSourceOrderDetailService = new Mock<IOrderDetailDataService>();

            mockServiceProvider = new Mock<IServiceProvider>();
            mockServiceProvider
                .Setup(x => x.GetService(typeof(ILoggerFactory)))
                .Returns(mockILoggerFactory.Object);

            mockServiceProvider
                .Setup(x => x.GetService(typeof(IOrderDataService)))
                .Returns(mockSourceService.Object);
            mockServiceProvider
                .Setup(x => x.GetService(typeof(IStatusDataService)))
                .Returns(mockSourceStatusService.Object);
            mockServiceProvider
                .Setup(x => x.GetService(typeof(IOrderDetailDataService)))
                .Returns(mockSourceOrderDetailService.Object);

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
        public void Get_Orders_Return_Orders()
        {
            // Arrange
            var service = new OrderService(mockSourceService.Object, mockILoggerFactory.Object.CreateLogger<OrderService>());
            var idForCheck = 2;
            var orders = new List<Order>()
            {
                new Order()
                {
                    OrderId = 1,
                    OrderStatusId =2,
                    DateCreated = new DateTime()
                },
                new Order()
                {
                    OrderId = idForCheck,
                    OrderStatusId = 1,
                    DateCreated = new DateTime()
                },
                new Order()
                {
                   OrderId = 3,
                    OrderStatusId = 1,
                    DateCreated = new DateTime()
                },
                new Order()
                {
                    OrderId = 4,
                    OrderStatusId = 2,
                    DateCreated = new DateTime()
                }
            };

            // Also check by others parameters should added.
            mockSourceService.Setup(nt => nt.GetOrders()).ReturnsAsync(() => {
                var res = orders.ToList();
                return res;
            });

            // Action
            var result = service.GetOrders().Result;

            // Assert
            var forCheck = result.FirstOrDefault(x => x.OrderId == idForCheck);
            var sourceForCheck = orders.FirstOrDefault(x => x.OrderId == idForCheck);
            mockSourceService.Verify(x => x.GetOrders(), Times.Exactly(1));
            Assert.AreEqual(orders.Count, result.Count);
            Assert.IsNotNull(forCheck);
            Assert.AreEqual(sourceForCheck, forCheck);
        }

        [TestMethod]
        public void Get_OrdersStatuses_Return_Statuses()
        {
            // Arrange
            var service = new StatusService(mockSourceStatusService.Object, mockILoggerFactory.Object.CreateLogger<StatusService>());
            var idForCheck = 2;
            var statuses = new List<OrderStatus>()
            {
                new OrderStatus()
                {
                    Name = "Test 1",
                    OrderStatusId = 1
                },
                new OrderStatus()
                {
                    Name = "Test 2",
                    OrderStatusId = idForCheck
                },
                new OrderStatus()
                {
                    Name = "Test 3",
                    OrderStatusId = 3
                },
                new OrderStatus()
                {
                    Name = "Test 4",
                    OrderStatusId = 4
                },
                new OrderStatus()
                {
                    Name = "Test 5",
                    OrderStatusId = 5
                },
            };

            // Also check by others parameters should added.
            mockSourceStatusService.Setup(nt => nt.GetStatuses()).ReturnsAsync(() => {
                return statuses.ToList();
            });

            // Action
            var result = service.GetOrderStatuses().Result;

            // Assert
            var forCheck = result.FirstOrDefault(x => x.OrderStatusId == idForCheck);
            var sourceForCheck = statuses.FirstOrDefault(x => x.OrderStatusId == idForCheck);
            mockSourceStatusService.Verify(x => x.GetStatuses(), Times.Exactly(1));
            Assert.AreEqual(statuses.Count, result.Count);
            Assert.IsNotNull(forCheck);
            Assert.AreEqual(sourceForCheck, forCheck);
        }

        [TestMethod]
        public void Get_OrderDetails_Return_Details()
        {
            // Arrange
            var service = new OrderDetailService(mockSourceOrderDetailService.Object, mockILoggerFactory.Object.CreateLogger<OrderDetailService>());
            var rnd = new Random();
            var orderDetails = new List<OrderDetail>()
            {
                new OrderDetail()
                {
                    OrderId = 1,
                    OrderDetailId = rnd.Next(500),
                    Price = rnd.Next(500) * 3.1415M,
                    ProductId = rnd.Next(1000),
                    Quantity = rnd.Next(55)
                },
                new OrderDetail()
                {
                    OrderId = 1,
                    OrderDetailId = rnd.Next(500),
                    Price = rnd.Next(500) * 3.1415M,
                    ProductId = rnd.Next(1000),
                    Quantity = rnd.Next(55)
                },
                new OrderDetail()
                {
                    OrderId = 2,
                    OrderDetailId = rnd.Next(500),
                    Price = rnd.Next(500) * 3.1415M,
                    ProductId = rnd.Next(1000),
                    Quantity = rnd.Next(55)
                },
                new OrderDetail()
                {
                    OrderId = 3,
                    OrderDetailId = rnd.Next(500),
                    Price = rnd.Next(500) * 3.1415M,
                    ProductId = rnd.Next(1000),
                    Quantity = rnd.Next(55)
                },
                new OrderDetail()
                {
                    OrderId = 3,
                    OrderDetailId = rnd.Next(500),
                    Price = rnd.Next(500) * 3.1415M,
                    ProductId = rnd.Next(1000),
                    Quantity = rnd.Next(55)
                },
                new OrderDetail()
                {
                    OrderId = 3,
                    OrderDetailId = rnd.Next(500),
                    Price = rnd.Next(500) * 3.1415M,
                    ProductId = rnd.Next(1000),
                    Quantity = rnd.Next(55)
                },
            };

            // Also check by others parameters should added.
            var idFromRequest = 0;
            mockSourceOrderDetailService.Setup(nt => nt.GetOrderDetails(It.IsAny<Int32>())).Callback<Int32>((x) => {
                idFromRequest = x;
            }).ReturnsAsync(() => {
                var res = orderDetails.Where(x=>x.OrderId == idFromRequest);
                return res.ToList();
            });

            // Action
            var result0 = service.GetOrderDetails(0).Result;
            var result1 = service.GetOrderDetails(1).Result;
            var result2 = service.GetOrderDetails(2).Result;
            var result3 = service.GetOrderDetails(3).Result;

            // Assert
            mockSourceOrderDetailService.Verify(x => x.GetOrderDetails(It.IsAny<Int32>()), Times.Exactly(4));
            Assert.AreEqual(0, result0.Count);
            Assert.AreEqual(2, result1.Count);
            Assert.AreEqual(1, result2.Count);
            Assert.AreEqual(3, result3.Count);

            Assert.IsTrue(result1.TrueForAll(x => x.OrderId == 1));
            Assert.IsTrue(result2.TrueForAll(x => x.OrderId == 2));
            Assert.IsTrue(result3.TrueForAll(x => x.OrderId == 3));
        }
    }
}
