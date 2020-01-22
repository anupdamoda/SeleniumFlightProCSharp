using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPWebAutomation_MSTests.PageObjects
{
    class clsMainPage_TopbarMenu
    {

        public WebDriverWait wait;

        public clsMainPage_TopbarMenu()
        {
            PageFactory.InitElements(PropertiesCollection.driver, this);
        }

        public IWebElement lnkAdmin => PropertiesCollection.driver.FindElement(By.LinkText("Admin"));
        public IWebElement lnkReports => PropertiesCollection.driver.FindElement(By.LinkText("Reports"));
        public IWebElement lnkLogout => PropertiesCollection.driver.FindElement(By.PartialLinkText("Logout"));
        public IWebElement lnkShiftAdministration => PropertiesCollection.driver.FindElement(By.PartialLinkText("Shift Administration"));
        public IWebElement lnkRosterAdministration => PropertiesCollection.driver.FindElement(By.PartialLinkText("Roster Administration"));
        public IWebElement lnkTemplates => PropertiesCollection.driver.FindElement(By.LinkText("Templates"));
        
        public clsMainPage_TopbarMenu NavigatetoShiftAdministration()
        {
            var wait = new WebDriverWait(PropertiesCollection.driver, TimeSpan.FromSeconds(30));

            wait.Until(ExpectedConditions.ElementToBeClickable(lnkAdmin));
            lnkAdmin.Click();           

            IWebElement element = lnkShiftAdministration;
            Actions actions = new OpenQA.Selenium.Interactions.Actions(PropertiesCollection.driver);
            actions.MoveToElement(element).Perform();

            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.PartialLinkText("Shift Administration")));

            IJavaScriptExecutor executor = (IJavaScriptExecutor)PropertiesCollection.driver;
            executor.ExecuteScript("arguments[0].click();", element);

            return new clsMainPage_TopbarMenu();
        }

        public void NavigatetoRosterAdministration()
        {
            var wait = new WebDriverWait(PropertiesCollection.driver, TimeSpan.FromSeconds(30));

            System.Threading.Thread.Sleep(3000);
            lnkAdmin.Click();

            wait.Until(ExpectedConditions.ElementToBeClickable(lnkRosterAdministration));
            lnkRosterAdministration.Click();
        }

        public void NavigatetoTemplates()
        {
            System.Threading.Thread.Sleep(3000);
            lnkTemplates.Click();
        }

        public void Logout()
        {
            System.Threading.Thread.Sleep(4000);

            if (lnkLogout.Displayed == true) { 
            lnkLogout.Click();
            }
        }
    }
}
