using System;
using LINQtoCSV;

namespace Northwind
{
    public class Order
    {
        [CsvColumn(Name = "OrderID")]
        public long OrderId { get; set; }

        [CsvColumn(Name = "CustomerID")]
        public string CustomerId { get; set; }

        [CsvColumn(Name = "EmployeeID")]
        public long EmployeeId { get; set; }

        [CsvColumn(Name = "OrderDate")]
        public DateTime OrderDate { get; set; }

        [CsvColumn(Name = "RequiredDate")]
        public DateTime RequiredDate { get; set; }

        [CsvColumn(Name = "ShippedDate")]
        public DateTime ShippedDate { get; set; }

        [CsvColumn(Name = "ShipVia")]
        public int ShipVia { get; set; }

        [CsvColumn(Name = "Freight")]
        public double Freight { get; set; }

        [CsvColumn(Name = "ShipName")]
        public string ShipName { get; set; }

        [CsvColumn(Name = "ShipAddress")]
        public string ShipAddress { get; set; }

        [CsvColumn(Name = "ShipCity")]
        public string ShipCity { get; set; }

        [CsvColumn(Name = "ShipRegion")]
        public string ShipRegion { get; set; }

        [CsvColumn(Name = "ShipPostalCode")]
        public string ShipPostalCode { get; set; }

        [CsvColumn(Name = "ShipCountry")]
        public string ShipCountry { get; set; }
    }
}