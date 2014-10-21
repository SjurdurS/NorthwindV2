using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind_Part_3
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new NorthWindContext())
            {
                var rm = new ReportModule(db);
                var topOrdersByTotalPrice = rm.TopOrdersByTotalPrice(1);
                topOrdersByTotalPrice.Data.ToList().ForEach(x => Console.WriteLine(x.CustomerContactName));
            }
        }
    }
}
