using FPWebAutomation.Libraries;
using FPWebAutomation.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPWebAutomation.TestCases
{
    class TC02_PeopleSelector
    {


        [SetUp]
        public void Setup()
        {
            PropertiesCollection.driver = new ChromeDriver(@ConfigurationManager.AppSettings["ChromeDriverPath"]);
            PropertiesCollection.driver.Manage().Window.Maximize();
        }

        [Test]
        public void TC03_FPWeb_ValidatePeopleSelector_SummaryPage()
        {
            var dr = Lib_Login.Login();

            var Summary = new clsSummaryPage();

            PageFactory.InitElements(dr, Summary);

            
            Summary.btn_PeopleSearch.Click();

            var wait = new WebDriverWait(dr, new TimeSpan(0, 0, 30));
            var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//input[@type='text' and @placeholder='Type part of Surname, Given Names, Nickname or Organisation/People Group and press Enter to search.']")));

            Summary.txtSearch_grdPeople.SendKeys("SurnameTestUser6");

            Summary.btn_Search.Click();

            var element2 = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"grdPeople\"]/div[2]/table")));
            Summary.grdPeopleSelectorResult.Click();

            Summary.btnApply.Click();


          //  Lib_Logout.Logout(dr);
        }



        [TearDown]
        public void TearDown()
        {

        }

    


}
}
