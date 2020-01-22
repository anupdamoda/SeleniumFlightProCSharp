/* Regression Test Case -- FlightPro Web -> Admin > General > Strip Sub Groups
   Author -- Vandana Kalluru
   Dated - 4th Dec 2019*/

using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using FPWebAutomation_MSTests.PageObjects;
using FPWebAutomation_MSTests.Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using System.Configuration;
using System.IO;
using System.Threading;

namespace FPWebAutomation_MSTests.TestCases.Regression_Tests
{
    class StripSubGroups
    {
        [TestClass]
        public class StripSubGroupsAdministration
        {
            public TestContext TestContext { get; set; }
            string strTestCaseNo;
            string strTDShortCode;
            string strTDSubGroupName;
            string strTDNewShortCode;
            string strTDNewSubGroupName;
            const string strtblname = "automation_stripsubgroup";
            const string strTestType = "Regression";

            [ClassInitialize]
            public static void ClassInitialize(TestContext context)
            {
                PropertiesCollection.htmlReporter = new ExtentHtmlReporter(@"C:\Report\Report.html");

                string currentDir = Directory.GetCurrentDirectory();
                var configFile = currentDir.Replace("bin\\Debug", "extent-config.xml");

                PropertiesCollection.htmlReporter.LoadConfig(configFile);
                PropertiesCollection.extent = new ExtentReports();
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
            }

            [TestMethod]
            public void VerifyStripSubGroups()
            {
                AddStripSubGroup();
                EditStripSubGroup();
                VerifyErrorMessages();
                ClickOnReturnWithoutSave();
                DeactivateStripSubGroup();
            }

            public void AddStripSubGroup()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TC01_AddStripSubGroup");

                strTestCaseNo = "TC001_Reg";
                
                var connection = new ConnectToSQLServer_Fetch_TestData();
                var testdataStripSubGroup = connection.Select(strtblname, strTestCaseNo, strTestType);

                strTDShortCode = testdataStripSubGroup[3];
                strTDSubGroupName = testdataStripSubGroup[4];

                FpStripSubGroupsPage.AddStripSubGroup(strTDShortCode, strTDSubGroupName);
                Thread.Sleep(2000);

                string[] strSubGroupdata = FpStripSubGroupsPage.FetchSubGroupData(strTDShortCode);

                try
                {
                    Assert.AreEqual(strSubGroupdata[0], strTDShortCode);
                    PropertiesCollection.test.Log(Status.Pass, "Add Strip Sub Group => Short code validation is successful");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Add Strip Sub Group => Short code validation is not successful");
                }
                try
                {
                    Assert.AreEqual(strSubGroupdata[1], strTDSubGroupName);
                    PropertiesCollection.test.Log(Status.Pass, "Add Strip Sub Group => Strip Sub group name validation is successful");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Add Strip Sub Group => Strip Sub group name validation is not successful");
                }
            }
            public void EditStripSubGroup()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TC02_EditStripSubGroup");

                strTestCaseNo = "TC002_Reg";

                var connection = new ConnectToSQLServer_Fetch_TestData();
                var testdataStripSubGroup = connection.Select(strtblname, strTestCaseNo, strTestType);

                strTDShortCode = testdataStripSubGroup[3];
                strTDSubGroupName = testdataStripSubGroup[4];
                strTDNewSubGroupName = testdataStripSubGroup[5];
                strTDNewShortCode = testdataStripSubGroup[6];

                FpStripSubGroupsPage.EditStripSubGroup(strTDShortCode, strTDNewShortCode, strTDNewSubGroupName);
                Thread.Sleep(3000);

                string[] strSubGroupData = FpStripSubGroupsPage.FetchSubGroupData(strTDNewShortCode);

                try
                {
                    Assert.AreEqual(strSubGroupData[0], strTDNewShortCode);
                    PropertiesCollection.test.Log(Status.Pass, "Edit Strip Sub Group => Editing of Short code is successful");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Edit Strip Sub Group => Editing of Short code is not successful");
                }
                try
                {
                    Assert.AreEqual(strSubGroupData[1], strTDNewSubGroupName);
                    PropertiesCollection.test.Log(Status.Pass, "Edit Strip Sub Group => Editing of Sub group name is successful");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Edit Strip Sub Group => Editing of Sub group name is not successful");
                }
            }
            public void VerifyErrorMessages()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TC03_VerifyErrorMessages");

                strTestCaseNo = "TC003_Reg";

                var connection = new ConnectToSQLServer_Fetch_TestData();
                var testdataStripSubGroup = connection.Select(strtblname, strTestCaseNo, strTestType);

                strTDShortCode = testdataStripSubGroup[3];
                strTDSubGroupName = testdataStripSubGroup[4];

