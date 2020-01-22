/* Regression Test Case -- Flight Pro Web -> Admin > Panes and Boards -> Define Planning Boards  
   Author -- Vandana Kalluru
   Dated - 5th Nov 2019*/

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
    class DefinePlanningBoard
    {
        [TestClass]
        public class PlanningBoard
        {
            public TestContext TestContext { get; set; }
            string strTestCaseNo;
            string strtblname;
            string strTestType;
            string strTDPlanningBoardName;
            string strTDOrganisationGroup;
            string strTDSelectOrgGroup;

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
            [TestCategory("Regression")]
            public void TestPlanningBoards()
            {
                AddPlanningBoard();
                //EditPlanningBoard();
                //VerifyErrorMessages();
                //ReturnWithOutSave();
                DeletePlanningBoard();               
            }

            public void AddPlanningBoard()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TC01_AddPlanningBoard");
                FpDefinePlanningBoardsPage planningBoardsPage = new FpDefinePlanningBoardsPage();

                strTestCaseNo = "TC001_Reg";
                strtblname = "automation_defineplanningboard";
                strTestType = "Regression";

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataDefinePlanningBoard = connection.Select(strtblname, strTestCaseNo, strTestType);

                strTDPlanningBoardName = testdataDefinePlanningBoard[3];
                strTDOrganisationGroup = testdataDefinePlanningBoard[5];
                strTDSelectOrgGroup = testdataDefinePlanningBoard[6];

                planningBoardsPage.AddPlanningBoard(strTDPlanningBoardName, strTDOrganisationGroup, strTDSelectOrgGroup);
                Thread.Sleep(5000);
                String[] strFPwebPlanningBoardName = planningBoardsPage.RetrievePlanningBoarddetails(strTDPlanningBoardName);

                try
                {
                    Assert.AreEqual(strTDPlanningBoardName, strFPwebPlanningBoardName[0]);
                    PropertiesCollection.test.Log(Status.Pass, "ADD PLANNING BOARD => Planning Board: " + strFPwebPlanningBoardName[0] + " created and validated");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "ADD PLANNING BOARD => Planning Board creation has failed");
                }
            }

            public void EditPlanningBoard()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TC02_EditPlanningBoard");
                FpDefinePlanningBoardsPage planningBoardsPage = new FpDefinePlanningBoardsPage();

                strTestCaseNo = "TC002_Reg";
                strtblname = "automation_defineplanningboard";
                strTestType = "Regression";

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataDefinePlanningBoard = connection.Select(strtblname, strTestCaseNo, strTestType);

                strTDPlanningBoardName = testdataDefinePlanningBoard[3];
                strTDOrganisationGroup = testdataDefinePlanningBoard[5];

                string SupportingOrgGroup = planningBoardsPage.EditPlanningBoard(strTDPlanningBoardName, strTDOrganisationGroup);

                try
                {
                    Assert.AreEqual(SupportingOrgGroup, "true");
                    PropertiesCollection.test.Log(Status.Pass, "EDIT PLANNING BOARD => Planning Board: " + strTDPlanningBoardName + " has been edited successfully");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "EDIT PLANNING BOARD => Planning Board editing has failed");
                }

                Thread.Sleep(4000);
            }

            public void VerifyErrorMessages()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TC03_VerifyErrorMessages");
                FpDefinePlanningBoardsPage planningBoardsPage = new FpDefinePlanningBoardsPage();

                strTestCaseNo = "TC003_Reg";
                strtblname = "automation_defineplanningboard";
                strTestType = "Regression";

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataDefinePlanningBoard = connection.Select(strtblname, strTestCaseNo, strTestType);

                strTDPlanningBoardName = testdataDefinePlanningBoard[3];
                strTDOrganisationGroup = testdataDefinePlanningBoard[5];
                string strTDEditPlanningBoardName = testdataDefinePlanningBoard[7];

                planningBoardsPage.btnOrgGroupSelector.Click();
                Thread.Sleep(3000);
                planningBoardsPage.txtOrganisationGroup.SendKeys(strTDOrganisationGroup);
                Thread.Sleep(2000);
                planningBoardsPage.cboOrganisationGroupSelection.Click();
                Thread.Sleep(3000);
                planningBoardsPage.btnApply.Click();
                Thread.Sleep(5000);

                for (int i = 0; i < planningBoardsPage.txtAllPlanningBoardName.Count; i++)
                {
                    if (planningBoardsPage.txtAllPlanningBoardName.ElementAt(i).Text.Equals(strTDPlanningBoardName))
                    {
                        planningBoardsPage.btnEditPlanningBoard.ElementAt(i).Click();
                        break;
                    }
                }
                Thread.Sleep(8000);
                planningBoardsPage.txtName.SendKeys(strTDEditPlanningBoardName);

                string errorMessage = planningBoardsPage.txtErrorMessage.Text;
                string expectedErrorMessage = "Name cannot be greater than 50 characters.";

                try
                {
                    Assert.AreEqual(errorMessage, expectedErrorMessage);
                    PropertiesCollection.test.Log(Status.Pass, "VERIFY ERROR MESSAGE => Error message received has been validated successfully on Edit Planning board page");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "VERIFY ERROR MESSAGE => Validation failed for Error message received on Edit Planning board page");
                }

                Thread.Sleep(5000);
                planningBoardsPage.txtName.Clear();
                planningBoardsPage.txtName.SendKeys(strTDPlanningBoardName);
                planningBoardsPage.btnSave.Click();
                Thread.Sleep(5000);
            }

            public void ReturnWithOutSave()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TC04_ReturnWithOutSave");
                FpDefinePlanningBoardsPage planningBoardsPage = new FpDefinePlanningBoardsPage();

                strTestCaseNo = "TC004_Reg";
                strtblname = "automation_defineplanningboard";
                strTestType = "Regression";

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataDefinePlanningBoard = connection.Select(strtblname, strTestCaseNo, strTestType);

                strTDPlanningBoardName = testdataDefinePlanningBoard[3];
                strTDOrganisationGroup = testdataDefinePlanningBoard[5];

                planningBoardsPage.btnOrgGroupSelector.Click();
                Thread.Sleep(3000);
                planningBoardsPage.txtOrganisationGroup.SendKeys(strTDOrganisationGroup);
                Thread.Sleep(2000);
                planningBoardsPage.cboOrganisationGroupSelection.Click();
                Thread.Sleep(3000);
                planningBoardsPage.btnApply.Click();
                Thread.Sleep(5000);

                for (int i = 0; i < planningBoardsPage.txtAllPlanningBoardName.Count; i++)
                {
                    if (planningBoardsPage.txtAllPlanningBoardName.ElementAt(i).Text.Equals(strTDPlanningBoardName))
                    {
                        planningBoardsPage.btnEditPlanningBoard.ElementAt(i).Click();
                        break;
                    }
                }
                Thread.Sleep(10000);
                planningBoardsPage.chkSupportingOrgGroups.Click();
                planningBoardsPage.btnReturn.Click();
                Thread.Sleep(5000);

                string errorMessage = planningBoardsPage.txtConfirmationMsg.Text;
                string expectedErrorMessage = "Any unsaved changes will be lost. Are you sure?";

                try
                {
                    Assert.AreEqual(errorMessage, expectedErrorMessage);
                    PropertiesCollection.test.Log(Status.Pass, "CLICK ON RETURN BEFORE SAVE => Validation for confirmation message received when returning to planning board list before save has been successful");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "CLICK ON RETURN BEFORE SAVE => Validation for confirmation message received when returning to planning board list before save has not been successful");
                }

                planningBoardsPage.btnConfirmationCancel.Click();
                Thread.Sleep(2000);
                planningBoardsPage.btnSave.Click();
                Thread.Sleep(5000);
            }

            public void DeletePlanningBoard()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TC05_DeletePlanningBoard");
                FpDefinePlanningBoardsPage planningBoardsPage = new FpDefinePlanningBoardsPage();

                strTestCaseNo = "TC005_Reg";
                strtblname = "automation_defineplanningboard";
                strTestType = "Regression";

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataDefinePlanningBoard = connection.Select(strtblname, strTestCaseNo, strTestType);

                strTDPlanningBoardName = testdataDefinePlanningBoard[3];
                strTDOrganisationGroup = testdataDefinePlanningBoard[5];

                PropertiesCollection.driver.Navigate().Refresh();
                planningBoardsPage.DeletePlanningBoard(strTDPlanningBoardName, strTDOrganisationGroup);

                Thread.Sleep(3000);
                string[] strFPwebPlanningBoardName = planningBoardsPage.RetrievePlanningBoarddetails(strTDPlanningBoardName);

                try
                {
                    Assert.AreNotEqual(strTDPlanningBoardName, strFPwebPlanningBoardName);
                    PropertiesCollection.test.Log(Status.Pass, "DELETE PLANNING BOARD => Planning Board: " + strTDPlanningBoardName + " has been deleted");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "DELETE PLANNING BOARD => Planning Board deletion has failed");
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
                email.Email();
            }
        }
    }
}
