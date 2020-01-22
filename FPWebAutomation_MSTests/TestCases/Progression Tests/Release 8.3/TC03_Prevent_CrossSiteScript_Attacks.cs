using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Configuration;
using FPWebAutomation_MSTests.Database;
using FPWebAutomation_MSTests.Email;
using FPWebAutomation_MSTests.PageObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FPWebAutomation_MSTests.Database.ConnectToMySQL_Fetch_TestData;

namespace FPWebAutomation_MSTests.TestCases.Progression_Tests.Release_8._3
{
    [TestClass]
    public class TC03_Prevent_CrossSiteScript_Attacks
    {
        IList<string> crossSiteScripts = System.IO.File.ReadLines(Path.Combine(Environment.CurrentDirectory, @"..\..\Resources\", "XSSInjection.txt")).ToList();
        string strtblname = "automation_studentresults";
        string strTestType = "Progression";

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
            PropertiesCollection.htmlReporter = new ExtentHtmlReporter(@"C:\FlightPro\Dev\_main\Test Automation\FlightPro\FightPro_WebAutomation\FPWebAutomation\Vanguard\Report\Report.html");
            PropertiesCollection.htmlReporter.LoadConfig(@"C:\extent-configfile\extent-config.xml");
            PropertiesCollection.extent = new ExtentReports();
            PropertiesCollection.extent.AttachReporter(PropertiesCollection.htmlReporter);
            PropertiesCollection.extent.AddSystemInfo("Automation Database", "8.1");
            PropertiesCollection.extent.AddSystemInfo("Browser", "Chrome");
            PropertiesCollection.extent.AddSystemInfo("Application Under Test (AUT)", "FlightProWeb");
            PropertiesCollection.extent.AddSystemInfo("Application URL", "http://oc-svr-at1/Fltpro_Automation_main/");
        }

        [TestCategory("Progression")]
        [TestCategory("US_42555_PreventCrossSiteScripts_Calendar")]
        [TestMethod]
        public void US_42555_PreventCrossSiteScripts_Calendar()
        {
            PropertiesCollection.test = PropertiesCollection.extent.CreateTest("US_42555_PreventCrossSiteScripts_Calendar");
            FpSideMenus SideMenu = new FpSideMenus();
            SideMenu.CalendarClick();
            System.Threading.Thread.Sleep(2000);
            FpCalendarPage CalendarPage = new FpCalendarPage();
            CalendarPage.BtnToday.Click();
            System.Threading.Thread.Sleep(2000);
            Actions actions = new Actions(PropertiesCollection.driver);
            actions.MoveToElement(CalendarPage.Grid).Perform();
            actions.DoubleClick(CalendarPage.Grid).Perform();
            System.Threading.Thread.Sleep(2000);

            foreach (var script in crossSiteScripts)
            {
                CalendarPage.TxtDescription.SendKeys(script);
                CalendarPage.BtnSave.Click();
                System.Threading.Thread.Sleep(1000);

                try
                { 
                Assert.IsTrue(CalendarPage.lblPeopleUnavailability.Displayed);
                PropertiesCollection.test.Log(Status.Pass,"CrossSiteScript: \n \'"+script.Length+"\'\n was prevented");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail,"CrossSiteScript: \n \'"+script.Length+"\'\n was not prevented");
                }
                CalendarPage.TxtDescription.Clear();
            }                   
        }

