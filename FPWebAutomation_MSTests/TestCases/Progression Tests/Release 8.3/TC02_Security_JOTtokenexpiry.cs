using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Configuration;
using FPWebAutomation_MSTests.Database;
using FPWebAutomation_MSTests.PageObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPWebAutomation_MSTests.TestCases.Progression_Tests.Release_8._3
{
    [TestClass]
    public class TC02_Security_JOTtokenexpiry
    {
        [TestInitialize]
        public void TestInitialize()
        {
            PropertiesCollection.driver = new ChromeDriver(System.Configuration.ConfigurationManager.AppSettings["ChromeDriverPath"]);
            PropertiesCollection.driver.Manage().Window.Maximize();
            FpLoginPage loginPage = new FpLoginPage();
            loginPage.Login();
        }

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            PropertiesCollection.htmlReporter = new ExtentHtmlReporter(@"C:\FlightPro\Dev\_main\Test Automation\FlightPro\FightPro_WebAutomation\FPWebAutomation\Report\Report.html");
            PropertiesCollection.htmlReporter.LoadConfig(@"C:\extent-configfile\extent-config.xml");
            PropertiesCollection.extent = new ExtentReports();
            PropertiesCollection.extent.AttachReporter(PropertiesCollection.htmlReporter);
            PropertiesCollection.extent.AddSystemInfo("Automation Database", "8.1");
            PropertiesCollection.extent.AddSystemInfo("Browser", "Chrome");
            PropertiesCollection.extent.AddSystemInfo("Application Under Test (AUT)", "FlightProWeb");
            PropertiesCollection.extent.AddSystemInfo("Application URL", "http://oc-svr-at1/Fltpro_Automation_main/");
        }

        [TestCategory("Progression")]
        [TestCategory("US_41937_Security: JOT token expiry")]
        [TestMethod]
        public void US_41937_Security_JOT_token_expiry()
        {
            PropertiesCollection.test = PropertiesCollection.extent.CreateTest("US_41937_Security: JOT token expiry");
            var sidebarMenu = new FpSideMenus();
            FpCombinedSchedulePage combinedSchedule = new FpCombinedSchedulePage();

            sidebarMenu.LnkCombinedSchedule.Click();
            IWebElement framesource = PropertiesCollection.driver.FindElement(By.XPath("//iframe[@class='iframe-placeholder']"));

            var frameSourceValue = framesource.GetAttribute("src");
            PropertiesCollection.driver.Close();
            PropertiesCollection.driver.Quit();
            System.Threading.Thread.Sleep(20000);

            PropertiesCollection.driver = new ChromeDriver(@ConfigurationManager.AppSettings["ChromeDriverPath"]);
            PropertiesCollection.driver.Navigate().GoToUrl(frameSourceValue);
            PropertiesCollection.driver.Manage().Window.Maximize();

            IWebElement errorCode = PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"error-information-popup-content\"]/div[2]"));
            try
            {
                Assert.AreEqual("HTTP ERROR 401", errorCode.Text);
                PropertiesCollection.test.Log(Status.Pass, "The web page is not accessible");
            }
            catch
            {
                PropertiesCollection.test.Log(Status.Fail, "The web page is accessible ");
            }
        }
    }
}
