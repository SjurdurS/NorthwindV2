using System.Collections.Generic;
using System.Linq;
using LINQtoCSV;

namespace Northwind
{
    public class Repository : IRepository
    {
        private List<Category> _categories;
        private List<OrderDetail> _orderDetails;
        private List<Order> _orders;
        private List<Product> _products;

        public List<Product> Products()
        {
            return _products;
        }

        public List<Category> Categories()
        {
            return _categories;
        }

        public List<Order> Orders()
        {
            return _orders;
        }

        /// <summary>
        ///     Create a new order and add it to the repository.
        /// </summary>
        /// <param name="order">The order to create</param>
        /// <returns>Returns the new maximum id in the list.</returns>
        public long CreateOrder(Order order)
        {
            // Or store max in field variable.
            long maxId = _orders.Max(x => x.OrderId);
            order.OrderId = maxId + 1;
            _orders.Add(order);
            return (maxId + 1);
        }

        /// <summary>
        ///     Parse the CSV files into the repository.
        /// </summary>
        public void LoadFiles()
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

            _categories = categoriesEnumerable.ToList();
            _products = productsEnumerable.ToList();
            _orders = ordersEnumerable.ToList();
            _orderDetails = orderDetailsEnumerable.ToList();

            UpdateObjectReferences();
        }

        /// <summary>
        ///     Update the Object references.
        /// </summary>
        private void UpdateObjectReferences()
        {
            _products.ForEach(p => p.GetCategoryReference(_categories));

            _orderDetails.ForEach(o => o.GetProductReference(_products));
        }
    }
}