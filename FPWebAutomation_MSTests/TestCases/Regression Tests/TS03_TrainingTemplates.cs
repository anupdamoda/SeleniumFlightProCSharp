using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Configuration;
using OpenQA.Selenium.Chrome;
using FPWebAutomation_MSTests.PageObjects;
using static FPWebAutomation_MSTests.Database.ConnectToMySQL_Fetch_TestData;
using FPWebAutomation_MSTests.Database;

namespace FPWebAutomation_MSTests.TestCases.Regression_Tests
{
    [TestClass]
    public class TS03_TrainingTemplates
    {
        string strTestCaseNo = "TC003";
        string strtblname = "automation_templates";
        string strTestType = "Regression";

        [TestMethod]
        public void TC01_AddTemplates()
        {
            
        }

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            PropertiesCollection.driver = new ChromeDriver(@ConfigurationManager.AppSettings["ChromeDriverPath"]);
            PropertiesCollection.driver.Manage().Window.Maximize();
            FpLoginPage loginPage = new FpLoginPage();
            loginPage.Login();
        }


        [TestMethod]
        public void TC03_Admin_AddTemplates()
        {

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

            RosterAdministration.AddRosterdetails(strTDRosterName, strTDPane, strTDTimeZoneorLocation, strTDTimeZone, strTDLocation, strTDPeople, strTDShiftDetails, strTDStatus);

            System.Threading.Thread.Sleep(2000);

            String[] rosterdetails = RosterAdministration.RetrieveRosterdetails(strTDRosterName);

            String strfbwebrostername = rosterdetails[0];
            String strfbwebpane = rosterdetails[1];
            String strfbwebshifttypes = rosterdetails[2];
            String strfbwebstatus = rosterdetails[3];


            Assert.AreEqual(strTDRosterName, strfbwebrostername);
            Assert.AreEqual(strTDPane, strfbwebpane);
            Assert.AreEqual(strTDShiftDetails, strfbwebshifttypes);
            Assert.AreEqual(strTDStatus, strfbwebstatus);

        }

        [TestMethod]
        public void TC06_RosterAdmin_DeleteRoster()
        {

            string[] list;
            var connection = new ConnectToMySQL_Fetch_TestData();
            list = connection.Select(strtblname, strTestCaseNo, strTestType);
            var TopbarMenu = new clsMainPage_TopbarMenu();
            var RosterAdministration = new clsRosterAdministration();
            TopbarMenu.NavigatetoRosterAdministration();
            RosterAdministration.DeleteRosterdetails(list[4]);

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

