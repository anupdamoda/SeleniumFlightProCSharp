using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPWebAutomation_MSTests.PageObjects
{
    class FpCalendarDeleteEventPopup
    {
        /****** WebElements on Calendar Delete Event Popup Page ********/

        public IWebElement BtnDelete => PropertiesCollection.driver.FindElement(By.XPath("//*[@class='k-button k-primary k-scheduler-delete']"));
    }
}
