using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;

namespace FPWebAutomation.PageObjects
{
     class clsMainPage_TopbarMenu
    {
        

        public WebDriverWait wait;
        

        public clsMainPage_TopbarMenu()
        {
            PageFactory.InitElements(PropertiesCollection.driver,this);
       }

        // Capturing the object properties of Admin link of Top Menu
        [FindsBy(How = How.LinkText, Using = "Admin")]
        public IWebElement linkAdmin { get; set; }

        // Capturing the object properties of Reports link of Top Menu
        [FindsBy(How = How.LinkText, Using = "Reports")]
        public IWebElement linkReports { get; set; }

        // Capturing the object properties of Logout link of Top Menu
        [FindsBy(How = How.PartialLinkText, Using = "Logout")]
        public IWebElement linkLogout { get; set; }

        // Capturing the object properties (absolute Xpath) of Shift Administration
        [FindsBy(How = How.PartialLinkText, Using = "Shift Administration")]
        public IWebElement linkShiftAdministration { get; set; }

        // Capturing the object properties (absolute Xpath) of Roster Administration
        [FindsBy(How = How.XPath, Using = "//*[@id=\"menu\"]/ul/li/ul/li[11]/a")]
        public IWebElement linkRosterAdministration { get; set; }
      

        public clsMainPage_TopbarMenu NavigatetoShiftAdministration()
        {
            System.Threading.Thread.Sleep(3000);
            linkAdmin.Click();
            var wait = new WebDriverWait(PropertiesCollection.driver, TimeSpan.FromSeconds(30));

            IWebElement element = linkShiftAdministration;
            Actions actions = new OpenQA.Selenium.Interactions.Actions(PropertiesCollection.driver);
            actions.MoveToElement(element).Perform();

            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.PartialLinkText("Shift Administration")));

            IJavaScriptExecutor executor = (IJavaScriptExecutor)PropertiesCollection.driver;
            executor.ExecuteScript("arguments[0].click();", element);

            return new clsMainPage_TopbarMenu();
        }

        public void NavigatetoRosterAdministration()
        {
            linkAdmin.Click();
            linkRosterAdministration.Click();
        }
  
        public void Logout()
        {

            
            WebDriverWait wait = new WebDriverWait(PropertiesCollection.driver, TimeSpan.FromSeconds(50));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//*[@id=\"ShiftTypeListGrid\"]/div/button")));

            //  PropertiesCollection.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(120);


            /*
            IWebElement element = linkLogout;
            Actions actions = new OpenQA.Selenium.Interactions.Actions(PropertiesCollection.driver);
            actions.MoveToElement(element).Perform();

            IJavaScriptExecutor executor = (IJavaScriptExecutor)PropertiesCollection.driver;
            executor.ExecuteScript("arguments[0].click();", element);
            */
            System.Threading.Thread.Sleep(3000);

            linkLogout.Click();
          //  return FPWeb_MainPage_TopbarMenu();
            
        }

    }
}
