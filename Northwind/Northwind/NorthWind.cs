using System;
using System.Linq;

namespace NorthWindNS
{
    public class NorthWind : IDisposable
    {
        public delegate void NewOrderEventHandler(Object sender, NewOrderEventArgs e);

        private readonly IRepository _context;

        public NorthWind(IRepository context)   // IRepository context = null
        {
            _context = context;                 // ?? new DbRepository();
        }

        public virtual IQueryable<Product> Products
        {
            get
            {
                return _context.GetProducts;
            }
        }

        public virtual IQueryable<Order> Orders
        {
            get
            {
                return _context.GetOrders;
            }
        }


        public void Dispose()
        {
            _context.Dispose();
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