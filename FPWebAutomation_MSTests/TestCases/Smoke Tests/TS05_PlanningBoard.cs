using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using FPWebAutomation_MSTests.Database;
using FPWebAutomation_MSTests.Email;
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
using static FPWebAutomation_MSTests.TestCases.Smoke_Tests.TS01_ValidateSideMenus;

namespace FPWebAutomation_MSTests.TestCases.Smoke_Tests
{
    class TS05_PlanningBoard
    {
        [TestClass]
        public class TC05_PlanningBoard
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
           [TestCategory("PlanningBoardAddActivity")]
           [TestCategory("PlanningBoard")]
           [Priority(1)]
           [TestMethod]
           public void TS05_PlanningBoard_TC01_AddActivityTypes()
           {
               PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TS05_PlanningBoard_TC01_AddActivityTypes");

               FpActivityTypesPage ActivityTypes = new FpActivityTypesPage();

               String strTestCaseNo = "TC001";
               String strtblname = "automation_activitytype";
               String strTestType = "Smoke";

                string strTDActivityShortCode = String.Empty;
                string strTDActivityName = String.Empty;
                string strTDActivityColour = String.Empty;

                /* Get test data from MySQL */

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataActivityTypes = connection.Select(strtblname, strTestCaseNo, strTestType);

                strTDActivityShortCode = testdataActivityTypes[3];
                strTDActivityName = testdataActivityTypes[4];
                strTDActivityColour = testdataActivityTypes[5];

                ActivityTypes.AddActivitydetails(strTDActivityShortCode, strTDActivityName, strTDActivityColour);
                 //ActivityTypes.AddActivitydetails(strTDActivityShortCode, strTDActivityName);
                String[] strFPwebActivityName = ActivityTypes.RetrieveActivitydetails(strTDActivityName);

                try
                {
                    Assert.AreEqual(strTDActivityName, strFPwebActivityName[1]);
                    PropertiesCollection.test.Log(Status.Pass, "Activity: " + strFPwebActivityName[1] + " created and validated");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Activity Name: " + strTDActivityName +"and" + strFPwebActivityName[1] + "is not matching");
                    throw;
                }

            }

            [TestCategory("Smoke")]
            [TestCategory("PlanningBoard")]
            [TestCategory("PlanningBoardAddPlanningBoard")]
            [Priority(2)]
            [TestMethod]
            public void TS05_PlanningBoard_TC02_AddPlanningBoard()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TS05_PlanningBoard_TC02_AddPlanningBoard");
                                
                String strTestCaseNo = "TC001";
                String strtblname = "automation_defineplanningboard";
                String strTestType = "Smoke";

                                             
                /* Get test data from MySQL */

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataDefinePlanningBoard = connection.Select(strtblname, strTestCaseNo, strTestType);

                string strTDPlanningBoardName = testdataDefinePlanningBoard[3];
                string strTDOrganisationGroup = testdataDefinePlanningBoard[5];
                string strTDSelectOrgGroup = testdataDefinePlanningBoard[6];

                FpDefinePlanningBoardsPage PlanningBoard = new FpDefinePlanningBoardsPage();
                PlanningBoard.AddPlanningBoard(strTDPlanningBoardName, strTDOrganisationGroup, strTDSelectOrgGroup);
                String[] strFPwebPlanningBoardName = PlanningBoard.RetrievePlanningBoarddetails(strTDPlanningBoardName);

                try
                {
                    Assert.AreEqual(strTDPlanningBoardName, strFPwebPlanningBoardName[0]);
                    PropertiesCollection.test.Log(Status.Pass, "Planning Board: " + strFPwebPlanningBoardName[0] + " created and validated");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Planning Board Name is not matching");
                    throw;
                }

            }


            [TestCategory("Smoke")]
            [TestCategory("PlanningBoard")]
            [TestCategory("DefinePlanningBoard")]
            [Priority(3)]
            [TestMethod]
            public void TS05_PlanningBoard_TC03_AssignPlanningBoardToSecurityGroup()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TS05_PlanningBoard_TC03_AssignPlanningBoardToSecurityGroup");

                String strTestCaseNo = "TC001";
                String strtblname = "automation_defineplanningboard";
                String strTestType = "Smoke";

                string strTDPlanningBoardName = String.Empty;
                string strTDPlanningSecurityGroupName = String.Empty;

                /* Get test data from MySQL */

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataDefinePlanningBoard = connection.Select(strtblname, strTestCaseNo, strTestType);

                strTDPlanningBoardName = testdataDefinePlanningBoard[3];
                strTDPlanningSecurityGroupName = testdataDefinePlanningBoard[4];
                
                //var conn = new ConnectToSQLServer();
                //bool result = conn.AssignPlanningBoardToSecurityGroup(strTDPlanningBoardName, strTDPlanningSecurityGroupName);
                                            
