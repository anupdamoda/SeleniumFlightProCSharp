using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPWebAutomation_MSTests.PageObjects
{
    class FpEventsReportPage
    {
        /****** WebElements on Events Report Page ********/
        private IWebElement ReportName => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"panelBarEventReport\"]/li/a/b"));
        public string GetReportName() => ReportName.Text;
    }
}
