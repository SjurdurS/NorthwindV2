using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindNS;

namespace UnitTestNorthwind
{
    public class MockCsvFileLoader : ICsvFileLoader
    {
        public MockCsvFileLoader()
        {
            orderLines = new[]
            {
                //OrderID;CustomerID;EmployeeID;OrderDate;RequiredDate;ShippedDate;ShipVia;Freight;ShipName;ShipAddress;ShipCity;ShipRegion;ShipPostalCode;ShipCountry

                "10248;VINET;5;04-07-1996 00:00;01-08-1996 00:00;16-07-1996 00:00;3;32.38;Vins et alcools Chevalier;59 rue de l'Abbaye;Reims;;51100;France"
                ,
                "10249;TOMSP;6;05-07-1996 00:00;16-08-1996 00:00;10-07-1996 00:00;1;11.61;Toms Spezialit„ten;Luisenstr. 48;Mnster;;44087;Germany"
                ,
                "10250;HANAR;4;08-07-1996 00:00;05-08-1996 00:00;;2;65.83;Hanari Carnes;Rua do Pa‡o, 67;Rio de Janeiro;RJ;05454-876;Brazil"
                ,
                "10251;VICTE;3;08-07-1996 00:00;05-08-1996 00:00;;1;41.34;Victuailles en stock;2, rue du Commerce;Lyon;;69004;France"
                ,
                "10252;SUPRD;4;09-07-1996 00:00;06-08-1996 00:00;11-07-1996 00:00;2;51.3;Suprˆmes d‚lices;Boulevard Tirou, 255;Charleroi;;B-6000;Belgium"
                ,
                "10253;HANAR;3;10-07-1996 00:00;24-07-1996 00:00;16-07-1996 00:00;2;58.17;Hanari Carnes;Rua do Pa‡o, 67;Rio de Janeiro;RJ;05454-876;Brazil"
                ,
                "10254;CHOPS;5;11-07-1996 00:00;08-08-1996 00:00;23-07-1996 00:00;2;22.98;Chop-suey Chinese;Hauptstr. 31;Bern;;3012;Switzerland"
                ,
                "10255;RICSU;9;12-07-1996 00:00;09-08-1996 00:00;15-07-1996 00:00;3;148.33;Richter Supermarkt;Starenweg 5;GenŠve;;1204;Switzerland"
                ,
                "10256;WELLI;3;15-07-1996 00:00;12-08-1996 00:00;17-07-1996 00:00;2;13.97;Wellington Importadora;Rua do Mercado, 12;Resende;SP;08737-363;Brazil"
                ,
                "10257;HILAA;4;16-07-1996 00:00;13-08-1996 00:00;22-07-1996 00:00;3;81.91;HILARION-Abastos;Carrera 22 con Ave. Carlos Soublette #8-35;San Crist¢bal;T chira;5022;Venezuela"
            };

            productLines = new[]
            {
                //ProductID;ProductName;SupplierID;CategoryID;QuantityPerUnit;UnitPrice;UnitsInStock;UnitsOnOrder;ReorderLevel;Discontinued
                "1;Chai;1;1;10 boxes x 20 bags;18;39;0;10;0",
                "2;Chang;1;1;24 - 12 oz bottles;19;17;40;25;0",
                "3;Aniseed Syrup;1;2;12 - 550 ml bottles;10;13;70;25;0",
                "4;Chef Anton's Cajun Seasoning;2;2;48 - 6 oz jars;22;53;0;0;0",
                "5;Chef Anton's Gumbo Mix;2;2;36 boxes;21.35;0;0;0;-1",
                "6;Grandma's Boysenberry Spread;3;2;12 - 8 oz jars;25;120;0;25;0",
                "7;Uncle Bob's Organic Dried Pears;3;1;12 - 1 lb pkgs.;30;15;0;10;0",
                "8;Northwoods Cranberry Sauce;3;2;12 - 12 oz jars;40;6;0;0;0",
                "9;Mishi Kobe Niku;4;1;18 - 500 g pkgs.;97;29;0;0;-1",
                "10;Ikura;4;1;12 - 200 ml jars;31;31;0;0;0"
            };

            categoryLines = new[]
            {
                //CategoryID;CategoryName;Description
                "1;Beverages;Soft drinks, coffees, teas, beers, and ales",
                "2;Condiments;Sweet and savory sauces, relishes, spreads, and seasonings"
            };

            orderDetailLines = new[]
            {
                //OrderID;ProductID;UnitPrice;Quantity;Discount
                "10248;1;14;12;0",
                "10248;2;9.8;10;0",
                "10248;3;34.8;5;0",
                "10249;1;18.6;9;0",
                "10249;5;42.4;40;0",
                "10250;3;7.7;10;0",
                "10250;4;42.4;35;0.15000001",
                "10250;5;16.8;15;0.15000001",
                "10251;1;16.8;6;5.0000001E-2",
                "10251;2;15.6;15;5.0000001E-2",
                "10251;3;16.8;20;0",
                "10252;4;64.8;40;5.0000001E-2",
                "10253;5;2;25;5.0000001E-2",
                "10253;6;27.2;40;0",
                "10254;7;10;20;0",
                "10255;8;14.4;42;0",
                "10256;9;16;40;0",
                "10256;10;3.6;15;0.15000001",
                "10257;10;19.2;21;0.15000001"
            };
        }

