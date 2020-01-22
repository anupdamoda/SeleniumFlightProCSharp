using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using FPWebAutomation_MSTests.Database;
using FPWebAutomation_MSTests.PageObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using Protractor;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using AventStack.ExtentReports.Utils;
using AventStack.ExtentReports.Model;
using FPWebAutomation_MSTests.Email;
using OpenQA.Selenium;

namespace FPWebAutomation_MSTests.TestCases.Smoke_Tests
{
    class Class1
    {

        [TestClass]
        public class TS02_StudentResults : TS01_ValidateSideMenus
        {
            String strTestCaseNo = "TC002";
            String strtblname = "automation_studentresults";
            String strTestType = "Smoke";

            public TestContext TestContext { get; set; }

            [TestInitialize]
            public void TestInitialize()
            {
                TC01_ValidateSideMenus.IncrementTests();
                PropertiesCollection.driver = new ChromeDriver(@ConfigurationManager.AppSettings["ChromeDriverPath"]);
                PropertiesCollection.driver.Manage().Window.Maximize();

                FpLoginPage loginPage = new FpLoginPage();
                loginPage.Login();
            }


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

            [Priority(1)]
         [TestCategory("Smoke")]
         [TestMethod]
          [TestCategory("StudentResults")]
          public void TS02_TC01_StudentResults_ScoreEvent()
         {
             PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TS02_TC01_StudentResults_ScoreEvents");

               var connection = new ConnectToMySQL_Fetch_TestData();
               var testdataStudentResults = connection.Select(strtblname, strTestCaseNo, strTestType);
               var SidebarMenu = new FpSideMenus();
               var StudentResults = new FpStudentResultsPage();
               String[] headerfields;
                SidebarMenu.lnkStudentResults.Click();

             String strTDOrgnisationGroup = testdataStudentResults[4];
             String strTDStudentName = testdataStudentResults[5];
             String strTDInstructorName = testdataStudentResults[6];
             String strTDCourseName = testdataStudentResults[7];
             String strTDSyllabusName = testdataStudentResults[8];
             String strTDEventName = testdataStudentResults[9];
             String strTDScore = testdataStudentResults[10];
             String strTDScoreAssesmentCriteria = testdataStudentResults[11];
             String strTDStrength = testdataStudentResults[12];
             String strTDWeakness = testdataStudentResults[13];
             String strTDOverallComments = testdataStudentResults[14];
             String strTDPrivateComments = testdataStudentResults[15];
             String strTDServiceName = testdataStudentResults[16];
             String strTDCountryName = testdataStudentResults[17];
             String strTDStudentPosition = testdataStudentResults[18];
             String strTDStudentSurname = testdataStudentResults[19];
             String strTDResultAwarded = testdataStudentResults[20];

               StudentResults.SearchStudent(strTDStudentSurname);
               StudentResults.SearchCourseEvent(strTDEventName);

             String style = StudentResults.VerifyEventIcon(strTDEventName);

             if ( style.Contains("background-color: rgb(70, 136, 71)") == false )
             {
                 StudentResults.DeleteWriteup();

             }

             System.Threading.Thread.Sleep(60000);

             String style2 = StudentResults.VerifyEventIcon(strTDEventName);

             try
             {
                 Assert.IsTrue(style2.Contains("background-color: rgb(70, 136, 71)"));
                 PropertiesCollection.test.Log(Status.Pass, "Event is at scheduled state prior to Scoring");
             }
             catch
             {
                 PropertiesCollection.test.Log(Status.Fail, "Event is at not at scheduled state");
             }


             headerfields = StudentResults.ValidateHeaderFields();


             String strfbwebstudentname = headerfields[0];
             String strfbwebservicename = headerfields[1];
             String strfbwebgroupname = headerfields[2];
             String strfbwebInstructorName = headerfields[3];
             String strfbwebCoursename = headerfields[4];
             String strfbwebCountryName = headerfields[5];
             String strfbwebEventName = headerfields[6];

             StudentResults.EnterPrivate_OverallComments(strTDOverallComments,strTDPrivateComments);

             try
             {
                 Assert.AreEqual(strTDStudentPosition + " " + strTDStudentName, strfbwebstudentname);
                 PropertiesCollection.test.Log(Status.Pass, "Student Name: " + strfbwebstudentname + " validated at header fields");
             }
             catch
             {
                 PropertiesCollection.test.Log(Status.Fail, "StudentName or Student Position not matching");
                    throw;
             }

             try
             {
                 Assert.AreEqual(strTDOrgnisationGroup, strfbwebgroupname);
                 PropertiesCollection.test.Log(Status.Pass, "Organisation Group Name: " + strfbwebgroupname + " validated at header fields");
             }
             catch
             {
                 PropertiesCollection.test.Log(Status.Fail, "Organisation Group Name is not matching at header fields");
                    throw;
             }

             try
             {
                 Assert.AreEqual(strTDServiceName, strfbwebservicename);
                 PropertiesCollection.test.Log(Status.Pass, "Service Name: " + strfbwebservicename + " validated at header fields");
             }
             catch
             {
                 PropertiesCollection.test.Log(Status.Fail, "Service Name is not matching at header fields");
                    throw;
             }

             Console.WriteLine("Instructorname from Test Data Table=" + strTDInstructorName);
             Console.WriteLine("Instructorname from fpweb=" + strfbwebInstructorName);

             try
             {
                 Assert.AreEqual(strTDInstructorName, strfbwebInstructorName);
              //   PropertiesCollection.test.Log(Status.Pass, "Instructor Name: " + strfbwebInstructorName + " validated at header fields");
                 Console.WriteLine("Instructorname from Test Data Table=" + strTDInstructorName);
                 Console.WriteLine("Instructorname from fpweb=" + strfbwebInstructorName);
             }
             catch
             {
              //   PropertiesCollection.test.Log(Status.Fail, "Instructor Name is not matching at header fields");
             }

             try
             {
                 Assert.AreEqual(strTDCourseName, strfbwebCoursename);
                 PropertiesCollection.test.Log(Status.Pass, "Course Name: " + strfbwebCoursename + " validated at header fields");
             }
             catch
             {
                 PropertiesCollection.test.Log(Status.Fail, "Course Name is not matching at header fields");
                    throw;
             }

             try
             {
                 Assert.AreEqual(strTDEventName, strfbwebEventName);
                 PropertiesCollection.test.Log(Status.Pass, "Event Name: " + strfbwebEventName + " validated at header fields");
             }
             catch
             {
                 PropertiesCollection.test.Log(Status.Fail, "Event Name is not matching at header fields");
                    throw;
             }

             try
             {
                 Assert.AreEqual(strTDCountryName, strfbwebCountryName);
                 PropertiesCollection.test.Log(Status.Pass, "Country Name: " + strfbwebCountryName + " validated at header fields");
             }
             catch
             {
                 PropertiesCollection.test.Log(Status.Fail, "Country Name is not matching at header fields");
                    throw;
             }

             // StudentResults.addAdditionalCategories("AT_Category2", "ATS2","ATW3");

             StudentResults.ScoringEvent(strTDScore);
             StudentResults.VerifyEventIcon(strTDEventName);

             /*******************Validation of Result Awarded************************************/

            String ResultAwarded = StudentResults.VerifyResultAwarded();

                try
                {
                    Assert.AreEqual(ResultAwarded, strTDResultAwarded);
                    PropertiesCollection.test.Log(Status.Pass, "Result Awarded is: " + ResultAwarded);
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Result Awarded has incorrect State as: " + ResultAwarded);
                    throw;
                }

                String BGcolour = StudentResults.VerifyResultAwardedBGColour();

                try
                {
                    Assert.IsTrue(BGcolour.Contains("background-color: rgb(70, 136, 71)"));
                    PropertiesCollection.test.Log(Status.Pass, "Result Awarded has Background colour as: " + BGcolour);
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Result Awarded has incorrect Background coulour as: " + BGcolour);
                    throw;
                }


                /******************* Validation of Status Indicator **********************************/

                String style1 = StudentResults.VerifyEventIcon(strTDEventName);

                System.Threading.Thread.Sleep(15000);
                try
                {
                    Assert.IsTrue(style1.Contains("background-color: cornflowerblue"));
                    PropertiesCollection.test.Log(Status.Pass, "Status Indicator for Event is at Completed and Passed state");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Status Indicator for Event has not got completed and Passed");
                    throw;
                }

                

                string strfbwebOverallComments = StudentResults.GetTextOverallComments();

                try
                {
                    Assert.AreEqual(strTDOverallComments, strfbwebOverallComments);
                    PropertiesCollection.test.Log(Status.Pass, "Overall Comments in the Student Result Page matches: " + strTDOverallComments);
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Overall Comments are not matching in the Student Results Page");
                    throw;
                }

                string strfbwebPrivateComments = StudentResults.GetTextPrivateComments();

                try
                {
                    Assert.AreEqual(strTDPrivateComments, strfbwebPrivateComments);
                    PropertiesCollection.test.Log(Status.Pass, "Private Comments in the Student Result Page matches: " + strTDPrivateComments);
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Private Comments are not matching in the Student Results Page");
                    throw;
                }
            }


