using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using FPWebAutomation_MSTests.Database;
using FPWebAutomation_MSTests.PageObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using FPWebAutomation_MSTests.Email;
using static FPWebAutomation_MSTests.TestCases.Smoke_Tests.TS01_ValidateSideMenus;

namespace FPWebAutomation_MSTests.TestCases.Smoke_Tests
{

    class TS04_CombinedSchedule
    {
        [TestClass]
        public class TS04_TC01_CreateCombinedScheduleReport
        {
            public TestContext TestContext { get; set; }

            /*************************To be used only in case of the indivisual tests - for troubleshooting******************************* 
         [ClassInitialize]
         public static void ClassInitialize(TestContext context)
         {

              PropertiesCollection.htmlReporter = new ExtentHtmlReporter(@"C:\FlightPro\Dev\_main\Test Automation\FlightPro\FightPro_WebAutomation\FPWebAutomation\Report\Report.html");
              PropertiesCollection.htmlReporter.LoadConfig(@"C:\extent-configfile\extent-config.xml");
              PropertiesCollection.extent = new ExtentReports();
              PropertiesCollection.extent.AttachReporter(PropertiesCollection.htmlReporter);
              PropertiesCollection.extent.AddSystemInfo("Browser", "Chrome");
              PropertiesCollection.extent.AddSystemInfo("Application Under Test (AUT)", "FlightProWeb");
              PropertiesCollection.extent.AddSystemInfo("Application URL", ConfigurationManager.AppSettings["URL"]);
              PropertiesCollection.extent.AddSystemInfo("Application Database Initial Catalog", ConfigurationManager.AppSettings["SQLServerInitialCatalog"]);
           }
           ******************************************************************************************************************************/

            [TestInitialize]
          public void TestInitialize()
          {
              TC01_ValidateSideMenus.IncrementTests();
              PropertiesCollection.driver = new ChromeDriver(@ConfigurationManager.AppSettings["ChromeDriverPath"]);
              PropertiesCollection.driver.Manage().Window.Maximize();
              FpLoginPage loginPage = new FpLoginPage();
              loginPage.Login();
          }

