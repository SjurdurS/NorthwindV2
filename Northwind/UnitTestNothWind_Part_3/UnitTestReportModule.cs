﻿using System;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthWind_Part_3;

namespace UnitTestNothWind_Part_3
{
    [TestClass]
    public class UnitTestReportModule
    {
        [TestMethod]
        public void GetEmployeeId4_MargaretPeacock()
        {
            using (var db = new NorthWindContext())
            {
                var rm = new ReportModule(db);
                var employeeSale = rm.EmployeeSale(4);
                string expectedResult = "Margaret Peacock";
                Assert.AreEqual(employeeSale.Data.EmployeeName, expectedResult);
            }
        }

        [TestMethod]
        public void GetTop1_OrderCoustomerContactName()
        {
            using (var db = new NorthWindContext())
            {

                var rm = new ReportModule(db);
                var topOrdersByTotalPrice = rm.TopOrdersByTotalPrice(1);
                string expectedResult = "Horst Kloss";
                Assert.AreEqual(topOrdersByTotalPrice.Data.First().CustomerContactName, expectedResult);

            }
        }

        [TestMethod]
        public void GetTop1_ProductsBySale()
        {

            using (var db = new NorthWindContext())
            {
                var rm = new ReportModule(db);
                var topProductsBySale = rm.TopProductsBySale(1);
                string expectedResult = "Camembert Pierrot";
                Assert.AreEqual(topProductsBySale.Data.First().ProductName, expectedResult);
            }
        }
    }
}
