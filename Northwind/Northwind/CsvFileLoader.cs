using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindNS
{
    class CsvFileLoader : ICsvFileLoader
    {
        public string[] categoryLines { get; private set; }
        public string[] productLines { get; private set; }
        public string[] orderLines { get; private set; }
        public string[] orderDetailLines { get; private set; }

        public CsvFileLoader()
        {
            // Skip 1 because of the headers.
            categoryLines = File.ReadAllLines(@"../../Resources/categories.csv").Skip(1).ToArray();
            productLines = File.ReadAllLines(@"../../Resources/products.csv").Skip(1).ToArray();
            orderLines = File.ReadAllLines(@"../../Resources/orders.csv").Skip(1).ToArray();
            orderDetailLines = File.ReadAllLines(@"../../Resources/order_details.csv").Skip(1).ToArray();
        }
    }
}
