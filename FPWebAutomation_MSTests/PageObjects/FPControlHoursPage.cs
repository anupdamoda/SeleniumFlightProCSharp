using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPWebAutomation_MSTests.PageObjects
{
    class FPControlHoursPage
    {
        /****** WebElements on Control Hours Page ********/

        private IWebElement ControlHoursTitle => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"form\"]/div/div[4]/table/tbody/tr[1]/td/div/h4"));

        public string Title() => ControlHoursTitle.Text;
    }
}
