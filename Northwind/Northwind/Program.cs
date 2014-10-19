using System;

namespace Northwind
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var r = new Repository();
            r.LoadFiles();

            foreach (Order item in r.GetOrders())
            {
                Console.WriteLine(item.ShipName);
            }
        }
    }
}