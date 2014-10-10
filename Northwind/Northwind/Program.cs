using System;
using System.Collections.Generic;
using System.Linq;
using LINQtoCSV;

namespace Northwind
{
    internal class Program
    {
        private List<Category> Categories;
        private List<OrderDetail> OrderDetails;
        private List<Order> Orders;
        private List<Product> Products;




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
            
            CsvContext cc = new CsvContext();
            
            IEnumerable<Category> categoriesEnumerable = cc.Read<Category>("../../Resources/categories.csv",inputFileDescription);
            IEnumerable<Product> productsEnumerable = cc.Read<Product>("../../Resources/products.csv",inputFileDescription);
            IEnumerable<Order> ordersEnumerable = cc.Read<Order>("../../Resources/orders.csv", inputFileDescription);
            IEnumerable<OrderDetail> orderDetailsEnumerable = cc.Read<OrderDetail>("../../Resources/order_details.csv",inputFileDescription);

            Categories = categoriesEnumerable.ToList();
            Products = productsEnumerable.ToList();
            Orders = ordersEnumerable.ToList();
            OrderDetails = orderDetailsEnumerable.ToList();

            UpdateObjectReferences();
        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdateObjectReferences()
        {
            foreach (Product p in Products)
            {
                p.GetCategoryReference(Categories);
            }

            foreach (OrderDetail o in OrderDetails)
            {
                o.GetProductReference(Products);
            }
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
            Orders.Add(order);
        }

        private static void Main(string[] args)
        {
            var p = new Program();
            p.ReadFiles();

            foreach (var item in p.Orders)
            {
                
            }

        }
    }
}