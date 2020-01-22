using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPWebAutomation_MSTests.PageObjects
{
    class FpRosterAdministrationPage
    {
        /****** WebElements on Roster Administration Page ********/

        public IWebElement Title => PropertiesCollection.driver.FindElement(By.XPath("//td[@class='tableColumnSubHeader'][contains(.,'Roster Administration')]"));
    }
}
