using System.Collections.Generic;
using System.Linq;
using LINQtoCSV;

namespace Northwind
{
    public class OrderDetail
    {
        [CsvColumn(Name = "OrderID")]
        public int OrderId { get; set; }

        [CsvColumn(Name = "ProductID")]
        public int ProductId { get; set; }

        public Product Product { get; set; }

        [CsvColumn(Name = "UnitPrice")]
        public double UnitPrice { get; set; }

        [CsvColumn(Name = "Quantity")]
        public int Quantity { get; set; }

        [CsvColumn(Name = "Discount")]
        public double Discount { get; set; }


        /// <summary>
        ///     Get the Product object reference. Search through a list of categories to find the same product ID.
        /// </summary>
        /// <param name="products">IEnumerable of Products</param>
        public void GetProductReference(List<Product> products)
        {
            Product = (from p in products
                where p.Id == ProductId
                select p).FirstOrDefault();
        }
    }
}