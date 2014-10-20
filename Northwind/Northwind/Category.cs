using LINQtoCSV;

namespace Northwind
{
    /// <summary>
    ///     This class represents a Category in the NorthWind System.
    /// </summary>
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