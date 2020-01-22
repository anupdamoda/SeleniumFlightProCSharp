using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPWebAutomation_MSTests.PageObjects
{
    class FpControlHoursReportPage
    {
        /****** WebElements on Control Hours Report Page ********/
        private IWebElement ReportName => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"pbCH\"]/li/a/b"));
        public string GetReportName() => ReportName.Text;
    }
}

