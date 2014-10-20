using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Northwind;

namespace NorthWind_Part_3
{
    class Repository : IDisposable
    {
        private readonly INorthWindContext _context;

        public Repository(INorthWindContext context = null)
        {
            _context = context ?? new NorthWindContext();
        }

        public List<Product> Products()
        {
            throw new NotImplementedException();
        }

        public List<Category> Categories()
        {
            throw new NotImplementedException();
        }

        public List<Order> Orders()
        {
            throw new NotImplementedException();
        }

        public long CreateOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
