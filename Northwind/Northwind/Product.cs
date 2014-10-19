using System.Collections.Generic;
using System.Linq;
using LINQtoCSV;

namespace Northwind
{
    public class Product
    {
        public Category Category;

        [CsvColumn(Name = "ProductID")]
        public long Id { get; set; }

        [CsvColumn(Name = "ProductName")]
        public string Name { get; set; }

        [CsvColumn(Name = "SupplierID")]
        public long SupplierId { get; set; }

        [CsvColumn(Name = "CategoryID")]
        public long CategoryId { get; set; }

        [CsvColumn(Name = "QuantityPerUnit")]
        public string QuantityPerUnit { get; set; }

        [CsvColumn(Name = "UnitPrice")]
        public double UnitPrice { get; set; }

        [CsvColumn(Name = "UnitsInStock")]
        public int UnitsInStock { get; set; }

        [CsvColumn(Name = "UnitsOnOrder")]
        public int UnitsOnOrder { get; set; }

        [CsvColumn(Name = "ReorderLevel")]
        public int ReorderLevel { get; set; }

        [CsvColumn(Name = "Discontinued")]
        public int Discontinued { get; set; }

        /// <summary>
        ///     Get the Category object reference. Search through a list of categories to find the same Category ID.
        /// </summary>
        /// <param name="categories">IEnumerable of CategoriesEnumerable</param>
        public void GetCategoryReference(List<Category> categories)
        {
            this.Category = (from c in categories
                            where c.Id == this.CategoryId
                            select c).FirstOrDefault();

            /*
            foreach (Category cat in categories)
            {
                if (CategoryId == cat.Id)
                {
                    Category = cat;
                    break;
                }
            }
            */
        }

    }
}