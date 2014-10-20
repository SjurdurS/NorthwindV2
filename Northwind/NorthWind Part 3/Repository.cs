using System;

namespace NorthWind_Part_3
{
    internal class Repository : IDisposable
    {
        private readonly INorthWindContext _context;

        public Repository(INorthWindContext context = null)
        {
            _context = context ?? new NorthWindContext();
        }

        public void test()
        {
            _context.CreateOrder(lkksdkakaæaklæ)
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}