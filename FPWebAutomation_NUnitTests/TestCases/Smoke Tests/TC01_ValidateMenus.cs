using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using FPWebAutomation.Libraries;
using FPWebAutomation.PageObjects;
using NUnit.Framework;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPWebAutomation.TestCases
{
    class TC01_ValidateMenus
    {
        
        private static ExtentReports extent;
        private static ExtentHtmlReporter htmlReporter;
        private static ExtentTest test;

        [SetUp]
        public void Setup()
        {
            htmlReporter = new ExtentHtmlReporter(@"C:\FlightPro\Dev\_main\Test Automation\.NETFramework_v3\FightPro_WebAutomation\FPWebAutomation\Report\Report.html");

            htmlReporter.Config.Theme = Theme.Dark;

            htmlReporter.Config.DocumentTitle = "TestAutomationReport_FlightPro";

            htmlReporter.Config.ReportName = "TestAutomationReport_FlightPro";

            
            htmlReporter.Config.JS = "$('.brand-logo').text('').append('<img src=C:\\Users\anupd\\Documents\\Ocean Logo.jpg>')";
            extent = new ExtentReports();

            extent.AttachReporter(htmlReporter);

        }

        [Test]
        public void TC01_FPWeb_ValidateMenu()
        {
            test = extent.CreateTest("TC01_FPWeb_Validation");

            var dr = Lib_Login.Login();

            var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(dr, new TimeSpan(0, 0, 30));

            var SidebarMenu = new clsMainPage_SidebarMenu();

            PageFactory.InitElements(dr, SidebarMenu);

            

            SidebarMenu.Summary_link.Click();

            test.Pass("Assertion Passed");

            

            //      SidebarMenu.Calendar_link.Click();

            //        var element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("href12005")));

            //         SidebarMenu.ControlHoursReport_link.Click();

            //  SidebarMenu.CurrencyHistoryReport_link.Click();

             

               

            

            //SidebarMenu.EventsReport_link.Click();

            //  SidebarMenu.Roster_link.Click();

            //  SidebarMenu.DailyScheduling_link.Click();

            //  SidebarMenu.DutyTimes_link.Click();

            //  SidebarMenu.ProgrammeViewer_link.Click();

            //   SidebarMenu.KnowledgeBase_link.Click();

            //          SidebarMenu.StatusBoard_link.Click();

            //    SidebarMenu.StudentResults_link.Click();


          //  Lib_Logout.Logout(dr);

          
       


    }

    [TearDown]
    public void TearDown()
    {
            
     }

        [OneTimeTearDown]

    public void GenerateReport()
        {

            extent.Flush();
        }




    }
}