                FpStripSubGroupsPage.NavigateToStripSubGroupPage();

                FpStripSubGroupsPage.ClickEditStripSubGroup(strTDShortCode);
                Thread.Sleep(2000);

                FpStripSubGroupsPage.ClearShortCode();
                FpStripSubGroupsPage.ClickSubGroupName();

                string expectedMsg = "A Short Code is required.";
                string actualMsg = FpStripSubGroupsPage.GetToolTip_ShortCode();

                try
                {
                    Assert.AreEqual(expectedMsg, actualMsg);
                    PropertiesCollection.test.Log(Status.Pass, "Verify Error Message => Validation for Tooltip message when short code value is null is successful");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Verify Error Message => Validation for Tooltip message when short code value is null is not successful");
                }

                FpStripSubGroupsPage.EnterShortCode(strTDShortCode);
                Thread.Sleep(2000);

                FpStripSubGroupsPage.ClearSubGroupName();
                FpStripSubGroupsPage.ClickShortCode();

                expectedMsg = "A Strip Sub Group Name is required.";
                actualMsg = FpStripSubGroupsPage.GetToolTip_SubGroupName();
                try
                {
                    Assert.AreEqual(expectedMsg, actualMsg);
                    PropertiesCollection.test.Log(Status.Pass, "Verify Error Message => Validation for Tooltip message when sub group name value is null is successful");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Verify Error Message => Validation for Tooltip message when sub group name value is null is not successful");
                }

                FpStripSubGroupsPage.EnterSubGroupName(strTDSubGroupName);

                FpStripSubGroupsPage.ClickSave();
                Thread.Sleep(2000);
            }
            public void ClickOnReturnWithoutSave()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TC04_ClickOnReturnWithoutSave");

                strTestCaseNo = "TC004_Reg";

                var connection = new ConnectToSQLServer_Fetch_TestData();
                var testdataStripSubGroup = connection.Select(strtblname, strTestCaseNo, strTestType);

                strTDShortCode = testdataStripSubGroup[3];
                strTDNewSubGroupName = testdataStripSubGroup[5];

                FpStripSubGroupsPage.NavigateToStripSubGroupPage();

                FpStripSubGroupsPage.ClickEditStripSubGroup(strTDShortCode);
                Thread.Sleep(2000);

                FpStripSubGroupsPage.ClearSubGroupName();
                Thread.Sleep(1000);

                FpStripSubGroupsPage.ClickReturn();

                string expectedMsg = "Any unsaved changes will be lost. Are you sure?";
                string actualMsg = FpStripSubGroupsPage.GetConfirmationText();
                Thread.Sleep(1000);
                try
                {
                    Assert.AreEqual(expectedMsg, actualMsg);
                    PropertiesCollection.test.Log(Status.Pass, "Click On Return Without Save => Validation for confirmation message when return button is clicked without save is successful");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Click On Return Without Save => Validation for confirmation message when return button is clicked without save is not successful");
                }

                FpStripSubGroupsPage.ClickConfirmationCancel();
                Thread.Sleep(2000);

                FpStripSubGroupsPage.EnterSubGroupName(strTDNewSubGroupName);
                FpStripSubGroupsPage.ClickSave();
                Thread.Sleep(3000);
            }

            public void DeactivateStripSubGroup()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TC05_DeactivateStripSubGroup");

                strTestCaseNo = "TC005_Reg";

                var connection = new ConnectToSQLServer_Fetch_TestData();
                var testdataStripSubGroup = connection.Select(strtblname, strTestCaseNo, strTestType);

                strTDShortCode = testdataStripSubGroup[3];

                FpStripSubGroupsPage.DeactivateStripSubGroup(strTDShortCode);

                string[] subGroupData = FpStripSubGroupsPage.FetchSubGroupData(strTDShortCode);

                try
                {
                    Assert.AreNotEqual(subGroupData[0], strTDShortCode);
                    PropertiesCollection.test.Log(Status.Pass, "Make Strip SubGroup Inactive => Deactivating Strip sub group is successful");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Make Strip SubGroup Inactive => Deactivating Strip sub group is not successful");
                }
            }

            [TestCleanup]
            public void TestCleanup()
            {
                var status = TestContext.CurrentTestOutcome;

                if (TestContext.CurrentTestOutcome != UnitTestOutcome.Passed)
                {
                    PropertiesCollection.test.Log(Status.Fail, "Test failed and aborted");
                }
                PropertiesCollection.driver.Close();
                PropertiesCollection.driver.Quit();
                PropertiesCollection.driver.Dispose();
                Thread.Sleep(5000);
            }

            [ClassCleanup]
            public static void ClassCleanup()
            {
                PropertiesCollection.extent.Flush();
            }
        }
    }
}
