using FPWebAutomation_MSTests.PageObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPWebAutomation_MSTests.Email;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using AventStack.ExtentReports.Utils;
using AventStack.ExtentReports.Model;
using System.Configuration;

namespace FPWebAutomation_MSTests.TestCases.Smoke_Tests
{


    class TS01_ValidateSideMenus 
    {
        [TestClass]
        public class TC01_ValidateSideMenus
        {
            public static int _executedTests;
            public static int _passedTests;
            public TestContext TestContext { get; set; }

            public static void IncrementTests()
            {
                _executedTests++;
            }
            public static void IncrementPassedTests()
            {
                _passedTests++;
            }

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

            [TestInitialize]
            public void TestInitialize()
            {
                IncrementTests();
                PropertiesCollection.driver = new ChromeDriver(@ConfigurationManager.AppSettings["ChromeDriverPath"]);
                PropertiesCollection.driver.Manage().Window.Maximize();
                FpLoginPage loginPage = new FpLoginPage();
                loginPage.Login();
            }

            [TestCategory("Smoke")]
            [TestMethod]
            public void TS01_TC01_ValidateSideMenus()
            {

                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TS01_TC01_ValidateSideMenus");

                /* Validate Summary Page*/

                FpSideMenus SideMenu = new FpSideMenus();
                SideMenu.SummaryClick();
                System.Threading.Thread.Sleep(30000);
                FpSummaryPage Summary = new FpSummaryPage();

                try
                {
                    Assert.AreEqual(Summary.UserName.Text, "testuser2, AT");
                    PropertiesCollection.test.Log(Status.Pass, "Summary Page loaded");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Summary Page not loaded");
                    throw;
                }

                /* Validate Calendar Page*/

                Actions actions = new Actions(PropertiesCollection.driver);
                actions.MoveToElement(SideMenu.LnkCalendar).Perform();
                System.Threading.Thread.Sleep(30000);

                SideMenu.CalendarClick();
                System.Threading.Thread.Sleep(30000);

                try
                {
                    //Assert.AreEqual(FpCalendarPage.GetLoggedInUserName(), "testuser2, AT");
                    PropertiesCollection.test.Log(Status.Pass, "Calendar Page loaded");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Calendar Page not loaded");
                    throw;
                }

                /* Validate Control Hours Report Page*/

                SideMenu.ControlHoursReportClick();
                System.Threading.Thread.Sleep(30000);
                FpControlHoursReportPage ControlHoursReport = new FpControlHoursReportPage();

                try
                {
                    Assert.AreEqual(ControlHoursReport.GetReportName(), "Control Hours Report");
                    PropertiesCollection.test.Log(Status.Pass, "Control Hours Report Page loaded");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Control Hours Report Page not loaded");
                    throw;
                }

                /* Validate Currency History Report Page*/

                SideMenu.CurrencyHistoryReportClick();
                System.Threading.Thread.Sleep(30000);
                FpCurrencyHistoryReportPage CurrencyHistoryReport = new FpCurrencyHistoryReportPage();

                try
                {
                    Assert.AreEqual(CurrencyHistoryReport.GetReportName(), "Currency History Report");
                    PropertiesCollection.test.Log(Status.Pass, "Currency History Report Page loaded");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Currency History Report Page not loaded");
                    throw;
                }

                /* Validate Events Report Page*/

                SideMenu.EventsReportClick();
                System.Threading.Thread.Sleep(30000);
                FpEventsReportPage EventsReport = new FpEventsReportPage();

                try
                {
                    Assert.AreEqual(EventsReport.GetReportName(), "Events Report");
                    PropertiesCollection.test.Log(Status.Pass, "Events Report Page loaded");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Events Report Page not loaded");
                    throw;
                }

                /*Validate Roster Page */

                SideMenu.RosterClick();
                System.Threading.Thread.Sleep(30000);
                FpRosterPage Roster = new FpRosterPage();

                try
                {
                    Assert.IsTrue(Roster.RosterTable.Displayed);
                    PropertiesCollection.test.Log(Status.Pass, "Roster Page loaded");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Roster Page not loaded");
                    throw;
                }

                /* Validate Daily Scheduling Page*/

                SideMenu.DailySchedulingClick();
                System.Threading.Thread.Sleep(30000);
                FpDailySchedulingPage DailyScheduling = new FpDailySchedulingPage();

                try
                {
                    Assert.IsTrue(DailyScheduling.Table.Displayed);
                    PropertiesCollection.test.Log(Status.Pass, "Daily Scheduling Page loaded");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Daily Scheduling Page not loaded");
                    throw;
                }

                /* Validate Duty Times Page*/

                SideMenu.DutyTimesClick();
                System.Threading.Thread.Sleep(30000);
                FpControlHoursPage DutyTimes = new FpControlHoursPage();

                try
                {
                    Assert.AreEqual((DutyTimes.Title()), "testuser2, AT");
                    PropertiesCollection.test.Log(Status.Pass, "Duty Times Page loaded");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Duty Times Page not loaded");
                    throw;
                }

                /* Validate Control Hours Page*/

                SideMenu.ControlHoursClick();
                System.Threading.Thread.Sleep(30000);
                FPControlHoursPage ControlHours = new FPControlHoursPage();

                try
                {
                    Assert.AreEqual((ControlHours.Title()), "Control Hours");
                    PropertiesCollection.test.Log(Status.Pass, "Control Hours Page loaded");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Control Hours Page not loaded");
                    throw;
                }

                /* Validate Programme Viewer Page */

                SideMenu.ProgrammeViewerClick();
                System.Threading.Thread.Sleep(30000);
                FpProgrammeViewerPage ProgrammeViewer = new FpProgrammeViewerPage();

                try
                {
                    Assert.IsTrue(ProgrammeViewer.Grid.Displayed);
                    PropertiesCollection.test.Log(Status.Pass, "Programme Viewer Page loaded");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Programme Viewer Page not loaded");
                    throw;
                }

                /* Validate Knowledge Base Page */

                SideMenu.KnowledgeBaseClick();
                System.Threading.Thread.Sleep(30000);
                FpKnowledgeBasePage KnowledgeBase = new FpKnowledgeBasePage();

                try
                {
                    Assert.IsTrue(KnowledgeBase.RowValue.Displayed);
                    PropertiesCollection.test.Log(Status.Pass, "Knowledge Base Page loaded");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Knowledge Base Page not loaded");
                    throw;
                }

                /* validate status board page*/

                SideMenu.LnkStatusBoard.Click();
                System.Threading.Thread.Sleep(5000);
                FpStatusBoardPage StatusBoard = new FpStatusBoardPage();

                try
                {
                    Assert.IsTrue(StatusBoard.StatusBoard.Displayed);
                    PropertiesCollection.test.Log(Status.Pass, "Status Board Page loaded");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Status Board Page not loaded");
                    throw;
                }

                /* Validate Combined Schedule Page */

                SideMenu.CombinedScheduleClick();
                System.Threading.Thread.Sleep(30000);
                FpCombinedSchedulePage CombinedSchedule = new FpCombinedSchedulePage();
                PropertiesCollection.driver.SwitchTo().Frame(CombinedSchedule.frame);

                try
                {
                    Assert.IsTrue(CombinedSchedule.title.Displayed);
                    PropertiesCollection.test.Log(Status.Pass, "Combined Schedule Page loaded");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Combined Schedule Page not loaded");
                    throw;
                }

            }



