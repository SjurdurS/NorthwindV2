using System;
using System.Security.Cryptography.X509Certificates;

namespace Northwind
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var r = new Repository();
            r.LoadFiles();

            NorthWind nw = new NorthWind(r);

            // Subscriber..
            
        }
    }
}