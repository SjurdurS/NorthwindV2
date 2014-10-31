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
            var mockRository = new Mock<IRepository>();

            mockRository.Setup(t => t.CreateOrder(It.IsAny<Order>())).Returns(1);

            var northWind = new NorthWind(mockRository.Object);
            northWind.AddOrder("", "", "", "", "", "");

            mockRository.Verify(t => t.CreateOrder(It.IsAny<Order>()));
        }

        [TestMethod]
        public void Test_Orders()
        {
            var mockRository = new Mock<IRepository>();

            mockRository.Setup(t => t.GetOrders).Returns(It.IsAny<IQueryable<Order>>);

            var northWind = new NorthWind(mockRository.Object);
            IQueryable<Order> orders = northWind.Orders;

            mockRository.Verify(t => t.GetOrders);
        }

        [TestMethod]
        public void Test_Products()
        {
            var mockRository = new Mock<IRepository>();

            mockRository.Setup(t => t.GetProducts).Returns(It.IsAny<IQueryable<Product>>);

            var northWind = new NorthWind(mockRository.Object);
            IQueryable<Product> orders = northWind.Products;

            mockRository.Verify(t => t.GetProducts);
        }
    }
}