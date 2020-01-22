using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using FPWebAutomation_MSTests.Database;
using FPWebAutomation_MSTests.PageObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPWebAutomation_MSTests.TestCases.Progression_Tests.Release_8._3
{

    class TC05_ViewWorkflow_ViewHeader
    {
        [TestClass]
        public class TC05_View_Workflow_ViewHeader
        {
            [TestInitialize]
            public void TestInitialize()
            {
                PropertiesCollection.driver = new ChromeDriver(ConfigurationManager.AppSettings["ChromeDriverPath"]);
                PropertiesCollection.driver.Manage().Window.Maximize();
                ConnectToMySQL_Fetch_TestData MySQLConnect = new ConnectToMySQL_Fetch_TestData();
                String[] TestData = new string[3];
                TestData = MySQLConnect.GetLoginDetails("Login1");

                string strTDUsername = TestData[1];
                string strTDPassword = TestData[2];
                Console.WriteLine(strTDUsername);
                Console.WriteLine(strTDPassword);

                FpLoginPage loginPage = new FpLoginPage();
                loginPage.LoginWithUserCredentials(strTDUsername, strTDPassword);
            }

            [ClassInitialize]
            public static void ClassInitialize(TestContext context)
            {
                PropertiesCollection.htmlReporter = new ExtentHtmlReporter(@"C:\FlightPro\Dev\_main\Test Automation\FlightPro\FightPro_WebAutomation\FPWebAutomation\Report\Report.html");
                PropertiesCollection.htmlReporter.LoadConfig(@"C:\extent-configfile\extent-config.xml");
                PropertiesCollection.extent = new ExtentReports();
                PropertiesCollection.extent.AttachReporter(PropertiesCollection.htmlReporter);
                PropertiesCollection.extent.AddSystemInfo("Automation Database", "8.1");
                PropertiesCollection.extent.AddSystemInfo("Browser", "Chrome");
                PropertiesCollection.extent.AddSystemInfo("Application Under Test (AUT)", "FlightProWeb");
                PropertiesCollection.extent.AddSystemInfo("Application URL", "http://oc-svr-at1/Fltpro_Automation_main/");
            }

            [TestCategory("Progression")]
            [TestCategory("US_41948_Workflows_ViewHeader")]
            [TestMethod]
            public void US_41948_Workflows_ViewHeader()
              { 
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("US_41948_Workflows_ViewHeader");
                FpSideMenus sidebarMenu = new FpSideMenus();
                
                sidebarMenu.LnkSummary.Click();
                FpSummaryPage summary = new FpSummaryPage();
                System.Threading.Thread.Sleep(4000);
                Console.WriteLine("Count" +  summary.RetriveWorkflowCount());
                var noofWorkflows = summary.RetriveWorkflowCount();

                Console.WriteLine();

               for(int i=0; i< summary.grdWorkflow.Count ; i++)
                {
                    System.Threading.Thread.Sleep(2000);
                    summary.grdWorkflow.ElementAt(i).Click();
                    System.Threading.Thread.Sleep(2000);
                   
                    System.Threading.Thread.Sleep(2000);
                   // Assert.AreEqual(summary.grdWorkflowTypes.ElementAt(i).Text,summary.txtboxWorkFlowType.GetAttribute("value"));
                    
                    Console.WriteLine("WorkFlowLinkedTo:", summary.grdLinkedTo.ElementAt(i).Text);
                   // Assert.AreEqual(summary.grdLinkedTo.ElementAt(i).Text,summary.txtboxWorkFlowLinkedTo.GetAttribute("value"));
                    
                    Console.WriteLine("WorkFlowTitleDescription:", summary.grdTitleDescription.ElementAt(i).Text);
                   // Assert.AreEqual(summary.grdTitleDescription.ElementAt(i).Text,summary.txtboxWorkFlowTitle.GetAttribute("value"));
                   
                    Console.WriteLine("WorkFlowNotes:", summary.grdNotes.ElementAt(i).Text);
                   // Assert.AreEqual(summary.grdTitleDescription.ElementAt(i).Text,summary.txtboxWorkFlowNotes.GetAttribute("value"));
                    
                    Console.WriteLine("WorkFlowDueDate:", summary.grdDue.ElementAt(i).Text);
                }
            }

            [TestCleanup]
            public void TestCleanup()
            {
                PropertiesCollection.driver.Close();
                PropertiesCollection.driver.Quit();
                PropertiesCollection.driver.Dispose();            
            }

            [ClassCleanup]
            public static void ClassCleanup()
            {
                PropertiesCollection.extent.Flush();
            }
        }
    }
}
