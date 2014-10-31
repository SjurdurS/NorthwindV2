using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NorthwindNS;

namespace UnitTestNorthwind
{
    [TestClass]
    public class ReportModuleUnitTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", AppDomain.CurrentDomain.BaseDirectory);
        }

        [TestMethod]
        public void GetEmployeeId4_MargaretPeacock()
        {
            using (var db = new DbRepository())
            {
                var rm = new ReportModule(db);
                Report<EmployeeSaleDto, ReportError> employeeSale = rm.EmployeeSale(4);
                string expectedResult = "Margaret Peacock";
                Assert.AreEqual(employeeSale.Data.EmployeeName, expectedResult);
            }
        }

        [TestMethod]
        public void GetTop1_OrderCoustomerContactName()
        {
            using (var db = new DbRepository())
            {
                var rm = new ReportModule(db);
                Report<IList<OrdersByTotalPriceDto>, ReportError> topOrdersByTotalPrice = rm.TopOrdersByTotalPrice(1);
                string expectedResult = "Horst Kloss";
                Assert.AreEqual(topOrdersByTotalPrice.Data.First().CustomerContactName, expectedResult);
            }
        }

        [TestMethod]
        public void GetTop1_ProductsBySale()
        {
            using (var db = new DbRepository())
            {
                var rm = new ReportModule(db);
                Report<IList<ProductsBySaleDto>, ReportError> topProductsBySale = rm.TopProductsBySale(1);
                string expectedResult = "Camembert Pierrot";
                Assert.AreEqual(topProductsBySale.Data.First().ProductName, expectedResult);
            }
        }
    }
}