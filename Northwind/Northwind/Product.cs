using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace NorthwindNS
{
    public class Product
    {
        public Product()
        {
            Order_Details = new HashSet<Order_Detail>();
        }

        [Key]
        public int ProductID { get; set; }

        [Required]
        [StringLength(40)]
        public string ProductName { get; set; }

        public int? SupplierID { get; set; }

        public int? CategoryID { get; set; }

        [StringLength(20)]
        public string QuantityPerUnit { get; set; }

        [Column(TypeName = "money")]
        public decimal? UnitPrice { get; set; }

        public short? UnitsInStock { get; set; }

        public short? UnitsOnOrder { get; set; }

        public short? ReorderLevel { get; set; }

        public bool Discontinued { get; set; }


        public virtual Category Category { get; set; }

        public virtual ICollection<Order_Detail> Order_Details { get; set; }

        public virtual Supplier Supplier { get; set; }

        /// <summary>
        ///     Get the OrderDetail object references. 
        ///     Search through a collection of order details to find the ones that reference to this Product.
        /// </summary>
        /// <param name="orderDetails">IEnumerable of Order_Details</param>
        public void GetOrderDetailsReferences(IEnumerable<Order_Detail> orderDetails)
        {
            var odReferences = (from od in orderDetails
                                where od.Product.ProductID == this.ProductID
                                select od);
            odReferences.ToList().ForEach(odReference => Order_Details.Add(odReference));
        }
    }
}