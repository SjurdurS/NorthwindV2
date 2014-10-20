using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Northwind;

namespace UnitTestNorthWind
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AddOrderToRepository_CheckTotalCount()
        {
            var r = new Repository();
            r.LoadFiles();

            var nw = new NorthWind(r);
            int totalOrdersBefore = nw.Orders.Count;
            nw.AddOrder("Doegn Netto", "Rued Langgaards Vej 23a", "Copenhagen", "South", "2300", "Denmark");
            int totalOrdersAfter = nw.Orders.Count;
            Assert.AreEqual(totalOrdersBefore + 1, totalOrdersAfter);
        }


        [TestMethod]
        [ExpectedException(typeof (NullReferenceException))]
        public void InitializeNorthWind_Without_Loading_Files()
        {
            var r = new Repository();
            var nw = new NorthWind(r);
            int ordersCount = nw.Orders.Count;
        }

        [TestMethod]
        public void Get_Order_With_Id_10571()
        {
            var r = new Repository();
            r.LoadFiles();

            var nw = new NorthWind(r);
            var orders = nw.Orders;
            var productName = (from o in orders
                where o.OrderId == 10571
                select o.ShipName).FirstOrDefault();

            string expectedResult = "Ernst Handel";

            Assert.AreEqual(productName, expectedResult);
        }        
        
        [TestMethod]
        public void Get_Product_With_Id_11()
        {
            var r = new Repository();
            r.LoadFiles();

            var nw = new NorthWind(r);
            var products = nw.Products;
            var productName = (from p in products
                where p.Id == 11
                select p.Name).FirstOrDefault();

            string expectedResult = "Queso Cabrales";

            Assert.AreEqual(productName, expectedResult);
        } 
       
        [TestMethod]
        public void Get_Category_With_CategoryID_1()
        {
            var r = new Repository();
            r.LoadFiles();

            var categories = r.Categories();
            var category = (from c in categories
                where c.Id == 1
                select c).FirstOrDefault();

            string expectedCategoryName = "Beverages";
            string expectedDescription = "Soft drinks, coffees, teas, beers, and ales";

            Assert.AreEqual(category.Name, expectedCategoryName);
            Assert.AreEqual(category.Description, expectedDescription);
        }
    }
}