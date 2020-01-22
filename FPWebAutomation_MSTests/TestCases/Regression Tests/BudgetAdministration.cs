/* Regression Test Case -- FlightPro Web -> Admin > Organisation > Budget Administration Page 
   Author -- Vandana Kalluru
   Dated - 19th Nov 2019*/

using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using FPWebAutomation_MSTests.Database;
using FPWebAutomation_MSTests.Email;
using FPWebAutomation_MSTests.PageObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading;

namespace FPWebAutomation_MSTests.TestCases.Regression_Tests
{
    class Budget
    {
        [TestClass]
        public class BudgetAdministration
        {
            public TestContext TestContext { get; set; }
            string strTestCaseNo;
            string strtblname;
            string strTestType;
            SqlConnection sqlCon;
            string connectionString;

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
            public void TestBudgetAdministration()
            {
                AddBudget();
                ViewBudget();
                EditBudget();
                VerifyErrorMessages();
                ReturnWithOutSave();
                DeleteBudget();                
            }
           
            public void AddBudget()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TC01_AddBudget");
                FpBudgetAdministrationPage budgetAdministrationPage = new FpBudgetAdministrationPage();
               
                strTestCaseNo = "TC001_Reg";
                strtblname = "automation_budgetadministration";
                strTestType = "Regression";

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataBudget = connection.Select(strtblname, strTestCaseNo, strTestType);

                string strBudgetName = testdataBudget[3];
                string strDescription = testdataBudget[4];
                string strOrgGroup = testdataBudget[5];
                string strPerson = testdataBudget[6];
                string strPane = testdataBudget[7];
                string strSubGroup = testdataBudget[8];
                string strAssetType = testdataBudget[9];
                string strStripType = testdataBudget[10];
                string strDateFrom = testdataBudget[11];
                string strDateTo = testdataBudget[12];
                string strAllocation = testdataBudget[13];

                budgetAdministrationPage.AddBudget(strBudgetName, strDescription, strOrgGroup, strPerson, strPane, strSubGroup, strAssetType, strStripType, strDateFrom, strDateTo, strAllocation);

                string[] budgetDetails = budgetAdministrationPage.RetrieveBudgetDetails(strBudgetName);

                try
                {
                    Assert.AreEqual(strBudgetName, budgetDetails[0]);
                    PropertiesCollection.test.Log(Status.Pass, "ADD BUDGET => BUDGET: " + strBudgetName + " created and validated");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "ADD BUDGET => BUDGET: " + strBudgetName + " created has failed");
                }
            }

            public void ViewBudget()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TC02_ViewBudget");
                FpBudgetAdministrationPage budgetAdministrationPage = new FpBudgetAdministrationPage();
                
                strTestCaseNo = "TC002_Reg";
                strtblname = "automation_budgetadministration";
                strTestType = "Regression";

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataBudget = connection.Select(strtblname, strTestCaseNo, strTestType);

                string strBudgetName = testdataBudget[3];
                string strOrgGroup = testdataBudget[5];

