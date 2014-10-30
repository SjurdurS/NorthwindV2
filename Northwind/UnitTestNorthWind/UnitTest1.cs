using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindNS;

namespace UnitTestNorthwind
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AddOrderToRepository_CheckTotalCount()
        {
            var csvRepository = new CsvRepository();
            csvRepository.LoadFiles();

            NorthWind nw = new NorthWind(csvRepository);
            int totalOrdersBefore = nw.Orders.Count();
            nw.AddOrder("Doegn Netto", "Rued Langgaards Vej 23a", "Copenhagen", "South", "2300", "Denmark");
            int totalOrdersAfter = nw.Orders.Count();
            Assert.AreEqual(totalOrdersBefore + 1, totalOrdersAfter);
        }


        [TestMethod]
        public void Get_Order_With_Id_10571()
        {
            var csvRepository = new CsvRepository();
            csvRepository.LoadFiles();

            NorthWind nw = new NorthWind(csvRepository);
            var orders = nw.Orders;
            var productName = (from o in orders
                where o.OrderID == 10571
                select o.ShipName).FirstOrDefault();

            string expectedResult = "Ernst Handel";

            Assert.AreEqual(productName, expectedResult);
        }

        [TestMethod]
        public void Get_Product_With_Id_11()
        {
            var csvRepository = new CsvRepository();
            csvRepository.LoadFiles();

            NorthWind nw = new NorthWind(csvRepository);
            var products = nw.Products;
            var productName = (from p in products
                where p.ProductID == 11
                select p.ProductName).FirstOrDefault();

            string expectedResult = "Queso Cabrales";

            Assert.AreEqual(productName, expectedResult);
        }


    }
}