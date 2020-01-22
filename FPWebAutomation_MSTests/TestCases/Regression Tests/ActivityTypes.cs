/* Regression Test Case -- Flight Pro Web -> Admin > Panes and Boards > Activity Types
   Author -- Vandana Kalluru
   Dated - 11th Nov 2019*/

using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using FPWebAutomation_MSTests.Database;
using FPWebAutomation_MSTests.Email;
using FPWebAutomation_MSTests.PageObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;

namespace FPWebAutomation_MSTests.TestCases.Regression_Tests
{
    class ActivityTypes
    {
        [TestClass]
        public class ActivityTypesAdministration
        {
            public TestContext TestContext { get; set; }
            string strTestCaseNo;
            string strtblname;
            string strTestType;
            string strTDShortCode;
            string strTDActivityTypeName;
            string strTDColour;

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
            public void ActivityTypes()
            {
                AddActivityType();
                VerifyErrorMessage();
                EditActivityType();
                ReturnWithOutSave();
                DeleteActivityType();
            }

            public void AddActivityType()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TC01_AddActivityType");
                FpActivityTypesPage activityTypesPage = new FpActivityTypesPage();

                strTestCaseNo = "TC001_Reg";
                strtblname = "automation_activitytype";
                strTestType = "Regression";

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataActivityBoard = connection.Select(strtblname, strTestCaseNo, strTestType);

                string strTDShortCode = testdataActivityBoard[3];
                string strTDActivityTypeName = testdataActivityBoard[4];
                string strTDColour = testdataActivityBoard[5];

                activityTypesPage.AddActivitydetails(strTDShortCode, strTDActivityTypeName, strTDColour);

                Thread.Sleep(5000);
                string[] strFPActivityName = activityTypesPage.RetrieveActivitydetails(strTDActivityTypeName);

                try
                {
                    Assert.AreEqual(strFPActivityName[1], strTDActivityTypeName);
                    PropertiesCollection.test.Log(Status.Pass, "ADD ACTIVITY TYPE => Activity Type: " + strTDActivityTypeName + " created and validated");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "ADD ACTIVITY TYPE => Activity Type creation has failed");
                }
            }

            public void VerifyErrorMessage()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TC02_VerifyErrorMessage");
                FpActivityTypesPage activityTypesPage = new FpActivityTypesPage();

                strTestCaseNo = "TC002_Reg";
                strtblname = "automation_activitytype";
                strTestType = "Regression";

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataActivityBoard = connection.Select(strtblname, strTestCaseNo, strTestType);

                strTDActivityTypeName = testdataActivityBoard[4];
                string strTDEditText = testdataActivityBoard[6];

                for (int i = 0; i < activityTypesPage.txtAllActivityName.Count; i++)
                {
                    if (activityTypesPage.txtAllActivityName.ElementAt(i).Text.Equals(strTDActivityTypeName))
                    {
                        activityTypesPage.btnEditActivityType.ElementAt(i).Click();
                        break;
                    }
                }
                Thread.Sleep(8000);

                activityTypesPage.txtName.Clear();
                activityTypesPage.txtName.SendKeys(strTDEditText);

                Thread.Sleep(3000);

                string errorMessage = activityTypesPage.txtErrorMessage.Text;
                string expectedErrorMessage = "Name cannot be greater than 50 characters.";