        public string[] categoryLines { get; private set; }
        public string[] productLines { get; private set; }
        public string[] orderLines { get; private set; }
        public string[] orderDetailLines { get; private set; }
    }

    [TestClass]
    public class CsvRepositoryUnitTest
    {
        [TestMethod]
        public void Test_Create_Order()
        {
            var csvRepository = new CsvRepository(new MockCsvFileLoader());

            int totalOrdersBefore = csvRepository.GetOrders.Count();
            csvRepository.CreateOrder(new Order());
            int totalOrdersAfter = csvRepository.GetOrders.Count();
            Assert.AreEqual(totalOrdersBefore + 1, totalOrdersAfter);
        }


        [TestMethod]
        public void Check_If_Order_With_ID_10250_References_Three_Specific_Products()
        {
            var csvRepository = new CsvRepository(new MockCsvFileLoader());

            ICollection<Order_Detail> orderDetails = (from order in csvRepository.GetOrders
                where order.OrderID == 10250
                select order.Order_Details).FirstOrDefault();

            IEnumerable<string> orderProductNames = (from od in orderDetails
                select od.Product.ProductName);

            var expectedProductNames = new List<object>();

            expectedProductNames.Add("Aniseed Syrup");
            expectedProductNames.Add("Chef Anton's Cajun Seasoning");
            expectedProductNames.Add("Chef Anton's Gumbo Mix");
            IEnumerable<object> expectedProductNamesEnumerable = expectedProductNames.AsEnumerable();

            bool result = expectedProductNames.SequenceEqual(expectedProductNamesEnumerable);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Check_If_Order_With_ID_10248_References_Three_OrderDetails()
        {
            var csvRepository = new CsvRepository(new MockCsvFileLoader());

            ICollection<Order_Detail> orderDetails = (from order in csvRepository.GetOrders
                where order.OrderID == 10248
                select order.Order_Details).FirstOrDefault();

            const int expectedResult = 3;

            Assert.AreEqual(expectedResult, orderDetails.Count());
        }

        [TestMethod]
        public void Check_If_Product_Northwoods_Cranberry_Sauce_References_Category_Condiments()
        {
            var csvRepository = new CsvRepository(new MockCsvFileLoader());

            string northwoodsCranberrySauceCategoryName = (from p in csvRepository.GetProducts
                where p.ProductName == "Northwoods Cranberry Sauce"
                select p.Category.CategoryName).FirstOrDefault();

            string expectedResult = "Condiments";

            Assert.AreEqual(expectedResult, northwoodsCranberrySauceCategoryName);
        }

        [TestMethod]
        public void Get_All_Products()
        {
            var csvRepository = new CsvRepository(new MockCsvFileLoader());

            int products = csvRepository.GetProducts.Count();

            const int expectedResult = 10;

            Assert.AreEqual(expectedResult, products);
        }

        [TestMethod]
        public void Get_All_Orders()
        {
            var csvRepository = new CsvRepository(new MockCsvFileLoader());

            int orders = csvRepository.GetOrders.Count();

            const int expectedResult = 10;

            Assert.AreEqual(expectedResult, orders);
        }

        [TestMethod]
        public void Get_All_Categories()
        {
            var csvRepository = new CsvRepository(new MockCsvFileLoader());

            int orders = csvRepository.GetCategories.Count();

            const int expectedResult = 2;

            Assert.AreEqual(expectedResult, orders);
        }
    }
}