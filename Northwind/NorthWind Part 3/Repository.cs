using System;

namespace NorthWind_Part_3
{
    internal class Repository : IDisposable
    {
        private readonly INorthWindContext _context;

        public Repository(INorthWindContext context = null)
        {
            _context = context ?? new NorthWindContext();
        }

        public void AddOrder(string name, string address, string city, string region, string postalCode, string country)
        {
             var order = new Order
            {
                ShipName = name,
                ShipAddress = address,
                ShipCity = city,
                ShipRegion = region,
                ShipPostalCode = postalCode,
                ShipCountry = country,
                RequiredDate = DateTime.Now
            };
            _context.CreateOrder(order);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}