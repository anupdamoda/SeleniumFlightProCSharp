/* Regression Test Case -- FlightPro Web -> Admin > Organisation > Organisation Group Settings
   Author -- Vandana Kalluru
   Dated - 27th Nov 2019*/

using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using FPWebAutomation_MSTests.Database;
using FPWebAutomation_MSTests.Email;
using FPWebAutomation_MSTests.PageObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using System.Configuration;
using System.IO;
using System.Threading;

namespace FPWebAutomation_MSTests.TestCases.Regression_Tests
{
    class OrganisationGroupSettings
    {
        [TestClass]
        public class OrgGroupSettings
        {
            public TestContext TestContext { get; set; }
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
            }

            [TestMethod]
            [TestCategory("Regression")]
            public void OrgGroupSettingsVerification()
            {
                EditOrgGroupSettings();
                VerifyErrorMessage();
                ReturnWithOutSave();
            }

            public string[] FetchTestData(string testCaseNo)
            {
                string[] testData = new string[11];

                strtblname = "automation_organisationgroupsettings";
                strTestType = "Regression";

                var connection = new ConnectToMySQL_Fetch_TestData();
                testData = connection.Select(strtblname, testCaseNo, strTestType);

                return testData;
            }

            public void EditOrgGroupSettings()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TC01_EditAssetSystem");

                strTestCaseNo = "TC001_Reg";
                string[] testDataOrgGroup = FetchTestData(strTestCaseNo);

                string orgGroup = testDataOrgGroup[3];
                string durationFormat = testDataOrgGroup[4];
                string breakDuration = testDataOrgGroup[5];
                string maxConsecutiveTasking = testDataOrgGroup[6];
                string standDown = testDataOrgGroup[7];
                string acknowledgementTime = testDataOrgGroup[8];
                string acknowledgementFor = testDataOrgGroup[9];
                string peopleGroup = testDataOrgGroup[10];

                FpOrganisationGroupSettingsPage.EditOrgGroupSettings(orgGroup, durationFormat, breakDuration, maxConsecutiveTasking, standDown, acknowledgementTime, acknowledgementFor, peopleGroup);

                Thread.Sleep(5000);

                string[] webData = FpOrganisationGroupSettingsPage.FetchValuesAfterEdit(orgGroup);

                try
                {
                    Assert.AreEqual(webData[0], breakDuration);
                    PropertiesCollection.test.Log(Status.Pass, "Editing of Break Duration value is successful");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Editing of Break Duration value is not successful");
                }
                try
                {
                    Assert.AreEqual(webData[1], maxConsecutiveTasking);
                    PropertiesCollection.test.Log(Status.Pass, "Editing of Max ConsecutiveTasking value is successful");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Editing of Max ConsecutiveTasking value is not successful");
                }
                try
                {
                    Assert.AreEqual(webData[2], standDown);
                    PropertiesCollection.test.Log(Status.Pass, "Editing of Stand down value is successful");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Editing of Stand down value is not successful");
                }
                try
                {
                    Assert.AreEqual(webData[3], "Yes");
                    PropertiesCollection.test.Log(Status.Pass, "Editing of Currency Audit value is successful");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Editing of Currency Audit value is not successful");
                }
                try
                {
                    Assert.AreEqual(webData[4], "Yes");
                    PropertiesCollection.test.Log(Status.Pass, "Editing of Event Acknowledgement value is successful");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Editing of Event Acknowledgement value is not successful");
                }
            }

            public void VerifyErrorMessage()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TC02_VerifyErrorMessage");

                strTestCaseNo = "TC002_Reg";
                string[] testDataOrgGroup = FetchTestData(strTestCaseNo);

                string orgGroup = testDataOrgGroup[3];
                string acknowledgementTime = testDataOrgGroup[8];
                string acknowledgementFor = testDataOrgGroup[9];

                FpOrganisationGroupSettingsPage.NavigateToOrgGroupSettingsPage();
                Thread.Sleep(3000);

                FpOrganisationGroupSettingsPage.ClickEditOrgGroup(orgGroup);
                Thread.Sleep(3000);

                FpOrganisationGroupSettingsPage.ClearAcknowledgementTime();
                FpOrganisationGroupSettingsPage.ClickBreakDuration();
                Thread.Sleep(2000);

                string errorMsg = FpOrganisationGroupSettingsPage.GetErrorMessageForAckTime();
                string expectedMsg = "An Acknowledgement Time is required.";

                try
                {
                    Assert.AreEqual(errorMsg, expectedMsg);
                    PropertiesCollection.test.Log(Status.Pass, "Validation for error message thrown when Acknowledgement time is null is successful");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Validation for error message thrown when Acknowledgement time is null is not successful");
                }

                FpOrganisationGroupSettingsPage.EnterAcknowledgementTime(acknowledgementTime);
                Thread.Sleep(2000);

                FpOrganisationGroupSettingsPage.ClearAcknowledgementDays();
                FpOrganisationGroupSettingsPage.ClickBreakDuration();
                Thread.Sleep(2000);

                errorMsg = FpOrganisationGroupSettingsPage.GetErrorMessageForAckFor();
                expectedMsg = "An Acknowledgement For is required.";

                try
                {
                    Assert.AreEqual(errorMsg, expectedMsg);
                    PropertiesCollection.test.Log(Status.Pass, "Validation for error message thrown when Acknowledgement for is null is successful");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Validation for error message thrown when Acknowledgement for is null is not successful");
                }

                FpOrganisationGroupSettingsPage.EnterAcknowledgementDays(acknowledgementFor);
                Thread.Sleep(2000);

                FpOrganisationGroupSettingsPage.ClickSave();

                Thread.Sleep(2000);
            }

            public void ReturnWithOutSave()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TC03_ReturnWithoutSave");

                strTestCaseNo = "TC003_Reg";
                string[] testDataOrgGroup = FetchTestData(strTestCaseNo);
                
                string orgGroup = testDataOrgGroup[3];
                string breakDuration = testDataOrgGroup[5];

                FpOrganisationGroupSettingsPage.NavigateToOrgGroupSettingsPage();
                Thread.Sleep(3000);

                FpOrganisationGroupSettingsPage.ClickEditOrgGroup(orgGroup);
                Thread.Sleep(3000);
                FpOrganisationGroupSettingsPage.ClearBreakDuration();
                Thread.Sleep(2000);
                FpOrganisationGroupSettingsPage.ClickReturn();
                Thread.Sleep(3000);

                string confirmationMsg = FpOrganisationGroupSettingsPage.GetConfirmationText();
                string expectedMsg = "Any unsaved changes will be lost. Are you sure?";

                try
                {
                    Assert.AreEqual(confirmationMsg, expectedMsg);
                    PropertiesCollection.test.Log(Status.Pass, "Validation for confirmation message received when Return button is clicked without Save is successful");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Validation for confirmation message received when Return button is clicked without Save is not successful");
                }

                FpOrganisationGroupSettingsPage.ClickConfirmationCancel();
                Thread.Sleep(2000);

                FpOrganisationGroupSettingsPage.EnterBreakDuration(breakDuration);
                Thread.Sleep(2000);

                FpOrganisationGroupSettingsPage.ClickSave();
                Thread.Sleep(3000);
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
                var email = new SendEmail();
                //email.Email();
            }
        }
    }
}
