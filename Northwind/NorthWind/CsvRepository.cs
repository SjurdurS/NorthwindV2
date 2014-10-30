using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace NorthwindNS
{
    /// <summary>
    /// </summary>
    public class CsvRepository : IRepository
    {
        private List<Category> _categories = new List<Category>();
        private List<Order_Detail> _orderDetails = new List<Order_Detail>();
        private List<Order> _orders = new List<Order>();
        private List<Product> _products = new List<Product>();

        public CsvRepository()
        {
            LoadFiles();
        }

        public IQueryable<Category> GetCategories
        {
            get { return _categories.AsQueryable(); }
        }

        public IQueryable<Order> GetOrders
        {
            get { return _orders.AsQueryable(); }
        }

        public IQueryable<Product> GetProducts
        {
            get { return _products.AsQueryable(); }
        }

        /// <summary>
        ///     Create a new order and add it to the repository.
        /// </summary>
        /// <param name="order">The order to create</param>
        /// <returns>Returns the new maximum id in the list.</returns>
        public int CreateOrder(Order order)
        {
            // Or store max in field variable.
            int maxId = _orders.Max(x => x.OrderID);
            order.OrderID = maxId + 1;
            _orders.Add(order);
            return (maxId + 1);
        }

        public void Dispose()
        {
            // Do nothing.
        }

        public int SaveChanges()
        {
            return 0; // Does nothing
        }

        /// <summary>
        ///     Parse the CSV files into the repository.
        ///     Parsing removes relational IDs but keeps Primary keys of the entities.
        ///     We reuse the entity classes that are also used by the Entity Framework
        ///     for the DatabaseRepository to avoid duplication of the entity classes.
        /// </summary>
        public void LoadFiles()
        {
            string[] allLinesCategories = File.ReadAllLines(@"../../Resources/categories.csv").Skip(1).ToArray();

            IEnumerable<Category> categoriesEnumerable = from line in allLinesCategories
                let data = line.Split(';')
                select new Category
                {
                    CategoryID = Convert.ToInt32(data[0]),
                    CategoryName = data[1],
                    Description = data[2]
                };


            string[] allLinesProducts = File.ReadAllLines(@"../../Resources/products.csv").Skip(1).ToArray();

            IEnumerable<Product> productsEnumerable = from line in allLinesProducts
                let data = line.Split(';')
                select new Product
                {
                    ProductID = Convert.ToInt32(data[0]),
                    ProductName = data[1],
                    Category = (from c in categoriesEnumerable
                        where c.CategoryID == StringConverter.ToNullableInt32(data[3])
                        select c).FirstOrDefault(),
                    QuantityPerUnit = data[4],
                    UnitPrice = StringConverter.ToNullableDecimal(data[5]),
                    UnitsInStock = StringConverter.ToNullableInt16(data[6]),
                    UnitsOnOrder = StringConverter.ToNullableInt16(data[7]),
                    ReorderLevel = StringConverter.ToNullableInt16(data[8]),
                    Discontinued = Convert.ToBoolean(Convert.ToInt32(data[9]))
                };


            string[] allLinesOrders = File.ReadAllLines(@"../../Resources/orders.csv").Skip(1).ToArray();

            IEnumerable<Order> ordersEnumerable = from line in allLinesOrders
                let data = line.Split(';')
                select new Order
                {
                    OrderID = Convert.ToInt32(data[0]),
                    OrderDate = StringConverter.ToNullableDateTime(data[3]),
                    RequiredDate = StringConverter.ToNullableDateTime(data[4]),
                    ShippedDate = StringConverter.ToNullableDateTime(data[5]),
                    ShipVia = StringConverter.ToNullableInt32(data[6]),
                    Freight = StringConverter.ToNullableDecimal(data[7]),
                    ShipName = data[8],
                    ShipAddress = data[9],
                    ShipCity = data[10],
                    ShipRegion = data[11],
                    ShipPostalCode = data[12],
                    ShipCountry = data[13]
                };

            string[] allLinesOrderDetails = File.ReadAllLines(@"../../Resources/order_details.csv").Skip(1).ToArray();

            IEnumerable<Order_Detail> orderDetailsEnumerable = from line in allLinesOrderDetails
                let data = line.Split(';')
                select new Order_Detail
                {
                    Order = (from o in ordersEnumerable
                        where o.OrderID == Convert.ToInt32(data[0])
                        select o).FirstOrDefault(),
                    Product = (from p in productsEnumerable
                        where p.ProductID == Convert.ToInt32(data[1])
                        select p).FirstOrDefault(),
                    UnitPrice = Convert.ToDecimal(data[2], CultureInfo.InvariantCulture),
                    Quantity = Convert.ToInt16(data[3]),
                    Discount = StringConverter.ToNullableFloat(data[4])
                };

            _categories = categoriesEnumerable.ToList();
            _products = productsEnumerable.ToList();
            _orders = ordersEnumerable.ToList();
            _orderDetails = orderDetailsEnumerable.ToList();

            _orders.ForEach(x => x.GetOrderDetailsReferences(_orderDetails));
            _products.ForEach(x => x.GetOrderDetailsReferences(_orderDetails));
        }
    }
}