using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace NorthwindNS
{
    internal class Program
    {
        private static void NewOrder_Load(object sender, NorthWind.NewOrderEventArgs e)
        {
            Console.WriteLine("New Order Added:\nID: " + e.orderId + "\nDate: " + e.orderDate);
        }

        private static void Main(string[] args)
        {
            var csvRepository = new CsvRepository(new CsvFileLoader());

            var nwCsvPart2 = new NorthWind(csvRepository);


            // Subscribe..
            nwCsvPart2.NewOrder += NewOrder_Load;

            nwCsvPart2.AddOrder("Doegn Netto", "Rued Langgaards Vej 23a", "Copenhagen", "South", "2300", "Denmark");

            Console.WriteLine();
            Console.WriteLine("First five products:");
            List<Product> firstFiveProducts = nwCsvPart2.Products.Take(5).ToList();
            firstFiveProducts.ForEach(product => Console.WriteLine(product.ProductName));

            Console.WriteLine();
            var shippingCountryCount =
                nwCsvPart2.Orders.GroupBy(order => order.ShipCountry)
                    .Select(shipCountry => new
                    {
                        ShipCountry = shipCountry.Key,
                        Count = shipCountry.Count()
                    }).OrderByDescending(x => x.Count).ToList();
            shippingCountryCount.ForEach(
                shippingCountry => Console.WriteLine(shippingCountry.ShipCountry + " - " + shippingCountry.Count));


            var csvRepository2 = new CsvRepository();
            var nw1 = new NorthWind(csvRepository2);
            Console.WriteLine("\nTest CSV 1:");
            List<Order> csvOrders = nw1.Orders.AsEnumerable().Take(10).ToList();
            foreach (Order order in csvOrders)
            {
                order.Order_Details.ToList().ForEach(x => Console.WriteLine(x.Order.OrderID + " " + x.UnitPrice));
            }


            var dbRepository = new DbRepository();
            var nw = new NorthWind(dbRepository);
            Console.WriteLine("\nTest DB1:");
            List<Order> dbOrders = nw.Orders.AsEnumerable().Take(10).ToList();
            foreach (Order order in dbOrders)
            {
                order.Order_Details.ToList().ForEach(x => Console.WriteLine(x.Order.OrderID + " " + x.UnitPrice));
            }


            Console.WriteLine("\nTest2:");

            List<Product> products2 = nw.Products.AsEnumerable().Skip(10).Take(10).ToList();

            foreach (Product product in products2)
            {
                Console.WriteLine("Product2 name " + product.ProductID);
            }


            Console.WriteLine("\nTest3:");
            List<Product> products3 = nw.Products.AsEnumerable().Take(10).ToList();

            foreach (Product product in products3)
            {
                Console.WriteLine("Product3 name " + product.ProductID);
            }
        }
    }
}