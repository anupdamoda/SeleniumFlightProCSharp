using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using AventStack.ExtentReports.Utils;
using AventStack.ExtentReports.Model;
using Configuration;
using FPWebAutomation_MSTests.Database;
using FPWebAutomation_MSTests.PageObjects;
using Microsoft.TeamFoundation.TestManagement.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports.MarkupUtils;
using FPWebAutomation_MSTests.Email;
using Protractor;
using static FPWebAutomation_MSTests.TestCases.Smoke_Tests.TS01_ValidateSideMenus;
using System.Data.SqlClient;
using System.Data;

namespace FPWebAutomation_MSTests.TestCases.Smoke_Tests
{
    class TS03_Roster
    {

        [TestClass]
        public class TC03_Roster
        {

            String strTestCaseNo = "TC003";
            String strtblname = "automation_shiftadministration";
            String strTestType = "Smoke";
            

            //       private static ExtentReports extent;
            //       private static ExtentHtmlReporter htmlReporter;
            //       private static ExtentTest test;
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
                PropertiesCollection.driver = new ChromeDriver(@ConfigurationManager.AppSettings["ChromeDriverPath"]);
                PropertiesCollection.driver.Manage().Window.Maximize();
                FpLoginPage loginPage = new FpLoginPage();
                loginPage.Login();
            }


           [Priority(1)]
           [TestCategory("Smoke")]
           [TestMethod]
           [TestCategory("Roster")]
           public void TS03_TC01_ShiftAdmin_Addshift()
           {

               PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TS03_TC01_Smoke_ShiftAdmin_Addshift");

               var connection = new ConnectToMySQL_Fetch_TestData();
               var testdataShift = connection.Select(strtblname, strTestCaseNo, strTestType);
               var TopbarMenu = new clsMainPage_TopbarMenu();
               var ShiftAdministration = new clsShiftAdministration();
               TopbarMenu.NavigatetoShiftAdministration();

               String strTDShiftName = testdataShift[4];
               String strTDShortCode = testdataShift[5];
               String strTDStartTime = testdataShift[6];
               String strTDDuration = testdataShift[7];
               String strTDCurrencies = testdataShift[8];
               String strTDStatus = testdataShift[9];

               /********* Move to Roster and check if the 'Exit Edit' is enabled***********/




            /********* Delete the shift if its already present********/

            String[] shiftdetails1 = ShiftAdministration.RetrieveShiftdetails(strTDShiftName);

                Console.WriteLine(shiftdetails1[0]);
                if (shiftdetails1[0].IsNullOrEmpty() == false)
                {
                    ShiftAdministration.DeleteShiftdetails(shiftdetails1[0]);
                }

                /**********************************************************/

                ShiftAdministration.AddShiftdetails(strTDShiftName, strTDShortCode, strTDStartTime, strTDDuration, strTDCurrencies);
                String[] shiftdetails = ShiftAdministration.RetrieveShiftdetails(strTDShiftName);

                String strfbwebshiftname = shiftdetails[0];
                String strfbwebshortcode = shiftdetails[1];
                String strfbwebstarttime = shiftdetails[2];
                String strfbwebDuration = shiftdetails[3];
                String strfbwebCurrencies = shiftdetails[4];
                String strfbwebStatus = shiftdetails[5];

                try
                {
                    Assert.AreEqual(strTDShiftName, strfbwebshiftname);

                    PropertiesCollection.test.Log(Status.Pass, "Shift Name: " + strfbwebshiftname + " created and validated");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Shift Name is not matching");
                    throw;
                }

                try
                {
                    Assert.AreEqual(strTDStartTime, strfbwebstarttime);
                    PropertiesCollection.test.Log(Status.Pass, "Shift Start time: " + strfbwebstarttime + " created and validated on Shift Admin Screen");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Shift Start time is not matching on Shift Admin screen");
                    throw;
                }

                try
                {
                    Assert.AreEqual(strTDDuration, strfbwebDuration);
                    PropertiesCollection.test.Log(Status.Pass, "Shift Duration: " + strfbwebDuration + " created and validated on Shift Admin Screen");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Shift Duration is not matching on Shift Admin screen");
                    throw;
                }

                try
                {
                    Assert.AreEqual(strTDCurrencies, strfbwebCurrencies);
                    PropertiesCollection.test.Log(Status.Pass, "Currency: " + strfbwebCurrencies + " created and validated on Shift Admin Screen");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Currencies are not matching on Shift Admin screen");
                    throw;
                }

                try
                {

                    Assert.AreEqual(strTDStatus, strfbwebStatus);
                    PropertiesCollection.test.Log(Status.Pass, "Shift Status Active?: " + strfbwebStatus + " created and validated on Shift Admin screen");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Shift Status is not matching");
                    throw;
                }

            }


