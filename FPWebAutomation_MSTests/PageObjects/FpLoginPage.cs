using OpenQA.Selenium;
using System.Configuration;


namespace FPWebAutomation_MSTests.PageObjects
{
    public class FpLoginPage
    {
        /****** WebElements on Login Page ********/

        private IWebElement TxtUserName => PropertiesCollection.driver.FindElement(By.Id("UserName"));
        private IWebElement TxtPassword => PropertiesCollection.driver.FindElement(By.Id("Password"));
        private IWebElement BtnSubmit => PropertiesCollection.driver.FindElement(By.Id("login_button"));

        public void Login()
        {
            PropertiesCollection.driver.Url = ConfigurationManager.AppSettings["URL"];
            TxtUserName.SendKeys(ConfigurationManager.AppSettings["Login_UserName"]);
            TxtPassword.SendKeys(ConfigurationManager.AppSettings["Login_Password"]);
            BtnSubmit.Click();
        }
        public void LoginWithUserCredentials(string Username, string Password)
        {
            PropertiesCollection.driver.Url = ConfigurationManager.AppSettings["URL"];
            TxtUserName.SendKeys(Username);
            TxtPassword.SendKeys(Password);
            BtnSubmit.Click();
        }
    }
}