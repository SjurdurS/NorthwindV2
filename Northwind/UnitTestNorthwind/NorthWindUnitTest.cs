using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NorthwindNS;

namespace UnitTestNorthwind
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

            mockNorthWind.Setup(t => t.GetOrders).Returns(It.IsAny<IQueryable<Order>>);

            var northWind = new NorthWind(mockNorthWind.Object);
            IQueryable<Order> orders = northWind.Orders;

            mockNorthWind.Verify(t => t.GetOrders);
        }

        [TestMethod]
        public void Test_Products()
        {
            var mockNorthWind = new Mock<IRepository>();

            mockNorthWind.Setup(t => t.GetProducts).Returns(It.IsAny<IQueryable<Product>>);

            var northWind = new NorthWind(mockNorthWind.Object);
            IQueryable<Product> orders = northWind.Products;

            mockNorthWind.Verify(t => t.GetProducts);
        }
    }
}