using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Configuration;
using FPWebAutomation_MSTests.PageObjects;
using OpenQA.Selenium.Chrome;
using FPWebAutomation_MSTests.Database;
using AventStack.ExtentReports;
using System.Collections.Generic;
using AventStack.ExtentReports.Reporter;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Configuration;
using System.Collections;
using System.Linq;

namespace FPWebAutomation_MSTests.TestCases.Progression_Tests
{
    [TestClass]
    public class TS02_PreventMaliciousFileupload
    {
        

        [TestInitialize]
        public void TestInitialize()
        {
            PropertiesCollection.driver = new ChromeDriver(System.Configuration.ConfigurationManager.AppSettings["ChromeDriverPath"]);
            PropertiesCollection.driver.Manage().Window.Maximize();
            PropertiesCollection.driver.Navigate().GoToUrl(Configuration.ConfigurationManager.AppSettings["URL"]);

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

        [TestMethod]
        public void US_41888_ProPlatform ()
        {
            System.Threading.Thread.Sleep(3000);
            IWebElement btnDashboard = PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[1]/div/ul[1]/div/a[2]"));
            btnDashboard.Click();

            IWebElement btnAddmoreUsers = PropertiesCollection.driver.FindElement(By.XPath("//button[@class='btn btnAddUsers']"));
            System.Threading.Thread.Sleep(2000);
            btnAddmoreUsers.Click();
        }

        //[TestCleanup]
        //public void TestCleanup()
        //{
        //    PropertiesCollection.driver.Close();
        //    PropertiesCollection.driver.Quit();
        //    PropertiesCollection.driver.Dispose();
        //    System.Threading.Thread.Sleep(30000);
        //}

        //[ClassCleanup]
        //public static void ClassCleanup()
        //{
        //    PropertiesCollection.extent.Flush();
        //}
    }
}
