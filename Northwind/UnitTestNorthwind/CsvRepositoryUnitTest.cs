using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindNS;

namespace UnitTestNorthwind
{
    [TestClass]
    public class CsvRepositoryUnitTest
    {
        [TestMethod]
        public void Add_Order_To_Repository()
        {
            var csvRepository = new CsvRepository();
            var nw = new NorthWind(csvRepository);

            int totalOrdersBefore = nw.Orders.Count();
            nw.AddOrder("", "", "", "", "", "");
            int totalOrdersAfter = nw.Orders.Count();
            Assert.AreEqual(totalOrdersBefore + 1, totalOrdersAfter);
        }


        [TestMethod]
        public void Get_Order_With_Id_10571()
        {
            var csvRepository = new CsvRepository();

            var nw = new NorthWind(csvRepository);
            IQueryable<Order> orders = nw.Orders;
            string productName = (from o in orders
                where o.OrderID == 10571
                select o.ShipName).FirstOrDefault();

            string expectedResult = "Ernst Handel";

            Assert.AreEqual(productName, expectedResult);
        }
    }
}