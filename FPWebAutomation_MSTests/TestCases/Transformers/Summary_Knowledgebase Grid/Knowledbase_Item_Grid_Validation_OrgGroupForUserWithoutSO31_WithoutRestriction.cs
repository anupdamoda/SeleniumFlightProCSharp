using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using FPWebAutomation_MSTests.Database;
using FPWebAutomation_MSTests.PageObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FPWebAutomation_MSTests.TestCases.Transformers
{
    class Knowledbase_Item_Grid_Validation_OrgGroupForUserWithoutSO31_WithoutRestriction
    {
        [TestClass]
        public class Summary_Knowledgebase_Grid
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
                PropertiesCollection.extent.AddSystemInfo("Application Under Test (AUT)", "FlightProWeb");
                PropertiesCollection.extent.AddSystemInfo("Application URL", "http://oc-svr-at1/Fltpro_Automation_main/");
            }


            [TestInitialize]
            public void TestInitialize()
            {
                PropertiesCollection.driver = new ChromeDriver(@ConfigurationManager.AppSettings["ChromeDriverPath"]);
                PropertiesCollection.driver.Manage().Window.Maximize();
            }

            private double GetDaysOldUnreadKnowledgeBaseItem(DateTime strTDDate)
            {
                return Math.Round((DateTime.Now - strTDDate).TotalDays);
            }


            [TestMethod]           
            public void Add_Knowledbase_Item_OrgGroupForUserWithoutSO31_WithoutRestriction()
            {
                /* Add Knowledgebase automation for user without SO 31 access and without restricted viewing */

                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("Verify Add Knowledgebase automation assigned to Org Group where user does not have SO 31 access and without restricted viewing");

                String strTestCaseNo = "TC007";
                String strtblname = "automation_summary_knowledgebase";
                String strTestType = "Progression";


                /* Get test data from MySQL */

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataSummary_Knowledgebase = connection.Select(strtblname, strTestCaseNo, strTestType);


                string strTDTitle = testdataSummary_Knowledgebase[3];
                string strTDContent = testdataSummary_Knowledgebase[4];
                string strTDVersion = testdataSummary_Knowledgebase[5];
                string strTDDate = testdataSummary_Knowledgebase[6];
                string strTDRestrictViewing = testdataSummary_Knowledgebase[7];
                string strTDYourDate = testdataSummary_Knowledgebase[8];
                string strTDYourVersion = testdataSummary_Knowledgebase[9];
                string strTDStatus = testdataSummary_Knowledgebase[10];
                string strTDUser = testdataSummary_Knowledgebase[11];
                string strTDOtherUser = testdataSummary_Knowledgebase[16];
                string strTDLogin = testdataSummary_Knowledgebase[20];

                ConnectToMySQL_Fetch_TestData MySQLConnect = new ConnectToMySQL_Fetch_TestData();
                String[] TestData = new string[3];
                TestData = MySQLConnect.GetLoginDetails(strTDLogin);

                string strTDUsername = TestData[1];
                string strTDPassword = TestData[2];
                Console.WriteLine(strTDUsername);
                Console.WriteLine(strTDPassword);

                FpLoginPage loginPage = new FpLoginPage();
                loginPage.LoginWithUserCredentials(strTDUsername, strTDPassword);

                /* Calculate Days Old */

                string strTDDaysOld = GetDaysOldUnreadKnowledgeBaseItem(Convert.ToDateTime(strTDDate)).ToString();
                Console.WriteLine(strTDDaysOld);

                /* Identify OrgGroupId for the OrgGroup */

                string strSQLtblname = "tblPeopleGroup";
                string strtblselectcolumn = "PeopleGroupID";
                string strtblcolumn = "PeopleGroupName";
                string strwherecondn = strTDUser;
                var conn = new ConnectToSQLServer();
                string OrgGroupID = conn.Select(strSQLtblname, strtblselectcolumn, strtblcolumn, strwherecondn);

                /* Identify OrgGroupId for second OrgGroup */

                
                strwherecondn = strTDOtherUser;
                conn = new ConnectToSQLServer();
                string OrgGroupIDOther = conn.Select(strSQLtblname, strtblselectcolumn, strtblcolumn, strwherecondn);

                /* Add Knowledgebase Item for user with SO #31 access and with restricted viewing */

                FpKnowledgeBasePage Knowledgebase = new FpKnowledgeBasePage();
                System.Threading.Thread.Sleep(15000);
                Knowledgebase.Add_Knowledgebase_Item_AssigntoOrgGroup(strTDTitle, strTDContent, strTDVersion, strTDDate, strTDRestrictViewing, OrgGroupID, OrgGroupIDOther);
                System.Threading.Thread.Sleep(15000);
                FpSideMenus SideMenu = new FpSideMenus();
                SideMenu.SummaryClick();
                System.Threading.Thread.Sleep(15000);
                FpSummaryPage Summary = new FpSummaryPage();

                Summary.BtnPeopleSelector.Click();
                System.Threading.Thread.Sleep(5000);
                Summary.TxtPeopleSearch.SendKeys(strTDUser);
                System.Threading.Thread.Sleep(5000);
                Summary.BtnSearch.Click();
                System.Threading.Thread.Sleep(5000);
                Summary.PersonSelection.Click();
                System.Threading.Thread.Sleep(5000);
                Summary.BtnApply.Click();
                System.Threading.Thread.Sleep(5000);

                String[] Knowledgebasedetails = Summary.RetrieveKnowledgebaseGriddetails(strTDTitle);
                String strFPwebItem = Knowledgebasedetails[0];
                String strFPwebNowDated = Knowledgebasedetails[1];
                String strFPwebYourDate = Knowledgebasedetails[2];
                String strFPwebNowVersioned = Knowledgebasedetails[3];
                String strFPwebYourVersion = Knowledgebasedetails[4];
                String strFPwebDaysOld = Knowledgebasedetails[5];
                String strFPwebStatus = Knowledgebasedetails[6];

                System.Threading.Thread.Sleep(15000);

                /* Validate Item column */
                try
                {
                    Assert.AreEqual(strTDTitle, strFPwebItem);
                    PropertiesCollection.test.Log(Status.Pass, "Item column on grid validated for logged in user and passed");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Item column on grid validated for logged in user and failed");
                }

                /* Validate Now Dated column */
                try
                {

                    Assert.AreEqual(strTDDate, strFPwebNowDated);
                    PropertiesCollection.test.Log(Status.Pass, "Now Dated column on grid validated for logged in user and passed");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Now Dated column on grid validated for logged in user and failed");
                }

                /* Validate Your Date column */
                try
                {
                    Assert.AreEqual(strTDYourDate, strFPwebYourDate);
                    PropertiesCollection.test.Log(Status.Pass, "Your Date column on grid validated for logged in user and passed");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Your Date column on grid validated for logged in user and failed");
                }

                /* Validate Now Versioned column */
                try
                {
                    Assert.AreEqual(strTDVersion, strFPwebNowVersioned);
                    PropertiesCollection.test.Log(Status.Pass, "Now Versioned column on grid validated for logged in user and passed");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Now Versioned column on grid validated for logged in user and failed");
                }

                /* Validate Your Version column */
                try
                {
                    Assert.AreEqual(strTDYourVersion, strFPwebYourVersion);
                    PropertiesCollection.test.Log(Status.Pass, "Your Version column on grid validated for logged in user and passed");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Your Version column on grid validated for logged in user and failed");
                }

                /* Validate Days Old column */

                try
                {
                    Assert.AreEqual(strTDDaysOld, strFPwebDaysOld);
                    PropertiesCollection.test.Log(Status.Pass, "Days Old column on grid validated for logged in user and passed");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Days Old column on grid validated for logged in user and failed");
                }

                /* Validate Status column */
                try
                {
                    Assert.AreEqual(strTDStatus, strFPwebStatus);
                    PropertiesCollection.test.Log(Status.Pass, "Status column on grid validated for logged in user and passed");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Status column on grid validated for logged in user and failed");
                }

                int Never = Summary.RetrieveKnowledgebaseNeverCount();
                int Old = Summary.RetrieveKnowledgebaseOldCount();

                try
                {
                    Assert.AreEqual(Never, Convert.ToInt32(Summary.CountNever.Text.Substring(0, 1)));
                    PropertiesCollection.test.Log(Status.Pass, "Count of Never items validated for logged in user and passed");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Count of Never items validated for logged in user and failed");
                }

                try
                {
                    Assert.AreEqual(Old, Convert.ToInt32(Summary.CountOld.Text.Substring(0, 1)));
                    PropertiesCollection.test.Log(Status.Pass, "Count of Old items validated for logged in user and passed");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Count of Old items validated for logged in user and failed");
                }

                /* Validation for other user */


                Summary.BtnPeopleSelector.Click();
                System.Threading.Thread.Sleep(5000);
                Summary.TxtPeopleSearch.SendKeys(strTDOtherUser);
                System.Threading.Thread.Sleep(5000);
                Summary.BtnSearch.Click();
                System.Threading.Thread.Sleep(5000);
                Summary.PersonSelection.Click();
                System.Threading.Thread.Sleep(15000);
                Summary.BtnApply.Click();
                System.Threading.Thread.Sleep(5000);

                try
                {
                    Assert.IsTrue(Summary.LblNoData.Displayed);
                    PropertiesCollection.test.Log(Status.Pass, "Add Knowledgebase Item  for other user validated and passed");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Add Knowledgebase Item for other user validated and failed");
                }
                Never = Summary.RetrieveKnowledgebaseNeverCount();
                Old = Summary.RetrieveKnowledgebaseOldCount();

                try
                {
                    Assert.AreEqual(Never, Convert.ToInt32(Summary.CountNever.Text.Substring(0, 1)));
                    PropertiesCollection.test.Log(Status.Pass, "Count of Never items for other user validated and passed");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Count of Never items for other user validated and failed");
                }

                try
                {
                    Assert.AreEqual(Old, Convert.ToInt32(Summary.CountOld.Text.Substring(0, 1)));
                    PropertiesCollection.test.Log(Status.Pass, "Count of Old items for other user validated and passed");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Count of Old items for other user validated and failed");
                }

            }

            [TestMethod]         
            public void MarkAsRead_Knowledbase_Item_OrgGroupForUserWithoutSO31_WithoutRestrictio()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("Mark As Read Knowledgebase Item assigned to Org Group where user does not have SO 31 access and without restricted viewing");

                String strTestCaseNo = "TC007";
                String strtblname = "automation_summary_knowledgebase";
                String strTestType = "Progression";


                /* Get test data from MySQL */

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataSummary_Knowledgebase = connection.Select(strtblname, strTestCaseNo, strTestType);


                string strTDTitle = testdataSummary_Knowledgebase[3];
                string strTDOtherUser = testdataSummary_Knowledgebase[16];
                string strTDLogin = testdataSummary_Knowledgebase[20];

                ConnectToMySQL_Fetch_TestData MySQLConnect = new ConnectToMySQL_Fetch_TestData();
                String[] TestData = new string[3];
                TestData = MySQLConnect.GetLoginDetails(strTDLogin);

                string strTDUsername = TestData[1];
                string strTDPassword = TestData[2];

                FpLoginPage loginPage = new FpLoginPage();
                loginPage.LoginWithUserCredentials(strTDUsername, strTDPassword);

                FpKnowledgeBasePage Knowledgebase = new FpKnowledgeBasePage();
                Knowledgebase.MarkAsRead_Knowledgebase_Item(strTDTitle);

                FpSummaryPage Summary = new FpSummaryPage();

                String[] Knowledgebasedetails = Summary.RetrieveKnowledgebaseGriddetails(strTDTitle);
                String strFPwebItem = Knowledgebasedetails[0];

                System.Threading.Thread.Sleep(15000);

                /* Validate for logged in user */

                /* Validate Item  */
                try
                {
                    Assert.IsNull(strFPwebItem);
                    PropertiesCollection.test.Log(Status.Pass, "Data on grid validated for logged in user and passed");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Data on grid validated for logged in user and failed");
                }

                int Never = Summary.RetrieveKnowledgebaseNeverCount();
                int Old = Summary.RetrieveKnowledgebaseOldCount();

                try
                {
                    Assert.AreEqual(Never, Convert.ToInt32(Summary.CountNever.Text.Substring(0, 1)));
                    PropertiesCollection.test.Log(Status.Pass, "Count of Never items validated for logged in user and passed");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Count of Never items validated for logged in user and failed");
                }

                try
                {
                    Assert.AreEqual(Old, Convert.ToInt32(Summary.CountOld.Text.Substring(0, 1)));
                    PropertiesCollection.test.Log(Status.Pass, "Count of Old items validated for logged in user and passed");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Count of Old items validated for logged in user and failed");
                }

                /* Validate for other user */

                Summary.BtnPeopleSelector.Click();
                System.Threading.Thread.Sleep(5000);
                Summary.TxtPeopleSearch.SendKeys(strTDOtherUser);
                System.Threading.Thread.Sleep(5000);
                Summary.BtnSearch.Click();
                System.Threading.Thread.Sleep(5000);
                Summary.PersonSelection.Click();
                System.Threading.Thread.Sleep(15000);
                Summary.BtnApply.Click();
                System.Threading.Thread.Sleep(5000);

                /* Validate Item  */
                try
                {
                    Assert.IsNull(strFPwebItem);
                    PropertiesCollection.test.Log(Status.Pass, "Data on grid for other user validated and passed");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Data on grid for other user validated and failed");
                }

                Never = Summary.RetrieveKnowledgebaseNeverCount();
                Old = Summary.RetrieveKnowledgebaseOldCount();

                try
                {
                    Assert.AreEqual(Never, Convert.ToInt32(Summary.CountNever.Text.Substring(0, 1)));
                    PropertiesCollection.test.Log(Status.Pass, "Count of Never items for other user validated and passed");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Count of Never items for other user validated and failed");
                }

                try
                {
                    Assert.AreEqual(Old, Convert.ToInt32(Summary.CountOld.Text.Substring(0, 1)));
                    PropertiesCollection.test.Log(Status.Pass, "Count of Old items for other user validated and passed");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Count of Old items for other user validated and failed");
                }

            }

            [TestMethod]            
            public void Delete_Knowledbase_Item_OrgGroupForUserWithoutSO31_WithoutRestriction()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("Delete Knowledgebase Item assigned to Org Group where user does not have SO 31 access and without restricted viewing");

                String strTestCaseNo = "TC007";
                String strtblname = "automation_summary_knowledgebase";
                String strTestType = "Progression";


                /* Get test data from MySQL */

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataSummary_Knowledgebase = connection.Select(strtblname, strTestCaseNo, strTestType);


                string strTDTitle = testdataSummary_Knowledgebase[3];
                string strTDRestrictViewing = testdataSummary_Knowledgebase[7];
                string strTDUser = testdataSummary_Knowledgebase[11];
                string strTDLogin = testdataSummary_Knowledgebase[20];


                /* Identify OrgGroupId for the OrgGroup */

                string strSQLtblname = "tblPeopleGroup";
                string strtblselectcolumn = "PeopleGroupID";
                string strtblcolumn = "PeopleGroupName";
                string strwherecondn = strTDUser;
                var conn = new ConnectToSQLServer();
                string OrgGroupID = conn.Select(strSQLtblname, strtblselectcolumn, strtblcolumn, strwherecondn);

                ConnectToMySQL_Fetch_TestData MySQLConnect = new ConnectToMySQL_Fetch_TestData();
                String[] TestData = new string[3];
                TestData = MySQLConnect.GetLoginDetails(strTDLogin);

                string strTDUsername = TestData[1];
                string strTDPassword = TestData[2];

                FpLoginPage loginPage = new FpLoginPage();
                loginPage.LoginWithUserCredentials(strTDUsername, strTDPassword);

                FpKnowledgeBasePage Knowledgebase = new FpKnowledgeBasePage();
                Knowledgebase.Delete_Knowledgebase_Item_AssignedtoOrgGroup(strTDTitle, strTDRestrictViewing, OrgGroupID);

                try
                {
                    Assert.IsTrue(Knowledgebase.txtKBItem.Displayed);
                    PropertiesCollection.test.Log(Status.Fail, "Knowledgebase Item could not be deleted");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Pass, "Knowledgebase Item deleted");
                }
            }

            [TestCleanup]
            public void TestCleanup()
            {
                var status = TestContext.CurrentTestOutcome;
                Status status1;

                //   LogSatus logstatus;

                Console.WriteLine("TestContext:" + TestContext.CurrentTestOutcome);
                Console.WriteLine("UnitTestOutcome.Passed" + UnitTestOutcome.Passed);

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


