using FPWebAutomation.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPWebAutomation.Libraries
{
    class Lib_Login
    {
        public static IWebDriver Login()
        {
             
            IWebDriver driver = new ChromeDriver(@ConfigurationManager.AppSettings["ChromeDriverPath"]);
            driver.Manage().Window.Maximize();

            driver.Url = ConfigurationManager.AppSettings["URL"];

            clsLoginPage loginPage = new clsLoginPage();

            PageFactory.InitElements(driver, loginPage);

            

            loginPage.txtUserName.SendKeys(ConfigurationManager.AppSettings["Login_UserName"]);

            loginPage.txtPassword.SendKeys(ConfigurationManager.AppSettings["Login_Password"]);

            loginPage.btnSubmit.Click();

            return driver;
        }


    }
}
