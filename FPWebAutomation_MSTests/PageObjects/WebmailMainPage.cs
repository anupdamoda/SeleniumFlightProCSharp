using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPWebAutomation_MSTests.PageObjects
{
    class WebmailMainPage
    {

        /***************** WebElements on Webmail Main Page *******************/

        private IWebElement MnuNavigation => PropertiesCollection.driver.FindElement(By.Id("O365_MainLink_NavMenu"));

        /***************** WebElements on Webmail Navigation Page *******************/
        private IWebElement MnuCalendar => PropertiesCollection.driver.FindElement(By.Id("O365_AppTile_ShellCalendar"));
        
        public void NavigationMenuClick() => MnuNavigation.Click();
        public void CalendarClick() => MnuCalendar.Click();

    }
}

