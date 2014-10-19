using System.Collections.Generic;
using System.Linq;
using LINQtoCSV;

namespace Northwind
{
    internal class Repository
    {
        private List<Category> Categories;
        private List<OrderDetail> OrderDetails;
        private List<Order> Orders;
        private List<Product> Products;

        private int maxOrderID = 0;

        /// <summary>
        ///     Parse the CSV files into
        /// </summary>
        public void ReadFiles()
        {
            var inputFileDescription = new CsvFileDescription
            {
                SeparatorChar = ';',
                FirstLineHasColumnNames = true
            };

            var cc = new CsvContext();

            IEnumerable<Category> categoriesEnumerable = cc.Read<Category>("../../Resources/categories.csv",
                inputFileDescription);
            IEnumerable<Product> productsEnumerable = cc.Read<Product>("../../Resources/products.csv",
                inputFileDescription);
            IEnumerable<Order> ordersEnumerable = cc.Read<Order>("../../Resources/orders.csv", inputFileDescription);
            IEnumerable<OrderDetail> orderDetailsEnumerable = cc.Read<OrderDetail>("../../Resources/order_details.csv",
                inputFileDescription);

            Categories = categoriesEnumerable.ToList();
            Products = productsEnumerable.ToList();
            Orders = ordersEnumerable.ToList();
            OrderDetails = orderDetailsEnumerable.ToList();

            UpdateObjectReferences();
        }

        /// <summary>
        ///     Update the Object references.
        /// </summary>
        private void UpdateObjectReferences()
        {
            Products.ForEach(p => p.GetCategoryReference(Categories));

            OrderDetails.ForEach(o => o.GetProductReference(Products));
        }

        public List<Product> GetProducts()
        {
            return Products;
        }

        public List<Category> GetCategories()
        {
            return Categories;
        }

        public List<Order> GetOrders()
        {
            return Orders;
        }

        public void CreateOrder(Order order)
        {
            var maxId = Orders.Max(x => x.OrderId);
            order.OrderId = maxId + 1;
            Orders.Add(order);
        }
    }
}