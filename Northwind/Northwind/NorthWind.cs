using System;
using System.Collections.Generic;

namespace Northwind
{

    public class NorthWind
    {
        public delegate void NewOrderEventHandler(Object sender, NewOrderEventArgs e);

        private readonly IRepository _repository;

        /// <summary>
        ///     Initialize a new NorthWind object.
        /// </summary>
        /// <param name="repository">The repository to use</param>
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

        /// <summary>
        ///     Add a new order to the repository. Creates a new order event that informs that this method has been invoked.
        /// </summary>
        /// <param name="name">Shipping name of the order.</param>
        /// <param name="address">Shipping address of the order.</param>
        /// <param name="city">Shipping city of the order.</param>
        /// <param name="region">Shipping region of the order.</param>
        /// <param name="postalCode">Shipping postal code of the order</param>
        /// <param name="country">Shipping country of the order.</param>
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
    }
}