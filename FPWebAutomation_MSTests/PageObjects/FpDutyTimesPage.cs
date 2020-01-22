using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPWebAutomation_MSTests.PageObjects
{
    class FpControlHoursPage
    {
        /****** WebElements on Duty Times Page ********/
        private IWebElement UserName => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"peopleName\"]"));

        public string Title() => UserName.Text.Trim();
    }
}
