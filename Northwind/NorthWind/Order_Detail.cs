using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace NorthWindNS
{
    [Table("Order Details")]
    public class Order_Detail
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductID { get; set; }

        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }

        public short Quantity { get; set; }

        public float? Discount { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }

        /// <summary>
        ///     Get the Product object reference. Search through a list of categories to find the same product ID.
        /// </summary>
        /// <param name="products">List of GetProducts</param>
        public void GetProductReference(List<Product> products)
        {
            Product = (from p in products
                where p.ProductID == ProductID
                select p).FirstOrDefault();
        }
    }
}