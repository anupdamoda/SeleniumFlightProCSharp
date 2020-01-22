using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Configuration;
using FPWebAutomation_MSTests.Database;
using FPWebAutomation_MSTests.Email;
using FPWebAutomation_MSTests.PageObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FPWebAutomation_MSTests.Database.ConnectToMySQL_Fetch_TestData;

namespace FPWebAutomation_MSTests.TestCases.Progression_Tests.Release_8._3
{
    [TestClass]
    public class TC04_View_Workflow_SummaryPage
    {
        [TestInitialize]
        public void TestInitialize()
        {
            PropertiesCollection.driver = new ChromeDriver(System.Configuration.ConfigurationManager.AppSettings["ChromeDriverPath"]);
            PropertiesCollection.driver.Manage().Window.Maximize();
            FpLoginPage loginPage = new FpLoginPage();
            loginPage.Login();
        }

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            PropertiesCollection.htmlReporter = new ExtentHtmlReporter(@"C:\FlightPro\Dev\_main\Test Automation\FlightPro\FightPro_WebAutomation\FPWebAutomation\Vanguard\Report\Report.html");
            PropertiesCollection.htmlReporter.LoadConfig(@"C:\extent-configfile\extent-config.xml");
            PropertiesCollection.extent = new ExtentReports();
            PropertiesCollection.extent.AttachReporter(PropertiesCollection.htmlReporter);
            PropertiesCollection.extent.AddSystemInfo("Automation Database", "8.1");
            PropertiesCollection.extent.AddSystemInfo("Browser", "Chrome");
            PropertiesCollection.extent.AddSystemInfo("Application Under Test (AUT)", "FlightProWeb");
            PropertiesCollection.extent.AddSystemInfo("Application URL", "http://oc-svr-at1/Fltpro_Automation_main/");
        }

