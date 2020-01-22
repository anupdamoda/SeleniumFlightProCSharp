using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPWebAutomation_MSTests.PageObjects
{
    class FpDailySchedulingPage
    {

        /***** WebElements on Daily Scheduling  Page ********/

        public IWebElement Table => PropertiesCollection.driver.FindElement(By.XPath("//*[@ng-controller='DailySchedulingController']"));

        
    }
}