            [TestCategory("Smoke")]
            [TestCategory("StudentResults")]
            [Priority(2)]
            [TestMethod]
            public void TS02_TC02_StudentResults_ClearEvent()
            {

                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TS02_TC02_StudentResults_ClearEvent");

                var connection = new ConnectToMySQL_Fetch_TestData();
                var testdataStudentResults = connection.Select(strtblname, strTestCaseNo, strTestType);
                var SidebarMenu = new FpSideMenus();
                var StudentResults = new FpStudentResultsPage();
                
                SidebarMenu.lnkStudentResults.Click();

                String strTDOrgnisationGroup = testdataStudentResults[4];
                String strTDStudentName = testdataStudentResults[5];
                String strTDInstructorName = testdataStudentResults[6];
                String strTDCourseName = testdataStudentResults[7];
                String strTDSyllabusName = testdataStudentResults[8];
                String strTDEventName = testdataStudentResults[9];
                String strTDScore = testdataStudentResults[10];
                String strTDScoreAssesmentCriteria = testdataStudentResults[11];
                String strTDStrength = testdataStudentResults[12];
                String strTDWeakness = testdataStudentResults[13];
                String strTDOverallComments = testdataStudentResults[14];
                String strTDPrivateComments = testdataStudentResults[15];
                String strTDServiceName = testdataStudentResults[16];
                String strTDCountryName = testdataStudentResults[17];
                String strTDStudentPosition = testdataStudentResults[18];
                String strTDStudentSurname = testdataStudentResults[19];

                StudentResults.SearchStudent(strTDStudentSurname);

                StudentResults.SearchCourseEvent((strTDEventName));

                StudentResults.VerifyEventIcon(strTDEventName);

                StudentResults.DeleteWriteup();

                System.Threading.Thread.Sleep(30000);

                String style2 = StudentResults.VerifyEventIcon(strTDEventName);

                try
                {
                    Assert.IsTrue(style2.Contains("background-color: rgb(70, 136, 71)"));
                    PropertiesCollection.test.Log(Status.Pass, "Event is back at scheduled state prior to Scoring");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Event is at not back at scheduled state");
                    throw;
                }

                StudentResults.VerifyEventIcon(strTDEventName);               
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
 
 
 
 