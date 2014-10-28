using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace NorthWindNS
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
        ///     Get the Category object reference. Search through a list of categories to find the same Category ID.
        /// </summary>
        /// <param name="categories">List of GetCategories</param>
        public void GetCategoryReference(List<Category> categories)
        {
            Category = (from c in categories
                        where c.CategoryID == CategoryID
                        select c).FirstOrDefault();
        }
    }
}