            [TestCategory("Smoke")]
            [TestMethod]
            public void TS01_TC02_ValidateFlightProAdminMenus()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TS01_TC02_ValidateFlightProAdminMenus");

                /* Validate Strip Sub Groups Page */

                FpAdminMenus AdminMenu = new FpAdminMenus();
                AdminMenu.AdminClick();
                AdminMenu.StripSubGroupsClick();
                System.Threading.Thread.Sleep(30000);
                FpStripSubGroupsPage StripSubGroups = new FpStripSubGroupsPage();

                try
                {
                    Assert.IsTrue(StripSubGroups.Title.Displayed);
                    PropertiesCollection.test.Log(Status.Pass, "Strip Sub Groups Page loaded");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Strip Sub Groups Page not loaded");
                    throw;
                }

                /* Validate Catalogue Administration Page */

                AdminMenu.AdminClick();
                AdminMenu.CatalogueAdministrationClick();
                System.Threading.Thread.Sleep(30000);
                FpCatalogueAdministrationPage CatalogueAdministration = new FpCatalogueAdministrationPage();

                try
                {
                    Assert.IsTrue(CatalogueAdministration.Title.Displayed);
                    PropertiesCollection.test.Log(Status.Pass, "Catalogue Administration Page loaded");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Catalogue Administration Page not loaded");
                    throw;
                }

                /* Validate Organisation Group Settings Page */

                AdminMenu.AdminClick();
                AdminMenu.OrganisationGroupSettingsClick();
                System.Threading.Thread.Sleep(30000);
                FpOrganisationGroupSettingsPage OrganisationGroupSettings = new FpOrganisationGroupSettingsPage();

