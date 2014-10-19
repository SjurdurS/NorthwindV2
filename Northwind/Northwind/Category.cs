using LINQtoCSV;

namespace Northwind
{
    public class Category
    {
        [CsvColumn(Name = "CategoryID")]
        public long Id { get; set; }

        [CsvColumn(Name = "CategoryName")]
        public string Name { get; set; }

        [CsvColumn(Name = "Description")]
        public string Description { get; set; }
    }
}