            [TestCategory("Smoke")]
            [Priority(2)]
            [TestMethod]
            [TestCategory("Roster")]
            public void TS03_TC02_RosterAdmin_AddRoster()
            {

                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TS03_TC02_RosterAdmin_AddRoster");
                String strtblname = "automation_rosteradministration";
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

                /********* Delete the Roster if its already present************/

                String[] rosterdetails1 = RosterAdministration.RetrieveRosterdetails(strTDRosterName);

                Console.WriteLine(rosterdetails1[0]);
                if (rosterdetails1[0].IsNullOrEmpty() == false)
                {
                    RosterAdministration.DeleteRosterdetails(rosterdetails1[0]);

                }

                /***************************************************************/

                RosterAdministration.AddRosterdetails(strTDRosterName, strTDPane, strTDTimeZoneorLocation, strTDTimeZone, strTDLocation, strTDPeople, strTDShiftDetails, strTDStatus);

                System.Threading.Thread.Sleep(6000);

                String[] rosterdetails = RosterAdministration.RetrieveRosterdetails(strTDRosterName);

                String strfbwebrostername = rosterdetails[0];
                String strfbwebpane = rosterdetails[1];
                String strfbwebshifttypes = rosterdetails[2];
                String strfbwebstatus = rosterdetails[3];

                try
                {
                    Assert.AreEqual(strTDRosterName, strfbwebrostername);
                    PropertiesCollection.test.Log(Status.Pass, "Roster Name: " + strfbwebrostername + " created and validated on Roster Admin Screen");
                }
                catch (AssertFailedException e)
                {
                    PropertiesCollection.test.Log(Status.Fail, "Roster Name is not matching");
                    throw;
                }

                try
                {
                    Assert.AreEqual(strTDPane, strfbwebpane);
                    PropertiesCollection.test.Log(Status.Pass, "Pane: " + strfbwebpane + " created and validated on Roster Admin Screen");
                }
                catch (AssertFailedException e)
                {
                    PropertiesCollection.test.Log(Status.Fail, "Pane is not matching");
                    throw;
                }

                try
                {
                    Assert.AreEqual(strTDShiftDetails, strfbwebshifttypes);
                    PropertiesCollection.test.Log(Status.Pass, "Shift Types: " + strfbwebrostername + " created and validated on Roster Admin Screen");
                }
                catch (AssertFailedException e)
                {
                    PropertiesCollection.test.Log(Status.Fail, "Shift Types is not matching");
                    throw;
                }

                try
                {
                    Assert.AreEqual(strTDStatus, strfbwebstatus);
                    PropertiesCollection.test.Log(Status.Pass, "Roster : " + strfbwebrostername + " Status is validated ");
                }
                catch (AssertFailedException e)
                {
                    PropertiesCollection.test.Log(Status.Fail, "Roster Status is not matching");
                    throw;
                }


            }


            [TestCategory("Smoke")]
            [Priority(3)]
            [TestMethod]
            [TestCategory("Roster")]
            public void TS03_TC03_Roster_ScheduleRoster()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TS03_TC03_Roster_ScheduleRoster");
                String strtblname = "automation_shiftadministration";
                String OrgRoster = "AT_Org Group1 - AT_Roster";

                var connection = new ConnectToMySQL_Fetch_TestData();
                var SidebarMenu = new FpSideMenus();
                var PersonnelScheduleRoster = new clsPersonnelSchedulingRoster();

