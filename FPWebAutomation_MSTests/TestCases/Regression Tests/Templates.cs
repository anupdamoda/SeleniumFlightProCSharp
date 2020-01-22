using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using System.Configuration;
using FPWebAutomation_MSTests.PageObjects;
using System.Threading;
using System.IO;
using AventStack.ExtentReports.Reporter;

namespace FPWebAutomation_MSTests.TestCases.Regression_Tests
{
    [TestClass]
    public class Templates
    {
        string strTestCaseNo;
        string strtblname;
        string strTestType;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            PropertiesCollection.htmlReporter = new ExtentHtmlReporter(@"C:\Report\Report.html");

            string currentDir = Directory.GetCurrentDirectory();
            var configFile = currentDir.Replace("bin\\Debug", "extent-config.xml");

            PropertiesCollection.htmlReporter.LoadConfig(configFile);
            PropertiesCollection.extent = new AventStack.ExtentReports.ExtentReports();
            PropertiesCollection.extent.AttachReporter(PropertiesCollection.htmlReporter);
            PropertiesCollection.extent.AddSystemInfo("Automation Database", "8.1");
            PropertiesCollection.extent.AddSystemInfo("Browser", "Chrome");
            PropertiesCollection.extent.AddSystemInfo("Application Under Test (AUT)", "FlightProWeb");
            PropertiesCollection.extent.AddSystemInfo("Application URL", ConfigurationManager.AppSettings["URL"]);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            PropertiesCollection.driver = new ChromeDriver(@ConfigurationManager.AppSettings["ChromeDriverPath"]);
            PropertiesCollection.driver.Manage().Window.Maximize();
            FpLoginPage loginPage = new FpLoginPage();
            loginPage.Login();
            Thread.Sleep(3000);
        }

        [TestMethod]
        public void TemplatesValidation()
        {
            AddTemplates();
            //VerifyErrorMessages();
            //EditTemplates();
            //DeleteTemplates();
            //ErrorBeforeSave();
        }

        public void AddTemplates()
        {
            strTestCaseNo = "TC001";
            strtblname = "automation_templates";
            strTestType = "Regression";
            var AdminMenus = new FpAdminMenus();
            AdminMenus.AdminClick();
            AdminMenus.TemplatesClick();
        }

        public void VerifyErrorMessages()
        {

        }

        public void EditTemplates()
        {

        }

        public void DeleteTemplates()
        {

        }

        [TestCleanup]
        public void TestCleanup()
        {
            var TopbarMenu = new clsMainPage_TopbarMenu();
            TopbarMenu.Logout();
            PropertiesCollection.driver.Close();
            PropertiesCollection.driver.Quit();
            PropertiesCollection.driver.Dispose();
        }
    }
}