        [TestCategory("Progression")]
        [TestCategory("US_42183_PreventCrossSiteScripts_KnowledgeBase")]
        [TestMethod]
        public void US_42183_PreventCrossSiteScripts_KnowledgeBase()
        {
            PropertiesCollection.test = PropertiesCollection.extent.CreateTest("US_42183_PreventCrossSiteScripts_KnowledgeBase");
            FpSideMenus SideMenu = new FpSideMenus();
            System.Threading.Thread.Sleep(5000);
            SideMenu.LnkKnowledgeBase.Click();
            System.Threading.Thread.Sleep(8000);
            FpKnowledgeBasePage KnowledgeBasePage = new FpKnowledgeBasePage();

            foreach (var script in crossSiteScripts)
            {
                KnowledgeBasePage.btnAddKnowledgebaseByEntry.Click();
                KnowledgeBasePage.txtboxKnowledgeBaseTitle.SendKeys(script);
                PropertiesCollection.driver.SwitchTo().Frame(KnowledgeBasePage.iFrame);
                KnowledgeBasePage.txtAreaFrameKnowledgeBase.SendKeys(script);
                System.Threading.Thread.Sleep(2000);
                PropertiesCollection.driver.SwitchTo().DefaultContent();
                KnowledgeBasePage.btnSave.Click();
                Assert.IsTrue(KnowledgeBasePage.btnSave.Displayed);
                KnowledgeBasePage.txtboxKnowledgeBaseTitle.Clear();
                KnowledgeBasePage.txtboxKnowledgeBaseTitle.SendKeys("Modified Title");
                KnowledgeBasePage.btnSave.Click();
                System.Threading.Thread.Sleep(2000);
                Assert.AreEqual(KnowledgeBasePage.countbtnSave, 0);
                System.Threading.Thread.Sleep(2000);
                Assert.AreEqual(KnowledgeBasePage.lbliFrameKnowledgeBase.Text, script);
                KnowledgeBasePage.btnDelete.Click();
                KnowledgeBasePage.btnOK.Click();
                System.Threading.Thread.Sleep(2000);
            }
        }

        [TestCategory("Progression")]
        [TestCategory("US_42553_PreventCrossSiteScripts_StudentResults")]
        [TestMethod]
        public void US_42553_PreventCrossSiteScripts_StudentResults()
        {
            PropertiesCollection.test = PropertiesCollection.extent.CreateTest("US_41888_PreventMaliciousFileUpload_StudentResults_ValidDocuments");
            var strTestCaseNo = "TC001";
            var connection = new ConnectToMySQL_Fetch_TestData();
            var testdataStudentResults = connection.Select(strtblname, strTestCaseNo, strTestType);
            var sidebarMenu = new FpSideMenus();
            var studentResults = new FpStudentResultsPage();

            sidebarMenu.lnkStudentResults.Click();

            var strTDOrgnisationGroup = testdataStudentResults[4];
            var strTDStudentName = testdataStudentResults[5];
            var strTDInstructorName = testdataStudentResults[6];
            var strTDCourseName = testdataStudentResults[7];
            var strTDSyllabusName = testdataStudentResults[8];
            var strTDEventName = testdataStudentResults[9];
            var strTDScore = testdataStudentResults[10];
            var strTDScoreAssesmentCriteria = testdataStudentResults[11];
            var strTDStrength = testdataStudentResults[12];
            var strTDWeakness = testdataStudentResults[13];
            var strTDOverallComments = testdataStudentResults[14];
            var strTDPrivateComments = testdataStudentResults[15];
            var strTDServiceName = testdataStudentResults[16];
            var strTDCountryName = testdataStudentResults[17];
            var strTDStudentPosition = testdataStudentResults[18];
            var strTDStudentSurname = testdataStudentResults[19];
            var strTDResultAwarded = testdataStudentResults[20];

            studentResults.SearchStudent(strTDStudentSurname);
            studentResults.SearchCourseEvent(strTDEventName);

            WebDriverWait wait = new WebDriverWait(PropertiesCollection.driver, TimeSpan.FromSeconds(10));

            foreach (var script in crossSiteScripts)
            {
                studentResults.txtboxPrivateComments.SendKeys(script);
                studentResults.btnSave.Click();
                var lblPrivateComments = studentResults.GetTextPrivateComments();
                Assert.AreEqual(lblPrivateComments, script);
                System.Threading.Thread.Sleep(2000);
                studentResults.DeleteWriteup();
            }
        }

        [TestCleanup]
        public void TestCleanup()
        {
            PropertiesCollection.driver.Close();
            PropertiesCollection.driver.Quit();
            PropertiesCollection.driver.Dispose();
            System.Threading.Thread.Sleep(30000);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            PropertiesCollection.extent.Flush();
            //var email = new SendEmail();
            //email.Email(TotalTestCases,PassedTestCases);
        }
    }
}
