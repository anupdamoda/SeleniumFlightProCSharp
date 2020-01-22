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
    class WebmailLoginPage
    {

        /***************** WebElements on Webmail Login Page *******************/

        private IWebElement TxtUserName => PropertiesCollection.driver.FindElement(By.Id("username"));
        private IWebElement TxtPassword => PropertiesCollection.driver.FindElement(By.Id("password"));        
        private IWebElement BtnSubmit => PropertiesCollection.driver.FindElement(By.ClassName("signinbutton"));


        public void LoginWebmailWithUserCredentials()
        {
            PropertiesCollection.driver.Url = ConfigurationManager.AppSettings["FACT_Outlook_URL"];
            String Username = ConfigurationManager.AppSettings["Outlook_Username"];
            String Password = ConfigurationManager.AppSettings["Outlook_Password"];
            TxtUserName.SendKeys(Username);
            TxtPassword.SendKeys(Password);
            BtnSubmit.Click();

        }
    }
}

