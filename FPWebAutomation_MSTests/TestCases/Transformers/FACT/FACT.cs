using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using FPWebAutomation_MSTests.API;
using FPWebAutomation_MSTests.Database;
using FPWebAutomation_MSTests.PageObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace FPWebAutomation_MSTests.TestCases.Transformers
{
    class FACT
    {
        [TestClass]
        public class Verify_FACT
        {
            public TestContext TestContext { get; set; }

            [ClassInitialize]
            public static void ClassInitialize(TestContext context)
            {

                PropertiesCollection.htmlReporter = new ExtentHtmlReporter(@"C:\Report\Report.html");
                PropertiesCollection.htmlReporter.LoadConfig(@"C:\extent-configfile\extent-config.xml");
                PropertiesCollection.extent = new AventStack.ExtentReports.ExtentReports();
                PropertiesCollection.extent.AttachReporter(PropertiesCollection.htmlReporter);
                PropertiesCollection.extent.AddSystemInfo("Automation Database", "8.2");
                PropertiesCollection.extent.AddSystemInfo("Browser", "Chrome");
                PropertiesCollection.extent.AddSystemInfo("Application Under Test (AUT)", "FACT");
            }


            [TestInitialize]
            public void TestInitialize()
            {
                PropertiesCollection.driver = new ChromeDriver(@ConfigurationManager.AppSettings["ChromeDriverPath"]);
                PropertiesCollection.driver.Manage().Window.Maximize();
            }

           
            [TestMethod]
            public void MissionStripAtCurrentDateTime()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("Mission Strip At Current Date and Time");
                Strips Strip = new Strips();

                /*Create Mission Strip at current date and time */

                String strTestCaseNo = "TC001";
                String strtblname = "automation_strips";
                String strTestType = "Regression";

                /* Get test data from MySQL */

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataStrip_details = connection.Select(strtblname, strTestCaseNo, strTestType);

                string strTDAssetTypeID = testdataStrip_details[5];
                string strTDPaneID = testdataStrip_details[11];
                string strTDType = testdataStrip_details[15];
                string strTDPersonID = testdataStrip_details[12];
                string strTDSlotNumber = testdataStrip_details[13];
                string strTDWeatherStateID = testdataStrip_details[16];
                String Details = "AT Mission Strip created at current date and time";

                DateTime PlannedStartTime = DateTime.Now.ToUniversalTime().AddHours(2);
                DateTime PlannedEndTime = PlannedStartTime.AddHours(1);

                String strPlannedStartTime = PlannedStartTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");
                String strPlannedEndTime = PlannedEndTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");

                strPlannedStartTime = strPlannedStartTime.Remove(14, 2);
                strPlannedStartTime = strPlannedStartTime.Insert(14, "05");

                strPlannedEndTime = strPlannedEndTime.Remove(14, 2);
                strPlannedEndTime = strPlannedEndTime.Insert(14, "05");

                String StripID = Strip.InsertMissionStrip(strTDAssetTypeID, strTDPaneID, strTDPersonID, strTDSlotNumber, strTDWeatherStateID, Details, strPlannedStartTime, strPlannedEndTime);
                System.Threading.Thread.Sleep(5000);

                /* Run FACT Scheduled Task */

                RunFACT fact = new RunFACT();
                fact.RunFACTScheduledJob();
                System.Threading.Thread.Sleep(240000);

                /* Login to Webmail */

                WebmailLoginPage WebmailLoginPage = new WebmailLoginPage();
                WebmailLoginPage.LoginWebmailWithUserCredentials();
                System.Threading.Thread.Sleep(5000);

                /* Navigate to Calendar */

                WebmailMainPage WebmailMainPage = new WebmailMainPage();
                WebmailMainPage.NavigationMenuClick();
                WebmailMainPage.CalendarClick();
                System.Threading.Thread.Sleep(5000);
                WebmailCalendarPage WebmailCalendarPage = new WebmailCalendarPage();
                WebmailCalendarPage.BtnDay.Click();
                System.Threading.Thread.Sleep(5000);
                
                try
                {                   
                    Assert.IsTrue(WebmailCalendarPage.MissionStrip.Displayed);
                    PropertiesCollection.test.Log(Status.Pass, "Mission Strip displayed on Outlook");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Mission Strip not displayed on Outlook");
                }

                /* Delete Strip */
                
                Strip.Delete_Strip(StripID);
            }

            [TestMethod]
            public void MissionStripBeforeFACTWindow()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("Mission Strip Before the FACT Window");
                Strips Strip = new Strips();

                /*Create Mission Strip at current date and time */

                String strTestCaseNo = "TC001";
                String strtblname = "automation_strips";
                String strTestType = "Regression";

                /* Get test data from MySQL */

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataStrip_details = connection.Select(strtblname, strTestCaseNo, strTestType);

                string strTDAssetTypeID = testdataStrip_details[5];
                string strTDPaneID = testdataStrip_details[11];
                string strTDType = testdataStrip_details[15];
                string strTDPersonID = testdataStrip_details[12];
                string strTDSlotNumber = testdataStrip_details[13];
                string strTDWeatherStateID = testdataStrip_details[16];
                String Details = "AT Mission Strip created before the FACT Window";

                DateTime PlannedStartTime = DateTime.Now.ToUniversalTime().AddHours(2).AddDays(-3);
                DateTime PlannedEndTime = PlannedStartTime.AddHours(1);
               
                String strPlannedStartTime = PlannedStartTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");
                String strPlannedEndTime = PlannedEndTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");

                strPlannedStartTime = strPlannedStartTime.Remove(14, 2);
                strPlannedStartTime = strPlannedStartTime.Insert(14, "05");

                strPlannedEndTime = strPlannedEndTime.Remove(14, 2);
                strPlannedEndTime = strPlannedEndTime.Insert(14, "05");

                int Day = PlannedStartTime.Day;
                String Month = PlannedStartTime.ToString("MMM");
                int Year = PlannedStartTime.Year;

                String StripID = Strip.InsertMissionStrip(strTDAssetTypeID, strTDPaneID, strTDPersonID, strTDSlotNumber, strTDWeatherStateID, Details, strPlannedStartTime, strPlannedEndTime);
                System.Threading.Thread.Sleep(5000);

                /* Run FACT Scheduled Task */

                RunFACT fact = new RunFACT();
                fact.RunFACTScheduledJob();
                System.Threading.Thread.Sleep(240000);

                /* Login to Webmail */

                WebmailLoginPage WebmailLoginPage = new WebmailLoginPage();
                WebmailLoginPage.LoginWebmailWithUserCredentials();
                System.Threading.Thread.Sleep(5000);

                /* Navigate to Calendar */

                WebmailMainPage WebmailMainPage = new WebmailMainPage();
                WebmailMainPage.NavigationMenuClick();
                WebmailMainPage.CalendarClick();
                System.Threading.Thread.Sleep(15000);
                WebmailCalendarPage WebmailCalendarPage = new WebmailCalendarPage();
                WebmailCalendarPage.BtnDay.Click();
                String CalendarValue = DateTime.Now.ToString("MMMM yyyy");
                System.Threading.Thread.Sleep(5000);
                WebmailCalendarPage.DateClick(Day, Month, Year, CalendarValue);
                System.Threading.Thread.Sleep(5000);
            
                try
                {
                    Assert.IsTrue(WebmailCalendarPage.MissionStrip.Displayed);
                    PropertiesCollection.test.Log(Status.Fail, "Mission Strip displayed on Outlook");                     
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Pass, "Mission Strip not displayed on Outlook");
                }

                /* Delete Strip */

                Strip.Delete_Strip(StripID);
            }

            [TestMethod]
            public void MissionStripAfterFACTWindow()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("Mission Strip After the FACT Window");
                Strips Strip = new Strips();

                /*Create Mission Strip at current date and time */

                String strTestCaseNo = "TC001";
                String strtblname = "automation_strips";
                String strTestType = "Regression";

                /* Get test data from MySQL */

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataStrip_details = connection.Select(strtblname, strTestCaseNo, strTestType);

                string strTDAssetTypeID = testdataStrip_details[5];
                string strTDPaneID = testdataStrip_details[11];
                string strTDType = testdataStrip_details[15];
                string strTDPersonID = testdataStrip_details[12];
                string strTDSlotNumber = testdataStrip_details[13];
                string strTDWeatherStateID = testdataStrip_details[16];
                String Details = "AT Mission Strip created after the FACT Window";

                DateTime PlannedStartTime = DateTime.Now.ToUniversalTime().AddHours(2).AddDays(8);
                DateTime PlannedEndTime = PlannedStartTime.AddHours(1);
                

                String strPlannedStartTime = PlannedStartTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");
                String strPlannedEndTime = PlannedEndTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");

                strPlannedStartTime = strPlannedStartTime.Remove(14, 2);
                strPlannedStartTime = strPlannedStartTime.Insert(14, "05");

                strPlannedEndTime = strPlannedEndTime.Remove(14, 2);
                strPlannedEndTime = strPlannedEndTime.Insert(14, "05");

                int Day = PlannedStartTime.Day;
                String Month = PlannedStartTime.ToString("MMM");
                int Year = PlannedStartTime.Year;

                String StripID = Strip.InsertMissionStrip(strTDAssetTypeID, strTDPaneID, strTDPersonID, strTDSlotNumber, strTDWeatherStateID, Details, strPlannedStartTime, strPlannedEndTime);
                System.Threading.Thread.Sleep(5000);

                /* Run FACT Scheduled Task */

                RunFACT fact = new RunFACT();
                fact.RunFACTScheduledJob();
                System.Threading.Thread.Sleep(240000);

                /* Login to Webmail */

                WebmailLoginPage WebmailLoginPage = new WebmailLoginPage();
                WebmailLoginPage.LoginWebmailWithUserCredentials();
                System.Threading.Thread.Sleep(5000);

                /* Navigate to Calendar */

                WebmailMainPage WebmailMainPage = new WebmailMainPage();
                WebmailMainPage.NavigationMenuClick();
                WebmailMainPage.CalendarClick();
                System.Threading.Thread.Sleep(15000);
                WebmailCalendarPage WebmailCalendarPage = new WebmailCalendarPage();
                WebmailCalendarPage.BtnDay.Click();
                String CalendarValue = DateTime.Now.ToString("MMMM yyyy");
                System.Threading.Thread.Sleep(5000);
                WebmailCalendarPage.DateClick(Day,Month, Year, CalendarValue);
                System.Threading.Thread.Sleep(5000);

                try
                {
                    Assert.IsTrue(WebmailCalendarPage.MissionStrip.Displayed);
                    PropertiesCollection.test.Log(Status.Fail, "Mission Strip displayed on Outlook");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Pass, "Mission Strip not displayed on Outlook");
                }

                /* Delete Strip */

                Strip.Delete_Strip(StripID);
            }

            [TestMethod]
            public void MissionStripOnFACTWindowTimeSpanFrom()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("Mission Strip On the FACT Window lying on the Time Span From");
                Strips Strip = new Strips();

                /*Create Mission Strip at current date and time */

                String strTestCaseNo = "TC001";
                String strtblname = "automation_strips";
                String strTestType = "Regression";

                /* Get test data from MySQL */

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataStrip_details = connection.Select(strtblname, strTestCaseNo, strTestType);

                string strTDAssetTypeID = testdataStrip_details[5];
                string strTDPaneID = testdataStrip_details[11];
                string strTDType = testdataStrip_details[15];
                string strTDPersonID = testdataStrip_details[12];
                string strTDSlotNumber = testdataStrip_details[13];
                string strTDWeatherStateID = testdataStrip_details[16];
                String Details = "AT Mission Strip created On the FACT Window lying on the Time Span From";

                DateTime PlannedStartTime = DateTime.Now.ToUniversalTime().AddDays(-2).AddHours(1);
                DateTime PlannedEndTime = PlannedStartTime.AddHours(2);                             

                String strPlannedStartTime = PlannedStartTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");
                String strPlannedEndTime = PlannedEndTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");

                strPlannedStartTime = strPlannedStartTime.Remove(14, 2);
                strPlannedStartTime = strPlannedStartTime.Insert(14, "05");

                strPlannedEndTime = strPlannedEndTime.Remove(14, 2);
                strPlannedEndTime = strPlannedEndTime.Insert(14, "05");

                int Day = PlannedEndTime.Day;
                String Month = PlannedEndTime.ToString("MMM");
                int Year = PlannedEndTime.Year;

                String StripID = Strip.InsertMissionStrip(strTDAssetTypeID, strTDPaneID, strTDPersonID, strTDSlotNumber, strTDWeatherStateID, Details, strPlannedStartTime, strPlannedEndTime);
                System.Threading.Thread.Sleep(5000);

                /* Run FACT Scheduled Task */

                RunFACT fact = new RunFACT();
                fact.RunFACTScheduledJob();
                System.Threading.Thread.Sleep(240000);

                /* Login to Webmail */

                WebmailLoginPage WebmailLoginPage = new WebmailLoginPage();
                WebmailLoginPage.LoginWebmailWithUserCredentials();
                System.Threading.Thread.Sleep(5000);

                /* Navigate to Calendar */

                WebmailMainPage WebmailMainPage = new WebmailMainPage();
                WebmailMainPage.NavigationMenuClick();
                WebmailMainPage.CalendarClick();
                System.Threading.Thread.Sleep(15000);
                WebmailCalendarPage WebmailCalendarPage = new WebmailCalendarPage();
                WebmailCalendarPage.BtnDay.Click();
                String CalendarValue = DateTime.Now.ToString("MMMM yyyy");
                System.Threading.Thread.Sleep(5000);
                WebmailCalendarPage.DateClick(Day, Month, Year, CalendarValue);
                System.Threading.Thread.Sleep(5000);

                try
                {
                    Assert.IsTrue(WebmailCalendarPage.MissionStrip.Displayed);
                    PropertiesCollection.test.Log(Status.Pass, "Mission Strip displayed on Outlook");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Mission Strip not displayed on Outlook");
                }

                /* Delete Strip */
                System.Threading.Thread.Sleep(5000);
                Strip.Delete_Strip(StripID);
            }

            [TestMethod]
            public void MissionStripOnFACTWindowTimeSpanTo()
            {

                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("Mission Strip On the FACT Window lying on the Time Span To");
                Strips Strip = new Strips();

                /*Create Mission Strip at current date and time */

                String strTestCaseNo = "TC001";
                String strtblname = "automation_strips";
                String strTestType = "Regression";

                /* Get test data from MySQL */

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataStrip_details = connection.Select(strtblname, strTestCaseNo, strTestType);

                string strTDAssetTypeID = testdataStrip_details[5];
                string strTDPaneID = testdataStrip_details[11];
                string strTDType = testdataStrip_details[15];
                string strTDPersonID = testdataStrip_details[12];
                string strTDSlotNumber = testdataStrip_details[13];
                string strTDWeatherStateID = testdataStrip_details[16];
                String Details = "AT Mission Strip created On the FACT Window lying on the Time Span To";

                DateTime PlannedStartTime = DateTime.Now.ToUniversalTime().AddDays(7).AddHours(-1);
                DateTime PlannedEndTime = PlannedStartTime.AddHours(2);
                
                String strPlannedStartTime = PlannedStartTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");
                String strPlannedEndTime = PlannedEndTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");

                strPlannedStartTime = strPlannedStartTime.Remove(14, 2);
                strPlannedStartTime = strPlannedStartTime.Insert(14, "05");

                strPlannedEndTime = strPlannedEndTime.Remove(14, 2);
                strPlannedEndTime = strPlannedEndTime.Insert(14, "05");

                int Day = PlannedStartTime.Day;
                String Month = PlannedStartTime.ToString("MMM");
                int Year = PlannedStartTime.Year;

                String StripID = Strip.InsertMissionStrip(strTDAssetTypeID, strTDPaneID, strTDPersonID, strTDSlotNumber, strTDWeatherStateID, Details, strPlannedStartTime, strPlannedEndTime);
                System.Threading.Thread.Sleep(5000);

                /* Run FACT Scheduled Task */

                RunFACT fact = new RunFACT();
                fact.RunFACTScheduledJob();
                System.Threading.Thread.Sleep(240000);

                /* Login to Webmail */

                WebmailLoginPage WebmailLoginPage = new WebmailLoginPage();
                WebmailLoginPage.LoginWebmailWithUserCredentials();
                System.Threading.Thread.Sleep(5000);

                /* Navigate to Calendar */

                WebmailMainPage WebmailMainPage = new WebmailMainPage();
                WebmailMainPage.NavigationMenuClick();
                WebmailMainPage.CalendarClick();
                System.Threading.Thread.Sleep(15000);
                WebmailCalendarPage WebmailCalendarPage = new WebmailCalendarPage();
                WebmailCalendarPage.BtnDay.Click();
                String CalendarValue = DateTime.Now.ToString("MMMM yyyy");
                System.Threading.Thread.Sleep(5000);
                WebmailCalendarPage.DateClick(Day, Month, Year, CalendarValue);
                System.Threading.Thread.Sleep(15000);

                try
                {
                    Assert.IsTrue(WebmailCalendarPage.MissionStrip.Displayed);
                    PropertiesCollection.test.Log(Status.Pass, "Mission Strip displayed on Outlook");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Mission Strip not displayed on Outlook");
                }

                /* Delete Strip */

                Strip.Delete_Strip(StripID);
            }

            [TestMethod]
            public void TaskStripAtCurrentDateTime()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("Task Strip At Current Date and Time");
                Strips Strip = new Strips();

                /*Create Task Strip at current date and time */

                String strTestCaseNo = "TC001";
                String strtblname = "automation_strips";
                String strTestType = "Regression";

                /* Get test data from MySQL */

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataStrip_details = connection.Select(strtblname, strTestCaseNo, strTestType);

                string strTDAssetTypeID = testdataStrip_details[5];
                string strTDPaneID = testdataStrip_details[11];
                string strTDType = testdataStrip_details[15];
                string strTDPersonID = testdataStrip_details[12];
                string strTDSlotNumber = testdataStrip_details[13];
                string strTDWeatherStateID = testdataStrip_details[16];
                String Details = "AT Task Strip created at current date and time";

                DateTime PlannedStartTime = DateTime.Now.ToUniversalTime().AddHours(2);
                DateTime PlannedEndTime = PlannedStartTime.AddHours(1);               

                int Year = PlannedStartTime.Year;
                String strPlannedStartTime = PlannedStartTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");
                String strPlannedEndTime = PlannedEndTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");

                strPlannedStartTime = strPlannedStartTime.Remove(14, 2);
                strPlannedStartTime = strPlannedStartTime.Insert(14, "05");

                strPlannedEndTime = strPlannedEndTime.Remove(14, 2);
                strPlannedEndTime = strPlannedEndTime.Insert(14, "05");

                String StripID = Strip.InsertTaskStrip(strTDAssetTypeID, strTDPaneID, strTDPersonID, strTDSlotNumber, Details, strPlannedStartTime, strPlannedEndTime);
                System.Threading.Thread.Sleep(5000);

                /* Run FACT Scheduled Task */

                RunFACT fact = new RunFACT();
                fact.RunFACTScheduledJob();
                System.Threading.Thread.Sleep(240000);

                /* Login to Webmail */

                WebmailLoginPage WebmailLoginPage = new WebmailLoginPage();
                WebmailLoginPage.LoginWebmailWithUserCredentials();
                System.Threading.Thread.Sleep(5000);

                /* Navigate to Calendar */

                WebmailMainPage WebmailMainPage = new WebmailMainPage();
                WebmailMainPage.NavigationMenuClick();
                WebmailMainPage.CalendarClick();
                System.Threading.Thread.Sleep(15000);
                WebmailCalendarPage WebmailCalendarPage = new WebmailCalendarPage();
                WebmailCalendarPage.BtnDay.Click();
                System.Threading.Thread.Sleep(5000);

                try
                {
                    Assert.IsTrue(WebmailCalendarPage.TaskStrip.Displayed);
                    PropertiesCollection.test.Log(Status.Pass, "Task Strip displayed on Outlook");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Task Strip not displayed on Outlook");
                }

                /* Delete Strip */

                Strip.Delete_Strip(StripID);
            }

            [TestMethod]
            public void TaskStripBeforeFACTWindow()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("Task Strip Before the FACT Window");
                Strips Strip = new Strips();

                /*Create Task Strip Before the FACT Window */

                String strTestCaseNo = "TC001";
                String strtblname = "automation_strips";
                String strTestType = "Regression";

                /* Get test data from MySQL */

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataStrip_details = connection.Select(strtblname, strTestCaseNo, strTestType);

                string strTDAssetTypeID = testdataStrip_details[5];
                string strTDPaneID = testdataStrip_details[11];
                string strTDType = testdataStrip_details[15];
                string strTDPersonID = testdataStrip_details[12];
                string strTDSlotNumber = testdataStrip_details[13];
                string strTDWeatherStateID = testdataStrip_details[16];
                String Details = "AT Task Strip created before the FACT Window";

                DateTime PlannedStartTime = DateTime.Now.ToUniversalTime().AddHours(2).AddDays(-3);
                DateTime PlannedEndTime = PlannedStartTime.AddHours(1);
               
                String strPlannedStartTime = PlannedStartTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");
                String strPlannedEndTime = PlannedEndTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");

                strPlannedStartTime = strPlannedStartTime.Remove(14, 2);
                strPlannedStartTime = strPlannedStartTime.Insert(14, "05");

                strPlannedEndTime = strPlannedEndTime.Remove(14, 2);
                strPlannedEndTime = strPlannedEndTime.Insert(14, "05");

                int Day = PlannedStartTime.Day;
                String Month = PlannedStartTime.ToString("MMM");
                int Year = PlannedStartTime.Year;

                String StripID = Strip.InsertTaskStrip(strTDAssetTypeID, strTDPaneID, strTDPersonID, strTDSlotNumber, Details, strPlannedStartTime, strPlannedEndTime);
                System.Threading.Thread.Sleep(5000);

                /* Run FACT Scheduled Task */

                RunFACT fact = new RunFACT();
                fact.RunFACTScheduledJob();
                System.Threading.Thread.Sleep(240000);

                /* Login to Webmail */

                WebmailLoginPage WebmailLoginPage = new WebmailLoginPage();
                WebmailLoginPage.LoginWebmailWithUserCredentials();
                System.Threading.Thread.Sleep(5000);

                /* Navigate to Calendar */

                WebmailMainPage WebmailMainPage = new WebmailMainPage();
                WebmailMainPage.NavigationMenuClick();
                WebmailMainPage.CalendarClick();
                System.Threading.Thread.Sleep(15000);
                WebmailCalendarPage WebmailCalendarPage = new WebmailCalendarPage();
                WebmailCalendarPage.BtnDay.Click();
                String CalendarValue = DateTime.Now.ToString("MMMM yyyy");
                System.Threading.Thread.Sleep(5000);
                WebmailCalendarPage.DateClick(Day, Month, Year, CalendarValue);
                System.Threading.Thread.Sleep(15000);
                try
                {
                    Assert.IsTrue(WebmailCalendarPage.TaskStrip.Displayed);
                    PropertiesCollection.test.Log(Status.Fail, "Task Strip displayed on Outlook");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Pass, "Task Strip not displayed on Outlook");
                }

                /* Delete Strip */

                Strip.Delete_Strip(StripID);
            }

            [TestMethod]
            public void TaskStripAfterFACTWindow()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("Task Strip After the FACT Window");
                Strips Strip = new Strips();

                /*Create Task Strip After the FACT Window*/

                String strTestCaseNo = "TC001";
                String strtblname = "automation_strips";
                String strTestType = "Regression";

                /* Get test data from MySQL */

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataStrip_details = connection.Select(strtblname, strTestCaseNo, strTestType);

                string strTDAssetTypeID = testdataStrip_details[5];
                string strTDPaneID = testdataStrip_details[11];
                string strTDType = testdataStrip_details[15];
                string strTDPersonID = testdataStrip_details[12];
                string strTDSlotNumber = testdataStrip_details[13];
                string strTDWeatherStateID = testdataStrip_details[16];
                String Details = "AT Task Strip created after the FACT Window";

                DateTime PlannedStartTime = DateTime.Now.ToUniversalTime().AddHours(2).AddDays(8);
                DateTime PlannedEndTime = PlannedStartTime.AddHours(1);
             
                String strPlannedStartTime = PlannedStartTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");
                String strPlannedEndTime = PlannedEndTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");

                strPlannedStartTime = strPlannedStartTime.Remove(14, 2);
                strPlannedStartTime = strPlannedStartTime.Insert(14, "05");

                strPlannedEndTime = strPlannedEndTime.Remove(14, 2);
                strPlannedEndTime = strPlannedEndTime.Insert(14, "05");

                int Day = PlannedStartTime.Day;
                String Month = PlannedStartTime.ToString("MMM");
                int Year = PlannedStartTime.Year;

                String StripID = Strip.InsertTaskStrip(strTDAssetTypeID, strTDPaneID, strTDPersonID, strTDSlotNumber, Details, strPlannedStartTime, strPlannedEndTime);
                System.Threading.Thread.Sleep(5000);

                /* Run FACT Scheduled Task */

                RunFACT fact = new RunFACT();
                fact.RunFACTScheduledJob();
                System.Threading.Thread.Sleep(240000);

                /* Login to Webmail */

                WebmailLoginPage WebmailLoginPage = new WebmailLoginPage();
                WebmailLoginPage.LoginWebmailWithUserCredentials();
                System.Threading.Thread.Sleep(5000);

                /* Navigate to Calendar */

                WebmailMainPage WebmailMainPage = new WebmailMainPage();
                WebmailMainPage.NavigationMenuClick();
                WebmailMainPage.CalendarClick();
                System.Threading.Thread.Sleep(15000);
                WebmailCalendarPage WebmailCalendarPage = new WebmailCalendarPage();
                WebmailCalendarPage.BtnDay.Click();
                String CalendarValue = DateTime.Now.ToString("MMMM yyyy");
                System.Threading.Thread.Sleep(5000);
                WebmailCalendarPage.DateClick(Day, Month, Year, CalendarValue);
                System.Threading.Thread.Sleep(15000);
                try
                {
                    Assert.IsTrue(WebmailCalendarPage.TaskStrip.Displayed);
                    PropertiesCollection.test.Log(Status.Fail, "Task Strip displayed on Outlook");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Pass, "Task Strip not displayed on Outlook");
                }

                /* Delete Strip */

                Strip.Delete_Strip(StripID);
            }

            [TestMethod]
            public void TaskStripOnFACTWindowTimeSpanFrom()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("Task Strip On the FACT Window lying on the Time Span From");
                Strips Strip = new Strips();

                /*Create Task Strip On the FACT Window lying on the Time Span From */

                String strTestCaseNo = "TC001";
                String strtblname = "automation_strips";
                String strTestType = "Regression";

                /* Get test data from MySQL */

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataStrip_details = connection.Select(strtblname, strTestCaseNo, strTestType);

                string strTDAssetTypeID = testdataStrip_details[5];
                string strTDPaneID = testdataStrip_details[11];
                string strTDType = testdataStrip_details[15];
                string strTDPersonID = testdataStrip_details[12];
                string strTDSlotNumber = testdataStrip_details[13];
                string strTDWeatherStateID = testdataStrip_details[16];
                String Details = "AT TAsk Strip created On the FACT Window lying on the Time Span From";

                DateTime PlannedStartTime = DateTime.Now.ToUniversalTime().AddDays(-2).AddHours(1);
                DateTime PlannedEndTime = PlannedStartTime.AddHours(2);
                
                String strPlannedStartTime = PlannedStartTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");
                String strPlannedEndTime = PlannedEndTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");

                strPlannedStartTime = strPlannedStartTime.Remove(14, 2);
                strPlannedStartTime = strPlannedStartTime.Insert(14, "05");

                strPlannedEndTime = strPlannedEndTime.Remove(14, 2);
                strPlannedEndTime = strPlannedEndTime.Insert(14, "05");

                int Day = PlannedEndTime.Day;
                String Month = PlannedEndTime.ToString("MMM");
                int Year = PlannedEndTime.Year;

                String StripID = Strip.InsertTaskStrip(strTDAssetTypeID, strTDPaneID, strTDPersonID, strTDSlotNumber, Details, strPlannedStartTime, strPlannedEndTime);
                System.Threading.Thread.Sleep(5000);

                /* Run FACT Scheduled Task */

                RunFACT fact = new RunFACT();
                fact.RunFACTScheduledJob();
                System.Threading.Thread.Sleep(240000);

                /* Login to Webmail */

                WebmailLoginPage WebmailLoginPage = new WebmailLoginPage();
                WebmailLoginPage.LoginWebmailWithUserCredentials();
                System.Threading.Thread.Sleep(5000);

                /* Navigate to Calendar */

                WebmailMainPage WebmailMainPage = new WebmailMainPage();
                WebmailMainPage.NavigationMenuClick();
                WebmailMainPage.CalendarClick();
                System.Threading.Thread.Sleep(15000);
                WebmailCalendarPage WebmailCalendarPage = new WebmailCalendarPage();
                WebmailCalendarPage.BtnDay.Click();
                String CalendarValue = DateTime.Now.ToString("MMMM yyyy");
                System.Threading.Thread.Sleep(5000);
                WebmailCalendarPage.DateClick(Day, Month, Year, CalendarValue);
                System.Threading.Thread.Sleep(15000);
                try
                {
                    Assert.IsTrue(WebmailCalendarPage.TaskStrip.Displayed);
                    PropertiesCollection.test.Log(Status.Pass, "Task Strip displayed on Outlook");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Task Strip not displayed on Outlook");
                }

                /* Delete Strip */

                Strip.Delete_Strip(StripID);
            }

            [TestMethod]
            public void TaskStripOnFACTWindowTimeSpanTo()
            {

                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("Task Strip On the FACT Window lying on the Time Span To");
                Strips Strip = new Strips();

                /*Create Task Strip On the FACT Window lying on the Time Span To */

                String strTestCaseNo = "TC001";
                String strtblname = "automation_strips";
                String strTestType = "Regression";

                /* Get test data from MySQL */

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataStrip_details = connection.Select(strtblname, strTestCaseNo, strTestType);

                string strTDAssetTypeID = testdataStrip_details[5];
                string strTDPaneID = testdataStrip_details[11];
                string strTDType = testdataStrip_details[15];
                string strTDPersonID = testdataStrip_details[12];
                string strTDSlotNumber = testdataStrip_details[13];
                string strTDWeatherStateID = testdataStrip_details[16];
                String Details = "AT Task Strip created On the FACT Window lying on the Time Span To";

                DateTime PlannedStartTime = DateTime.Now.ToUniversalTime().AddDays(7).AddHours(-1);
                DateTime PlannedEndTime = PlannedStartTime.AddHours(2);
               
                String strPlannedStartTime = PlannedStartTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");
                String strPlannedEndTime = PlannedEndTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");

                strPlannedStartTime = strPlannedStartTime.Remove(14, 2);
                strPlannedStartTime = strPlannedStartTime.Insert(14, "05");

                strPlannedEndTime = strPlannedEndTime.Remove(14, 2);
                strPlannedEndTime = strPlannedEndTime.Insert(14, "05");

                int Day = PlannedStartTime.Day;
                String Month = PlannedStartTime.ToString("MMM");
                int Year = PlannedStartTime.Year;

                String StripID = Strip.InsertTaskStrip(strTDAssetTypeID, strTDPaneID, strTDPersonID, strTDSlotNumber,  Details, strPlannedStartTime, strPlannedEndTime);
                System.Threading.Thread.Sleep(5000);

                /* Run FACT Scheduled Task */

                RunFACT fact = new RunFACT();
                fact.RunFACTScheduledJob();
                System.Threading.Thread.Sleep(240000);

                /* Login to Webmail */

                WebmailLoginPage WebmailLoginPage = new WebmailLoginPage();
                WebmailLoginPage.LoginWebmailWithUserCredentials();
                System.Threading.Thread.Sleep(5000);

                /* Navigate to Calendar */

                WebmailMainPage WebmailMainPage = new WebmailMainPage();
                WebmailMainPage.NavigationMenuClick();
                WebmailMainPage.CalendarClick();
                System.Threading.Thread.Sleep(15000);
                WebmailCalendarPage WebmailCalendarPage = new WebmailCalendarPage();
                WebmailCalendarPage.BtnDay.Click();
                String CalendarValue = DateTime.Now.ToString("MMMM yyyy");
                System.Threading.Thread.Sleep(5000);
                WebmailCalendarPage.DateClick(Day, Month, Year, CalendarValue);
                System.Threading.Thread.Sleep(15000);

                try
                {
                    Assert.IsTrue(WebmailCalendarPage.TaskStrip.Displayed);
                    PropertiesCollection.test.Log(Status.Pass, "Task Strip displayed on Outlook");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Task Strip not displayed on Outlook");
                }

                /* Delete Strip */

                Strip.Delete_Strip(StripID);
            }

            [TestMethod]
            public void BriefStripAtCurrentDateTime()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("Brief Strip At Current Date and Time");
                Strips Strip = new Strips();

                /*Create Brief Strip at current date and time */

                String strTestCaseNo = "TC001";
                String strtblname = "automation_strips";
                String strTestType = "Regression";

                /* Get test data from MySQL */

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataStrip_details = connection.Select(strtblname, strTestCaseNo, strTestType);

                string strTDAssetTypeID = testdataStrip_details[5];
                string strTDPaneID = testdataStrip_details[11];
                string strTDType = testdataStrip_details[15];
                string strTDPersonID = testdataStrip_details[12];
                string strTDSlotNumber = testdataStrip_details[13];
                String Details = "AT Brief Strip created at current date and time";

                DateTime PlannedStartTime = DateTime.Now.ToUniversalTime().AddHours(2);
                DateTime PlannedEndTime = PlannedStartTime.AddHours(1);

                String strPlannedStartTime = PlannedStartTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");
                String strPlannedEndTime = PlannedEndTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");

                strPlannedStartTime = strPlannedStartTime.Remove(14, 2);
                strPlannedStartTime = strPlannedStartTime.Insert(14, "05");

                strPlannedEndTime = strPlannedEndTime.Remove(14, 2);
                strPlannedEndTime = strPlannedEndTime.Insert(14, "05");

                String StripID = Strip.InsertBriefStrip(strTDAssetTypeID, strTDPaneID, strTDPersonID, strTDSlotNumber, Details, strPlannedStartTime, strPlannedEndTime);
                System.Threading.Thread.Sleep(5000);

                /* Run FACT Scheduled Task */

                RunFACT fact = new RunFACT();
                fact.RunFACTScheduledJob();
                System.Threading.Thread.Sleep(240000);

                /* Login to Webmail */

                WebmailLoginPage WebmailLoginPage = new WebmailLoginPage();
                WebmailLoginPage.LoginWebmailWithUserCredentials();
                System.Threading.Thread.Sleep(5000);

                /* Navigate to Calendar */

                WebmailMainPage WebmailMainPage = new WebmailMainPage();
                WebmailMainPage.NavigationMenuClick();
                WebmailMainPage.CalendarClick();
                System.Threading.Thread.Sleep(15000);
                WebmailCalendarPage WebmailCalendarPage = new WebmailCalendarPage();
                WebmailCalendarPage.BtnDay.Click();
                System.Threading.Thread.Sleep(5000);
                
                try
                {
                    Assert.IsTrue(WebmailCalendarPage.BriefStrip.Displayed);
                    PropertiesCollection.test.Log(Status.Pass, "Brief Strip displayed on Outlook");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Brief Strip not displayed on Outlook");
                }

                /* Delete Strip */

                Strip.Delete_Strip(StripID);
            }

            [TestMethod]
            public void BriefStripBeforeFACTWindow()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("Brief Strip Before the FACT Window");
                Strips Strip = new Strips();

                /*Create Brief Strip Before the FACT Window */

                String strTestCaseNo = "TC001";
                String strtblname = "automation_strips";
                String strTestType = "Regression";

                /* Get test data from MySQL */

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataStrip_details = connection.Select(strtblname, strTestCaseNo, strTestType);

                string strTDAssetTypeID = testdataStrip_details[5];
                string strTDPaneID = testdataStrip_details[11];
                string strTDType = testdataStrip_details[15];
                string strTDPersonID = testdataStrip_details[12];
                string strTDSlotNumber = testdataStrip_details[13];
                String Details = "AT Brief Strip created before the FACT Window";

                DateTime PlannedStartTime = DateTime.Now.ToUniversalTime().AddHours(2).AddDays(-3);
                DateTime PlannedEndTime = PlannedStartTime.AddHours(1);
                
                String strPlannedStartTime = PlannedStartTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");
                String strPlannedEndTime = PlannedEndTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");

                strPlannedStartTime = strPlannedStartTime.Remove(14, 2);
                strPlannedStartTime = strPlannedStartTime.Insert(14, "05");

                strPlannedEndTime = strPlannedEndTime.Remove(14, 2);
                strPlannedEndTime = strPlannedEndTime.Insert(14, "05");

                int Day = PlannedEndTime.Day;
                String Month = PlannedEndTime.ToString("MMM");
                int Year = PlannedEndTime.Year;

                String StripID = Strip.InsertBriefStrip(strTDAssetTypeID, strTDPaneID, strTDPersonID, strTDSlotNumber, Details, strPlannedStartTime, strPlannedEndTime);
                System.Threading.Thread.Sleep(5000);

                /* Run FACT Scheduled Task */

                RunFACT fact = new RunFACT();
                fact.RunFACTScheduledJob();
                System.Threading.Thread.Sleep(240000);

                /* Login to Webmail */

                WebmailLoginPage WebmailLoginPage = new WebmailLoginPage();
                WebmailLoginPage.LoginWebmailWithUserCredentials();
                System.Threading.Thread.Sleep(5000);

                /* Navigate to Calendar */

                WebmailMainPage WebmailMainPage = new WebmailMainPage();
                WebmailMainPage.NavigationMenuClick();
                WebmailMainPage.CalendarClick();
                System.Threading.Thread.Sleep(15000);
                WebmailCalendarPage WebmailCalendarPage = new WebmailCalendarPage();
                WebmailCalendarPage.BtnDay.Click();
                String CalendarValue = DateTime.Now.ToString("MMMM yyyy");
                System.Threading.Thread.Sleep(5000);
                WebmailCalendarPage.DateClick(Day, Month, Year, CalendarValue);
                System.Threading.Thread.Sleep(15000);
                try
                {
                    Assert.IsTrue(WebmailCalendarPage.BriefStrip.Displayed);
                    PropertiesCollection.test.Log(Status.Fail, "Brief Strip displayed on Outlook");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Pass, "Brief Strip not displayed on Outlook");
                }

                /* Delete Strip */

                Strip.Delete_Strip(StripID);
            }

            [TestMethod]
            public void BriefStripAfterFACTWindow()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("Brief Strip After the FACT Window");
                Strips Strip = new Strips();

                /*Create Brief Strip After the FACT Window*/

                String strTestCaseNo = "TC001";
                String strtblname = "automation_strips";
                String strTestType = "Regression";

                /* Get test data from MySQL */

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataStrip_details = connection.Select(strtblname, strTestCaseNo, strTestType);

                string strTDAssetTypeID = testdataStrip_details[5];
                string strTDPaneID = testdataStrip_details[11];
                string strTDType = testdataStrip_details[15];
                string strTDPersonID = testdataStrip_details[12];
                string strTDSlotNumber = testdataStrip_details[13];
                String Details = "AT Brief Strip created after the FACT Window";

                DateTime PlannedStartTime = DateTime.Now.ToUniversalTime().AddHours(2).AddDays(8);
                DateTime PlannedEndTime = PlannedStartTime.AddHours(1);
                
                String strPlannedStartTime = PlannedStartTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");
                String strPlannedEndTime = PlannedEndTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");

                strPlannedStartTime = strPlannedStartTime.Remove(14, 2);
                strPlannedStartTime = strPlannedStartTime.Insert(14, "05");

                strPlannedEndTime = strPlannedEndTime.Remove(14, 2);
                strPlannedEndTime = strPlannedEndTime.Insert(14, "05");

                int Day = PlannedStartTime.Day;
                String Month = PlannedStartTime.ToString("MMM");
                int Year = PlannedStartTime.Year;

                String StripID = Strip.InsertBriefStrip(strTDAssetTypeID, strTDPaneID, strTDPersonID, strTDSlotNumber, Details, strPlannedStartTime, strPlannedEndTime);
                System.Threading.Thread.Sleep(5000);

                /* Run FACT Scheduled Task */

                RunFACT fact = new RunFACT();
                fact.RunFACTScheduledJob();
                System.Threading.Thread.Sleep(240000);

                /* Login to Webmail */

                WebmailLoginPage WebmailLoginPage = new WebmailLoginPage();
                WebmailLoginPage.LoginWebmailWithUserCredentials();
                System.Threading.Thread.Sleep(5000);

                /* Navigate to Calendar */

                WebmailMainPage WebmailMainPage = new WebmailMainPage();
                WebmailMainPage.NavigationMenuClick();
                WebmailMainPage.CalendarClick();
                System.Threading.Thread.Sleep(15000);
                WebmailCalendarPage WebmailCalendarPage = new WebmailCalendarPage();
                WebmailCalendarPage.BtnDay.Click();
                String CalendarValue = DateTime.Now.ToString("MMMM yyyy");
                System.Threading.Thread.Sleep(5000);
                WebmailCalendarPage.DateClick(Day, Month, Year, CalendarValue);
                System.Threading.Thread.Sleep(15000);
                try
                {
                    Assert.IsTrue(WebmailCalendarPage.BriefStrip.Displayed);
                    PropertiesCollection.test.Log(Status.Fail, "Brief Strip displayed on Outlook");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Pass, "Brief Strip not displayed on Outlook");
                }

                /* Delete Strip */

                Strip.Delete_Strip(StripID);
            }

            [TestMethod]
            public void BriefStripOnFACTWindowTimeSpanFrom()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("Brief Strip On the FACT Window lying on the Time Span From");
                Strips Strip = new Strips();

                /*Create Brief Strip On the FACT Window lying on the Time Span From */

                String strTestCaseNo = "TC001";
                String strtblname = "automation_strips";
                String strTestType = "Regression";

                /* Get test data from MySQL */

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataStrip_details = connection.Select(strtblname, strTestCaseNo, strTestType);

                string strTDAssetTypeID = testdataStrip_details[5];
                string strTDPaneID = testdataStrip_details[11];
                string strTDType = testdataStrip_details[15];
                string strTDPersonID = testdataStrip_details[12];
                string strTDSlotNumber = testdataStrip_details[13];
                String Details = "AT Brief Strip created On the FACT Window lying on the Time Span From";

                DateTime PlannedStartTime = DateTime.Now.ToUniversalTime().AddDays(-2).AddHours(1);
                DateTime PlannedEndTime = PlannedStartTime.AddHours(2);
                
                String strPlannedStartTime = PlannedStartTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");
                String strPlannedEndTime = PlannedEndTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");

                strPlannedStartTime = strPlannedStartTime.Remove(14, 2);
                strPlannedStartTime = strPlannedStartTime.Insert(14, "05");

                strPlannedEndTime = strPlannedEndTime.Remove(14, 2);
                strPlannedEndTime = strPlannedEndTime.Insert(14, "05");

                int Day = PlannedEndTime.Day;
                String Month = PlannedEndTime.ToString("MMM");
                int Year = PlannedEndTime.Year;

                String StripID = Strip.InsertBriefStrip(strTDAssetTypeID, strTDPaneID, strTDPersonID, strTDSlotNumber, Details, strPlannedStartTime, strPlannedEndTime);
                System.Threading.Thread.Sleep(5000);

                /* Run FACT Scheduled Task */

                RunFACT fact = new RunFACT();
                fact.RunFACTScheduledJob();
                System.Threading.Thread.Sleep(240000);

                /* Login to Webmail */

                WebmailLoginPage WebmailLoginPage = new WebmailLoginPage();
                WebmailLoginPage.LoginWebmailWithUserCredentials();
                System.Threading.Thread.Sleep(5000);

                /* Navigate to Calendar */

                WebmailMainPage WebmailMainPage = new WebmailMainPage();
                WebmailMainPage.NavigationMenuClick();
                WebmailMainPage.CalendarClick();
                System.Threading.Thread.Sleep(15000);
                WebmailCalendarPage WebmailCalendarPage = new WebmailCalendarPage();
                WebmailCalendarPage.BtnDay.Click();
                String CalendarValue = DateTime.Now.ToString("MMMM yyyy");
                System.Threading.Thread.Sleep(5000);
                WebmailCalendarPage.DateClick(Day, Month, Year, CalendarValue);
                System.Threading.Thread.Sleep(15000);
                try
                {
                    Assert.IsTrue(WebmailCalendarPage.BriefStrip.Displayed);
                    PropertiesCollection.test.Log(Status.Pass, "Brief Strip displayed on Outlook");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Brief Strip not displayed on Outlook");
                }

                /* Delete Strip */

                Strip.Delete_Strip(StripID);
            }

            [TestMethod]
            public void BriefStripOnFACTWindowTimeSpanTo()
            {

                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("Brief Strip On the FACT Window lying on the Time Span To");
                Strips Strip = new Strips();

                /*Create Brief Strip On the FACT Window lying on the Time Span To */

                String strTestCaseNo = "TC001";
                String strtblname = "automation_strips";
                String strTestType = "Regression";

                /* Get test data from MySQL */

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataStrip_details = connection.Select(strtblname, strTestCaseNo, strTestType);

                string strTDAssetTypeID = testdataStrip_details[5];
                string strTDPaneID = testdataStrip_details[11];
                string strTDType = testdataStrip_details[15];
                string strTDPersonID = testdataStrip_details[12];
                string strTDSlotNumber = testdataStrip_details[13];                
                String Details = "AT Brief Strip created On the FACT Window lying on the Time Span To";

                DateTime PlannedStartTime = DateTime.Now.ToUniversalTime().AddDays(7).AddHours(-1);
                DateTime PlannedEndTime = PlannedStartTime.AddHours(2);
                
                String strPlannedStartTime = PlannedStartTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");
                String strPlannedEndTime = PlannedEndTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");

                strPlannedStartTime = strPlannedStartTime.Remove(14, 2);
                strPlannedStartTime = strPlannedStartTime.Insert(14, "05");

                strPlannedEndTime = strPlannedEndTime.Remove(14, 2);
                strPlannedEndTime = strPlannedEndTime.Insert(14, "05");

                int Day = PlannedStartTime.Day;
                String Month = PlannedStartTime.ToString("MMM");
                int Year = PlannedStartTime.Year;

                String StripID = Strip.InsertBriefStrip(strTDAssetTypeID, strTDPaneID, strTDPersonID, strTDSlotNumber, Details, strPlannedStartTime, strPlannedEndTime);
                System.Threading.Thread.Sleep(5000);

                /* Run FACT Scheduled Task */

                RunFACT fact = new RunFACT();
                fact.RunFACTScheduledJob();
                System.Threading.Thread.Sleep(240000);

                /* Login to Webmail */

                WebmailLoginPage WebmailLoginPage = new WebmailLoginPage();
                WebmailLoginPage.LoginWebmailWithUserCredentials();
                System.Threading.Thread.Sleep(5000);

                /* Navigate to Calendar */

                WebmailMainPage WebmailMainPage = new WebmailMainPage();
                WebmailMainPage.NavigationMenuClick();
                WebmailMainPage.CalendarClick();
                System.Threading.Thread.Sleep(15000);
                WebmailCalendarPage WebmailCalendarPage = new WebmailCalendarPage();
                WebmailCalendarPage.BtnDay.Click();
                String CalendarValue = DateTime.Now.ToString("MMMM yyyy");
                System.Threading.Thread.Sleep(5000);
                WebmailCalendarPage.DateClick(Day, Month, Year, CalendarValue);
                System.Threading.Thread.Sleep(15000);
                try
                {
                    Assert.IsTrue(WebmailCalendarPage.BriefStrip.Displayed);
                    PropertiesCollection.test.Log(Status.Pass, "Brief Strip displayed on Outlook");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Brief Strip not displayed on Outlook");
                }

                /* Delete Strip */

                Strip.Delete_Strip(StripID);
            }

            [TestMethod]
            public void StickyNoteAtCurrentDateTime()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("Sticky Note At Current Date and Time");
                Strips Strip = new Strips();

                /*Create Sticky Note at current date and time */

                String strTestCaseNo = "TC001";
                String strtblname = "automation_strips";
                String strTestType = "Regression";

                /* Get test data from MySQL */

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataStrip_details = connection.Select(strtblname, strTestCaseNo, strTestType);

                string strTDPaneID = testdataStrip_details[11];
                string strTDType = testdataStrip_details[15];
                string strTDPersonID = testdataStrip_details[12];
                string strTDSlotNumber = testdataStrip_details[13];
                String Details = "AT Sticky Note created at current date and time";

                DateTime PlannedStartTime = DateTime.Now.ToUniversalTime().AddHours(2);
                DateTime PlannedEndTime = PlannedStartTime.AddHours(1);               
            
                String strPlannedStartTime = PlannedStartTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");
                String strPlannedEndTime = PlannedEndTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");

                strPlannedStartTime = strPlannedStartTime.Remove(14, 2);
                strPlannedStartTime = strPlannedStartTime.Insert(14, "05");

                strPlannedEndTime = strPlannedEndTime.Remove(14, 2);
                strPlannedEndTime = strPlannedEndTime.Insert(14, "05");

                String StripID = Strip.InsertStickyNote(strTDPaneID, strTDPersonID, strTDSlotNumber, Details, strPlannedStartTime, strPlannedEndTime);
                System.Threading.Thread.Sleep(5000);

                /* Run FACT Scheduled Task */

                RunFACT fact = new RunFACT();
                fact.RunFACTScheduledJob();
                System.Threading.Thread.Sleep(120000);

                /* Login to Webmail */

                WebmailLoginPage WebmailLoginPage = new WebmailLoginPage();
                WebmailLoginPage.LoginWebmailWithUserCredentials();
                System.Threading.Thread.Sleep(5000);

                /* Navigate to Calendar */

                WebmailMainPage WebmailMainPage = new WebmailMainPage();
                WebmailMainPage.NavigationMenuClick();
                WebmailMainPage.CalendarClick();
                System.Threading.Thread.Sleep(15000);
                WebmailCalendarPage WebmailCalendarPage = new WebmailCalendarPage();
                WebmailCalendarPage.BtnDay.Click();
                System.Threading.Thread.Sleep(5000);
                
                try
                {
                    Assert.IsTrue(WebmailCalendarPage.StickyNoteStrip.Displayed);
                    PropertiesCollection.test.Log(Status.Pass, "Sticky Note displayed on Outlook");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Sticky Note not displayed on Outlook");
                }

                /* Delete Strip */

                Strip.Delete_Strip(StripID);
            }

            [TestMethod]
            public void StickyNoteBeforeFACTWindow()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("Sticky Note Before the FACT Window");
                Strips Strip = new Strips();

                /*Create Sticky Note Before the FACT Window */

                String strTestCaseNo = "TC001";
                String strtblname = "automation_strips";
                String strTestType = "Regression";

                /* Get test data from MySQL */

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataStrip_details = connection.Select(strtblname, strTestCaseNo, strTestType);

                string strTDPaneID = testdataStrip_details[11];
                string strTDType = testdataStrip_details[15];
                string strTDPersonID = testdataStrip_details[12];
                string strTDSlotNumber = testdataStrip_details[13];
                String Details = "AT Sticky Note created before the FACT Window";

                DateTime PlannedStartTime = DateTime.Now.ToUniversalTime().AddHours(2).AddDays(-3);
                DateTime PlannedEndTime = PlannedStartTime.AddHours(1);
             
                String strPlannedStartTime = PlannedStartTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");
                String strPlannedEndTime = PlannedEndTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");

                strPlannedStartTime = strPlannedStartTime.Remove(14, 2);
                strPlannedStartTime = strPlannedStartTime.Insert(14, "05");

                strPlannedEndTime = strPlannedEndTime.Remove(14, 2);
                strPlannedEndTime = strPlannedEndTime.Insert(14, "05");

                int Day = PlannedEndTime.Day;
                String Month = PlannedEndTime.ToString("MMM");
                int Year = PlannedEndTime.Year;

                String StripID = Strip.InsertStickyNote(strTDPaneID, strTDPersonID, strTDSlotNumber, Details, strPlannedStartTime, strPlannedEndTime);
                System.Threading.Thread.Sleep(5000);

                /* Run FACT Scheduled Task */

                RunFACT fact = new RunFACT();
                fact.RunFACTScheduledJob();
                System.Threading.Thread.Sleep(240000);

                /* Login to Webmail */

                WebmailLoginPage WebmailLoginPage = new WebmailLoginPage();
                WebmailLoginPage.LoginWebmailWithUserCredentials();
                System.Threading.Thread.Sleep(5000);

                /* Navigate to Calendar */

                WebmailMainPage WebmailMainPage = new WebmailMainPage();
                WebmailMainPage.NavigationMenuClick();
                WebmailMainPage.CalendarClick();
                System.Threading.Thread.Sleep(15000);
                WebmailCalendarPage WebmailCalendarPage = new WebmailCalendarPage();
                WebmailCalendarPage.BtnDay.Click();
                String CalendarValue = DateTime.Now.ToString("MMMM yyyy");
                System.Threading.Thread.Sleep(5000);
                WebmailCalendarPage.DateClick(Day, Month, Year, CalendarValue);
                System.Threading.Thread.Sleep(15000);
                try
                {
                    Assert.IsTrue(WebmailCalendarPage.StickyNoteStrip.Displayed);
                    PropertiesCollection.test.Log(Status.Fail, "Sticky Note displayed on Outlook");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Pass, "Sticky Note not displayed on Outlook");
                }

                /* Delete Strip */

                Strip.Delete_Strip(StripID);
            }

            [TestMethod]
            public void StickyNoteAfterFACTWindow()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("Sticky Note After the FACT Window");
                Strips Strip = new Strips();

                /*Create Sticky Note After the FACT Window*/

                String strTestCaseNo = "TC001";
                String strtblname = "automation_strips";
                String strTestType = "Regression";

                /* Get test data from MySQL */

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataStrip_details = connection.Select(strtblname, strTestCaseNo, strTestType);

                string strTDPaneID = testdataStrip_details[11];
                string strTDType = testdataStrip_details[15];
                string strTDPersonID = testdataStrip_details[12];
                string strTDSlotNumber = testdataStrip_details[13];
                String Details = "AT Sticky Note created after the FACT Window";

                DateTime PlannedStartTime = DateTime.Now.ToUniversalTime().AddHours(2).AddDays(8);
                DateTime PlannedEndTime = PlannedStartTime.AddHours(1);
                
                String strPlannedStartTime = PlannedStartTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");
                String strPlannedEndTime = PlannedEndTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");

                strPlannedStartTime = strPlannedStartTime.Remove(14, 2);
                strPlannedStartTime = strPlannedStartTime.Insert(14, "05");

                strPlannedEndTime = strPlannedEndTime.Remove(14, 2);
                strPlannedEndTime = strPlannedEndTime.Insert(14, "05");

                int Day = PlannedStartTime.Day;
                String Month = PlannedStartTime.ToString("MMM");
                int Year = PlannedStartTime.Year;

                String StripID = Strip.InsertStickyNote(strTDPaneID, strTDPersonID, strTDSlotNumber, Details, strPlannedStartTime, strPlannedEndTime);
                System.Threading.Thread.Sleep(5000);

                /* Run FACT Scheduled Task */

                RunFACT fact = new RunFACT();
                fact.RunFACTScheduledJob();
                System.Threading.Thread.Sleep(240000);

                /* Login to Webmail */

                WebmailLoginPage WebmailLoginPage = new WebmailLoginPage();
                WebmailLoginPage.LoginWebmailWithUserCredentials();
                System.Threading.Thread.Sleep(5000);

                /* Navigate to Calendar */

                WebmailMainPage WebmailMainPage = new WebmailMainPage();
                WebmailMainPage.NavigationMenuClick();
                WebmailMainPage.CalendarClick();
                System.Threading.Thread.Sleep(15000);
                WebmailCalendarPage WebmailCalendarPage = new WebmailCalendarPage();
                WebmailCalendarPage.BtnDay.Click();
                String CalendarValue = DateTime.Now.ToString("MMMM yyyy");
                System.Threading.Thread.Sleep(5000);
                WebmailCalendarPage.DateClick(Day, Month, Year, CalendarValue);
                System.Threading.Thread.Sleep(15000);
                try
                {
                    Assert.IsTrue(WebmailCalendarPage.StickyNoteStrip.Displayed);
                    PropertiesCollection.test.Log(Status.Fail, "Sticky Note displayed on Outlook");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Pass, "Sticky Note not displayed on Outlook");
                }

                /* Delete Strip */

                Strip.Delete_Strip(StripID);
            }

            [TestMethod]
            public void StickyNoteOnFACTWindowTimeSpanFrom()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("Sticky Note On the FACT Window lying on the Time Span From");
                Strips Strip = new Strips();

                /*Create Sticky Note On the FACT Window lying on the Time Span From */

                String strTestCaseNo = "TC001";
                String strtblname = "automation_strips";
                String strTestType = "Regression";

                /* Get test data from MySQL */

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataStrip_details = connection.Select(strtblname, strTestCaseNo, strTestType);

                string strTDPaneID = testdataStrip_details[11];
                string strTDType = testdataStrip_details[15];
                string strTDPersonID = testdataStrip_details[12];
                string strTDSlotNumber = testdataStrip_details[13];
                String Details = "AT Sticky Note created On the FACT Window lying on the Time Span From";

                DateTime PlannedStartTime = DateTime.Now.ToUniversalTime().AddHours(-1).AddDays(-2);
                DateTime PlannedEndTime = PlannedStartTime.AddHours(2);

                String strPlannedStartTime = PlannedStartTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");
                String strPlannedEndTime = PlannedEndTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");

                strPlannedStartTime = strPlannedStartTime.Remove(14, 2);
                strPlannedStartTime = strPlannedStartTime.Insert(14, "05");

                strPlannedEndTime = strPlannedEndTime.Remove(14, 2);
                strPlannedEndTime = strPlannedEndTime.Insert(14, "05");

                int Day = PlannedEndTime.Day;
                String Month = PlannedEndTime.ToString("MMM");
                int Year = PlannedEndTime.Year;

                String StripID = Strip.InsertStickyNote(strTDPaneID, strTDPersonID, strTDSlotNumber, Details, strPlannedStartTime, strPlannedEndTime);
                System.Threading.Thread.Sleep(5000);

                /* Run FACT Scheduled Task */

                RunFACT fact = new RunFACT();
                fact.RunFACTScheduledJob();
                System.Threading.Thread.Sleep(240000);

                /* Login to Webmail */

                WebmailLoginPage WebmailLoginPage = new WebmailLoginPage();
                WebmailLoginPage.LoginWebmailWithUserCredentials();
                System.Threading.Thread.Sleep(5000);

                /* Navigate to Calendar */

                WebmailMainPage WebmailMainPage = new WebmailMainPage();
                WebmailMainPage.NavigationMenuClick();
                WebmailMainPage.CalendarClick();
                System.Threading.Thread.Sleep(15000);
                WebmailCalendarPage WebmailCalendarPage = new WebmailCalendarPage();
                WebmailCalendarPage.BtnDay.Click();
                String CalendarValue = DateTime.Now.ToString("MMMM yyyy");
                System.Threading.Thread.Sleep(5000);
                WebmailCalendarPage.DateClick(Day, Month, Year, CalendarValue);
                System.Threading.Thread.Sleep(15000);
                try
                {
                    Assert.IsTrue(WebmailCalendarPage.StickyNoteStrip.Displayed);
                    PropertiesCollection.test.Log(Status.Pass, "Sticky Note displayed on Outlook");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Sticky Note not displayed on Outlook");
                }

                /* Delete Strip */

                Strip.Delete_Strip(StripID);
            }

            [TestMethod]
            public void StickyNoteOnFACTWindowTimeSpanTo()
            {

                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("Sticky Note On the FACT Window lying on the Time Span To");
                Strips Strip = new Strips();

                /*Create Sticky Note On the FACT Window lying on the Time Span To */

                String strTestCaseNo = "TC001";
                String strtblname = "automation_strips";
                String strTestType = "Regression";

                /* Get test data from MySQL */

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataStrip_details = connection.Select(strtblname, strTestCaseNo, strTestType);

                string strTDPaneID = testdataStrip_details[11];
                string strTDType = testdataStrip_details[15];
                string strTDPersonID = testdataStrip_details[12];
                string strTDSlotNumber = testdataStrip_details[13];
                String Details = "AT Sticky Note created On the FACT Window lying on the Time Span To";

                DateTime PlannedStartTime = DateTime.Now.ToUniversalTime().AddHours(1).AddDays(7);
                DateTime PlannedEndTime = PlannedStartTime.AddHours(2);
                
                String strPlannedStartTime = PlannedStartTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");
                String strPlannedEndTime = PlannedEndTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");

                strPlannedStartTime = strPlannedStartTime.Remove(14, 2);
                strPlannedStartTime = strPlannedStartTime.Insert(14, "05");

                strPlannedEndTime = strPlannedEndTime.Remove(14, 2);
                strPlannedEndTime = strPlannedEndTime.Insert(14, "05");

                int Day = PlannedStartTime.Day;
                String Month = PlannedStartTime.ToString("MMM");
                int Year = PlannedStartTime.Year;

                String StripID = Strip.InsertStickyNote(strTDPaneID, strTDPersonID, strTDSlotNumber, Details, strPlannedStartTime, strPlannedEndTime);
                System.Threading.Thread.Sleep(5000);

                /* Run FACT Scheduled Task */

                RunFACT fact = new RunFACT();
                fact.RunFACTScheduledJob();
                System.Threading.Thread.Sleep(240000);

                /* Login to Webmail */

                WebmailLoginPage WebmailLoginPage = new WebmailLoginPage();
                WebmailLoginPage.LoginWebmailWithUserCredentials();
                System.Threading.Thread.Sleep(5000);

                /* Navigate to Calendar */

                WebmailMainPage WebmailMainPage = new WebmailMainPage();
                WebmailMainPage.NavigationMenuClick();
                WebmailMainPage.CalendarClick();
                System.Threading.Thread.Sleep(15000);
                WebmailCalendarPage WebmailCalendarPage = new WebmailCalendarPage();
                WebmailCalendarPage.BtnDay.Click();
                String CalendarValue = DateTime.Now.ToString("MMMM yyyy");
                System.Threading.Thread.Sleep(5000);
                WebmailCalendarPage.DateClick(Day, Month, Year, CalendarValue);
                System.Threading.Thread.Sleep(15000);
                try
                {
                    Assert.IsTrue(WebmailCalendarPage.StickyNoteStrip.Displayed);
                    PropertiesCollection.test.Log(Status.Pass, "Sticky Note displayed on Outlook");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Sticky Note not displayed on Outlook");
                }

                /* Delete Strip */

                Strip.Delete_Strip(StripID);
            }

            [TestMethod]
            public void FormationGroupAtCurrentDateTime()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("Formation Group At Current Date and Time");
                Strips Strip = new Strips();

                /*Create Formation Group at current date and time */

                String strTestCaseNo = "TC001";
                String strtblname = "automation_strips";
                String strTestType = "Regression";

                /* Get test data from MySQL */

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataStrip_details = connection.Select(strtblname, strTestCaseNo, strTestType);

                string strTDAssetTypeID = testdataStrip_details[5];
                string strTDPaneID = testdataStrip_details[11];
                string strTDPersonID1 = testdataStrip_details[12];
                string strTDSlotNumber1 = testdataStrip_details[13];
                string strTDWeatherStateID = testdataStrip_details[16];
                string strTDPersonID2 = testdataStrip_details[17];
                string strTDSlotNumber2 = testdataStrip_details[18];                
                string strTDGroupHeaderAssetTypeID = testdataStrip_details[19];
                
                String Details = "AT Formation Group created at current date and time";

                DateTime PlannedStartTime = DateTime.Now.ToUniversalTime().AddHours(2);
                DateTime PlannedEndTime = PlannedStartTime.AddHours(1);
                
                String strPlannedStartTime = PlannedStartTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");
                String strPlannedEndTime = PlannedEndTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");

                strPlannedStartTime = strPlannedStartTime.Remove(14, 2);
                strPlannedStartTime = strPlannedStartTime.Insert(14, "05");

                strPlannedEndTime = strPlannedEndTime.Remove(14, 2);
                strPlannedEndTime = strPlannedEndTime.Insert(14, "05");

                Strip.InsertFormationGroup(strTDGroupHeaderAssetTypeID, strTDAssetTypeID, strTDPaneID, strTDPersonID1, strTDSlotNumber1, strTDPersonID2, strTDSlotNumber2, strTDWeatherStateID, Details, strPlannedStartTime, strPlannedEndTime, out string StripIDHeader, out string StripID1, out string StripID2);

                System.Threading.Thread.Sleep(5000);

                /* Run FACT Scheduled Task */

                RunFACT fact = new RunFACT();
                fact.RunFACTScheduledJob();
                System.Threading.Thread.Sleep(240000);

                /* Login to Webmail */

                WebmailLoginPage WebmailLoginPage = new WebmailLoginPage();
                WebmailLoginPage.LoginWebmailWithUserCredentials();
                System.Threading.Thread.Sleep(5000);

                /* Navigate to Calendar */

                WebmailMainPage WebmailMainPage = new WebmailMainPage();
                WebmailMainPage.NavigationMenuClick();
                WebmailMainPage.CalendarClick();
                System.Threading.Thread.Sleep(15000);
                WebmailCalendarPage WebmailCalendarPage = new WebmailCalendarPage();
                WebmailCalendarPage.BtnDay.Click();
                System.Threading.Thread.Sleep(5000);
               
                try
                {
                    Assert.IsTrue(WebmailCalendarPage.FormationStrip.Displayed);
                    PropertiesCollection.test.Log(Status.Pass, "Formation Group displayed on Outlook");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Formation Group Strip not displayed on Outlook");
                }

                /* Delete Strip */

                Strip.Delete_Strip(StripIDHeader);
                Strip.Delete_Strip(StripID1);
                Strip.Delete_Strip(StripID2);
            }

            [TestMethod]
            public void FormationGroupBeforeFACTWindow()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("Formation Group Before the FACT Window");
                Strips Strip = new Strips();

                /*Create Formation Group Before the FACT Window */

                String strTestCaseNo = "TC001";
                String strtblname = "automation_strips";
                String strTestType = "Regression";

                /* Get test data from MySQL */

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataStrip_details = connection.Select(strtblname, strTestCaseNo, strTestType);

                string strTDAssetTypeID = testdataStrip_details[5];
                string strTDPaneID = testdataStrip_details[11];
                string strTDPersonID1 = testdataStrip_details[12];
                string strTDSlotNumber1 = testdataStrip_details[13];
                string strTDWeatherStateID = testdataStrip_details[16];
                string strTDPersonID2 = testdataStrip_details[17];
                string strTDSlotNumber2 = testdataStrip_details[18];
                string strTDGroupHeaderAssetTypeID = testdataStrip_details[19];

                String Details = "AT Formation Group created before the FACT Window";

                DateTime PlannedStartTime = DateTime.Now.ToUniversalTime().AddDays(-1);
                DateTime PlannedEndTime = PlannedStartTime.AddHours(1);
                
                String strPlannedStartTime = PlannedStartTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");
                String strPlannedEndTime = PlannedEndTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");

                strPlannedStartTime = strPlannedStartTime.Remove(14, 2);
                strPlannedStartTime = strPlannedStartTime.Insert(14, "05");

                strPlannedEndTime = strPlannedEndTime.Remove(14, 2);
                strPlannedEndTime = strPlannedEndTime.Insert(14, "05");

                int Day = PlannedStartTime.Day;
                String Month = PlannedStartTime.ToString("MMM");
                int Year = PlannedStartTime.Year;

                Strip.InsertFormationGroup(strTDGroupHeaderAssetTypeID, strTDAssetTypeID, strTDPaneID, strTDPersonID1, strTDSlotNumber1, strTDPersonID2, strTDSlotNumber2, strTDWeatherStateID, Details, strPlannedStartTime, strPlannedEndTime, out string StripIDHeader, out string StripID1, out string StripID2);

                System.Threading.Thread.Sleep(5000);

                /* Run FACT Scheduled Task */

                RunFACT fact = new RunFACT();
                fact.RunFACTScheduledJob();
                System.Threading.Thread.Sleep(240000);

                /* Login to Webmail */

                WebmailLoginPage WebmailLoginPage = new WebmailLoginPage();
                WebmailLoginPage.LoginWebmailWithUserCredentials();
                System.Threading.Thread.Sleep(5000);

                /* Navigate to Calendar */

                WebmailMainPage WebmailMainPage = new WebmailMainPage();
                WebmailMainPage.NavigationMenuClick();
                WebmailMainPage.CalendarClick();
                System.Threading.Thread.Sleep(15000);
                WebmailCalendarPage WebmailCalendarPage = new WebmailCalendarPage();
                WebmailCalendarPage.BtnDay.Click();
                String CalendarValue = DateTime.Now.ToString("MMMM yyyy");
                System.Threading.Thread.Sleep(5000);
                WebmailCalendarPage.DateClick(Day, Month, Year, CalendarValue);
                System.Threading.Thread.Sleep(15000);
                try
                {
                    Assert.IsTrue(WebmailCalendarPage.FormationStrip.Displayed);
                    PropertiesCollection.test.Log(Status.Fail, "Formation Group displayed on Outlook");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Pass, "Formation Group Strip not displayed on Outlook");
                }

                /* Delete Strip */

                Strip.Delete_Strip(StripIDHeader);
                Strip.Delete_Strip(StripID1);
                Strip.Delete_Strip(StripID2);
            }

            [TestMethod]
            public void FormationGroupAfterFACTWindow()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("Formation Group After the FACT Window");
                Strips Strip = new Strips();

                /*Create Formation Group After the FACT Window*/

                String strTestCaseNo = "TC001";
                String strtblname = "automation_strips";
                String strTestType = "Regression";

                /* Get test data from MySQL */

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataStrip_details = connection.Select(strtblname, strTestCaseNo, strTestType);

                string strTDAssetTypeID = testdataStrip_details[5];
                string strTDPaneID = testdataStrip_details[11];
                string strTDPersonID1 = testdataStrip_details[12];
                string strTDSlotNumber1 = testdataStrip_details[13];
                string strTDWeatherStateID = testdataStrip_details[16];
                string strTDPersonID2 = testdataStrip_details[17];
                string strTDSlotNumber2 = testdataStrip_details[18];
                string strTDGroupHeaderAssetTypeID = testdataStrip_details[19];

                String Details = "AT Formation Group created after the FACT Window";

                DateTime PlannedStartTime = DateTime.Now.ToUniversalTime().AddHours(2).AddDays(8);
                DateTime PlannedEndTime = PlannedStartTime.AddHours(1);
                
                String strPlannedStartTime = PlannedStartTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");
                String strPlannedEndTime = PlannedEndTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");

                strPlannedStartTime = strPlannedStartTime.Remove(14, 2);
                strPlannedStartTime = strPlannedStartTime.Insert(14, "05");

                strPlannedEndTime = strPlannedEndTime.Remove(14, 2);
                strPlannedEndTime = strPlannedEndTime.Insert(14, "05");

                int Day = PlannedStartTime.Day;
                String Month = PlannedStartTime.ToString("MMM");
                int Year = PlannedStartTime.Year;

                Strip.InsertFormationGroup(strTDGroupHeaderAssetTypeID, strTDAssetTypeID, strTDPaneID, strTDPersonID1, strTDSlotNumber1, strTDPersonID2, strTDSlotNumber2, strTDWeatherStateID, Details, strPlannedStartTime, strPlannedEndTime, out string StripIDHeader, out string StripID1, out string StripID2);

                System.Threading.Thread.Sleep(5000);

                /* Run FACT Scheduled Task */

                RunFACT fact = new RunFACT();
                fact.RunFACTScheduledJob();
                System.Threading.Thread.Sleep(240000);

                /* Login to Webmail */

                WebmailLoginPage WebmailLoginPage = new WebmailLoginPage();
                WebmailLoginPage.LoginWebmailWithUserCredentials();
                System.Threading.Thread.Sleep(5000);

                /* Navigate to Calendar */

                WebmailMainPage WebmailMainPage = new WebmailMainPage();
                WebmailMainPage.NavigationMenuClick();
                WebmailMainPage.CalendarClick();
                System.Threading.Thread.Sleep(15000);
                WebmailCalendarPage WebmailCalendarPage = new WebmailCalendarPage();
                WebmailCalendarPage.BtnDay.Click();
                String CalendarValue = DateTime.Now.ToString("MMMM yyyy");
                System.Threading.Thread.Sleep(5000);
                WebmailCalendarPage.DateClick(Day, Month, Year, CalendarValue);
                System.Threading.Thread.Sleep(15000);
                try
                {
                    Assert.IsTrue(WebmailCalendarPage.FormationStrip.Displayed);
                    PropertiesCollection.test.Log(Status.Fail, "Formation Group displayed on Outlook");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Pass, "Formation Group Strip not displayed on Outlook");
                }

                /* Delete Strip */

                Strip.Delete_Strip(StripIDHeader);
                Strip.Delete_Strip(StripID1);
                Strip.Delete_Strip(StripID2);
            }

            [TestMethod]
            public void FormationGroupOnFACTWindowTimeSpanFrom()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("Formation Group On the FACT Window lying on the Time Span From");
                Strips Strip = new Strips();

                /*Create Formation Group On the FACT Window lying on the Time Span From */

                String strTestCaseNo = "TC001";
                String strtblname = "automation_strips";
                String strTestType = "Regression";

                /* Get test data from MySQL */

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataStrip_details = connection.Select(strtblname, strTestCaseNo, strTestType);

                string strTDAssetTypeID = testdataStrip_details[5];
                string strTDPaneID = testdataStrip_details[11];
                string strTDPersonID1 = testdataStrip_details[12];
                string strTDSlotNumber1 = testdataStrip_details[13];
                string strTDWeatherStateID = testdataStrip_details[16];
                string strTDPersonID2 = testdataStrip_details[17];
                string strTDSlotNumber2 = testdataStrip_details[18];
                string strTDGroupHeaderAssetTypeID = testdataStrip_details[19];

                String Details = "AT Formation Group created On the FACT Window lying on the Time Span From";

                DateTime PlannedStartTime = DateTime.Now.ToUniversalTime().AddDays(-2).AddHours(1);
                DateTime PlannedEndTime = PlannedStartTime.AddHours(2);
                
                String strPlannedStartTime = PlannedStartTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");
                String strPlannedEndTime = PlannedEndTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");

                strPlannedStartTime = strPlannedStartTime.Remove(14, 2);
                strPlannedStartTime = strPlannedStartTime.Insert(14, "05");

                strPlannedEndTime = strPlannedEndTime.Remove(14, 2);
                strPlannedEndTime = strPlannedEndTime.Insert(14, "05");

                int Day = PlannedEndTime.Day;
                String Month = PlannedEndTime.ToString("MMM");
                int Year = PlannedEndTime.Year;

                Strip.InsertFormationGroup(strTDGroupHeaderAssetTypeID, strTDAssetTypeID, strTDPaneID, strTDPersonID1, strTDSlotNumber1, strTDPersonID2, strTDSlotNumber2, strTDWeatherStateID, Details, strPlannedStartTime, strPlannedEndTime, out string StripIDHeader, out string StripID1, out string StripID2);

                System.Threading.Thread.Sleep(5000);

                /* Run FACT Scheduled Task */

                RunFACT fact = new RunFACT();
                fact.RunFACTScheduledJob();
                System.Threading.Thread.Sleep(240000);

                /* Login to Webmail */

                WebmailLoginPage WebmailLoginPage = new WebmailLoginPage();
                WebmailLoginPage.LoginWebmailWithUserCredentials();
                System.Threading.Thread.Sleep(5000);

                /* Navigate to Calendar */

                WebmailMainPage WebmailMainPage = new WebmailMainPage();
                WebmailMainPage.NavigationMenuClick();
                WebmailMainPage.CalendarClick();
                System.Threading.Thread.Sleep(15000);
                WebmailCalendarPage WebmailCalendarPage = new WebmailCalendarPage();
                WebmailCalendarPage.BtnDay.Click();
                String CalendarValue = DateTime.Now.ToString("MMMM yyyy");
                System.Threading.Thread.Sleep(5000);
                WebmailCalendarPage.DateClick(Day, Month, Year, CalendarValue);
                System.Threading.Thread.Sleep(15000);
                try
                {
                    Assert.IsTrue(WebmailCalendarPage.FormationStrip.Displayed);
                    PropertiesCollection.test.Log(Status.Pass, "Formation Group displayed on Outlook");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Formation Group Strip not displayed on Outlook");
                }

                /* Delete Strip */

                Strip.Delete_Strip(StripIDHeader);
                Strip.Delete_Strip(StripID1);
                Strip.Delete_Strip(StripID2);
            }

            [TestMethod]
            public void FormationGroupOnFACTWindowTimeSpanTo()
            {

                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("Formation Group On the FACT Window lying on the Time Span To");
                Strips Strip = new Strips();

                /*Create Formation Group On the FACT Window lying on the Time Span To */

                String strTestCaseNo = "TC001";
                String strtblname = "automation_strips";
                String strTestType = "Regression";

                /* Get test data from MySQL */

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataStrip_details = connection.Select(strtblname, strTestCaseNo, strTestType);

                string strTDAssetTypeID = testdataStrip_details[5];
                string strTDPaneID = testdataStrip_details[11];
                string strTDPersonID1 = testdataStrip_details[12];
                string strTDSlotNumber1 = testdataStrip_details[13];
                string strTDWeatherStateID = testdataStrip_details[16];
                string strTDPersonID2 = testdataStrip_details[17];
                string strTDSlotNumber2 = testdataStrip_details[18];
                string strTDGroupHeaderAssetTypeID = testdataStrip_details[19];
                String Details = "AT Formation Group created On the FACT Window lying on the Time Span To";

                DateTime PlannedStartTime = DateTime.Now.ToUniversalTime().AddDays(7).AddHours(-1);
                DateTime PlannedEndTime = PlannedStartTime.AddHours(2);
                
                String strPlannedStartTime = PlannedStartTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");
                String strPlannedEndTime = PlannedEndTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");

                strPlannedStartTime = strPlannedStartTime.Remove(14, 2);
                strPlannedStartTime = strPlannedStartTime.Insert(14, "05");

                strPlannedEndTime = strPlannedEndTime.Remove(14, 2);
                strPlannedEndTime = strPlannedEndTime.Insert(14, "05");

                int Day = PlannedStartTime.Day;
                String Month = PlannedStartTime.ToString("MMM");
                int Year = PlannedStartTime.Year;

                Strip.InsertFormationGroup(strTDGroupHeaderAssetTypeID, strTDAssetTypeID, strTDPaneID, strTDPersonID1, strTDSlotNumber1, strTDPersonID2, strTDSlotNumber2, strTDWeatherStateID, Details, strPlannedStartTime, strPlannedEndTime, out string StripIDHeader, out string StripID1, out string StripID2);

                System.Threading.Thread.Sleep(5000);

                /* Run FACT Scheduled Task */

                RunFACT fact = new RunFACT();
                fact.RunFACTScheduledJob();
                System.Threading.Thread.Sleep(240000);

                /* Login to Webmail */

                WebmailLoginPage WebmailLoginPage = new WebmailLoginPage();
                WebmailLoginPage.LoginWebmailWithUserCredentials();
                System.Threading.Thread.Sleep(5000);

                /* Navigate to Calendar */

                WebmailMainPage WebmailMainPage = new WebmailMainPage();
                WebmailMainPage.NavigationMenuClick();
                WebmailMainPage.CalendarClick();
                System.Threading.Thread.Sleep(15000);
                WebmailCalendarPage WebmailCalendarPage = new WebmailCalendarPage();
                WebmailCalendarPage.BtnDay.Click();
                String CalendarValue = DateTime.Now.ToString("MMMM yyyy");
                System.Threading.Thread.Sleep(5000);
                WebmailCalendarPage.DateClick(Day, Month, Year, CalendarValue);
                System.Threading.Thread.Sleep(15000);
                try
                {
                    Assert.IsTrue(WebmailCalendarPage.FormationStrip.Displayed);
                    PropertiesCollection.test.Log(Status.Pass, "Formation Group displayed on Outlook");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Formation Group Strip not displayed on Outlook");
                }

                /* Delete Strip */

                Strip.Delete_Strip(StripIDHeader);
                Strip.Delete_Strip(StripID1);
                Strip.Delete_Strip(StripID2);
            }

            [TestMethod]
            public void SortieGroupAtCurrentDateTime()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("Sortie Group At Current Date and Time");
                Strips Strip = new Strips();

                /*Create Sortie Group at current date and time */

                String strTestCaseNo = "TC001";
                String strtblname = "automation_strips";
                String strTestType = "Regression";

                /* Get test data from MySQL */

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataStrip_details = connection.Select(strtblname, strTestCaseNo, strTestType);

                string strTDAssetTypeID = testdataStrip_details[5];
                string strTDPaneID = testdataStrip_details[11];
                string strTDPersonID1 = testdataStrip_details[12];
                string strTDSlotNumber1 = testdataStrip_details[13];
                string strTDWeatherStateID = testdataStrip_details[16];
                string strTDPersonID2 = testdataStrip_details[17];
                string strTDSlotNumber2 = testdataStrip_details[18];
                string strTDGroupHeaderAssetTypeID = testdataStrip_details[19];

                String Details = "AT Sortie Group created at current date and time";

                DateTime PlannedStartTime = DateTime.Now.ToUniversalTime().AddHours(2);
                DateTime PlannedEndTime = PlannedStartTime.AddHours(1);
                
                String strPlannedStartTime = PlannedStartTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");
                String strPlannedEndTime = PlannedEndTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");

                strPlannedStartTime = strPlannedStartTime.Remove(14, 2);
                strPlannedStartTime = strPlannedStartTime.Insert(14, "05");

                strPlannedEndTime = strPlannedEndTime.Remove(14, 2);
                strPlannedEndTime = strPlannedEndTime.Insert(14, "05");


                Strip.InsertSortieGroup(strTDGroupHeaderAssetTypeID, strTDAssetTypeID, strTDPaneID, strTDPersonID1, strTDSlotNumber1, strTDPersonID2, strTDSlotNumber2, strTDWeatherStateID, Details, strPlannedStartTime, strPlannedEndTime, out string StripIDHeader, out string StripID1, out string StripID2);

                System.Threading.Thread.Sleep(5000);

                /* Run FACT Scheduled Task */

                RunFACT fact = new RunFACT();
                fact.RunFACTScheduledJob();
                System.Threading.Thread.Sleep(240000);

                /* Login to Webmail */

                WebmailLoginPage WebmailLoginPage = new WebmailLoginPage();
                WebmailLoginPage.LoginWebmailWithUserCredentials();
                System.Threading.Thread.Sleep(5000);

                /* Navigate to Calendar */

                WebmailMainPage WebmailMainPage = new WebmailMainPage();
                WebmailMainPage.NavigationMenuClick();
                WebmailMainPage.CalendarClick();
                System.Threading.Thread.Sleep(15000);
                WebmailCalendarPage WebmailCalendarPage = new WebmailCalendarPage();
                WebmailCalendarPage.BtnDay.Click();
                System.Threading.Thread.Sleep(5000);
                
                try
                {
                    Assert.IsTrue(WebmailCalendarPage.SortieStrip.Displayed);
                    PropertiesCollection.test.Log(Status.Pass, "Sortie Group displayed on Outlook");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Sortie Group not displayed on Outlook");
                }

                /* Delete Strip */

                Strip.Delete_Strip(StripIDHeader);
                Strip.Delete_Strip(StripID1);
                Strip.Delete_Strip(StripID2);
            }

            [TestMethod]
            public void SortieGroupBeforeFACTWindow()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("Sortie Group Before the FACT Window");
                Strips Strip = new Strips();

                /*Create Sortie Group Before the FACT Window */

                String strTestCaseNo = "TC001";
                String strtblname = "automation_strips";
                String strTestType = "Regression";

                /* Get test data from MySQL */

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataStrip_details = connection.Select(strtblname, strTestCaseNo, strTestType);

                string strTDAssetTypeID = testdataStrip_details[5];
                string strTDPaneID = testdataStrip_details[11];
                string strTDPersonID1 = testdataStrip_details[12];
                string strTDSlotNumber1 = testdataStrip_details[13];
                string strTDWeatherStateID = testdataStrip_details[16];
                string strTDPersonID2 = testdataStrip_details[17];
                string strTDSlotNumber2 = testdataStrip_details[18];
                string strTDGroupHeaderAssetTypeID = testdataStrip_details[19];

                String Details = "AT Sortie Group created before the FACT Window";

                DateTime PlannedStartTime = DateTime.Now.ToUniversalTime().AddDays(-1);
                DateTime PlannedEndTime = PlannedStartTime.AddHours(1);
                

                String strPlannedStartTime = PlannedStartTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");
                String strPlannedEndTime = PlannedEndTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");

                strPlannedStartTime = strPlannedStartTime.Remove(14, 2);
                strPlannedStartTime = strPlannedStartTime.Insert(14, "05");

                strPlannedEndTime = strPlannedEndTime.Remove(14, 2);
                strPlannedEndTime = strPlannedEndTime.Insert(14, "05");

                int Day = PlannedEndTime.Day;
                String Month = PlannedEndTime.ToString("MMM");
                int Year = PlannedEndTime.Year;

                Strip.InsertSortieGroup(strTDGroupHeaderAssetTypeID, strTDAssetTypeID, strTDPaneID, strTDPersonID1, strTDSlotNumber1, strTDPersonID2, strTDSlotNumber2, strTDWeatherStateID, Details, strPlannedStartTime, strPlannedEndTime, out string StripIDHeader, out string StripID1, out string StripID2);

                System.Threading.Thread.Sleep(5000);

                /* Run FACT Scheduled Task */

                RunFACT fact = new RunFACT();
                fact.RunFACTScheduledJob();
                System.Threading.Thread.Sleep(240000);

                /* Login to Webmail */

                WebmailLoginPage WebmailLoginPage = new WebmailLoginPage();
                WebmailLoginPage.LoginWebmailWithUserCredentials();
                System.Threading.Thread.Sleep(5000);

                /* Navigate to Calendar */

                WebmailMainPage WebmailMainPage = new WebmailMainPage();
                WebmailMainPage.NavigationMenuClick();
                WebmailMainPage.CalendarClick();
                System.Threading.Thread.Sleep(15000);
                WebmailCalendarPage WebmailCalendarPage = new WebmailCalendarPage();
                WebmailCalendarPage.BtnDay.Click();
                String CalendarValue = DateTime.Now.ToString("MMMM yyyy");
                System.Threading.Thread.Sleep(5000);
                WebmailCalendarPage.DateClick(Day, Month, Year, CalendarValue);
                System.Threading.Thread.Sleep(15000);
                try
                {
                    Assert.IsTrue(WebmailCalendarPage.SortieStrip.Displayed);
                    PropertiesCollection.test.Log(Status.Fail, "Sortie Group displayed on Outlook");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Pass, "Sortie Group not displayed on Outlook");
                }

                /* Delete Strip */

                Strip.Delete_Strip(StripIDHeader);
                Strip.Delete_Strip(StripID1);
                Strip.Delete_Strip(StripID2);
            }

            [TestMethod]
            public void SortieGroupAfterFACTWindow()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("Sortie Group After the FACT Window");
                Strips Strip = new Strips();

                /*Create Sortie Group After the FACT Window*/

                String strTestCaseNo = "TC001";
                String strtblname = "automation_strips";
                String strTestType = "Regression";

                /* Get test data from MySQL */

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataStrip_details = connection.Select(strtblname, strTestCaseNo, strTestType);

                string strTDAssetTypeID = testdataStrip_details[5];
                string strTDPaneID = testdataStrip_details[11];
                string strTDPersonID1 = testdataStrip_details[12];
                string strTDSlotNumber1 = testdataStrip_details[13];
                string strTDWeatherStateID = testdataStrip_details[16];
                string strTDPersonID2 = testdataStrip_details[17];
                string strTDSlotNumber2 = testdataStrip_details[18];
                string strTDGroupHeaderAssetTypeID = testdataStrip_details[19];

                String Details = "AT Sortie Group created after the FACT Window";

                DateTime PlannedStartTime = DateTime.Now.ToUniversalTime().AddHours(2).AddDays(8);
                DateTime PlannedEndTime = PlannedStartTime.AddHours(1);
                
                String strPlannedStartTime = PlannedStartTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");
                String strPlannedEndTime = PlannedEndTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");

                strPlannedStartTime = strPlannedStartTime.Remove(14, 2);
                strPlannedStartTime = strPlannedStartTime.Insert(14, "05");

                strPlannedEndTime = strPlannedEndTime.Remove(14, 2);
                strPlannedEndTime = strPlannedEndTime.Insert(14, "05");

                int Day = PlannedStartTime.Day;
                String Month = PlannedStartTime.ToString("MMM");
                int Year = PlannedStartTime.Year;

                Strip.InsertSortieGroup(strTDGroupHeaderAssetTypeID, strTDAssetTypeID, strTDPaneID, strTDPersonID1, strTDSlotNumber1, strTDPersonID2, strTDSlotNumber2, strTDWeatherStateID, Details, strPlannedStartTime, strPlannedEndTime, out string StripIDHeader, out string StripID1, out string StripID2);

                System.Threading.Thread.Sleep(5000);

                /* Run FACT Scheduled Task */

                RunFACT fact = new RunFACT();
                fact.RunFACTScheduledJob();
                System.Threading.Thread.Sleep(240000);

                /* Login to Webmail */

                WebmailLoginPage WebmailLoginPage = new WebmailLoginPage();
                WebmailLoginPage.LoginWebmailWithUserCredentials();
                System.Threading.Thread.Sleep(5000);

                /* Navigate to Calendar */

                WebmailMainPage WebmailMainPage = new WebmailMainPage();
                WebmailMainPage.NavigationMenuClick();
                WebmailMainPage.CalendarClick();
                System.Threading.Thread.Sleep(15000);
                WebmailCalendarPage WebmailCalendarPage = new WebmailCalendarPage();
                WebmailCalendarPage.BtnDay.Click();
                String CalendarValue = DateTime.Now.ToString("MMMM yyyy");
                System.Threading.Thread.Sleep(5000);
                WebmailCalendarPage.DateClick(Day, Month, Year, CalendarValue);
                System.Threading.Thread.Sleep(15000);
                try
                {
                    Assert.IsTrue(WebmailCalendarPage.SortieStrip.Displayed);
                    PropertiesCollection.test.Log(Status.Fail, "Sortie Group displayed on Outlook");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Pass, "Sortie Group not displayed on Outlook");
                }

                /* Delete Strip */

                Strip.Delete_Strip(StripIDHeader);
                Strip.Delete_Strip(StripID1);
                Strip.Delete_Strip(StripID2);
            }

            [TestMethod]
            public void SortieGroupOnFACTWindowTimeSpanFrom()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("Sortie Group On the FACT Window lying on the Time Span From");
                Strips Strip = new Strips();

                /*Create Sortie Group On the FACT Window lying on the Time Span From */

                String strTestCaseNo = "TC001";
                String strtblname = "automation_strips";
                String strTestType = "Regression";

                /* Get test data from MySQL */

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataStrip_details = connection.Select(strtblname, strTestCaseNo, strTestType);

                string strTDAssetTypeID = testdataStrip_details[5];
                string strTDPaneID = testdataStrip_details[11];
                string strTDPersonID1 = testdataStrip_details[12];
                string strTDSlotNumber1 = testdataStrip_details[13];
                string strTDWeatherStateID = testdataStrip_details[16];
                string strTDPersonID2 = testdataStrip_details[17];
                string strTDSlotNumber2 = testdataStrip_details[18];
                string strTDGroupHeaderAssetTypeID = testdataStrip_details[19];

                String Details = "AT Sortie Group created On the FACT Window lying on the Time Span From";

                DateTime PlannedStartTime = DateTime.Now.ToUniversalTime().AddDays(-2).AddHours(1);
                DateTime PlannedEndTime = PlannedStartTime.AddHours(2);
                

                String strPlannedStartTime = PlannedStartTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");
                String strPlannedEndTime = PlannedEndTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");

                strPlannedStartTime = strPlannedStartTime.Remove(14, 2);
                strPlannedStartTime = strPlannedStartTime.Insert(14, "05");

                strPlannedEndTime = strPlannedEndTime.Remove(14, 2);
                strPlannedEndTime = strPlannedEndTime.Insert(14, "05");

                int Day = PlannedEndTime.Day;
                String Month = PlannedEndTime.ToString("MMM");
                int Year = PlannedEndTime.Year;

                Strip.InsertSortieGroup(strTDGroupHeaderAssetTypeID, strTDAssetTypeID, strTDPaneID, strTDPersonID1, strTDSlotNumber1, strTDPersonID2, strTDSlotNumber2, strTDWeatherStateID, Details, strPlannedStartTime, strPlannedEndTime, out string StripIDHeader, out string StripID1, out string StripID2);

                System.Threading.Thread.Sleep(5000);

                /* Run FACT Scheduled Task */

                RunFACT fact = new RunFACT();
                fact.RunFACTScheduledJob();
                System.Threading.Thread.Sleep(240000);

                /* Login to Webmail */

                WebmailLoginPage WebmailLoginPage = new WebmailLoginPage();
                WebmailLoginPage.LoginWebmailWithUserCredentials();
                System.Threading.Thread.Sleep(5000);

                /* Navigate to Calendar */

                WebmailMainPage WebmailMainPage = new WebmailMainPage();
                WebmailMainPage.NavigationMenuClick();
                WebmailMainPage.CalendarClick();
                System.Threading.Thread.Sleep(15000);
                WebmailCalendarPage WebmailCalendarPage = new WebmailCalendarPage();
                WebmailCalendarPage.BtnDay.Click();
                String CalendarValue = DateTime.Now.ToString("MMMM yyyy");
                System.Threading.Thread.Sleep(5000);
                WebmailCalendarPage.DateClick(Day, Month, Year, CalendarValue);
                System.Threading.Thread.Sleep(15000);
                try
                {
                    Assert.IsTrue(WebmailCalendarPage.SortieStrip.Displayed);
                    PropertiesCollection.test.Log(Status.Pass, "Sortie Group displayed on Outlook");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Sortie Group not displayed on Outlook");
                }

                /* Delete Strip */

                Strip.Delete_Strip(StripIDHeader);
                Strip.Delete_Strip(StripID1);
                Strip.Delete_Strip(StripID2);
            }

            [TestMethod]
            public void SortieGroupOnFACTWindowTimeSpanTo()
            {

                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("Sortie Group On the FACT Window lying on the Time Span To");
                Strips Strip = new Strips();

                /*Create Sortie Group On the FACT Window lying on the Time Span To */

                String strTestCaseNo = "TC001";
                String strtblname = "automation_strips";
                String strTestType = "Regression";

                /* Get test data from MySQL */

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataStrip_details = connection.Select(strtblname, strTestCaseNo, strTestType);

                string strTDAssetTypeID = testdataStrip_details[5];
                string strTDPaneID = testdataStrip_details[11];
                string strTDPersonID1 = testdataStrip_details[12];
                string strTDSlotNumber1 = testdataStrip_details[13];
                string strTDWeatherStateID = testdataStrip_details[16];
                string strTDPersonID2 = testdataStrip_details[17];
                string strTDSlotNumber2 = testdataStrip_details[18];
                string strTDGroupHeaderAssetTypeID = testdataStrip_details[19];
                String Details = "AT Sortie Group created On the FACT Window lying on the Time Span To";

                DateTime PlannedStartTime = DateTime.Now.ToUniversalTime().AddDays(7).AddHours(-1);
                DateTime PlannedEndTime = PlannedStartTime.AddHours(2);
                
                String strPlannedStartTime = PlannedStartTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");
                String strPlannedEndTime = PlannedEndTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");

                strPlannedStartTime = strPlannedStartTime.Remove(14, 2);
                strPlannedStartTime = strPlannedStartTime.Insert(14, "05");

                strPlannedEndTime = strPlannedEndTime.Remove(14, 2);
                strPlannedEndTime = strPlannedEndTime.Insert(14, "05");

                int Day = PlannedStartTime.Day;
                String Month = PlannedStartTime.ToString("MMM");
                int Year = PlannedStartTime.Year;

                Strip.InsertSortieGroup(strTDGroupHeaderAssetTypeID, strTDAssetTypeID, strTDPaneID, strTDPersonID1, strTDSlotNumber1, strTDPersonID2, strTDSlotNumber2, strTDWeatherStateID, Details, strPlannedStartTime, strPlannedEndTime, out string StripIDHeader, out string StripID1, out string StripID2);

                System.Threading.Thread.Sleep(5000);

                /* Run FACT Scheduled Task */

                RunFACT fact = new RunFACT();
                fact.RunFACTScheduledJob();
                System.Threading.Thread.Sleep(240000);

                /* Login to Webmail */

                WebmailLoginPage WebmailLoginPage = new WebmailLoginPage();
                WebmailLoginPage.LoginWebmailWithUserCredentials();
                System.Threading.Thread.Sleep(5000);

                /* Navigate to Calendar */

                WebmailMainPage WebmailMainPage = new WebmailMainPage();
                WebmailMainPage.NavigationMenuClick();
                WebmailMainPage.CalendarClick();
                System.Threading.Thread.Sleep(15000);
                WebmailCalendarPage WebmailCalendarPage = new WebmailCalendarPage();
                WebmailCalendarPage.BtnDay.Click();
                String CalendarValue = DateTime.Now.ToString("MMMM yyyy");
                System.Threading.Thread.Sleep(5000);
                WebmailCalendarPage.DateClick(Day, Month, Year, CalendarValue);
                System.Threading.Thread.Sleep(15000);
                try
                {
                    Assert.IsTrue(WebmailCalendarPage.SortieStrip.Displayed);
                    PropertiesCollection.test.Log(Status.Pass, "Sortie Group displayed on Outlook");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Sortie Group not displayed on Outlook");
                }

                /* Delete Strip */

                Strip.Delete_Strip(StripIDHeader);
                Strip.Delete_Strip(StripID1);
                Strip.Delete_Strip(StripID2);
            }

            [TestMethod]
            public void PeopleUnavailabilityAtCurrentDateTime()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("People Unavailability At Current Date and Time");
                Strips Strip = new Strips();

                /*Create People Unavailability at current date and time */

                String strTestCaseNo = "TC001";
                String strtblname = "automation_strips";
                String strTestType = "Regression";

                /* Get test data from MySQL */

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataStrip_details = connection.Select(strtblname, strTestCaseNo, strTestType);

                string strTDPersonID = testdataStrip_details[12];
                string strTDUnavailabilityStripTypeID = testdataStrip_details[20];
                String Details = "AT People Unavailability created at current date and time";

                DateTime PlannedStartTime = DateTime.Now.ToUniversalTime().AddHours(2);
                DateTime PlannedEndTime = PlannedStartTime.AddHours(1);
               
                String strPlannedStartTime = PlannedStartTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");
                String strPlannedEndTime = PlannedEndTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");

                strPlannedStartTime = strPlannedStartTime.Remove(14, 2);
                strPlannedStartTime = strPlannedStartTime.Insert(14, "05");

                strPlannedEndTime = strPlannedEndTime.Remove(14, 2);
                strPlannedEndTime = strPlannedEndTime.Insert(14, "05");

                String StripID = Strip.InsertPeopleUnavailability(strTDPersonID, strTDUnavailabilityStripTypeID, Details, strPlannedStartTime, strPlannedEndTime);
                System.Threading.Thread.Sleep(5000);

                /* Run FACT Scheduled Task */

                RunFACT fact = new RunFACT();
                fact.RunFACTScheduledJob();
                System.Threading.Thread.Sleep(240000);

                /* Login to Webmail */

                WebmailLoginPage WebmailLoginPage = new WebmailLoginPage();
                WebmailLoginPage.LoginWebmailWithUserCredentials();
                System.Threading.Thread.Sleep(5000);

                /* Navigate to Calendar */

                WebmailMainPage WebmailMainPage = new WebmailMainPage();
                WebmailMainPage.NavigationMenuClick();
                WebmailMainPage.CalendarClick();
                System.Threading.Thread.Sleep(15000);
                WebmailCalendarPage WebmailCalendarPage = new WebmailCalendarPage();
                WebmailCalendarPage.BtnDay.Click();
                System.Threading.Thread.Sleep(5000);

                try
                {
                    Assert.IsTrue(WebmailCalendarPage.UnavailabilityStrip.Displayed);
                    PropertiesCollection.test.Log(Status.Pass, "People Unavailability Strip displayed on Outlook");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "People Unavailability Strip not displayed on Outlook");
                }

                /* Delete Strip */

                Strip.Delete_Strip(StripID);
            }

            [TestMethod]
            public void PeopleUnavailabilityBeforeFACTWindow()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("People Unavailability Before the FACT Window");
                Strips Strip = new Strips();

                /*Create People Unavailability Before the FACT Window */

                String strTestCaseNo = "TC001";
                String strtblname = "automation_strips";
                String strTestType = "Regression";

                /* Get test data from MySQL */

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataStrip_details = connection.Select(strtblname, strTestCaseNo, strTestType);

                string strTDPersonID = testdataStrip_details[12];
                string strTDSlotNumber = testdataStrip_details[13];
                String Details = "AT People Unavailability created before the FACT Window";

                DateTime PlannedStartTime = DateTime.Now.ToUniversalTime().AddDays(-1);
                DateTime PlannedEndTime = PlannedStartTime.AddHours(1);
                            
                String strPlannedStartTime = PlannedStartTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");
                String strPlannedEndTime = PlannedEndTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");

                strPlannedStartTime = strPlannedStartTime.Remove(14, 2);
                strPlannedStartTime = strPlannedStartTime.Insert(14, "05");

                strPlannedEndTime = strPlannedEndTime.Remove(14, 2);
                strPlannedEndTime = strPlannedEndTime.Insert(14, "05");

                int Day = PlannedEndTime.Day;
                String Month = PlannedEndTime.ToString("MMM");
                int Year = PlannedEndTime.Year;

                String StripID = Strip.InsertPeopleUnavailability(strTDPersonID, strTDSlotNumber, Details, strPlannedStartTime, strPlannedEndTime);
                System.Threading.Thread.Sleep(5000);

                /* Run FACT Scheduled Task */

                RunFACT fact = new RunFACT();
                fact.RunFACTScheduledJob();
                System.Threading.Thread.Sleep(240000);

                /* Login to Webmail */

                WebmailLoginPage WebmailLoginPage = new WebmailLoginPage();
                WebmailLoginPage.LoginWebmailWithUserCredentials();
                System.Threading.Thread.Sleep(5000);

                /* Navigate to Calendar */

                WebmailMainPage WebmailMainPage = new WebmailMainPage();
                WebmailMainPage.NavigationMenuClick();
                WebmailMainPage.CalendarClick();
                System.Threading.Thread.Sleep(15000);
                WebmailCalendarPage WebmailCalendarPage = new WebmailCalendarPage();
                WebmailCalendarPage.BtnDay.Click();
                String CalendarValue = DateTime.Now.ToString("MMMM yyyy");
                System.Threading.Thread.Sleep(5000);
                WebmailCalendarPage.DateClick(Day, Month, Year, CalendarValue);
                System.Threading.Thread.Sleep(15000);
                try
                {
                    Assert.IsTrue(WebmailCalendarPage.UnavailabilityStrip.Displayed);
                    PropertiesCollection.test.Log(Status.Fail, "People Unavailability Strip displayed on Outlook");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Pass, "People Unavailability Strip not displayed on Outlook");
                }

                /* Delete Strip */

                Strip.Delete_Strip(StripID);
            }

            [TestMethod]
            public void PeopleUnavailabilityAfterFACTWindow()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("People Unavailability After the FACT Window");
                Strips Strip = new Strips();

                /*Create People Unavailability After the FACT Window*/

                String strTestCaseNo = "TC001";
                String strtblname = "automation_strips";
                String strTestType = "Regression";

                /* Get test data from MySQL */

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataStrip_details = connection.Select(strtblname, strTestCaseNo, strTestType);

                string strTDPersonID = testdataStrip_details[12];
                string strTDSlotNumber = testdataStrip_details[13];
                String Details = "AT People Unavailability created after the FACT Window";

                DateTime PlannedStartTime = DateTime.Now.ToUniversalTime().AddHours(2).AddDays(8);
                DateTime PlannedEndTime = PlannedStartTime.AddHours(1);
                
                String strPlannedStartTime = PlannedStartTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");
                String strPlannedEndTime = PlannedEndTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");

                strPlannedStartTime = strPlannedStartTime.Remove(14, 2);
                strPlannedStartTime = strPlannedStartTime.Insert(14, "05");

                strPlannedEndTime = strPlannedEndTime.Remove(14, 2);
                strPlannedEndTime = strPlannedEndTime.Insert(14, "05");

                int Day = PlannedStartTime.Day;
                String Month = PlannedStartTime.ToString("MMM");
                int Year = PlannedStartTime.Year;

                String StripID = Strip.InsertPeopleUnavailability(strTDPersonID, strTDSlotNumber, Details, strPlannedStartTime, strPlannedEndTime);
                System.Threading.Thread.Sleep(5000);

                /* Run FACT Scheduled Task */

                RunFACT fact = new RunFACT();
                fact.RunFACTScheduledJob();
                System.Threading.Thread.Sleep(240000);

                /* Login to Webmail */

                WebmailLoginPage WebmailLoginPage = new WebmailLoginPage();
                WebmailLoginPage.LoginWebmailWithUserCredentials();
                System.Threading.Thread.Sleep(5000);

                /* Navigate to Calendar */

                WebmailMainPage WebmailMainPage = new WebmailMainPage();
                WebmailMainPage.NavigationMenuClick();
                WebmailMainPage.CalendarClick();
                System.Threading.Thread.Sleep(15000);
                WebmailCalendarPage WebmailCalendarPage = new WebmailCalendarPage();
                WebmailCalendarPage.BtnDay.Click();
                String CalendarValue = DateTime.Now.ToString("MMMM yyyy");
                System.Threading.Thread.Sleep(5000);
                WebmailCalendarPage.DateClick(Day, Month, Year, CalendarValue);
                System.Threading.Thread.Sleep(15000);
                try
                {
                    Assert.IsTrue(WebmailCalendarPage.UnavailabilityStrip.Displayed);
                    PropertiesCollection.test.Log(Status.Fail, "People Unavailability Strip displayed on Outlook");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Pass, "People Unavailability Strip not displayed on Outlook");
                }

                /* Delete Strip */

                Strip.Delete_Strip(StripID);
            }

            [TestMethod]
            public void PeopleUnavailabilityOnFACTWindowTimeSpanFrom()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("People Unavailability On the FACT Window lying on the Time Span From");
                Strips Strip = new Strips();

                /*Create People Unavailability On the FACT Window lying on the Time Span From */

                String strTestCaseNo = "TC001";
                String strtblname = "automation_strips";
                String strTestType = "Regression";

                /* Get test data from MySQL */

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataStrip_details = connection.Select(strtblname, strTestCaseNo, strTestType);

                string strTDPersonID = testdataStrip_details[12];
                string strTDSlotNumber = testdataStrip_details[13];
                String Details = "AT People Unavailability created On the FACT Window lying on the Time Span From";

                DateTime PlannedStartTime = DateTime.Now.ToUniversalTime().AddDays(-2).AddHours(1);
                DateTime PlannedEndTime = PlannedStartTime.AddHours(2);
                
                String strPlannedStartTime = PlannedStartTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");
                String strPlannedEndTime = PlannedEndTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");

                strPlannedStartTime = strPlannedStartTime.Remove(14, 2);
                strPlannedStartTime = strPlannedStartTime.Insert(14, "05");

                strPlannedEndTime = strPlannedEndTime.Remove(14, 2);
                strPlannedEndTime = strPlannedEndTime.Insert(14, "05");

                int Day = PlannedStartTime.Day;
                String Month = PlannedStartTime.ToString("MMM");
                int Year = PlannedStartTime.Year;

                String StripID = Strip.InsertPeopleUnavailability(strTDPersonID, strTDSlotNumber, Details, strPlannedStartTime, strPlannedEndTime);
                System.Threading.Thread.Sleep(5000);

                /* Run FACT Scheduled Task */

                RunFACT fact = new RunFACT();
                fact.RunFACTScheduledJob();
                System.Threading.Thread.Sleep(240000);

                /* Login to Webmail */

                WebmailLoginPage WebmailLoginPage = new WebmailLoginPage();
                WebmailLoginPage.LoginWebmailWithUserCredentials();
                System.Threading.Thread.Sleep(5000);

                /* Navigate to Calendar */

                WebmailMainPage WebmailMainPage = new WebmailMainPage();
                WebmailMainPage.NavigationMenuClick();
                WebmailMainPage.CalendarClick();
                System.Threading.Thread.Sleep(15000);
                WebmailCalendarPage WebmailCalendarPage = new WebmailCalendarPage();
                WebmailCalendarPage.BtnDay.Click();
                String CalendarValue = DateTime.Now.ToString("MMMM yyyy");
                System.Threading.Thread.Sleep(5000);
                WebmailCalendarPage.DateClick(Day, Month, Year, CalendarValue);
                System.Threading.Thread.Sleep(15000);
                try
                {
                    Assert.IsTrue(WebmailCalendarPage.UnavailabilityStrip.Displayed);
                    PropertiesCollection.test.Log(Status.Pass, "People Unavailability Strip displayed on Outlook");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "People Unavailability Strip not displayed on Outlook");
                }

                /* Delete Strip */

                Strip.Delete_Strip(StripID);
            }

            [TestMethod]
            public void PeopleUnavailabilityOnFACTWindowTimeSpanTo()
            {

                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("People Unavailability On the FACT Window lying on the Time Span To");
                Strips Strip = new Strips();

                /*Create People Unavailability On the FACT Window lying on the Time Span To */

                String strTestCaseNo = "TC001";
                String strtblname = "automation_strips";
                String strTestType = "Regression";

                /* Get test data from MySQL */

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataStrip_details = connection.Select(strtblname, strTestCaseNo, strTestType);

                string strTDPersonID = testdataStrip_details[12];
                string strTDSlotNumber = testdataStrip_details[13];
                String Details = "AT People Unavailability created On the FACT Window lying on the Time Span To";

                DateTime PlannedStartTime = DateTime.Now.ToUniversalTime().AddDays(7).AddHours(-1);
                DateTime PlannedEndTime = PlannedStartTime.AddHours(2);
                

                String strPlannedStartTime = PlannedStartTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");
                String strPlannedEndTime = PlannedEndTime.ToString("yyyy-MM-ddThh:mm:ss.fffZ");

                strPlannedStartTime = strPlannedStartTime.Remove(14, 2);
                strPlannedStartTime = strPlannedStartTime.Insert(14, "05");

                strPlannedEndTime = strPlannedEndTime.Remove(14, 2);
                strPlannedEndTime = strPlannedEndTime.Insert(14, "05");

                int Day = PlannedStartTime.Day;
                String Month = PlannedStartTime.ToString("MMM");
                int Year = PlannedStartTime.Year;

                String StripID = Strip.InsertPeopleUnavailability(strTDPersonID, strTDSlotNumber, Details, strPlannedStartTime, strPlannedEndTime);
                System.Threading.Thread.Sleep(5000);

                /* Run FACT Scheduled Task */

                RunFACT fact = new RunFACT();
                fact.RunFACTScheduledJob();
                System.Threading.Thread.Sleep(240000);

                /* Login to Webmail */

                WebmailLoginPage WebmailLoginPage = new WebmailLoginPage();
                WebmailLoginPage.LoginWebmailWithUserCredentials();
                System.Threading.Thread.Sleep(5000);

                /* Navigate to Calendar */

                WebmailMainPage WebmailMainPage = new WebmailMainPage();
                WebmailMainPage.NavigationMenuClick();
                WebmailMainPage.CalendarClick();
                System.Threading.Thread.Sleep(15000);
                WebmailCalendarPage WebmailCalendarPage = new WebmailCalendarPage();
                WebmailCalendarPage.BtnDay.Click();
                String CalendarValue = DateTime.Now.ToString("MMMM yyyy");
                System.Threading.Thread.Sleep(5000);
                WebmailCalendarPage.DateClick(Day, Month, Year, CalendarValue);
                System.Threading.Thread.Sleep(15000);
                try
                {
                    Assert.IsTrue(WebmailCalendarPage.UnavailabilityStrip.Displayed);
                    PropertiesCollection.test.Log(Status.Pass, "People Unavailability Strip displayed on Outlook");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "People Unavailability Strip not displayed on Outlook");
                }

                /* Delete Strip */

                Strip.Delete_Strip(StripID);
            }

            [TestMethod]
            public void CreateUnavailabilityfromOutlook()
            {

                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("Unavailability Creation from Outlook");
                Strips Strip = new Strips();
                /* Create Unavailability from Outlook */

                /* Login to Webmail */

                WebmailLoginPage WebmailLoginPage = new WebmailLoginPage();
                WebmailLoginPage.LoginWebmailWithUserCredentials();
                System.Threading.Thread.Sleep(5000);

                /* Navigate to Calendar */

                WebmailMainPage WebmailMainPage = new WebmailMainPage();
                WebmailMainPage.NavigationMenuClick();
                WebmailMainPage.CalendarClick();
                System.Threading.Thread.Sleep(15000);
                WebmailCalendarPage WebmailCalendarPage = new WebmailCalendarPage();
                WebmailCalendarPage.BtnDay.Click();
                System.Threading.Thread.Sleep(5000);
                String Details = "AT Outlook Unavailability";
                WebmailCalendarPage.AddIcon.Click();
                System.Threading.Thread.Sleep(5000);
                WebmailCalendarPage.Title.SendKeys(Details);
                System.Threading.Thread.Sleep(5000);
                WebmailCalendarPage.CboCategorize.Click();
                System.Threading.Thread.Sleep(5000);
                WebmailCalendarPage.CategorySelection.Click();
                System.Threading.Thread.Sleep(5000);
                WebmailCalendarPage.BtnSave.Click();
                System.Threading.Thread.Sleep(5000);

                /* Run FACT Scheduled Task */

                RunFACT fact = new RunFACT();
                fact.RunFACTScheduledJob();
                System.Threading.Thread.Sleep(60000);

                Int64 StripID = Strip.Search_Outlook_Strip();
                try
                {
                    Assert.IsTrue(StripID > 0);
                    PropertiesCollection.test.Log(Status.Pass, "People Unavailability Strip Displayed on Outlook");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "People Unavailability Strip not displayed on Outlook");
                }


                /* Delete Strip */

                Strip.Delete_Strip(Convert.ToString(StripID));
            }
            [TestCleanup]
            public void TestCleanup()
            {
                var status = TestContext.CurrentTestOutcome;
                Status status1;

                //   LogSatus logstatus;

                Console.WriteLine("TestContext:" + TestContext.CurrentTestOutcome);
                
                if (TestContext.CurrentTestOutcome != UnitTestOutcome.Passed)
                {
                    status1 = Status.Fail;
                    PropertiesCollection.test.Log(Status.Fail, "Test Failed and aborted");
                }

                System.Threading.Thread.Sleep(4000);

                PropertiesCollection.driver.Close();
                PropertiesCollection.driver.Quit();
                PropertiesCollection.driver.Dispose();
            }


            [ClassCleanup]
            public static void ClassCleanup()
            {
                PropertiesCollection.extent.Flush();
            }

        }

    }
}

