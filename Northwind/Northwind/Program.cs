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

        }
    }
}