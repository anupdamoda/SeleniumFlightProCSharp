using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPWebAutomation_MSTests.PageObjects
{
    class FpRosterPage
    {
        /****** WebElements on Roster Page ********/
        public IWebElement RosterTable => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"rosterViewTable\"]"));
    }
}
