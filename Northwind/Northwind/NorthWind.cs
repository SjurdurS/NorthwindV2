using System;
using System.Collections.Generic;

namespace Northwind
{
    internal class NorthWind
    {
        private readonly IRepository _repository;

        public NorthWind(IRepository repository)
        {
            _repository = repository;
        }

        public List<Product> Products
        {
            get { return _repository.Products(); }
        }

        public List<Order> Orders
        {
            get { return _repository.Orders(); }
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

            long id = _repository.CreateOrder(order);

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

        public delegate void NewOrderEventHandler(Object sender, NewOrderEventArgs e);
    }
}