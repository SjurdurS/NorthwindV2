using System;
using System.Collections.Generic;
using System.Linq;

namespace NorthWindNS
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var csvRepository = new CsvRepository();
            var nw1 = new NorthWind(csvRepository);


            Console.WriteLine("\nTest CSV 1:");

            List<Product> products0 = nw1.Products.AsEnumerable().Take(10).ToList();

            foreach (Product product in products0)
            {
                Console.WriteLine("Product name " + product.ProductID);
            }


            var dbRepository = new DbRepository();
            var nw = new NorthWind(dbRepository);


            Console.WriteLine("\nTest DB1:");

            List<Product> products = nw.Products.AsEnumerable().Take(10).ToList();

            foreach (Product product in products)
            {
                Console.WriteLine("Product name " + product.ProductID);
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