                try
                {
                    //Assert.AreEqual(result, true);
                    PropertiesCollection.test.Log(Status.Pass, "Planning Board assigned to Security Group");            
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Pass, "Planning Board: " + strTDPlanningBoardName + " is assigned to" + strTDPlanningSecurityGroupName);
                    throw;
                }
                
            }

            [TestCategory("Smoke")]
            [TestCategory("PlanningBoard")]
            [TestCategory("PlanningBoardCreateActivity")]
            [Priority(4)]
            [TestMethod]
            public void TS05_PlanningBoard_TC04_CreateActivity()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TS05_PlanningBoard_TC04_CreateActivity");

                String strTestCaseNo = "TC001";
                String strtblname = "automation_planningboard";
                String strTestType = "Smoke";

                /* Get test data from MySQL */

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataDefinePlanningBoard = connection.Select(strtblname, strTestCaseNo, strTestType);

                string strTDActivityCode = testdataDefinePlanningBoard[3];
                string strTDActivityName = testdataDefinePlanningBoard[4];
                string strTDOrganisationGroupName = testdataDefinePlanningBoard[5];
                               
                FpSideMenus SideMenu = new FpSideMenus();
                System.Threading.Thread.Sleep(30000);
                SideMenu.PlanningBoardClick();
                System.Threading.Thread.Sleep(60000);
                FpPlanningBoardPage PlanningBoard = new FpPlanningBoardPage();
                PropertiesCollection.driver.SwitchTo().Frame(PlanningBoard.Frame);
                PlanningBoard.TabActivityView.Click();
                System.Threading.Thread.Sleep(5000);
                PlanningBoard.TabDaily.Click();
                System.Threading.Thread.Sleep(5000);
               /* PlanningBoard.CboPlanningBoardName.Click();
                System.Threading.Thread.Sleep(15000);
                PlanningBoard.CboPlanningBoardName.SendKeys(strTDOrganisationGroupName);
                PlanningBoard.PlanningBoardSelection.Click();
*/
                try
                {
                    if (PlanningBoard.BtnClickHere.Displayed == true)
                    {                        
                        PlanningBoard.BtnClickHere.Click();
                        PlanningBoard.CreateActivity(strTDActivityCode, strTDActivityName, strTDOrganisationGroupName, "Test");
                    }
                    else if (PlanningBoard.BtnClickHere.Displayed != true)
                    {
                        Actions action = new Actions(PropertiesCollection.driver);
                        action.MoveToElement(PlanningBoard.GridRowSelection).DoubleClick().Perform();
                        System.Threading.Thread.Sleep(5000);
                        PlanningBoard.CreateActivity(strTDActivityCode, strTDActivityName, strTDOrganisationGroupName, "Automation Planning Board");
                    }                    
                }
                finally
                {
                    try
                    {
                        Assert.IsTrue(PlanningBoard.Activity.Displayed);
                        PropertiesCollection.test.Log(Status.Pass, "Activity Created Successfully");
                    }
                    catch
                    {
                        PropertiesCollection.test.Log(Status.Fail, "Activity not created");
                        throw;
                    }
                }
                
            }

            [TestCategory("Smoke")]
            [TestCategory("PlanningBoardCreateTaskPushToPane")]
            [TestCategory("PlanningBoard")]
            [Priority(5)]
            [TestMethod]
            public void TS05_PlanningBoard_TC05_CreateTask_PushToPane()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TS05_PlanningBoard_TC05_CreateTask_PushToPane");

                String strTestCaseNo = "TC001";
                String strtblname = "automation_planningboard";
                String strTestType = "Smoke";

                
                /* Get test data from MySQL */

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataDefinePlanningBoard = connection.Select(strtblname, strTestCaseNo, strTestType);

                string strTDOrganisationGroupName = testdataDefinePlanningBoard[5];
                string strTDTaskCode = testdataDefinePlanningBoard[6];
                string strTDAssetCode = testdataDefinePlanningBoard[7];
                string strTDShiftCode = testdataDefinePlanningBoard[8];
               
                

                FpPlanningBoardPage PlanningBoard = new FpPlanningBoardPage();
                PlanningBoard.CreateTask(strTDTaskCode, strTDAssetCode, strTDShiftCode);
                             

                try
                {
                    Assert.IsTrue(PlanningBoard.TaskPackageSelection.Displayed);
                    PropertiesCollection.test.Log(Status.Pass, "Task Package Created Successfully");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Task Package not created");
                    throw;
                }

                PlanningBoard.PushTaskToPane(strTDTaskCode, strTDOrganisationGroupName);
                System.Threading.Thread.Sleep(60000);
              
                try
                {
                    Assert.IsTrue(PlanningBoard.PushtoPaneTitle.Displayed);
                    PropertiesCollection.test.Log(Status.Pass, "Task Package pushed to Pane Successfully");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Task Package not pushed to Pane");
                    throw;
                }
                PlanningBoard.BtnOK.Click();
                System.Threading.Thread.Sleep(15000);
                PlanningBoard.BtnCancel.Click();
                System.Threading.Thread.Sleep(15000);
            }


            [TestCategory("Smoke")]
            [TestCategory("PlanningBoard")]
            [TestCategory("DeleteActivity")]
            [Priority(6)]
            [TestMethod]
            public void TS05_PlanningBoard_TC06_DeleteActivity()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TS05_PlanningBoard_TC06_DeleteActivity");

                String strTestCaseNo = "TC001";
                String strtblname = "automation_planningboard";
                String strTestType = "Smoke";

                string strTDActivityCode = String.Empty;
                string strTDActivityName = String.Empty;
                string strTDOrganisationGroupName = String.Empty;

                /* Get test data from MySQL */

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataDefinePlanningBoard = connection.Select(strtblname, strTestCaseNo, strTestType);

                strTDOrganisationGroupName = testdataDefinePlanningBoard[8];

                FpPlanningBoardPage PlanningBoard = new FpPlanningBoardPage();
                PlanningBoard.DeleteActivity(strTDOrganisationGroupName);
                
                try
                {

                    Assert.IsFalse(PlanningBoard.ActivitySelected.Displayed);
                    PropertiesCollection.test.Log(Status.Pass, "Activity deleted successfully on Planning Board");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Activity not deleted on Planning Board");
                    throw;
                }
            }


            [TestCategory("Smoke")]
          //  [Priority(7)]
            [TestMethod]
            [TestCategory("DeletePlanningBoard")]
            [TestCategory("PlanningBoard")]
            public void TS05_PlanningBoard_TC07_DeletePlanningBoard()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TS05_PlanningBoard_TC07_DeletePlanningBoard");

                FpAdminMenus AdminMenu = new FpAdminMenus();
                AdminMenu.AdminClick();
                AdminMenu.DefinePlanningBoardsClick();
                System.Threading.Thread.Sleep(30000);
                FpDefinePlanningBoardsPage PlanningBoard = new FpDefinePlanningBoardsPage();
                PropertiesCollection.driver.SwitchTo().Frame(PlanningBoard.frame);

                String strTestCaseNo = "TC001";
                String strtblname = "automation_defineplanningboard";
                String strTestType = "Smoke";

                /* Get test data from MySQL */

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataDefinePlanningBoard = connection.Select(strtblname, strTestCaseNo, strTestType);

                string strTDPlanningBoardName = testdataDefinePlanningBoard[3];
                string strTDOrganisationGroup = testdataDefinePlanningBoard[5];

                PlanningBoard.DeletePlanningBoard(strTDPlanningBoardName, strTDOrganisationGroup);


                String[] PlanningBoarddetails = PlanningBoard.RetrievePlanningBoarddetails(strTDPlanningBoardName);

                String strFPwebPlanningBoardName = PlanningBoarddetails[1];

                try
                {

                    Assert.IsNull(strFPwebPlanningBoardName);
                    PropertiesCollection.test.Log(Status.Pass, "Planning Board is deleted on Define Planning Board Admin screen");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Planning Board not deleted");
                    throw;
                }

            }

            [TestCategory("Smoke")]
            [Priority(8)]
            [TestMethod]
            [TestCategory("PlanningBoard")]
            [TestCategory("DeleteActivityType")]
            public void TS05_PlanningBoard_TC08_DeleteActivityType()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TS05_PlanningBoard_TC08_DeleteActivityType");

                FpAdminMenus AdminMenu = new FpAdminMenus();
                AdminMenu.AdminClick();
                AdminMenu.ActivityTypesClick();
                System.Threading.Thread.Sleep(30000);
                System.Threading.Thread.Sleep(30000);
                FpActivityTypesPage ActivityTypes = new FpActivityTypesPage();
                PropertiesCollection.driver.SwitchTo().Frame(ActivityTypes.frame);
                              

                String strTestCaseNo = "TC001";
                String strtblname = "automation_activitytype";
                String strTestType = "Smoke";

                string strTDActivityName = String.Empty;


                /* Get test data from MySQL */

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataActivityTypes = connection.Select(strtblname, strTestCaseNo, strTestType);
                                               
                strTDActivityName = testdataActivityTypes[4];
                ActivityTypes.DeleteActivity(strTDActivityName);
                
                String[] Activitydetails = ActivityTypes.RetrieveActivitydetails(strTDActivityName);
                String strFPwebActivityName = Activitydetails[1];
                Console.WriteLine(strFPwebActivityName);
                try
                {

                    Assert.IsNull(strFPwebActivityName);
                    PropertiesCollection.test.Log(Status.Pass, "Activity Type is deleted on Activity Types Admin screen");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Activity Type not deleted");
                    throw;
                }
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
                    PropertiesCollection.test.Log(Status.Fail, "Test failed and aborted");
                }

                System.Threading.Thread.Sleep(4000);

                PropertiesCollection.driver.Close();
                PropertiesCollection.driver.Quit();
                PropertiesCollection.driver.Dispose();
            }


            /*************************To be used only in case of the individual tests - for troubleshooting*******************************
           [ClassCleanup]
           public static void ClassCleanup()
           {
               PropertiesCollection.extent.Flush();
                //var email = new SendEmail();
                //email.Email();
            }
            *******************************************************************************************************************************/

        }


    }
}
