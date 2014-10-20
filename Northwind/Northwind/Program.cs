using System;
using System.Collections.Generic;
using System.Linq;

namespace Northwind
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var r = new Repository();
            r.LoadFiles();

            var nw = new NorthWind(r);

            // Subscribe..
            nw.NewOrder += NewOrder_Load;

            nw.AddOrder("Doegn Netto", "Rued Langgaards Vej 23a", "Copenhagen", "South", "2300", "Denmark");

            Console.WriteLine();
            Console.WriteLine("First five products:");
            List<Product> firstFiveProducts = nw.Products.Take(5).ToList();
            firstFiveProducts.ForEach(product => Console.WriteLine(product.Name));

            Console.WriteLine();
            var shippingCountryCount =
                nw.Orders.GroupBy(order => order.ShipCountry)
                    .Select(shipCountry => new
                    {
                        ShipCountry = shipCountry.Key,
                        Count = shipCountry.Count()
                    }).OrderByDescending(x => x.Count).ToList();
            shippingCountryCount.ForEach(
                shippingCountry => Console.WriteLine(shippingCountry.ShipCountry + " - " + shippingCountry.Count));
        }

        private static void NewOrder_Load(object sender, NorthWind.NewOrderEventArgs e)
        {
            Console.WriteLine("New Order Added:\nID: " + e.orderId + "\nDate: " + e.orderDate);
        }
    }
}