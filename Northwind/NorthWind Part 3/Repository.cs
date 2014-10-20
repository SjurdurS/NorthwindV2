using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Northwind;

namespace NorthWind_Part_3
{
    class Repository : IRepository
    {
        public List<Northwind.Product> Products()
        {
            throw new NotImplementedException();
        }

        public List<Northwind.Category> Categories()
        {
            throw new NotImplementedException();
        }

        public List<Northwind.Order> Orders()
        {
            throw new NotImplementedException();
        }

        public long CreateOrder(Northwind.Order order)
        {
            throw new NotImplementedException();
        }
    }
}
