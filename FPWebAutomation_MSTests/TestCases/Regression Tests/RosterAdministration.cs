using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using Configuration;
using FPWebAutomation_MSTests.PageObjects;
using FPWebAutomation_MSTests.Database;
using System.Threading;
using AventStack.ExtentReports.Reporter;
using System.IO;
using AventStack.ExtentReports;

namespace FPWebAutomation_MSTests.TestCases.Regression_Tests
{
    [TestClass]
    public class RosterAdministration
    {

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
            Thread.Sleep(3000);
        }

        [TestMethod]
        [TestCategory("Regression")]
        public void TestRosterAdministration()
        {
            AddRosterAdmin();
            VerifyErrorMessages();
            EditRosterAdmin();
            DeleteRosterAdmin();
            NavigateRosterAdminBeforeSave();
        }

        public void AddRosterAdmin()
        {
            strTestCaseNo = "TC001_Reg";
            strtblname = "automation_rosteradministration";
            strTestType = "Regression";

            PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TC01_AddRosterAdmin");           
            var connection = new Database.ConnectToMySQL_Fetch_TestData();
            var testdataRoster = connection.Select(strtblname, strTestCaseNo, strTestType);
            var TopbarMenu = new clsMainPage_TopbarMenu();

            var RosterAdministration = new clsRosterAdministration();
            TopbarMenu.NavigatetoRosterAdministration();

            String strTDRosterName = testdataRoster[4];
            String strTDPane = testdataRoster[5];
            String strTDTimeZoneorLocation = testdataRoster[6];
            String strTDTimeZone = testdataRoster[7];
            String strTDLocation = testdataRoster[8];
            String strTDPeople = testdataRoster[9];
            String strTDShiftDetails = testdataRoster[10];
            String strTDStatus = testdataRoster[11];

            Console.WriteLine("RosterName" + strTDRosterName);

            RosterAdministration.AddRosterdetails(strTDRosterName, strTDPane, strTDTimeZoneorLocation, strTDTimeZone, strTDLocation, strTDPeople, strTDShiftDetails,strTDStatus);

            System.Threading.Thread.Sleep(2000);

            String[] rosterdetails = RosterAdministration.RetrieveRosterdetails(strTDRosterName);

            String strfbwebrostername = rosterdetails[0];
            String strfbwebpane = rosterdetails[1];
            String strfbwebshifttypes = rosterdetails[2];
            String strfbwebstatus = rosterdetails[3];

            try
            {
                Assert.AreEqual(strTDRosterName, strfbwebrostername);
                PropertiesCollection.test.Log(Status.Pass, strfbwebrostername + " Roster created Succesfully");
            }
            catch
            {
                PropertiesCollection.test.Log(Status.Fail, strfbwebrostername + " Roster not created Succesfully");
            }
            try
            {
                Assert.AreEqual(strTDPane, strfbwebpane);
                PropertiesCollection.test.Log(Status.Pass, strfbwebpane + " Pane validated Succesfully");
            }
            catch
            {
                PropertiesCollection.test.Log(Status.Pass, strfbwebpane + " Pane not validated Succesfully");
            }
            try
            {
                Assert.AreEqual(strTDShiftDetails, strfbwebshifttypes);
                PropertiesCollection.test.Log(Status.Pass, strfbwebshifttypes + " Shift Type Validated Succesfully");
            }
            catch
            {
                PropertiesCollection.test.Log(Status.Pass, strfbwebshifttypes + " Shift Type not Validated Succesfully");
            }
            try
            {
                Assert.AreEqual(strTDStatus, strfbwebstatus);
                PropertiesCollection.test.Log(Status.Pass, strfbwebstatus + " Status validated Succesfully");
            }
            catch
            {
                PropertiesCollection.test.Log(Status.Pass, strfbwebstatus + " Status not validated Succesfully");
            }
            
           
            
        }
        public void VerifyErrorMessages()
        {
            string strTestCaseNo = "TC002_Reg";
            strTestType = "Regression";
            PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TC02_VerifyErrorMessages");
            string[] list;
            var connection = new ConnectToMySQL_Fetch_TestData();
            var testdataRoster = connection.Select(strtblname, strTestCaseNo, strTestType);
            list = connection.Select(strtblname, strTestCaseNo, strTestType);
            var TopbarMenu = new clsMainPage_TopbarMenu();
            System.Threading.Thread.Sleep(2000);
            var RosterAdministration = new clsRosterAdministration();
            TopbarMenu.NavigatetoRosterAdministration();
            String strTDRosterName = testdataRoster[4];
            String strTDPane = testdataRoster[5];
            String strTDTimeZoneorLocation = testdataRoster[6];
            String strTDTimeZone = testdataRoster[7];
            String strTDLocation = testdataRoster[8];
            String strTDPeople = testdataRoster[9];
            String strTDShiftDetails = testdataRoster[10];
            String strTDStatus = testdataRoster[11];

            RosterAdministration.VerifyErrorMessages(strTDRosterName, strTDPane, strTDTimeZoneorLocation, strTDTimeZone, strTDLocation, strTDPeople, strTDShiftDetails, strTDStatus);
        }

