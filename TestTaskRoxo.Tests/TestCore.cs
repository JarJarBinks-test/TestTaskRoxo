using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TestTaskRoxo.BaseClasses;
using TestTaskRoxo.Core;
using TestTaskRoxo.Core.Tables;

namespace TestTaskRoxo.Tests
{
    [TestClass]
    public class TestCore
    {
        Mock<IServiceProvider> serviceProvider;
        Mock<ITestTaskDbContext> sourceService;
        Mock<ILoggerFactory> mockILoggerFactory;

        [TestInitialize]
        public void Setup()
        {
            mockILoggerFactory = new Mock<ILoggerFactory>();
            mockILoggerFactory
                .Setup(x => x.CreateLogger(It.IsAny<String>()))
                .Returns(new Mock<ILogger>().Object);

            serviceProvider = new Mock<IServiceProvider>();
            serviceProvider
                .Setup(x => x.GetService(typeof(ILoggerFactory)))
                .Returns(mockILoggerFactory.Object);

            sourceService = new Mock<ITestTaskDbContext>();
            serviceProvider
                .Setup(x => x.GetService(typeof(ITestTaskDbContext)))
                .Returns(sourceService.Object);

            var serviceScope = new Mock<IServiceScope>();
            serviceScope.Setup(x => x.ServiceProvider).Returns(serviceProvider.Object);

            var serviceScopeFactory = new Mock<IServiceScopeFactory>();
            serviceScopeFactory
                .Setup(x => x.CreateScope())
                .Returns(serviceScope.Object);

            serviceProvider
                .Setup(x => x.GetService(typeof(IServiceScopeFactory)))
                .Returns(serviceScopeFactory.Object);
        }

        [TestCleanup]
        public void CleanUp()
        {
        }

        // FIXME: Disable for now. AsAsyncEnumerable issue.
        //[TestMethod]
        public void Get_Orders()
        {
            // Arrange
            var orderDataService = new OrderDataService(sourceService.Object, mockILoggerFactory.Object.CreateLogger<OrderDataService>());
            var rnd = new Random();
            var ordersQty = 20;
            var currDate = DateTime.UtcNow;

            var itms = Enumerable.Range(1, ordersQty).Select(x => new Order()
            {
                OrderId = rnd.Next(x * 1000),
                OrderStatusId = rnd.Next(x * 1000),
                DateCreated = currDate.AddDays(x)
            }.To<DbOrder>()).ToList();
            var idForCheck = itms[new Random().Next(ordersQty)].OrderId;

            var firstNum = rnd.Next(ordersQty - 1);
            var firstDate = itms[firstNum].DateCreated;
            var secondNum = rnd.Next(firstNum, ordersQty);
            var secondDate = itms[secondNum].DateCreated;

            var ordersMock = TestHelper.ToDbSetMock(itms);
            sourceService.Setup(x => x.Orders).Returns(ordersMock.Object);
            sourceService.Setup(x => x.Set<DbOrder>()).Returns(ordersMock.Object);

            // Action
            var result = orderDataService.GetOrders().Result;

            // Assert
            ordersMock.Verify(x => x.Where(It.IsAny<Expression<Func<DbOrder, bool>>>()), Times.Exactly(1));
            sourceService.Verify(x => x.Set<DbOrder>(), Times.Exactly(4));

            Assert.AreEqual(ordersQty, result.Count);
        }
    }
}