                try
                {
                    budgetAdministrationPage.ViewBudget(strOrgGroup, strBudgetName);
                    PropertiesCollection.test.Log(Status.Pass, "VIEW BUDGET => BUDGET: " + strBudgetName + " is successful");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Pass, "VIEW BUDGET => BUDGET: " + strBudgetName + " is unsuccessful");
                }
            }

            public void EditBudget()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TC03_EditBudget");
                FpBudgetAdministrationPage budgetAdministrationPage = new FpBudgetAdministrationPage();

                strTestCaseNo = "TC003_Reg";
                strtblname = "automation_budgetadministration";
                strTestType = "Regression";
                Int64 intDBAllocation = 0;
                string strDBBudgetDesc = "";

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataBudget = connection.Select(strtblname, strTestCaseNo, strTestType);

                string strBudgetName = testdataBudget[3];
                string strDescription = testdataBudget[4];
                string strOrgGroup = testdataBudget[5];               
                string strAllocation = testdataBudget[13];

                budgetAdministrationPage.EditBudget(strOrgGroup, strBudgetName, strDescription, strAllocation);

                connectionString = "Data Source=" + ConfigurationManager.AppSettings["SQLServerDataSource"] + ";Initial Catalog=" + ConfigurationManager.AppSettings["SQLServerInitialCatalog"] + ";Integrated Security=" + ConfigurationManager.AppSettings["SQLServerIntegratedSecurity"] + ';';
                sqlCon = new SqlConnection(connectionString);
                sqlCon.Open();

                string query = "select Description,Allocation From tblBudget where BudgetName = '" + strBudgetName + "'";
                SqlCommand command = new SqlCommand(query, sqlCon);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    strDBBudgetDesc = reader.GetString(0);
                    intDBAllocation = reader.GetInt64(1);                  
                }

                reader.Close();

                try
                {
                    Assert.AreEqual(strDBBudgetDesc, strDescription);
                    PropertiesCollection.test.Log(Status.Pass, "EDIT BUDGET => BUDGET: " + strBudgetName + " description edit is successful");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "EDIT BUDGET => BUDGET: " + strBudgetName + " description edit is not successful");
                }
                try
                {
                    intDBAllocation = intDBAllocation / 60;
                    int allocation = Int32.Parse(strAllocation);
                    Assert.AreEqual(intDBAllocation, allocation);
                    PropertiesCollection.test.Log(Status.Pass, "EDIT BUDGET => BUDGET: " + strBudgetName + " allocation edit is successful");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "EDIT BUDGET => BUDGET: " + strBudgetName + " allocation edit is not successful");
                }

            }

            public void VerifyErrorMessages()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TC04_VerifyErrorMessage");
                FpBudgetAdministrationPage budgetAdministrationPage = new FpBudgetAdministrationPage();

                strTestCaseNo = "TC004_Reg";
                strtblname = "automation_budgetadministration";
                strTestType = "Regression";

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataBudget = connection.Select(strtblname, strTestCaseNo, strTestType);

                string strBudgetName = testdataBudget[3];
                string strOrgGroup = testdataBudget[5];
                string strAllocation = testdataBudget[13];

                budgetAdministrationPage.NavigateToBudgetAdminPage();

                budgetAdministrationPage.cboOrgGroupSelector.Click();

                Thread.Sleep(5000);

                for (int i = 0; i < budgetAdministrationPage.cboOrgGroupName.Count; i++)
                {
                    if (budgetAdministrationPage.cboOrgGroupName.ElementAt(i).Text.Equals(strOrgGroup))
                    {
                        budgetAdministrationPage.cboOrgGroupName.ElementAt(i).Click();
                        break;
                    }
                }

                Thread.Sleep(4000);

                for (int i = 0; i < budgetAdministrationPage.lblBudgetName.Count; i++)
                {
                    if (budgetAdministrationPage.lblBudgetName.ElementAt(i).Text.Equals(strBudgetName))
                    {
                        budgetAdministrationPage.lstEditBudget.ElementAt(i).Click();
                        break;
                    }
                }

                Thread.Sleep(4000);

                budgetAdministrationPage.txtBudgetName.Clear();
                Thread.Sleep(2000);                

                try
                {
                    Assert.AreEqual(budgetAdministrationPage.txtBudgetName_ErrorMsg.Text, "Budget Name is required");
                    PropertiesCollection.test.Log(Status.Pass, "VERIFY ERROR MESSAGE ON EDIT BUDGET SCREEN => Error message validation when budget name is null is successful ");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "VERIFY ERROR MESSAGE ON EDIT BUDGET SCREEN => Error message validation when budget name is null is not successful ");
                }
               
                budgetAdministrationPage.txtBudgetName.SendKeys(strBudgetName);
                Thread.Sleep(3000);

                budgetAdministrationPage.txtAllocation.Clear();
                Thread.Sleep(3000);

                try
                {
                    Assert.AreEqual(budgetAdministrationPage.txtAllocation_ErrorMsg.Text, "A valid Allocation is required.");
                    PropertiesCollection.test.Log(Status.Pass, "VERIFY ERROR MESSAGE ON EDIT BUDGET SCREEN => Error message validation when Allocation has null value is successful ");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "VERIFY ERROR MESSAGE ON EDIT BUDGET SCREEN => Error message validation when Allocation has null value is not successful ");
                }

                budgetAdministrationPage.txtAllocation.SendKeys(strAllocation);
                Thread.Sleep(2000);

                budgetAdministrationPage.btnSave.Click();
                Thread.Sleep(3000);
            }

            public void ReturnWithOutSave()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TC05_ReturnWithOutSave");
                FpBudgetAdministrationPage budgetAdministrationPage = new FpBudgetAdministrationPage();

                strTestCaseNo = "TC005_Reg";
                strtblname = "automation_budgetadministration";
                strTestType = "Regression";

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataBudget = connection.Select(strtblname, strTestCaseNo, strTestType);

                string strBudgetName = testdataBudget[3];
                string strOrgGroup = testdataBudget[5];

                budgetAdministrationPage.NavigateToBudgetAdminPage();

                budgetAdministrationPage.cboOrgGroupSelector.Click();

                Thread.Sleep(5000);

                for (int i = 0; i < budgetAdministrationPage.cboOrgGroupName.Count; i++)
                {
                    if (budgetAdministrationPage.cboOrgGroupName.ElementAt(i).Text.Equals(strOrgGroup))
                    {
                        budgetAdministrationPage.cboOrgGroupName.ElementAt(i).Click();
                        break;
                    }
                }

                Thread.Sleep(4000);

                for (int i = 0; i < budgetAdministrationPage.lblBudgetName.Count; i++)
                {
                    if (budgetAdministrationPage.lblBudgetName.ElementAt(i).Text.Equals(strBudgetName))
                    {
                        budgetAdministrationPage.lstEditBudget.ElementAt(i).Click();
                        break;
                    }
                }

                Thread.Sleep(4000);

                budgetAdministrationPage.txtBudgetName.Clear();
                Thread.Sleep(2000);

                string mainWindow = PropertiesCollection.driver.CurrentWindowHandle;

                budgetAdministrationPage.btnReturn.Click();          

                string childWindow = PropertiesCollection.driver.CurrentWindowHandle;

                PropertiesCollection.driver.SwitchTo().Window(childWindow);

                Thread.Sleep(5000);

                String errorText = budgetAdministrationPage.txtConfirmationMessage.Text;

                try
                {
                    String ExpectedErrorMessage = "Any unsaved changes will be lost. Are you sure?";
                    Assert.AreEqual(ExpectedErrorMessage, errorText);
                    PropertiesCollection.test.Log(Status.Pass, "RETURN WITHOUT SAVE => Validation for message received when navigating to a different window without save has passed");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "RETURN WITHOUT SAVE => Validation for message received when navigating to a different window without save has not passed");
                }
                budgetAdministrationPage.btnCancel.Click();

                Thread.Sleep(2000);                
                PropertiesCollection.driver.SwitchTo().Window(mainWindow);

                budgetAdministrationPage.txtBudgetName.SendKeys(strBudgetName);
                Thread.Sleep(2000);
                budgetAdministrationPage.btnSave.Click();
                Thread.Sleep(3000);
            }

            public void DeleteBudget()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TC06_DeleteBudget");
                FpBudgetAdministrationPage budgetAdministrationPage = new FpBudgetAdministrationPage();

                strTestCaseNo = "TC006_Reg";
                strtblname = "automation_budgetadministration";
                strTestType = "Regression";

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataBudget = connection.Select(strtblname, strTestCaseNo, strTestType);

                string strBudgetName = testdataBudget[3];
                string strOrgGroup = testdataBudget[5];

                Console.WriteLine(strBudgetName);
                Console.WriteLine(strOrgGroup);

                budgetAdministrationPage.DeleteBudget(strOrgGroup, strBudgetName);

                string[] strBudgetDetails = budgetAdministrationPage.RetrieveBudgetDetails(strBudgetName);

                try
                {
                    Console.WriteLine("Budget name retrieved is: " + strBudgetDetails);
                    Assert.AreNotEqual(strBudgetDetails, strBudgetName);
                    PropertiesCollection.test.Log(Status.Pass, "DELETE BUDGET => Budget Deletion is successful");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "DELETE BUDGET => Budget Deletion is not successful");
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
