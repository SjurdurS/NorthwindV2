using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace NorthWindNS
{
    public class Order
    {
        public Order()
        {
            Order_Details = new HashSet<Order_Detail>();
        }

        [Key]
        public int OrderID { get; set; }

        [StringLength(5)]
        public string CustomerID { get; set; }

        public int? EmployeeID { get; set; }

        public DateTime? OrderDate { get; set; }

        public DateTime? RequiredDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        public int? ShipVia { get; set; }

        [Column(TypeName = "money")]
        public decimal? Freight { get; set; }

        [StringLength(40)]
        public string ShipName { get; set; }

        [StringLength(60)]
        public string ShipAddress { get; set; }

        [StringLength(15)]
        public string ShipCity { get; set; }

        [StringLength(15)]
        public string ShipRegion { get; set; }

        [StringLength(10)]
        public string ShipPostalCode { get; set; }

        [StringLength(15)]
        public string ShipCountry { get; set; }

        public virtual Customers Customers { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual ICollection<Order_Detail> Order_Details { get; set; }

        public virtual Shipper Shipper { get; set; }

        /// <summary>
        ///     Get the OrderDetail object references.
        ///     Search through a collection of order details to find the ones that reference to this Order.
        /// </summary>
        /// <param name="orderDetails">IEnumerable of Order_Details</param>
        public void GetOrderDetailsReferences(IEnumerable<Order_Detail> orderDetails)
        {
            IEnumerable<Order_Detail> odReferences = (from od in orderDetails
                where od.Order.OrderID == OrderID
                select od);
            odReferences.ToList().ForEach(odReference => Order_Details.Add(odReference));
        }
    }
}