        public void EditRosterAdmin()
        {
            string strTestCaseNo = "TC003_Reg";
            strTestType = "Regression";
            PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TC03_EditRosterAdmin");
            string[] list;
            var connection = new ConnectToMySQL_Fetch_TestData();
            var testdataRoster = connection.Select(strtblname, strTestCaseNo, strTestType);
            list = connection.Select(strtblname, strTestCaseNo, strTestType);
            var TopbarMenu = new clsMainPage_TopbarMenu();
            System.Threading.Thread.Sleep(2000);
            var RosterAdministration = new clsRosterAdministration();           
            TopbarMenu.NavigatetoRosterAdministration();
            string strTDRosterName = testdataRoster[4];
            string strTDShiftDetails = testdataRoster[10];

            RosterAdministration.EditRosterdetails(strTDRosterName,strTDShiftDetails);

            string[] rosterdetails = RosterAdministration.RetrieveRosterdetails(strTDRosterName);

            string strfbwebrostername = rosterdetails[0];
            string strfbwebshifttypes = rosterdetails[2];

            try
            {
                Assert.AreEqual(strTDRosterName, strfbwebrostername);
                PropertiesCollection.test.Log(Status.Pass, strfbwebrostername + " Updated Roster Validated successfully");
            }
            catch
            {
                PropertiesCollection.test.Log(Status.Fail, strfbwebrostername + " Updated Roster not Validated successfully");
            }
            try
            {
                Assert.AreEqual(strTDShiftDetails, strfbwebshifttypes);
                PropertiesCollection.test.Log(Status.Pass, strfbwebshifttypes + " Updated ShiftTypes validated successfully");
            }
            catch
            {
                PropertiesCollection.test.Log(Status.Fail, strfbwebshifttypes + " Updated ShiftTypes not validated successfully");
            }

        }

        public void DeleteRosterAdmin()
        {
            string strTestCaseNo = "TC004_Reg";
            strTestType = "Regression";
            PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TC04_DeleteRosterAdmin");
            string[] list;
            var connection = new ConnectToMySQL_Fetch_TestData();
            list = connection.Select(strtblname, strTestCaseNo, strTestType);
            var TopbarMenu = new clsMainPage_TopbarMenu();
            var RosterAdministration = new clsRosterAdministration();
            TopbarMenu.NavigatetoRosterAdministration();
            RosterAdministration.DeleteRosterdetails(list[4]);       
        }
        public void NavigateRosterAdminBeforeSave()
        {
            string strTestCaseNo = "TC005_Reg";
            strTestType = "Regression";
            PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TC05_NavigateRosterAdminBeforeSave");
            string[] list;
            var connection = new ConnectToMySQL_Fetch_TestData();
            var testdataRoster = connection.Select(strtblname, strTestCaseNo, strTestType);
            list = connection.Select(strtblname, strTestCaseNo, strTestType);
            var TopbarMenu = new clsMainPage_TopbarMenu();
            var RosterAdministration = new clsRosterAdministration();
            TopbarMenu.NavigatetoRosterAdministration();

            string strTDRosterName = testdataRoster[4];

            RosterAdministration.NavigateRosterAdminBeforeSave(strTDRosterName);

        }

        [TestCleanup]
        public void TestCleanup()
        {
            var TopbarMenu = new clsMainPage_TopbarMenu();
            TopbarMenu.Logout();
            PropertiesCollection.driver.Close();
            PropertiesCollection.driver.Quit();
            PropertiesCollection.driver.Dispose();
        }
    }
}
