using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindNS;

namespace UnitTestNorthwind
{
    [TestClass]
    public class DbRepositoryUnitTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", AppDomain.CurrentDomain.BaseDirectory);
        }

        [TestMethod]
        public void Get_Order_With_Id_10571()
        {
            var db = new DbRepository();

            IQueryable<Order> orders = db.Orders;
            string productName = (from o in orders
                where o.OrderID == 10571
                select o.ShipName).FirstOrDefault();

            string expectedResult = "Ernst Handel";

            Assert.AreEqual(productName, expectedResult);
        }

        [TestMethod]
        public void Get_Product_With_Id_11()
        {
            var db = new DbRepository();
            IQueryable<Product> products = db.Products;
            string productName = (from p in products
                where p.ProductID == 11
                select p.ProductName).FirstOrDefault();

            string expectedResult = "Queso Cabrales";

            Assert.AreEqual(productName, expectedResult);
        }

        [TestMethod]
        public void Get_Category_With_CategoryID_1()
        {
            using (var db = new DbRepository())
            {
                DbSet<Category> categories = db.Categories;
                Category category = (from c in categories
                    where c.CategoryID == 1
                    select c).FirstOrDefault();

                string expectedCategoryName = "Beverages";
                string expectedDescription = "Soft drinks, coffees, teas, beers, and ales";

                Assert.AreEqual(category.CategoryName, expectedCategoryName);
                Assert.AreEqual(category.Description, expectedDescription);
            }
        }

        [TestMethod]
        public void Test_Create_Order()
        {
            var dbRepository = new DbRepository();

            int totalOrdersBefore = dbRepository.GetOrders.Count();
            dbRepository.CreateOrder(new Order());
            dbRepository.SaveChanges();
            int totalOrdersAfter = dbRepository.GetOrders.Count();
            Assert.AreEqual(totalOrdersBefore + 1, totalOrdersAfter);
        }


        [TestMethod]
        public void Check_If_Order_With_ID_10250_References_Three_Specific_Products()
        {
            var dbRepository = new DbRepository();

            ICollection<Order_Detail> orderDetails = (from order in dbRepository.GetOrders
                where order.OrderID == 10250
                select order.Order_Details).FirstOrDefault();

            List<string> orderProductNames = (from od in orderDetails
                select od.Product.ProductName).ToList();

            var expectedProductNames = new List<string>();
            expectedProductNames.Add("Jack's New England Clam Chowder");
            expectedProductNames.Add("Manjimup Dried Apples");
            expectedProductNames.Add("Louisiana Fiery Hot Pepper Sauce");

            bool result = orderProductNames.All(expectedProductNames.Contains) &&
                          orderProductNames.Count == expectedProductNames.Count;
            ;

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Check_If_Order_With_ID_10248_References_Three_OrderDetails()
        {
            var dbRepository = new DbRepository();

            ICollection<Order_Detail> orderDetails = (from order in dbRepository.GetOrders
                where order.OrderID == 10248
                select order.Order_Details).FirstOrDefault();

            const int expectedResult = 3;

            Assert.AreEqual(expectedResult, orderDetails.Count());
        }

        [TestMethod]
        public void Check_If_Product_Northwoods_Cranberry_Sauce_References_Category_Condiments()
        {
            var dbRepository = new DbRepository();

            string northwoodsCranberrySauceCategoryName = (from p in dbRepository.GetProducts
                where p.ProductName == "Northwoods Cranberry Sauce"
                select p.Category.CategoryName).FirstOrDefault();

            string expectedResult = "Condiments";

            Assert.AreEqual(expectedResult, northwoodsCranberrySauceCategoryName);
        }

        [TestMethod]
        public void Get_All_Products()
        {
            var dbRepository = new DbRepository();

            int products = dbRepository.GetProducts.Count();

            const int expectedResult = 77;

            Assert.AreEqual(expectedResult, products);
        }

        [TestMethod]
        public void Get_All_Orders()
        {
            var dbRepository = new DbRepository();

            int orders = dbRepository.GetOrders.Count();

            const int expectedResult = 830;

            Assert.AreEqual(expectedResult, orders);
        }

        [TestMethod]
        public void Get_All_Categories()
        {
            var dbRepository = new DbRepository();

            int orders = dbRepository.GetCategories.Count();

            const int expectedResult = 8;

            Assert.AreEqual(expectedResult, orders);
        }
    }
}