                var testdataShift = connection.Select(strtblname, strTestCaseNo, strTestType);

                String strTDShiftName = testdataShift[4];
                String strTDShortCode = testdataShift[5];
                String strTDStartTime = testdataShift[6];
                String strTDDuration = testdataShift[7];
                String strTDCurrencies = testdataShift[8];
                String strTDStatus = testdataShift[9];

                System.Threading.Thread.Sleep(1000);
                SidebarMenu.LnkRoster.Click();

                System.Threading.Thread.Sleep(4000);

                PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"divMainBody\"]/div[2]/div[2]/div[1]/div[1]/span/span/span[2]")).Click();

                // Get all of the options
                IList<IWebElement> options = PropertiesCollection.driver.FindElements(By.XPath("//*[@id=\"divMainBody\"]/div[2]/div[2]/div[1]/div[1]/span/select/option"));
                // Loop through the options and select the one that matches

                System.Console.WriteLine("Count of options =" + options.Count);

                for (int i = 0; i < options.Count; i++)
                {
                    System.Console.WriteLine("Option" + options.ElementAt(i));
                    if (options.ElementAt(i).Text.Equals(OrgRoster))
                    {
                        options.ElementAt(i).Click();
                        break;
                    }
                }

                PersonnelScheduleRoster.ScheduleRoster(strTDShiftName, strTDShortCode, strTDStartTime, strTDDuration, strTDCurrencies);
                System.Threading.Thread.Sleep(4000);

            }

            [TestCategory("Smoke")]
            [Priority(4)]
            [TestMethod]
            [TestCategory("Roster")]
            public void TS03_TC04_Roster_RemoveRoster()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TS03_TC04_Roster_RemoveRoster");
                String strtblname = "automation_shiftadministration";
                String strtblname1 = "automation_rosteradministration";
                String OrgRoster = "FighterSQN - Test Automation";
                Int64 RosterID = 0;
                

                var connection = new ConnectToMySQL_Fetch_TestData();
                var SidebarMenu = new FpSideMenus();
                var PersonnelScheduleRoster = new clsPersonnelSchedulingRoster();

                var testdataShift = connection.Select(strtblname, strTestCaseNo, strTestType);

                String strTDShiftName = testdataShift[4];
                String strTDShortCode = testdataShift[5];
                String strTDStartTime = testdataShift[6];
                String strTDDuration = testdataShift[7];
                String strTDCurrencies = testdataShift[8];
                String strTDStatus = testdataShift[9];

                var testdataRoster = connection.Select(strtblname1, strTestCaseNo, strTestType);

                string strTDRosterName = testdataRoster[4];

                System.Threading.Thread.Sleep(1000);
                SidebarMenu.LnkRoster.Click();

                System.Threading.Thread.Sleep(4000);

                string strConnectionString = "Data Source=" + ConfigurationManager.AppSettings["SQLServerDataSource"] + ";Initial Catalog=" + ConfigurationManager.AppSettings["SQLServerInitialCatalog"] + ";User Id=" + ConfigurationManager.AppSettings["SQLServerUserId"] + ";Password=" + ConfigurationManager.AppSettings["SQLServerPassword"];
                SqlConnection myConnection = new SqlConnection(strConnectionString);
                myConnection.Open();
                SqlDataReader reader = null;
                String strQuery = "select RosterID from tblRoster where RosterName = '"+ strTDRosterName + "';";
                SqlCommand command = new SqlCommand(strQuery, myConnection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    RosterID = Convert.ToInt64(reader.GetValue(0));
                    Console.WriteLine("RosterID" + RosterID);          
                }
                reader.Close();

                string strQuery1 = "delete from tblRosterPerson where RosterID = '" + RosterID + "';";
                SqlCommand command1 = new SqlCommand(strQuery1, myConnection);
                command1.ExecuteNonQuery();
                myConnection.Close();          
                System.Threading.Thread.Sleep(4000);
            }



            [TestCategory("Smoke")]
            [Priority(5)]
            [TestMethod]
            [TestCategory("Roster")]
            public void TS03_TC05_RosterAdmin_DeleteRoster()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TS03_TC04_RosterAdmin_DeleteRoster");
                String strtblname = "automation_rosteradministration";
                string[] list;
                var connection = new ConnectToMySQL_Fetch_TestData();
                list = connection.Select(strtblname, strTestCaseNo, strTestType);
                string strTDRosterName = list[4];
                Int64 RosterID = 0;

                var TopbarMenu = new clsMainPage_TopbarMenu();
                var RosterAdministration = new clsRosterAdministration();
                TopbarMenu.NavigatetoRosterAdministration();
                
                string strConnectionString = "Data Source=" + ConfigurationManager.AppSettings["SQLServerDataSource"] + ";Initial Catalog=" + ConfigurationManager.AppSettings["SQLServerInitialCatalog"] + ";User Id=" + ConfigurationManager.AppSettings["SQLServerUserId"] + ";Password=" + ConfigurationManager.AppSettings["SQLServerPassword"];
                SqlConnection myConnection = new SqlConnection(strConnectionString);
                myConnection.Open();
                SqlDataReader reader = null;
                String strQuery = "select RosterID from tblRoster where RosterName = '" + strTDRosterName + "';";
                SqlCommand command = new SqlCommand(strQuery, myConnection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    RosterID = Convert.ToInt64(reader.GetValue(0));
                    Console.WriteLine("RosterID" + RosterID);
                }
                reader.Close();

                String strQuery1 = "delete from tblRoster where RosterID = '" + RosterID + "';";
                SqlCommand command1 = new SqlCommand(strQuery1, myConnection);
                command1.ExecuteNonQuery();
                myConnection.Close();

                System.Threading.Thread.Sleep(3000);

                string[] rosterdetails = RosterAdministration.RetrieveRosterdetails(list[4]);
                string strfbwebrostername = rosterdetails[0];

                //try
                //{
                //    Assert.IsNull(strfbwebrostername);
                //    PropertiesCollection.test.Log(Status.Pass, "Roster : " + list[4] + " is deleted and validated on Roster Admin screen");
                //}
                //catch
                //{
                //    PropertiesCollection.test.Log(Status.Fail, "Roster Status is not matching");
                //    throw;
                //}
            }

            [TestCategory("Smoke")]
            [Priority(6)]
            [TestMethod]
            [TestCategory("Roster")]
            public void TS03_TC06_ShiftAdmin_DeleteShift()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TS03_TC06_ShiftAdmin_DeleteShift");
                String strtblname = "automation_shiftadministration";
                string[] list;
                var connection = new ConnectToMySQL_Fetch_TestData();
                list = connection.Select(strtblname, strTestCaseNo, strTestType);
                string strTDShiftName = list[4];
                Int64 ShiftTypeID = 0;
                Int64 PeopleID = 0;
                Int64 RosterID = 0;
                Int64 RosterShiftTypeID = 0;
                Int64 ShiftPersonID = 0;

                var TopbarMenu = new clsMainPage_TopbarMenu();
                var ShiftAdministration = new clsShiftAdministration();
                TopbarMenu.NavigatetoShiftAdministration();
                
                string strConnectionString = "Data Source=" + ConfigurationManager.AppSettings["SQLServerDataSource"] + ";Initial Catalog=" + ConfigurationManager.AppSettings["SQLServerInitialCatalog"] + ";User Id=" + ConfigurationManager.AppSettings["SQLServerUserId"] + ";Password=" + ConfigurationManager.AppSettings["SQLServerPassword"];
                SqlConnection myConnection = new SqlConnection(strConnectionString);
                myConnection.Open();
                SqlDataReader reader = null;
                string strQuery = "select ShiftTypeID from tblShiftType where ShiftTypeName = '" + strTDShiftName + "';";
                SqlCommand command = new SqlCommand(strQuery, myConnection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ShiftTypeID = Convert.ToInt64(reader.GetValue(0));
                    Console.WriteLine("ShiftTypeID" + ShiftTypeID);
                }
                reader.Close();

                string strQuery1 = "select RosterID from tblRosterShiftType where ShiftTypeID = '" + ShiftTypeID + "';";
                SqlCommand command1 = new SqlCommand(strQuery1, myConnection);
                reader = command1.ExecuteReader();
                while (reader.Read())
                {
                    RosterID = Convert.ToInt64(reader.GetValue(0));
                    Console.WriteLine("RosterID" + RosterID);
                }
                reader.Close();

                string strQuery2 = "select RosterShiftTypeID from tblRosterShiftType where ShiftTypeID = '" + ShiftTypeID + "';";
                SqlCommand command2 = new SqlCommand(strQuery2, myConnection);
                reader = command2.ExecuteReader();
                while (reader.Read())
                {
                    RosterShiftTypeID = Convert.ToInt64(reader.GetValue(0));
                    Console.WriteLine("RosterShiftTypeID" + RosterShiftTypeID);
                }
                reader.Close();

                string strQuery3 = "select PeopleID from tblPeople where Surname = 'testuser1';";
                SqlCommand command3 = new SqlCommand(strQuery3, myConnection);
                reader = command3.ExecuteReader();
                while (reader.Read())
                {
                    PeopleID = Convert.ToInt64(reader.GetValue(0));
                    Console.WriteLine("PeopleID" + PeopleID);
                }
                reader.Close();

                string strQuery4 = "select ShiftPersonID from tblShiftPerson where PeopleID = '"+ PeopleID +"';";
                SqlCommand command4 = new SqlCommand(strQuery4, myConnection);
                reader = command4.ExecuteReader();
                while (reader.Read())
                {
                    ShiftPersonID = Convert.ToInt64(reader.GetValue(0));
                    Console.WriteLine("ShiftPersonID " + ShiftPersonID);
                }
                reader.Close();


                string strQuery5 = "delete from tblRosterShiftType where ShiftTypeID ='" + ShiftTypeID + "';";
                SqlCommand command5 = new SqlCommand(strQuery5, myConnection);
                command5.ExecuteNonQuery();

                string strQuery6 = "delete from tblShiftPersonCurrency where ShiftPersonID = '" + ShiftPersonID + "';";
                SqlCommand command6 = new SqlCommand(strQuery6, myConnection);
                command6.ExecuteNonQuery();

                string strQuery7 = "delete from tblShiftTypeCurrency where ShiftTypeID = '" + ShiftTypeID + "';";
                SqlCommand command7 = new SqlCommand(strQuery7, myConnection);
                command7.ExecuteNonQuery();

                string strQuery8 = "delete from tblShiftType where ShiftTypeName = '" + strTDShiftName + "';";
                SqlCommand command8 = new SqlCommand(strQuery8, myConnection);
                command8.ExecuteNonQuery();

                myConnection.Close();

                string[] shiftdetails = ShiftAdministration.RetrieveShiftdetails(list[4]);
                string strfbwebshiftname = shiftdetails[0];
                try
                {
                    Assert.IsNull(strfbwebshiftname);
                    PropertiesCollection.test.Log(Status.Pass, "Shift : " + list[4] + " is deleted and validated on Shift Admin screen");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Error: not deleted");
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

                Console.WriteLine("TestContext:" + TestContext.CurrentTestOutcome);
                Console.WriteLine("UnitTestOutcome.Passed" + UnitTestOutcome.Passed);
                Console.WriteLine("Status" + Status.Fail);

                if (TestContext.CurrentTestOutcome != UnitTestOutcome.Passed)
                {
                    PropertiesCollection.test.Log(Status.Fail, "Test failed and aborted");
                }
         
                System.Threading.Thread.Sleep(2000);

               
                PropertiesCollection.driver.Close();
                PropertiesCollection.driver.Quit();
                PropertiesCollection.driver.Dispose();
            }
            /*************************To be used only in case of the indivisual tests - for troubleshooting*******************************
            [ClassCleanup]
            public static void ClassCleanup()
            {
                PropertiesCollection.extent.Flush();
            }
           ******************************************************************************************************************************/
        }
    }
}
 
 
 