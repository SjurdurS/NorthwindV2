using System;
using System.Collections.Generic;

namespace Northwind
{
    internal class NorthWind
    {
        public List<Product> Products
        {
            get { return ; // list of products from the Repository somehow
            }
        }

        public List<Order> Orders
        {
            get { return  // list of orders from the Repository somehow
            }
        }

        public void AddOrder(string name, string address, string city, string region, string postalCode, string country)
        {
            Order order = new Order
            {
                ShipName = name,
                ShipAddress = address,
                ShipCity = city,
                ShipRegion = region,
                ShipPostalCode = postalCode,
                ShipCountry = country,
                RequiredDate = DateTime.Now
            };

            Repository.addOrder()
        }

        public void NewOrderEvent()
        {
        }
    }
}