                try
                {
                    Assert.IsTrue(OrganisationGroupSettings.IsTitleDisplayed());
                    PropertiesCollection.test.Log(Status.Pass, "Organisation Group Settings Page loaded");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Organisation Group Settings Page not loaded");
                    throw;
                }

                /* Validate Budget Administration Page */

                AdminMenu.AdminClick();
                AdminMenu.BudgetAdministrationClick();
                System.Threading.Thread.Sleep(30000);
                FpBudgetAdministrationPage BudgetAdministration = new FpBudgetAdministrationPage();

                try
                {
                    Assert.IsTrue(BudgetAdministration.title.Displayed);
                    PropertiesCollection.test.Log(Status.Pass, "Budget Administration Page loaded");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Budget Administration Page not loaded");
                    throw;
                }

                /* Validate Asset Type Settings Page */

                AdminMenu.AdminClick();
                AdminMenu.AssetTypeSettingsClick();
                System.Threading.Thread.Sleep(30000);
                FpAssetTypeSettingsPage AssetTypeSettings = new FpAssetTypeSettingsPage();

                try
                {
                    Assert.IsTrue(AssetTypeSettings.title.Displayed);
                    PropertiesCollection.test.Log(Status.Pass, "Asset Type Settings Page loaded");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Asset Type Settings Page not loaded");
                    throw;
                }

                /* Validate Asset Type Systems Page */

                System.Threading.Thread.Sleep(30000);
                AdminMenu.AdminClick();
                AdminMenu.AssetTypeSystemsClick();
                System.Threading.Thread.Sleep(30000);
                FpAssetTypeSystemsPage AssetTypeSystems = new FpAssetTypeSystemsPage();

                try
                {
                    Assert.IsTrue(AssetTypeSystems.Title.Displayed);
                    PropertiesCollection.test.Log(Status.Pass, "Asset Type Systems Page loaded");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Asset Type Systems Page not loaded");
                    throw;
                }

                /* Validate Roster Administration Page */


                AdminMenu.AdminClick();
                AdminMenu.RosterAdministrationClick();
                System.Threading.Thread.Sleep(30000);
                FpRosterAdministrationPage RosterAdministration = new FpRosterAdministrationPage();

                try
                {
                    Assert.IsTrue(RosterAdministration.Title.Displayed);
                    PropertiesCollection.test.Log(Status.Pass, "Roster Administration Page loaded");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Roster Administration Page not loaded");
                    throw;
                }

                /* Validate Shift Administration Page */

                AdminMenu.AdminClick();
                AdminMenu.ShiftAdministrationClick();
                System.Threading.Thread.Sleep(30000);
                FpShiftAdministrationPage ShiftAdministration = new FpShiftAdministrationPage();

                try
                {
                    Assert.IsTrue(ShiftAdministration.Title.Displayed);
                    PropertiesCollection.test.Log(Status.Pass, "Shift Administration Page loaded");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Shift Administration Page not loaded");
                    throw;
                }

                /* Validate Templates Page */

                AdminMenu.AdminClick();
                AdminMenu.TemplatesClick();
                System.Threading.Thread.Sleep(30000);
                FpTemplatesPage Templates = new FpTemplatesPage();

                try
                {
                    Assert.IsTrue(Templates.Title.Displayed);
                    PropertiesCollection.test.Log(Status.Pass, "Templates Page loaded");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Templates Page not loaded");
                    throw;
                }

                /* Validate Syllabi Page */

                AdminMenu.AdminClick();
                AdminMenu.SyllabiClick();
                System.Threading.Thread.Sleep(30000);
                FpSyllabiPage Syllabi = new FpSyllabiPage();

                try
                {
                    Assert.IsTrue(Syllabi.Title.Displayed);
                    PropertiesCollection.test.Log(Status.Pass, "Syllabi Page loaded");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Syllabi Page not loaded");
                    throw;
                }

                /* Validate Courses Page */

                AdminMenu.AdminClick();
                AdminMenu.CoursesClick();
                System.Threading.Thread.Sleep(30000);
                FpCoursesPage Courses = new FpCoursesPage();

                try
                {
                    Assert.IsTrue(Courses.Title.Displayed);
                    PropertiesCollection.test.Log(Status.Pass, "Courses Page loaded");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Courses Page not loaded");
                    throw;
                }
            }

