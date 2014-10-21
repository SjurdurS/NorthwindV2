using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthWind_Part_3;

namespace UnitTestNothWind_Part_3
{
    [TestClass]
    public class UnitTestRepository
    {
        [TestMethod]
        public void AddOrderToRepository_CheckTotalCount()
        {
            using (var db = new NorthWindContext())
            {
                NorthWind nw = new NorthWind(db);
                
                int totalOrdersBefore = nw.Orders.Count;
                nw.AddOrder("Doegn Netto", "Rued Langgaards Vej 23a", "Copenhagen", "South", "2300", "Denmark");
                db.SaveChanges();
                int totalOrdersAfter = nw.Orders.Count;
                Assert.AreEqual(totalOrdersBefore + 1, totalOrdersAfter);
            }
        }


        [TestMethod]
        public void Get_Order_With_Id_10571()
        {
            using (var db = new NorthWindContext())
            {
                NorthWind nw = new NorthWind(db);
                var orders = nw.Orders;
                var productName = (from o in orders
                    where o.OrderID == 10571
                    select o.ShipName).FirstOrDefault();

                string expectedResult = "Ernst Handel";

                Assert.AreEqual(productName, expectedResult);
            }
        }

        [TestMethod]
        public void Get_Product_With_Id_11()
        {
            using (var db = new NorthWindContext())
            {
                NorthWind nw = new NorthWind(db);
                var products = nw.Products;
                var productName = (from p in products
                    where p.ProductID == 11
                    select p.ProductName).FirstOrDefault();

                string expectedResult = "Queso Cabrales";

                Assert.AreEqual(productName, expectedResult);
            }
        }

        [TestMethod]
        public void Get_Category_With_CategoryID_1()
        {
            using (var db = new NorthWindContext())
            {


                var categories = db.Categories;
                var category = (from c in categories
                    where c.CategoryID == 1
                    select c).FirstOrDefault();

                string expectedCategoryName = "Beverages";
                string expectedDescription = "Soft drinks, coffees, teas, beers, and ales";

                Assert.AreEqual(category.CategoryName, expectedCategoryName);
                Assert.AreEqual(category.Description, expectedDescription);
            }
        }
    }
}