                try
                {
                    Assert.AreEqual(errorMessage, expectedErrorMessage);
                    PropertiesCollection.test.Log(Status.Pass, "VERIFY ERROR MESSAGE => Error message received has been validated successfully on Edit Activity Types page");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "VERIFY ERROR MESSAGE => Validation failed for Error message received on Edit Activity Types page");
                }

                Thread.Sleep(5000);
                activityTypesPage.txtName.Clear();
                activityTypesPage.txtName.SendKeys(strTDActivityTypeName);
                activityTypesPage.btnSave.Click();
                Thread.Sleep(5000);
            }

            public void EditActivityType()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TC03_EditActivityType");
                FpActivityTypesPage activityTypesPage = new FpActivityTypesPage();

                strTestCaseNo = "TC003_Reg";
                strtblname = "automation_activitytype";
                strTestType = "Regression";

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataActivityBoard = connection.Select(strtblname, strTestCaseNo, strTestType);

                strTDActivityTypeName = testdataActivityBoard[4];
                string strTDEditText = testdataActivityBoard[6];

                for (int i = 0; i < activityTypesPage.txtAllActivityName.Count; i++)
                {
                    if (activityTypesPage.txtAllActivityName.ElementAt(i).Text.Equals(strTDActivityTypeName))
                    {
                        activityTypesPage.btnEditActivityType.ElementAt(i).Click();
                        break;
                    }
                }
                Thread.Sleep(8000);

                activityTypesPage.txtName.Clear();
                activityTypesPage.txtName.SendKeys(strTDEditText);
                activityTypesPage.btnSave.Click();
                Thread.Sleep(5000);

                string modifiedActivityTypeName = "";

                for (int i = 0; i < activityTypesPage.txtAllActivityName.Count; i++)
                {
                    if (activityTypesPage.txtAllActivityName.ElementAt(i).Text.Equals(strTDEditText))
                    {
                        modifiedActivityTypeName = activityTypesPage.txtAllActivityName.ElementAt(i).Text;
                        break;
                    }
                }

                try
                {
                    Assert.AreEqual(modifiedActivityTypeName, strTDEditText);
                    PropertiesCollection.test.Log(Status.Pass, "EDIT ACTIVITY TYPE => Acitivity type name has been edited successfully. New name for Activity type is: " + modifiedActivityTypeName);
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "EDIT ACTIVITY TYPE => Acitivity type editing has failed");
                }
            }

            public void ReturnWithOutSave()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TC04_ReturnWithOutSave");
                FpActivityTypesPage activityTypesPage = new FpActivityTypesPage();

                strTestCaseNo = "TC004_Reg";
                strtblname = "automation_activitytype";
                strTestType = "Regression";

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataActivityBoard = connection.Select(strtblname, strTestCaseNo, strTestType);

                strTDActivityTypeName = testdataActivityBoard[4];
                string strTDEditText = testdataActivityBoard[6];

                for (int i = 0; i < activityTypesPage.txtAllActivityName.Count; i++)
                {
                    if (activityTypesPage.txtAllActivityName.ElementAt(i).Text.Equals(strTDActivityTypeName))
                    {
                        activityTypesPage.btnEditActivityType.ElementAt(i).Click();
                        break;
                    }
                }
                Thread.Sleep(8000);

                activityTypesPage.txtName.Clear();
                activityTypesPage.txtName.SendKeys(strTDEditText);
                activityTypesPage.btnReturn.Click();

                string errorMessage = activityTypesPage.txtConfirmationMsg.Text;
                string expectedErrorMessage = "Any unsaved changes will be lost. Are you sure?";

                try
                {
                    Assert.AreEqual(errorMessage, expectedErrorMessage);
                    PropertiesCollection.test.Log(Status.Pass, "ACTIVITY TYPES EDIT - CLICK ON RETURN BEFORE SAVE => Validation for confirmation message received when returning to activity type list before save has been successful");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "ACTIVITY TYPES EDIT - CLICK ON RETURN BEFORE SAVE => Validation for confirmation message received when returning to activity type list before save has not been successful");
                }

                activityTypesPage.btnCancel.Click();
                Thread.Sleep(2000);
                activityTypesPage.btnSave.Click();
                Thread.Sleep(5000);
            }

            public void DeleteActivityType()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TC05_DeleteActivityType");
                FpActivityTypesPage activityTypesPage = new FpActivityTypesPage();

                strTestCaseNo = "TC005_Reg";
                strtblname = "automation_activitytype";
                strTestType = "Regression";

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataActivityBoard = connection.Select(strtblname, strTestCaseNo, strTestType);

                strTDActivityTypeName = testdataActivityBoard[4];

                activityTypesPage.DeleteActivity(strTDActivityTypeName);
                string[] strActivityTypesName = activityTypesPage.RetrieveActivitydetails(strTDActivityTypeName);
                Console.WriteLine("Activity type name retrieved from web is: " + strActivityTypesName[1]);

                Thread.Sleep(4000);

                try
                {
                    Assert.AreNotEqual(strActivityTypesName[1], strTDActivityTypeName);
                    PropertiesCollection.test.Log(Status.Pass, "DELETE ACTIVITY TYPE => Acitivity type: " + strTDActivityTypeName +" has been deleted successfully.");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "DELETE ACTIVITY TYPE => Acitivity type: " + strTDActivityTypeName + "  Deletion has failed");
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
                Thread.Sleep(3000);
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

