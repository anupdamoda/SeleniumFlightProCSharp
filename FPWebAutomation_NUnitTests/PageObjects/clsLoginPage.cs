using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPWebAutomation.PageObjects
{
    class clsLoginPage
    {
        
        public clsLoginPage()
        {
            PageFactory.InitElements(PropertiesCollection.driver, this);
        }


        /***************** WebElements on Login Page *******************/

        [FindsBy(How = How.Id, Using = "UserName")]
        public IWebElement txtUserName { get; set; }

        [FindsBy(How = How.Id, Using = "Password")]
        public IWebElement txtPassword { get; set; }

        [FindsBy(How = How.Id, Using = "login_button")]
        public IWebElement btnSubmit { get; set; }


        public clsLoginPage LoginPage()
        {

            PropertiesCollection.driver.Url = ConfigurationManager.AppSettings["URL"];

            txtUserName.SendKeys(ConfigurationManager.AppSettings["Login_UserName"]);

            txtPassword.SendKeys(ConfigurationManager.AppSettings["Login_Password"]);

            btnSubmit.Click();

            return new clsLoginPage();

        }



    }
}