            [TestCategory("Smoke")]
            [TestCategory("ValidateStudentResultsSideMenu")]
            [TestMethod]
            public void TS01_TC03_ValidateStudentResultsSideMenu()
            {

                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TS01_TC03_ValidateStudentResultsSideMenu");

                /* Validate Students Results Page */

                FpSideMenus SideMenu = new FpSideMenus();
                SideMenu.StudentResultsClick();
                System.Threading.Thread.Sleep(30000);
                PropertiesCollection.driver.SwitchTo().Frame("myFrame");
                FpStudentResultsPage StudentResults = new FpStudentResultsPage();

                try
                {
                    Assert.IsTrue(StudentResults.UserName.Displayed);
                    PropertiesCollection. test.Log(Status.Pass, "Students Results Page loaded");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Students Results Page not loaded");
                    throw;
                }
            }

            [TestCategory("Smoke")]
            [TestCategory("ValidatePlanningBoardSideMenu")]
            [TestMethod]
            public void TS01_TC04_ValidatePlanningBoardSideMenu()
            {

                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TS01_TC04_ValidatePlanningBoardSideMenu");

                /* Validate Planning Board Page */

                FpSideMenus SideMenu = new FpSideMenus();
                SideMenu.PlanningBoardClick();
                System.Threading.Thread.Sleep(60000);
                FpPlanningBoardPage PlanningBoard = new FpPlanningBoardPage();
                PropertiesCollection.driver.SwitchTo().Frame(PlanningBoard.Frame);

                try
                {
              //      Assert.IsTrue(PlanningBoard.CboPlanningBoardName.Displayed);
              //      PropertiesCollection.test.Log(Status.Pass, " Planning Board Page loaded");
                }
                catch
                {
                //    PropertiesCollection.test.Log(Status.Fail, " Planning Board Page not loaded");
                }
            }

            [TestCategory("Smoke")]
            [TestCategory("ValidateDefinePlanningBoardsAdminMenu")]
            [TestMethod]          
            public void TS01_TC05_ValidateDefinePlanningBoardsAdminMenu()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TS01_TC05_ValidateDefinePlanningBoardsAdminMenu");

                /* Validate Define Planning Boards Page */

                FpAdminMenus AdminMenu = new FpAdminMenus();
                AdminMenu.AdminClick();
                AdminMenu.DefinePlanningBoardsClick();
                System.Threading.Thread.Sleep(30000);
                FpDefinePlanningBoardsPage DefinePlanningBoards = new FpDefinePlanningBoardsPage();
                PropertiesCollection.driver.SwitchTo().Frame(DefinePlanningBoards.frame);

                try
                {
                    Assert.IsTrue(DefinePlanningBoards.title.Displayed);
                    PropertiesCollection.test.Log(Status.Pass, "Define Planning Boards Page loaded");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Define Planning Boards Page not loaded");
                    throw;
                }
            }

            [TestCategory("Smoke")]
            [TestMethod]
            public void TS01_TC06_ValidateActivityTypesAdminMenu()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TS01_TC06_ValidateActivityTypesAdminMenu");

                /* Validate Activity Types Page */

                FpAdminMenus AdminMenu = new FpAdminMenus();
                AdminMenu.AdminClick();
                AdminMenu.ActivityTypesClick();
                System.Threading.Thread.Sleep(30000);
                FpActivityTypesPage ActivityTypes = new FpActivityTypesPage();
                PropertiesCollection.driver.SwitchTo().Frame(ActivityTypes.frame);

                try
                {
                    Assert.IsTrue(ActivityTypes.title.Displayed);
                    PropertiesCollection.test.Log(Status.Pass, "Activity Types Page loaded");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Activity Types Page not loaded");
                    throw;
                }
            }

            [TestCategory("Smoke")]
            [TestMethod]
            public void TS01_TC07_CreatePeopleUnavailability()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TS01_TC07_CreatePeopleUnavailability");

                //string PeopleUnavailabilityDescription = FpCalendarPage.CreatePeopleUnavailability("AT_Test Description");

                try
                    {
                        //Assert.AreEqual(PeopleUnavailabilityDescription, "AT_Test Description");
                        PropertiesCollection.test.Log(Status.Pass, "People Unavailability Created");
                    }
                    catch
                    {
                        PropertiesCollection.test.Log(Status.Fail, "People Unavailability not Created");
                    throw;
                    }               
            }


            [TestCleanup]
            public void TestCleanup()
            {
                if (TestContext.CurrentTestOutcome == UnitTestOutcome.Passed)
                {
                    IncrementPassedTests();
                }

                PropertiesCollection.driver.Close();
                PropertiesCollection.driver.Quit();
                PropertiesCollection.driver.Dispose();
                System.Threading.Thread.Sleep(30000);
            }
            /*
            [ClassCleanup]
            public static void ClassCleanup()
            {
                PropertiesCollection.extent.Flush();
                var email = new SendEmail();
                email.Email(_executedTests,_passedTests);
            }*/
            

        }
    }
}

