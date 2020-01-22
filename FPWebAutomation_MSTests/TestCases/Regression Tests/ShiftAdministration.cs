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
    public class ShiftAdministration
    {
        string strTestCaseNo;
        string strtblname;
        string strTestType;
        string strTDSelectOrgGroup;
        string strShiftName;
        string strShiftUpdatedName;
        string strUpdatedStartdate;
        string strUpdatedDuration;


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
        public void TestShiftAdministration()
        {
            AddShiftAdmin();
            VerifyErrorMessages();
            EditShiftAdmin();
            CopyShiftAdmin();
            DeleteShiftAdmin();
            NavigateShiftAdminBeforeSave();
        }

        public void AddShiftAdmin()
        {
            PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TC01_AddShiftAdmin");
            FpAdminMenus adminmenus = new FpAdminMenus();
            adminmenus.AdminClick();
            adminmenus.LnkShiftAdministration.Click();

            clsShiftAdministration shiftadmin = new clsShiftAdministration();

            strTestCaseNo = "TC001_Reg";
            strtblname = "automation_shiftadministration";
            strTestType = "Regression";

            var connection = new Database.ConnectToMySQL_Fetch_TestData();
            var testdataShift = connection.Select(strtblname, strTestCaseNo, strTestType);

            string strTDShiftName = testdataShift[4];
            string strTDShortCode = testdataShift[5];
            string strTDStartTime = testdataShift[6];
            string strTDDuration = testdataShift[7];
            string strTDCurrencies = testdataShift[8];
            string strTDStatus = testdataShift[9];

            shiftadmin.AddShiftdetails(strTDShiftName, strTDShortCode, strTDStartTime, strTDDuration, strTDCurrencies);
            string[] shiftdetails = shiftadmin.RetrieveShiftdetails(strTDShiftName);

            string strfbwebshiftname = shiftdetails[0];
            string strfbwebshortcode = shiftdetails[1];
            string strfbwebstarttime = shiftdetails[2];
            string strfbwebDuration = shiftdetails[3];
            string strfbwebCurrencies = shiftdetails[4];
            string strfbwebStatus = shiftdetails[5];
            try {
                Assert.AreEqual(strTDShiftName, strfbwebshiftname);
                PropertiesCollection.test.Log(Status.Pass,strfbwebshiftname + " Shift created Succesfully" );
            }
            catch {
                PropertiesCollection.test.Log(Status.Fail, strfbwebshiftname + " Shift not created Succesfully");
                throw;
            }
            try
            {
                Assert.AreEqual(strTDStartTime, strfbwebstarttime);
                PropertiesCollection.test.Log(Status.Pass, strfbwebstarttime + " Shift Start time validated Succesfully");
            }
            catch
            {
                PropertiesCollection.test.Log(Status.Fail, strfbwebstarttime + " Shift Start Time not validated Succesfully");
                throw;
            }
            try
            {
                Assert.AreEqual(strTDDuration, strfbwebDuration);
                PropertiesCollection.test.Log(Status.Pass, strfbwebDuration + " Shift Duration validated Succesfully");
            }
            catch
            {
                PropertiesCollection.test.Log(Status.Fail, strfbwebDuration+ " Shift Duration not validated Succesfully");
                throw;
            }
            try
            {
                Assert.AreEqual(strTDCurrencies, strfbwebCurrencies);
                PropertiesCollection.test.Log(Status.Pass, strfbwebDuration + " Shift Currencies validated Succesfully");
            }
            catch
            {
                PropertiesCollection.test.Log(Status.Fail, strfbwebCurrencies + " Shift Currencies not validated Succesfully");
                throw;
            }
            try
            {
                Assert.AreEqual(strTDStatus, strfbwebStatus);
                PropertiesCollection.test.Log(Status.Pass, strfbwebDuration + " Shift Status validated Succesfully");
            }
            catch
            {
                PropertiesCollection.test.Log(Status.Fail, strfbwebStatus + " Shift Status not validated Succesfully");
                throw;
            }
            }

        public void VerifyErrorMessages()
        {
            strTestCaseNo = "TC002_Reg";
            strtblname = "automation_shiftadministration";
            strTestType = "Regression";

            PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TC02_VerifyErrorMessages");
            FpAdminMenus adminmenus = new FpAdminMenus();
            adminmenus.AdminClick();
            adminmenus.LnkShiftAdministration.Click();

            clsShiftAdministration shiftadmin = new clsShiftAdministration();
            var connection = new Database.ConnectToMySQL_Fetch_TestData();
            var testdataShift = connection.Select(strtblname, strTestCaseNo, strTestType);

            string strTDShiftName = testdataShift[4];
            string strTDShortCode = testdataShift[5];
            string strTDStartTime = testdataShift[6];
            string strTDDuration = testdataShift[7];
            string strTDCurrencies = testdataShift[8];
            string strTDStatus = testdataShift[9];


            shiftadmin.VerifyErrorMessages(strTDShiftName, strTDShortCode, strTDStartTime, strTDDuration, strTDCurrencies);

        }

        public void EditShiftAdmin()
        {
            strTestCaseNo = "TC003_Reg";
            strtblname = "automation_shiftadministration";
            strTestType = "Regression";

            PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TC03_EditShiftAdmin");
            FpAdminMenus adminmenus = new FpAdminMenus();
            clsShiftAdministration shiftadmin = new clsShiftAdministration();
            adminmenus.AdminClick(); 
            adminmenus.LnkShiftAdministration.Click();
            if (shiftadmin.btnConfirmationOK.Displayed == true && shiftadmin.btnConfirmationOK.Enabled == true)
            {
                shiftadmin.btnConfirmationOK.Click();
            }


            var connection = new ConnectToMySQL_Fetch_TestData();
            var testdataShift = connection.Select(strtblname, strTestCaseNo, strTestType);

            strShiftName = testdataShift[4];
            strUpdatedStartdate = testdataShift[6];
            strUpdatedDuration = testdataShift[7];

            shiftadmin.EditShiftdetails(strShiftName, strUpdatedStartdate, strUpdatedDuration);
            string[] shiftdetails = shiftadmin.RetrieveShiftdetails(strShiftName);

            string strfbwebshiftname = shiftdetails[0];
            string strfbwebshortcode = shiftdetails[1];
            string strfbwebstarttime = shiftdetails[2];
            string strfbwebDuration = shiftdetails[3];
            string strfbwebCurrencies = shiftdetails[4];
            string strfbwebStatus = shiftdetails[5];

            try
            {
                Assert.AreEqual(strShiftName, strfbwebshiftname);
                PropertiesCollection.test.Log(Status.Pass, strfbwebstarttime + " Shift Name validated Succesfully");
            }
            catch{
                PropertiesCollection.test.Log(Status.Fail, strfbwebstarttime + " Shift Name not validated Succesfully");
            }
            try
            {
                Assert.AreEqual(strUpdatedStartdate, strfbwebstarttime);
                PropertiesCollection.test.Log(Status.Pass, strfbwebstarttime + " Updated Shift Start time validated Succesfully");
            }
            catch
            {
                PropertiesCollection.test.Log(Status.Fail, strfbwebstarttime + " Updated Shift Start time mot validated Succesfully");
            }
            try {
            Assert.AreEqual(strUpdatedDuration, strfbwebDuration);
                PropertiesCollection.test.Log(Status.Pass, strfbwebstarttime + "Updated Shift Duration validated Succesfully");
            }
            catch
            {
                PropertiesCollection.test.Log(Status.Fail, strfbwebstarttime + "Updated Shift Duration not validated Succesfully");
            }

            Thread.Sleep(4000);
        }

        public void CopyShiftAdmin()
        {
            strTestCaseNo = "TC004_Reg";
            strtblname = "automation_shiftadministration";
            strTestType = "Regression";

            PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TC04_CopyShiftAdmin");
            FpAdminMenus adminmenus = new FpAdminMenus();
            adminmenus.AdminClick();
            adminmenus.LnkShiftAdministration.Click();

            clsShiftAdministration shiftadmin = new clsShiftAdministration();

            var connection = new ConnectToMySQL_Fetch_TestData();
            var testdataShift = connection.Select(strtblname, strTestCaseNo, strTestType);

            strShiftName = testdataShift[4];

            shiftadmin.CopyShiftdetails(strShiftName);
            string[] shiftdetails = shiftadmin.RetrieveShiftdetails(strShiftName + " (Copy)");

            string strfbwebshiftname = shiftdetails[0];
            string strfbwebshortcode = shiftdetails[1];
            string strfbwebstarttime = shiftdetails[2];
            string strfbwebDuration = shiftdetails[3];
            string strfbwebCurrencies = shiftdetails[4];
            string strfbwebStatus = shiftdetails[5];

            try { 
            Assert.AreEqual(strShiftName + " (Copy)", strfbwebshiftname);
                PropertiesCollection.test.Log(Status.Pass, strfbwebstarttime + " Copied Shift Name validated Succesfully");
            }
            catch
            {
                PropertiesCollection.test.Log(Status.Fail, strfbwebstarttime + " Copied Shift Name not validated Succesfully");
            }
            try
            {
                Assert.AreEqual(strUpdatedStartdate, strfbwebstarttime);
                PropertiesCollection.test.Log(Status.Pass, strfbwebstarttime + " Copied Shift Start Date validated Succesfully");
            }
            catch
            {
                PropertiesCollection.test.Log(Status.Fail, strfbwebstarttime + " Copied Shift Start Date not validated Succesfully");
            }
            try
            {
                Assert.AreEqual(strUpdatedDuration, strfbwebDuration);
                PropertiesCollection.test.Log(Status.Pass, strfbwebstarttime + " Copied Shift Duration validated Succesfully");
            }
            catch
            {
                PropertiesCollection.test.Log(Status.Fail, strfbwebstarttime + " Copied Shift Duration not validated Succesfully");
            }
            Thread.Sleep(4000);
        }

        public void DeleteShiftAdmin()
        {
            strTestCaseNo = "TC005_Reg";
            strtblname = "automation_shiftadministration";
            strTestType = "Regression";

            PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TC05_DeleteShiftAdmin");
            FpAdminMenus adminmenus = new FpAdminMenus();
            adminmenus.AdminClick();
            adminmenus.LnkShiftAdministration.Click();

            clsShiftAdministration shiftadmin = new clsShiftAdministration();

            var connection = new ConnectToMySQL_Fetch_TestData();
            var testdataShift = connection.Select(strtblname, strTestCaseNo, strTestType);

            strShiftName = testdataShift[4];

            shiftadmin.DeleteShiftdetails(strShiftName);
            shiftadmin.DeleteShiftdetails(strShiftName + " (Copy)");
        }
        public void NavigateShiftAdminBeforeSave()
        {
            strTestCaseNo = "TC006_Reg";
            strtblname = "automation_shiftadministration";
            strTestType = "Regression";

            PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TC06_NavigateShiftAdminBeforeSave");
            FpAdminMenus adminmenus = new FpAdminMenus();
            adminmenus.AdminClick();
            adminmenus.LnkShiftAdministration.Click();

            clsShiftAdministration shiftadmin = new clsShiftAdministration();

            var connection = new ConnectToMySQL_Fetch_TestData();
            var testdataShift = connection.Select(strtblname, strTestCaseNo, strTestType);

            strShiftName = testdataShift[4];

            shiftadmin.NavigateShiftAdminBeforeSave(strShiftName);
            adminmenus.AdminClick();
            adminmenus.LnkShiftAdministration.Click();
            if (shiftadmin.btnConfirmationOK.Displayed == true && shiftadmin.btnConfirmationOK.Enabled == true)
            {
                shiftadmin.btnConfirmationOK.Click();
            }

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
