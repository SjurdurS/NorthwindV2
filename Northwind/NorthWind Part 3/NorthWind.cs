using System;
using System.Collections.Generic;
using System.Linq;

namespace NorthWind_Part_3
{
    internal class NorthWind : IDisposable
    {
        public delegate void NewOrderEventHandler(Object sender, NewOrderEventArgs e);

        private readonly INorthWindContext _context;

        public NorthWind(INorthWindContext context = null)
        {
            _context = context ?? new NorthWindContext();
        }

        public List<Product> Products
        {
            get { return _context.Products.ToList(); }
        }

        public List<Order> Orders
        {
            get { return _context.Orders.ToList(); }
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
            long id = _context.CreateOrder(order);
            var args = new NewOrderEventArgs();
            args.orderId = id;
            args.orderDate = DateTime.Now;
            OnNewOrder(args);
        }

        public void Dispose()
        {
            _context.Dispose();
        }


        public event EventHandler<NewOrderEventArgs> NewOrder;

        protected virtual void OnNewOrder(NewOrderEventArgs e)
        {
            EventHandler<NewOrderEventArgs> handler = NewOrder;
            if (handler != null) handler(this, e);
        }


        public class NewOrderEventArgs : EventArgs
        {
            public long orderId { get; set; }
            public DateTime orderDate { get; set; }
        }
    }
}