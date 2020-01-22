using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPWebAutomation_MSTests.PageObjects
{
    class FpPeopleUnavailabilityPopup
    {
        /****** WebElements on People Unavailability Popup ********/
                
        public IWebElement TxtDescription => PropertiesCollection.driver.FindElement(By.Id("unavailability-description"));
        public IWebElement BtnSave => PropertiesCollection.driver.FindElement(By.Id("btnSave"));
        public IWebElement BtnCancel => PropertiesCollection.driver.FindElement(By.Id("btnCancel"));
    }
}
