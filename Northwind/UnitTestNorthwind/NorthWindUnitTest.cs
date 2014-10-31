using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NorthwindNS;

namespace UnitTestNothWind
{
    [TestClass]
    public class NorthWindUnitTest
    {
        [TestMethod]
        public void Test_AddOrder()
        {
            var mockNorthWind = new Mock<IRepository>();

            mockNorthWind.Setup(t => t.CreateOrder(It.IsAny<Order>())).Returns(1);

            var northWind = new NorthWind(mockNorthWind.Object);
            northWind.AddOrder("", "", "", "", "", "");

            mockNorthWind.Verify(t => t.CreateOrder(It.IsAny<Order>()));
        }

        [TestMethod]
        public void Test_Orders()
        {
            var mockNorthWind = new Mock<IRepository>();

            mockNorthWind.Setup(t => t.GetOrders);

            var northWind = new NorthWind(mockNorthWind.Object);
            IQueryable<Order> orders = northWind.Orders;

            mockNorthWind.Verify(t => t.GetOrders);
        }

        [TestMethod]
        public void Test_Products()
        {
            var mockNorthWind = new Mock<IRepository>();

            mockNorthWind.Setup(t => t.GetProducts);

            var northWind = new NorthWind(mockNorthWind.Object);
            IQueryable<Order> orders = northWind.Orders;

            mockNorthWind.Verify(t => t.GetProducts);
        }
    }
}