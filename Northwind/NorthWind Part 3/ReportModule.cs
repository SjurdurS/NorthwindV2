using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthWind_Part_3
{

    public class ReportModule
    {
        private NorthWindContext _context;

        public ReportModule(NorthWindContext context)
        {
            _context = context;

        }


        /// <summary>
        /// 
        /// NOTE:
        ///     We assume   "The result will hold a list of the top count products ordered by TotalPrice with the following information:"
        ///     means       "The result will hold a list of the top count ORDERS ordered by TotalPrice with the following information:"
        /// </summary>
        /// <param name="count">The number of Orders to get.</param>
        /// <returns>Returns at most "count" number of Orders.</returns>
        public Report<IList<OrdersByTotalPriceDto>, ReportError> TopOrdersByTotalPrice(int count)
        {

            var topOrdersByTotalPrice = _context.Orders.OrderByDescending(order => order.Order_Details.Sum(od => od.UnitPrice * od.Quantity));
            var topCountOrdersByTotalPrice = topOrdersByTotalPrice.Take(count).Select(order => new OrdersByTotalPriceDto
            {
                OrderId = order.OrderID,
                OrderDate = order.OrderDate,
                CustomerContactName = order.Customers.ContactName,
                TotalPriceWithDiscount = order.Order_Details.Sum(od => (1 - (decimal) od.Discount) * od.UnitPrice * od.Quantity),
                TotalPrice = order.Order_Details.Sum(od => od.UnitPrice * od.Quantity)
            }).ToList();

            return new Report<IList<OrdersByTotalPriceDto>, ReportError>()
            {
                Data = topCountOrdersByTotalPrice,
                Error = null
            };

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count">The number of Products to get.</param>
        /// <returns>Returns at most "count" number of Products.</returns>
        public Report<IList<ProductsBySaleDto>, ReportError> TopProductsBySale(int count)
        {
            var topProductsBySale =
                _context.Products.OrderByDescending(product => product.Order_Details.Sum(od => od.Quantity));

            var topCountProductsBySale = topProductsBySale.Take(count).Select(product => new ProductsBySaleDto
            {
                ProductId = product.ProductID,
                ProductName = product.ProductName,
                UnitsSoldByMonth = (from od in product.Order_Details
                    group od by
                        new
                        {
                            month = od.Order.OrderDate.GetValueOrDefault().Month,
                            year = od.Order.OrderDate.GetValueOrDefault().Year
                        }
                    into odm
                    select new UnitsSoldByMonthDto
                    {
                        UnitsSold = odm.Sum(totalSold => totalSold.Quantity),
                        Count = odm.Sum(totalOrders => totalOrders.OrderID),
                        Month = odm.Key.month,
                        Year = odm.Key.year
                    }).ToList()
            }).ToList();

            return new Report<IList<ProductsBySaleDto>, ReportError>() {Data = topCountProductsBySale, Error = null};
        }
    }

    public class ReportError
    {
        public string ErrorMessage { get; set; }
    }

    /// <summary>
    /// This class represents a generic result from the reporting module
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    /// <typeparam name="TError"></typeparam>
    public class Report<TData, TError>
    {
        public TData Data { get; set; }
        public TError Error { get; set; }
    }

    public class OrdersByTotalPriceDto
    {
        public int OrderId { get; set; }
        public DateTime? OrderDate { get; set; }
        public string CustomerContactName { get; set; }
        public decimal TotalPriceWithDiscount { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class ProductsBySaleDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public List<UnitsSoldByMonthDto> UnitsSoldByMonth { get; set; }
    }

    public class UnitsSoldByMonthDto
    {
        public int UnitsSold { get; set; }
        public int Count { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
    }
    }
    public class EmployeeSaleDto
    {
        
    }
}