        [TestCategory("Progression")]
        [TestCategory("US_38078_View_Workflow_Settings")]
        [TestMethod]
        public void US_38078_View_Workflow_Settings()
        {
            PropertiesCollection.test = PropertiesCollection.extent.CreateTest("US_38078_View_Workflow_Settings");
            FpSideMenus SideMenu = new FpSideMenus();
            System.Threading.Thread.Sleep(10000);
            SideMenu.LnkSummary.Click();
            FpSummaryPage Summary = new FpSummaryPage();

            var countlblWorkflowicon = Summary.countlblWorkflowicon;
            var countlblworkflowTitle = Summary.countlblWorkflowicon;
            
            if (countlblWorkflowicon==0||countlblworkflowTitle==0)
            {
                Summary.btnSettings.Click();
                SelectElement dropdown1 = new SelectElement(PropertiesCollection.driver.FindElement(By.Id("optionDisplaySettings")));
                dropdown1.SelectByValue("4");
                PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"saveDisplaySettings\"]")).Click();
                Assert.IsTrue(Summary.lblWorkflowTitle.Displayed);
                Assert.IsTrue(Summary.lblWorkflowicon.Displayed);
            }
            
            Summary.btnSettings.Click();
            System.Threading.Thread.Sleep(2000);

            for (int i = 0; i < Summary.countDisplaySetting.Count; i++)
            {
                if (Summary.DisplaySetting.ElementAt(i).Text.Equals("Workflows"))
                {
                    Summary.countDisplaySetting.ElementAt(i).Click();       
                    break;
                }
            }
            System.Threading.Thread.Sleep(5000);
            var btnSave = PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"saveDisplaySettings\"]"));
            System.Threading.Thread.Sleep(2000);
            btnSave.Click();
            System.Threading.Thread.Sleep(2000);  
            Assert.AreEqual(Summary.countlblWorkflowTitle,0);
            Assert.AreEqual(Summary.countlblWorkflowicon,0); 
        }

        [TestCategory("Progression")]
        [TestCategory("US_38078_View_WorkflowList_Summary")]
        [TestMethod]
        public void US_42249_View_WorkflowList_LinkedToColumn()
        {
            PropertiesCollection.test = PropertiesCollection.extent.CreateTest("US_42249_View_WorkflowList_LinkedToColumn");
            FpSideMenus SideMenu = new FpSideMenus();
            System.Threading.Thread.Sleep(10000);
            SideMenu.LnkSummary.Click();
            FpSummaryPage Summary = new FpSummaryPage();
            Assert.IsTrue(Summary.lblWorkflowTitle.Displayed);
            Assert.IsTrue(Summary.lblWorkflowicon.Displayed);
            Summary.btnSettings.Click();
            System.Threading.Thread.Sleep(2000);
            IWebElement element = PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"extraOptions\"]/div/div[2]/div/div[2]/div"));
            new Actions(PropertiesCollection.driver).MoveToElement(element).MoveByOffset(10, 10).Click().Perform();
            SelectElement dropdown = new SelectElement(PropertiesCollection.driver.FindElement(By.Id("optionDisplaySettings")));
            dropdown.SelectByValue("0");
            PropertiesCollection.driver.FindElement(By.Id("saveDisplaySettings")).Click();

            Summary.btnSettings.Click();
            System.Threading.Thread.Sleep(2000);

            for (int i = 0; i < Summary.countDisplaySetting.Count; i++)
            {
                if (Summary.DisplaySetting.ElementAt(i).Text.Equals("Workflows"))
                {
                    Summary.countDisplaySetting.ElementAt(i).Click();
                    break;
                }
            }
            System.Threading.Thread.Sleep(5000);
            var btnSave = PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"saveDisplaySettings\"]"));
            System.Threading.Thread.Sleep(2000);
            btnSave.Click();
            System.Threading.Thread.Sleep(2000);
            Assert.AreEqual(Summary.countlblWorkflowTitle, 0);
            Assert.AreEqual(Summary.countlblWorkflowicon, 0);
        }

        [TestCategory("Progression")]
        [TestCategory("US_42249_View_WorkflowList_LinkedToColumn")]
        [TestMethod]
        public void US_38078_View_Workflow()
        {
            PropertiesCollection.test = PropertiesCollection.extent.CreateTest("US_42249_View_WorkflowList_LinkedToColumn");
            FpSideMenus SideMenu = new FpSideMenus();
            System.Threading.Thread.Sleep(10000);
            SideMenu.LnkSummary.Click();
            FpSummaryPage Summary = new FpSummaryPage();
            Assert.IsTrue(Summary.lblWorkflowTitle.Displayed);
            Assert.IsTrue(Summary.lblWorkflowicon.Displayed);
            Summary.btnSettings.Click();
            System.Threading.Thread.Sleep(2000);
            IWebElement element = PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"extraOptions\"]/div/div[2]/div/div[2]/div"));
            new Actions(PropertiesCollection.driver).MoveToElement(element).MoveByOffset(10, 10).Click().Perform();
            SelectElement dropdown = new SelectElement(PropertiesCollection.driver.FindElement(By.Id("optionDisplaySettings")));
            dropdown.SelectByValue("0");
            PropertiesCollection.driver.FindElement(By.Id("saveDisplaySettings")).Click();

            Summary.btnSettings.Click();
            System.Threading.Thread.Sleep(2000);

            for (int i = 0; i < Summary.countDisplaySetting.Count; i++)
            {
                if (Summary.DisplaySetting.ElementAt(i).Text.Equals("Workflows"))
                {
                    Summary.countDisplaySetting.ElementAt(i).Click();
                    break;
                }
            }
            System.Threading.Thread.Sleep(5000);
            var btnSave = PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"saveDisplaySettings\"]"));
            System.Threading.Thread.Sleep(2000);
            btnSave.Click();
            System.Threading.Thread.Sleep(2000);
            Assert.AreEqual(Summary.countlblWorkflowTitle, 0);
            Assert.AreEqual(Summary.countlblWorkflowicon, 0);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            //PropertiesCollection.driver.Close();
            //PropertiesCollection.driver.Quit();
            //PropertiesCollection.driver.Dispose();
            //System.Threading.Thread.Sleep(30000);
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
