﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindNS
{

    public class ReportModule
    {
        private readonly DbRepository _context;

        public ReportModule(DbRepository context)
        {
            _context = context ?? new DbRepository();

        }


        /// <summary>
        /// 
        /// NOTE:
        ///     We assume   "The result will hold a list of the top count products ordered by TotalPrice with the following information:"
        ///     means       "The result will hold a list of the top count ORDERS ordered by TotalPrice with the following information:"
        /// </summary>
        /// <param name="count">The number of GetOrders to get.</param>
        /// <returns>Returns at most "count" number of GetOrders.</returns>
        public virtual Report<IList<OrdersByTotalPriceDto>, ReportError> TopOrdersByTotalPrice(int count)
        {

            var topOrdersByTotalPrice =
                _context.Orders.OrderByDescending(order => order.Order_Details.Sum(od => od.UnitPrice*od.Quantity));
            var topCountOrdersByTotalPrice = topOrdersByTotalPrice.Take(count).Select(order => new OrdersByTotalPriceDto
            {
                OrderId = order.OrderID,
                OrderDate = order.OrderDate,
                CustomerContactName = order.Customers.ContactName,
                TotalPriceWithDiscount = 0.0m, // order.Order_Details.Sum(od => (1 - od.Discount) * (od.UnitPrice * od.Quantity)),
                TotalPrice = order.Order_Details.Sum(od => od.UnitPrice*od.Quantity)
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
        /// <param name="count">The number of GetProducts to get.</param>
        /// <returns>Returns at most "count" number of GetProducts.</returns>
        public virtual Report<IList<ProductsBySaleDto>, ReportError> TopProductsBySale(int count)
        {
            var topProductsBySale =
                _context.Products.OrderByDescending(product => product.Order_Details.Sum(od => od.Quantity));

            var topCountProductsBySale = topProductsBySale.Take(count).Select(product => new ProductsBySaleDto
            {
                ProductId = product.ProductID,
                ProductName = product.ProductName,
                UnitsSoldByMonth = (from od in product.Order_Details
                    let orderDate = od.Order.OrderDate
                    where orderDate != null
                    group od by
                        new
                        {
                            month = orderDate.Value.Month,
                            year = orderDate.Value.Year
                        }
                    into odm
                    select new UnitsSoldByMonthDto
                    {
                        UnitsSold = odm.Sum(totalSold => totalSold.Quantity),
                        Count = odm.Select(totalOrders => totalOrders.Order.OrderID).Distinct().Count(),
                        Month = odm.Key.month,
                        Year = odm.Key.year
                    }).ToList()
            }).ToList();

            return new Report<IList<ProductsBySaleDto>, ReportError>() {Data = topCountProductsBySale, Error = null};
        }

        public virtual Report<EmployeeSaleDto, ReportError> EmployeeSale(int id)
        {

            try
            {
                var employee = (from e in _context.Employees
                    where e.EmployeeID == id
                    select e).First();

                string employeeName = employee.FirstName + " " + employee.LastName;
                int? reportsToId = employee.ReportsTo;


                var ordersDto = (from o in employee.Orders
                    select new OrdersDto
                    {
                        OrderID = o.OrderID,
                        OrderDate = o.OrderDate,
                        TotalPrice = o.Order_Details.Sum(od => od.UnitPrice*od.Quantity),
                        Products = (from od in o.Order_Details
                            select new ProductsDto
                            {
                                ProductName = od.Product.ProductName,
                                UnitPrice = od.UnitPrice,
                                Quantity = od.Quantity
                            }).ToList()
                    }).ToList();

                return new Report<EmployeeSaleDto, ReportError>
                {
                    Data = new EmployeeSaleDto
                    {
                        EmployeeName = employeeName,
                        ReportsToId = reportsToId,
                        Orders = ordersDto
                    },
                    Error = null
                };
            }
            catch (ArgumentNullException)
            {
                return new Report<EmployeeSaleDto, ReportError>
                {
                    Data = null,
                    Error = new ReportError() { ErrorMessage = "No Employee found with ID: " + id}
                };

            }
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
        public decimal TotalPriceWithDiscount { get; set; } // We had issues with casting of Number types so we chose to use Float instead of Decimal
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


    public class EmployeeSaleDto
    {
        public string EmployeeName { get; set; }
        public int? ReportsToId { get; set; }
        public List<OrdersDto> Orders { get; set; }

    }

    public class OrdersDto
    {
        public int OrderID { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public List<ProductsDto> Products { get; set; }
    }

    public class ProductsDto
    {
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }

    }
}