          [TestCategory("Smoke")]
          [TestCategory("CombinedScheduledReport")]
          [TestMethod]
          public void TS04_TC01_Smoke_CreateCombinedScheduleReport()
          {
              String strTestCaseNo = "TC001";
              String strtblname = "automation_combinedschedule";
              String strTestType = "Smoke";

              PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TC01_Smoke_CreateCombinedScheduleReport");

              /* Get test data from MySQL */

            var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataCombinedSchedule = connection.Select(strtblname, strTestCaseNo, strTestType);


                string strTDReportName = testdataCombinedSchedule[3];
                string strTDOrganisationGroup = testdataCombinedSchedule[4];
                string strTDClassification = testdataCombinedSchedule[5];
                string strTDPane = testdataCombinedSchedule[6];
                string strTDNote = testdataCombinedSchedule[7];
                string strTDDate = testdataCombinedSchedule[8];            
                

                /* Delete Combined Schedule Report if existing already */

                string strSQLtblname = "tblCombinedScheduleReport";
                string strtblcolumn = "Name";
                string strwherecondn = strTDReportName;
                ////var conn = new ConnectToSQLServer();
                //bool result = conn.Delete(strSQLtblname, strtblcolumn, strwherecondn);

                                
                /* Create Combined Schedule Report*/

                 FpCombinedSchedulePage CombinedSchedule = new FpCombinedSchedulePage();
                 String ReportTitle = CombinedSchedule.CreateCombinedScheduleReport(strTDReportName, strTDOrganisationGroup, strTDClassification, strTDPane, strTDNote);
                 Console.WriteLine(ReportTitle);

                /* Verify Report Name */

                    try
                    {
                        Assert.AreEqual(ReportTitle, strTDReportName);
                        PropertiesCollection.test.Log(Status.Pass, "Combined Schedule Report created successfully");
                    }
                    catch
                    {
                        PropertiesCollection.test.Log(Status.Fail, "Combined Schedule Report not Created");
                    throw;
                    }


                    CombinedSchedule.btnDay.Click();
                    System.Threading.Thread.Sleep(5000);

                    int height = CombinedSchedule.date.Size.Height;
                    int width = CombinedSchedule.date.Size.Width;
                    Console.WriteLine(height);
                    Console.WriteLine(width);
                    Actions actions = new Actions(PropertiesCollection.driver);
                    actions.MoveToElement(CombinedSchedule.date).MoveByOffset((-width / 3), 2).Click().Perform();
                    System.Threading.Thread.Sleep(3000);
                    CombinedSchedule.date.SendKeys(strTDDate + Keys.Tab);
                    System.Threading.Thread.Sleep(15000);
                    CombinedSchedule.btnRefresh.Click();
                    System.Threading.Thread.Sleep(15000);

                /* Verify Mission Strip  */

                    try
                    {
                        Assert.IsTrue(CombinedSchedule.missionStrip.Displayed);
                        PropertiesCollection.test.Log(Status.Pass, "Mission Strip is displayed");
                    }
                    catch
                    {
                        PropertiesCollection.test.Log(Status.Fail, "Mission Strip is not displayed");
                    throw;
                }


                /* Verify Formation Group Strip  */

                    try
                    {
                        Assert.IsTrue(CombinedSchedule.formationStrip.Displayed);
                        PropertiesCollection.test.Log(Status.Pass, "Formation Group Strip is displayed");
                    }
                    catch
                    {
                        PropertiesCollection.test.Log(Status.Fail, "Formation Group Strip is not displayed");
                    throw;
                }


                /* Verify Sortie Group Strips  */

                    try
                    {
                        Assert.IsTrue(CombinedSchedule.sortieStrip1.Displayed);
                        PropertiesCollection.test.Log(Status.Pass, "Sortie Group Strip 1 is displayed");
                    }
                    catch
                    {
                        PropertiesCollection.test.Log(Status.Fail, "Sortie Group Strip 1 is not displayed");
                    throw;
                    }

                    try
                    {
                        Assert.IsTrue(CombinedSchedule.sortieStrip2.Displayed);
                        PropertiesCollection.test.Log(Status.Pass, "Sortie Group Strip 2 is displayed");
                    }
                    catch
                    {
                        PropertiesCollection.test.Log(Status.Fail, "Sortie Group Strip 2 is not displayed");
                    throw;
                    }

                    try
                    {
                        Assert.IsTrue(CombinedSchedule.sortieStrip3.Displayed);
                        PropertiesCollection.test.Log(Status.Pass, "Sortie Group Strip 3 is displayed");
                    }
                    catch
                    {
                        PropertiesCollection.test.Log(Status.Fail, "Sortie Group Strip 3 is not displayed");
                    throw;
                    }

                /* Delete Combined Schedule Report */

                 //result = conn.Delete(strSQLtblname, strtblcolumn, strwherecondn);

                 //   try
                 //   {
                 //       Assert.AreEqual(result, true);
                 //       PropertiesCollection.test.Log(Status.Pass, "Combined Schedule Report deleted");
                 //   }
                 //   catch
                 //   {
                 //       PropertiesCollection.test.Log(Status.Fail, "Combined Schedule Report not deleted");
                 //    throw;
                 //   }
                }

            
            [TestCleanup]
            public void TestCleanup()

            {
                if (TestContext.CurrentTestOutcome == UnitTestOutcome.Passed)
                {
                    TC01_ValidateSideMenus.IncrementPassedTests();
                }
                var status = TestContext.CurrentTestOutcome;
                Status status1;

                //   LogSatus logstatus;

                Console.WriteLine("TestContext:" + TestContext.CurrentTestOutcome);
                Console.WriteLine("UnitTestOutcome.Passed" + UnitTestOutcome.Passed);
                
                if (TestContext.CurrentTestOutcome != UnitTestOutcome.Passed)
                {
                    status1 = Status.Fail;
                    PropertiesCollection.test.Log(Status.Fail, "Combined Schedule Report creation failed");
                }
                               
                System.Threading.Thread.Sleep(4000);
                
                PropertiesCollection.driver.Close();
                PropertiesCollection.driver.Quit();
                PropertiesCollection.driver.Dispose();
            }

            /*************************To be used only in case of the indivisual tests - for troubleshooting*******************************
            [ClassCleanup]
            public static void ClassCleanup()
            {
                PropertiesCollection.extent.Flush();

                
            }
           /**********************************************************/
        }
    }
}
