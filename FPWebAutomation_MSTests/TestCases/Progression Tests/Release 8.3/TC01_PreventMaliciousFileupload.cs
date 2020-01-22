using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Configuration;
using FPWebAutomation_MSTests.PageObjects;
using OpenQA.Selenium.Chrome;
using FPWebAutomation_MSTests.Database;
using AventStack.ExtentReports;
using System.Collections.Generic;
using AventStack.ExtentReports.Reporter;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Configuration;
using System.Collections;
using System.Linq;

namespace FPWebAutomation_MSTests.TestCases.Progression_Tests
{
    [TestClass]
    public class TS01_PreventMaliciousFileupload
    {
        public TestContext TestContext { get; set; }

        string strtblname = "automation_studentresults";
        string strTestType = "Progression";

        public List<string> fileList_ValidDocuments = new List<string>
        {
            "TestFile001.txt",
            "TestFile002.log",
            "TestFile003.xml",
            "TestFile004.json",
            "TestFile005.doc",
            "TestFile006.docx",
            "TestFile007.docm",
            "TestFile008.dot",
            "TestFile009.dotx",
            "TestFile010.dotm",
            "TestFile011.xls",
            "TestFile012.xlsx",
            "TestFile013.xlsm",
            "TestFile014.xlt",
            "TestFile015.xltx",
            "TestFile016.xltm",
            "TestFile017.ppt",
            "TestFile018.pptx",
            "TestFile019.pptm",
            "TestFile020.pps",
            "TestFile021.ppsx",
            "TestFile022.ppsm",
            "TestFile023.pot",
            "TestFile024.potx",
            "TestFile025.potm",
            "TestFile026.odt",
            "TestFile027.pdf",
            "TestFile028.rtf",
            "TestFile029.csv",
            "TestFile030.key",
            "TestFile031.msg",
            "TestFile054.xps",
            "TestFile055.oxps"
        };

        public List<string> fileList_ValidImageFiles = new List<string>
        {
            "TestFile032.jpg",
            "TestFile033.jpeg",
            "TestFile034.png",
            "TestFile035.gif",
            "TestFile036.bmp",
            "TestFile037.tif",
            "TestFile038.tiff",
            "TestFile039.img",
            "TestFile040.wmf"
        };

        public List<string> fileList_ValidVideoFiles = new List<string>
        {
            "TestFile041.mp4",
            "TestFile042.m4v",
            "TestFile043.mp3",
            "TestFile044.mov",
            "TestFile045.wmv",
            "TestFile046.avi",
            "TestFile047.mpg",
            "TestFile048.mpeg",
            "TestFile049.ogv",
            "TestFile050.ogg",
            "TestFile051.3gp",
            "TestFile052.3g2",
            "TestFile053.swf"
        };

        public List<string> fileList_InvalidFiles = new List<string>
        {
            "TestFile055.chm",
            "TestFile055.htm",
            "TestFile055.html",
            "TestFile055.mht",
            "TestFile055.mhtml",
            "TestFile055.url"
        };

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
        [TestCategory("US_41888_StudentResults")]
        [TestMethod]
        public void US_41888_PreventMaliciousFileUpload_StudentResults_ValidDocuments()
        {
            PropertiesCollection.test = PropertiesCollection.extent.CreateTest("US_41888_PreventMaliciousFileUpload_StudentResults_ValidDocuments");
            var strTestCaseNo = "TC001";
            var connection = new ConnectToMySQL_Fetch_TestData();
            var testdataStudentResults = connection.Select(strtblname, strTestCaseNo, strTestType);
            var sidebarMenu = new FpSideMenus();
            var studentResults = new FpStudentResultsPage();
            var folderLocation = "Documents\\";

            sidebarMenu.lnkStudentResults.Click();

            string strTDOrgnisationGroup = testdataStudentResults[4];
            string strTDStudentName = testdataStudentResults[5];
            string strTDInstructorName = testdataStudentResults[6];
            string strTDCourseName = testdataStudentResults[7];
            string strTDSyllabusName = testdataStudentResults[8];
            string strTDEventName = testdataStudentResults[9];
            string strTDScore = testdataStudentResults[10];
            string strTDScoreAssesmentCriteria = testdataStudentResults[11];
            string strTDStrength = testdataStudentResults[12];
            string strTDWeakness = testdataStudentResults[13];
            string strTDOverallComments = testdataStudentResults[14];
            string strTDPrivateComments = testdataStudentResults[15];
            string strTDServiceName = testdataStudentResults[16];
            string strTDCountryName = testdataStudentResults[17];
            string strTDStudentPosition = testdataStudentResults[18];
            string strTDStudentSurname = testdataStudentResults[19];
            string strTDResultAwarded = testdataStudentResults[20];

            studentResults.SearchStudent(strTDStudentSurname);
            studentResults.SearchCourseEvent(strTDEventName);

            string style = studentResults.VerifyEventIcon(strTDEventName);

            if (style.Contains("background-color: rgb(70, 136, 71)") == false)
            {
                studentResults.DeleteWriteup();
            }

            WebDriverWait wait = new WebDriverWait(PropertiesCollection.driver, TimeSpan.FromSeconds(10));

            Console.WriteLine("The number of rows:" + PropertiesCollection.driver.FindElements(By.XPath("//*[@id=\"documentsGrid\"]/div/div[6]/div/div/div[1]/div/table/tbody/tr/td[1]")).Count);
            int Count = PropertiesCollection.driver.FindElements(By.XPath("//*[@id=\"documentsGrid\"]/div/div[6]/div/div/div[1]/div/table/tbody/tr/td[1]")).Count;

            for (int i = 0; i <= Count; i++)
            {
                System.Threading.Thread.Sleep(2000);
                if (PropertiesCollection.driver.FindElements(By.XPath("//*[@id=\"documentsGrid\"]/div/div[6]/div/div/div[1]/div/table/tbody/tr[1]/td[5]/div/dx-button")).Count != 0)
                {
                    IWebElement btnRemove = PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"documentsGrid\"]/div/div[6]/div/div/div[1]/div/table/tbody/tr[1]/td[5]/div/dx-button"));
                    btnRemove.Click();
                    System.Threading.Thread.Sleep(2000);
                    IWebElement btnYesPopup = PropertiesCollection.driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[3]/div/div[2]/div[1]/div/div"));
                    btnYesPopup.Click();
                    System.Threading.Thread.Sleep(2000);
                }
            }

            foreach (var file in fileList_ValidDocuments)
            {
                IWebElement btnAddDocument = wait.Until(ExpectedConditions.ElementToBeClickable(PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"psr-file-uploader\"]/div/div/div/div[1]/div[1]/div/span"))));
                btnAddDocument.Click();
                System.Threading.Thread.Sleep(2000);
                AutoIt.AutoItX.ControlFocus("Open", "", "Edit1");
                AutoIt.AutoItX.ControlSetText("Open", "", "Edit1", System.Configuration.ConfigurationManager.AppSettings["MaliciousFileUploadLocation"] + folderLocation + file);
                AutoIt.AutoItX.ControlClick("Open", "", "Button1");
                System.Threading.Thread.Sleep(3000);
                PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"psr-file-uploader\"]/div/div/div/div[3]/div/div[2]/div/div")).Click();
                System.Threading.Thread.Sleep(2000);

                IList<IWebElement> fileNames = PropertiesCollection.driver.FindElements(By.XPath("//*[@id=\"documentsGrid\"]/div/div[6]/div/div/div[1]/div/table/tbody/tr/td[1]"));
                System.Threading.Thread.Sleep(3000);

                string filename = fileNames.First().Text;
                try
                {
                    Assert.AreEqual(file, filename);
                    PropertiesCollection.test.Log(Status.Pass, "File: <strong>" + filename + "</strong> is successfully uploaded from Location: " + System.Configuration.ConfigurationManager.AppSettings["MaliciousFileUploadLocation"] + folderLocation);
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "File: " + filename + "is not successfully uploaded");
                }

                if (PropertiesCollection.driver.FindElements(By.XPath("//*[@id=\"documentsGrid\"]/div/div[6]/div/div/div[1]/div/table/tbody/tr[1]/td[5]/div/dx-button")).Count != 0)
                {
                    IWebElement btnRemove = PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"documentsGrid\"]/div/div[6]/div/div/div[1]/div/table/tbody/tr[1]/td[5]/div/dx-button"));
                    btnRemove.Click();
                    System.Threading.Thread.Sleep(2000);
                    IWebElement btnYesPopup = PropertiesCollection.driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[3]/div/div[2]/div[1]/div/div"));
                    btnYesPopup.Click();
                    System.Threading.Thread.Sleep(2000);
                }                                
                Console.WriteLine("Outside the loop");
                Console.WriteLine(fileNames.Count);
            }
        }

        [TestCategory("Progression")]
        [TestCategory("US_41889_KnowledgeBase")]
        [TestMethod]
        public void US_41889_PreventMaliciousFileUpload_KnowledgeBase_ValidDocuments()
        {
            PropertiesCollection.test = PropertiesCollection.extent.CreateTest("US_41889_PreventMaliciousFileUpload_KnowledgeBase_ValidDocuments");
            var connection = new ConnectToMySQL_Fetch_TestData();
            var sidebarMenu = new FpSideMenus();
            var knowledgeBase = new FpKnowledgeBasePage();
            var folderLocation = "Documents\\";

            sidebarMenu.KnowledgeBaseClick();
            System.Threading.Thread.Sleep(4000);
            
            foreach (var file in fileList_ValidDocuments)
            {
                System.Threading.Thread.Sleep(4000);
                knowledgeBase.btnAddKnowledgebaseByEntry.Click();
                System.Threading.Thread.Sleep(2000);
                knowledgeBase.btnBrowse.Click();
                System.Threading.Thread.Sleep(4000);
                AutoIt.AutoItX.ControlFocus("Open", "", "Edit1");
                AutoIt.AutoItX.ControlSetText("Open", "", "Edit1", System.Configuration.ConfigurationManager.AppSettings["MaliciousFileUploadLocation"] + folderLocation + file);
                AutoIt.AutoItX.ControlClick("Open", "", "Button1");
                System.Threading.Thread.Sleep(2000);
                knowledgeBase.btnSave.Click();
                System.Threading.Thread.Sleep(2000);

                string filename = knowledgeBase.grdFileNameRow.Text;
                try
                {
                    Assert.AreEqual(file, filename);
                    PropertiesCollection.test.Log(Status.Pass, "File: " + filename + " is successfully uploaded from Location: " + System.Configuration.ConfigurationManager.AppSettings["MaliciousFileUploadLocation"] + folderLocation);
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "File: " + filename + "is not successfully uploaded");
                }

                knowledgeBase.btnDelete.Click();
                IWebElement btnOKConfirmation = PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"_0_btn_modal\"]"));
                btnOKConfirmation.Click();            
            }
        }

        [TestCategory("Progression")]
        [TestCategory("US_41888_StudentResults")]
        [TestMethod]
        public void US_41888_PreventMaliciousFileUpload_StudentResults_ValidImageFiles()
        {
            PropertiesCollection.test = PropertiesCollection.extent.CreateTest("US_41888_PreventMaliciousFileUpload_StudentResults_ValidImages");
            var strTestCaseNo = "TC004";
            var connection = new ConnectToMySQL_Fetch_TestData();
            var testdataStudentResults = connection.Select(strtblname, strTestCaseNo, strTestType);
            var sidebarMenu = new FpSideMenus();
            var studentResults = new FpStudentResultsPage();
            var folderLocation = "Images\\";

            sidebarMenu.lnkStudentResults.Click();

            string strTDOrgnisationGroup = testdataStudentResults[4];
            string strTDStudentName = testdataStudentResults[5];
            string strTDInstructorName = testdataStudentResults[6];
            string strTDCourseName = testdataStudentResults[7];
            string strTDSyllabusName = testdataStudentResults[8];
            string strTDEventName = testdataStudentResults[9];
            string strTDScore = testdataStudentResults[10];
            string strTDScoreAssesmentCriteria = testdataStudentResults[11];
            string strTDStrength = testdataStudentResults[12];
            string strTDWeakness = testdataStudentResults[13];
            string strTDOverallComments = testdataStudentResults[14];
            string strTDPrivateComments = testdataStudentResults[15];
            string strTDServiceName = testdataStudentResults[16];
            string strTDCountryName = testdataStudentResults[17];
            string strTDStudentPosition = testdataStudentResults[18];
            string strTDStudentSurname = testdataStudentResults[19];
            string strTDResultAwarded = testdataStudentResults[20];

            studentResults.SearchStudent(strTDStudentSurname);
            studentResults.SearchCourseEvent(strTDEventName);

            string style = studentResults.VerifyEventIcon(strTDEventName);

            if (style.Contains("background-color: rgb(70, 136, 71)") == false)
            {
                studentResults.DeleteWriteup();
            }

            WebDriverWait wait = new WebDriverWait(PropertiesCollection.driver, TimeSpan.FromSeconds(10));
           
            Console.WriteLine("The number of rows:" + PropertiesCollection.driver.FindElements(By.XPath("//*[@id=\"documentsGrid\"]/div/div[6]/div/div/div[1]/div/table/tbody/tr/td[1]")).Count);
            int Count = PropertiesCollection.driver.FindElements(By.XPath("//*[@id=\"documentsGrid\"]/div/div[6]/div/div/div[1]/div/table/tbody/tr/td[1]")).Count;

            for (int i = 0; i <= Count; i++)
            {
                System.Threading.Thread.Sleep(2000);
                if (PropertiesCollection.driver.FindElements(By.XPath("//*[@id=\"documentsGrid\"]/div/div[6]/div/div/div[1]/div/table/tbody/tr[1]/td[5]/div/dx-button")).Count != 0)
                {
                    IWebElement btnRemove = PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"documentsGrid\"]/div/div[6]/div/div/div[1]/div/table/tbody/tr[1]/td[5]/div/dx-button"));
                    btnRemove.Click();
                    System.Threading.Thread.Sleep(2000);
                    IWebElement btnYesPopup = PropertiesCollection.driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[3]/div/div[2]/div[1]/div/div"));
                    btnYesPopup.Click();
                    System.Threading.Thread.Sleep(2000);
                }
            }

            foreach (var file in fileList_ValidImageFiles)
            {
                IWebElement btnAddDocument = wait.Until(ExpectedConditions.ElementToBeClickable(PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"psr-file-uploader\"]/div/div/div/div[1]/div[1]/div/span"))));
                btnAddDocument.Click();
                System.Threading.Thread.Sleep(2000);
                AutoIt.AutoItX.ControlFocus("Open", "", "Edit1");
                AutoIt.AutoItX.ControlSetText("Open", "", "Edit1", System.Configuration.ConfigurationManager.AppSettings["MaliciousFileUploadLocation"] + folderLocation + file);
                AutoIt.AutoItX.ControlClick("Open", "", "Button1");
                System.Threading.Thread.Sleep(3000);
                PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"psr-file-uploader\"]/div/div/div/div[3]/div/div[2]/div/div")).Click();
                System.Threading.Thread.Sleep(2000);

                IList<IWebElement> fileNames = PropertiesCollection.driver.FindElements(By.XPath("//*[@id=\"documentsGrid\"]/div/div[6]/div/div/div[1]/div/table/tbody/tr/td[1]"));
                System.Threading.Thread.Sleep(3000);

                string filename = fileNames.First().Text;
                try
                {
                    Assert.AreEqual(file, filename);
                    PropertiesCollection.test.Log(Status.Pass, "File: <strong>" + filename + "</strong> is successfully uploaded from Location: " + System.Configuration.ConfigurationManager.AppSettings["MaliciousFileUploadLocation"] + folderLocation);
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "File: " + filename + "is not successfully uploaded");
                }

                if (PropertiesCollection.driver.FindElements(By.XPath("//*[@id=\"documentsGrid\"]/div/div[6]/div/div/div[1]/div/table/tbody/tr[1]/td[5]/div/dx-button")).Count != 0)
                {
                    IWebElement btnRemove = PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"documentsGrid\"]/div/div[6]/div/div/div[1]/div/table/tbody/tr[1]/td[5]/div/dx-button"));
                    btnRemove.Click();
                    System.Threading.Thread.Sleep(2000);
                    IWebElement btnYesPopup = PropertiesCollection.driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[3]/div/div[2]/div[1]/div/div"));
                    btnYesPopup.Click();
                    System.Threading.Thread.Sleep(2000);
                }
                Console.WriteLine("Outside the loop");
                Console.WriteLine(fileNames.Count);
            }
        }

        [TestCategory("Progression")]
        [TestCategory("US_41889_KnowledgeBase")]
        [TestMethod]
        public void US_41889_PreventMaliciousFileUpload_KnowledgeBase_ValidImageFiles()
        {
            PropertiesCollection.test = PropertiesCollection.extent.CreateTest("US_41889_PreventMaliciousFileUpload_KnowledgeBase_ValidImageFiles");

            var connection = new ConnectToMySQL_Fetch_TestData();
            var sidebarMenu = new FpSideMenus();
            var knowledgeBase = new FpKnowledgeBasePage();
            var folderLocation = "Images\\";
            sidebarMenu.KnowledgeBaseClick();
            System.Threading.Thread.Sleep(4000);

            foreach (var file in fileList_ValidImageFiles)
            {
                System.Threading.Thread.Sleep(4000);
                knowledgeBase.btnAddKnowledgebaseByEntry.Click();
                System.Threading.Thread.Sleep(2000);
                knowledgeBase.btnBrowse.Click();
                System.Threading.Thread.Sleep(4000);
                AutoIt.AutoItX.ControlFocus("Open", "", "Edit1");
                AutoIt.AutoItX.ControlSetText("Open", "", "Edit1", System.Configuration.ConfigurationManager.AppSettings["MaliciousFileUploadLocation"] + folderLocation + file);
                AutoIt.AutoItX.ControlClick("Open", "", "Button1");
                System.Threading.Thread.Sleep(2000);
                knowledgeBase.btnSave.Click();
                System.Threading.Thread.Sleep(2000);

                string filename = knowledgeBase.grdFileNameRow.Text;
                try
                {
                    Assert.AreEqual(file, filename);
                    PropertiesCollection.test.Log(Status.Pass, "File: " + filename + " is successfully uploaded from Location: " + System.Configuration.ConfigurationManager.AppSettings["MaliciousFileUploadLocation"] + folderLocation);
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "File: " + filename + "is not successfully uploaded");
                }

                knowledgeBase.btnDelete.Click();
                IWebElement btnOKConfirmation = PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"_0_btn_modal\"]"));
                btnOKConfirmation.Click();
            }
        }

        [TestCategory("Progression")]
        [TestCategory("US_41888_StudentResults")]
        [TestMethod]
        public void US_41888_PreventMaliciousFileUpload_StudentResults_ValidVideoFiles()
        {
            PropertiesCollection.test = PropertiesCollection.extent.CreateTest("US_41888_PreventMaliciousFileUpload_StudentResults_ValidVideos");
            var strTestCaseNo = "TC003";
            var connection = new ConnectToMySQL_Fetch_TestData();
            var testdataStudentResults = connection.Select(strtblname, strTestCaseNo, strTestType);
            var sidebarMenu = new FpSideMenus();
            var studentResults = new FpStudentResultsPage();
            var folderLocation = "Videos\\";

            sidebarMenu.lnkStudentResults.Click();

            string strTDOrgnisationGroup = testdataStudentResults[4];
            string strTDStudentName = testdataStudentResults[5];
            string strTDInstructorName = testdataStudentResults[6];
            string strTDCourseName = testdataStudentResults[7];
            string strTDSyllabusName = testdataStudentResults[8];
            string strTDEventName = testdataStudentResults[9];
            string strTDScore = testdataStudentResults[10];
            string strTDScoreAssesmentCriteria = testdataStudentResults[11];
            string strTDStrength = testdataStudentResults[12];
            string strTDWeakness = testdataStudentResults[13];
            string strTDOverallComments = testdataStudentResults[14];
            string strTDPrivateComments = testdataStudentResults[15];
            string strTDServiceName = testdataStudentResults[16];
            string strTDCountryName = testdataStudentResults[17];
            string strTDStudentPosition = testdataStudentResults[18];
            string strTDStudentSurname = testdataStudentResults[19];
            string strTDResultAwarded = testdataStudentResults[20];

            studentResults.SearchStudent(strTDStudentSurname);
            studentResults.SearchCourseEvent(strTDEventName);
            string style = studentResults.VerifyEventIcon(strTDEventName);

            if (style.Contains("background-color: rgb(70, 136, 71)") == false)
            {
                studentResults.DeleteWriteup();
            }

            WebDriverWait wait = new WebDriverWait(PropertiesCollection.driver, TimeSpan.FromSeconds(10));

            Console.WriteLine("The number of rows:" + PropertiesCollection.driver.FindElements(By.XPath("//*[@id=\"documentsGrid\"]/div/div[6]/div/div/div[1]/div/table/tbody/tr/td[1]")).Count);
            int count = PropertiesCollection.driver.FindElements(By.XPath("//*[@id=\"documentsGrid\"]/div/div[6]/div/div/div[1]/div/table/tbody/tr/td[1]")).Count;

            for (int i = 0; i <= count; i++)
            {
                System.Threading.Thread.Sleep(2000);
                if (PropertiesCollection.driver.FindElements(By.XPath("//*[@id=\"documentsGrid\"]/div/div[6]/div/div/div[1]/div/table/tbody/tr[1]/td[5]/div/dx-button")).Count != 0)
                {
                    IWebElement btnRemove = PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"documentsGrid\"]/div/div[6]/div/div/div[1]/div/table/tbody/tr[1]/td[5]/div/dx-button"));
                    btnRemove.Click();
                    System.Threading.Thread.Sleep(2000);
                    IWebElement btnYesPopup = PropertiesCollection.driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[3]/div/div[2]/div[1]/div/div"));
                    btnYesPopup.Click();
                    System.Threading.Thread.Sleep(2000);
                }
            }

            foreach (var file in fileList_ValidVideoFiles)
            {
                IWebElement btnAddDocument = wait.Until(ExpectedConditions.ElementToBeClickable(PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"psr-file-uploader\"]/div/div/div/div[1]/div[1]/div/span"))));
                btnAddDocument.Click();
                System.Threading.Thread.Sleep(2000);
                AutoIt.AutoItX.ControlFocus("Open", "", "Edit1");
                AutoIt.AutoItX.ControlSetText("Open", "", "Edit1", System.Configuration.ConfigurationManager.AppSettings["MaliciousFileUploadLocation"] + folderLocation + file);
                AutoIt.AutoItX.ControlClick("Open", "", "Button1");
                System.Threading.Thread.Sleep(3000);
                PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"psr-file-uploader\"]/div/div/div/div[3]/div/div[2]/div/div")).Click();
                System.Threading.Thread.Sleep(2000);
                IList<IWebElement> fileNames = PropertiesCollection.driver.FindElements(By.XPath("//*[@id=\"documentsGrid\"]/div/div[6]/div/div/div[1]/div/table/tbody/tr/td[1]"));
                System.Threading.Thread.Sleep(3000);

                string filename = fileNames.First().Text;
                try
                {
                    Assert.AreEqual(file, filename);
                    PropertiesCollection.test.Log(Status.Pass, "File: <strong>" + filename + "</strong> is successfully uploaded from Location: " + System.Configuration.ConfigurationManager.AppSettings["MaliciousFileUploadLocation"] + folderLocation);
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "File: " + filename + "is not successfully uploaded");
                }

                if (PropertiesCollection.driver.FindElements(By.XPath("//*[@id=\"documentsGrid\"]/div/div[6]/div/div/div[1]/div/table/tbody/tr[1]/td[5]/div/dx-button")).Count != 0)
                {
                    IWebElement btnRemove = PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"documentsGrid\"]/div/div[6]/div/div/div[1]/div/table/tbody/tr[1]/td[5]/div/dx-button"));
                    btnRemove.Click();
                    System.Threading.Thread.Sleep(2000);
                    IWebElement btnYesPopup = PropertiesCollection.driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[3]/div/div[2]/div[1]/div/div"));
                    btnYesPopup.Click();
                    System.Threading.Thread.Sleep(2000);
                }
            }
        }

        [TestCategory("Progression")]
        [TestCategory("US_41889_KnowledgeBase")]
        [TestMethod]
        public void US_41889_PreventMaliciousFileUpload_KnowledgeBase_ValidVideoFiles()
        {
            PropertiesCollection.test = PropertiesCollection.extent.CreateTest("US_41889_PreventMaliciousFileUpload_KnowledgeBase_ValidVideoFiles");

            var connection = new ConnectToMySQL_Fetch_TestData();
            var sidebarMenu = new FpSideMenus();
            var knowledgeBase = new FpKnowledgeBasePage();
            var folderLocation = "Videos\\";
            sidebarMenu.KnowledgeBaseClick();
            System.Threading.Thread.Sleep(4000);

            foreach (var file in fileList_ValidVideoFiles)
            {
                System.Threading.Thread.Sleep(4000);
                knowledgeBase.btnAddKnowledgebaseByEntry.Click();
                System.Threading.Thread.Sleep(2000);
                knowledgeBase.btnBrowse.Click();
                System.Threading.Thread.Sleep(4000);
                AutoIt.AutoItX.ControlFocus("Open", "", "Edit1");
                AutoIt.AutoItX.ControlSetText("Open", "", "Edit1", System.Configuration.ConfigurationManager.AppSettings["MaliciousFileUploadLocation"] + folderLocation + file);
                AutoIt.AutoItX.ControlClick("Open", "", "Button1");
                System.Threading.Thread.Sleep(2000);
                knowledgeBase.btnSave.Click();
                System.Threading.Thread.Sleep(2000);

                string filename = knowledgeBase.grdFileNameRow.Text;
                try
                {
                    Assert.AreEqual(file, filename);
                    PropertiesCollection.test.Log(Status.Pass, "File: " + filename + " is successfully uploaded from Location: " + System.Configuration.ConfigurationManager.AppSettings["MaliciousFileUploadLocation"] + folderLocation);
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "File: " + filename + "is not successfully uploaded");
                }

                knowledgeBase.btnDelete.Click();
                IWebElement btnOKConfirmation = PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"_0_btn_modal\"]"));
                btnOKConfirmation.Click();
            }
        }

        [TestCategory("Progression")]
        [TestCategory("US_41888_StudentResults")]
        [TestMethod]
        public void US_41888_PreventMaliciousFileUpload_StudentResults_InvalidFiles()
        {
            PropertiesCollection.test = PropertiesCollection.extent.CreateTest("US_41888_PreventMaliciousFileUpload_StudentResults_InvalidFiles");
            var strTestCaseNo = "TC005";
            var connection = new ConnectToMySQL_Fetch_TestData();
            var testdataStudentResults = connection.Select(strtblname, strTestCaseNo, strTestType);
            var sidebarMenu = new FpSideMenus();
            var studentResults = new FpStudentResultsPage();
            var folderLocation = "Malicious Files\\";

            sidebarMenu.lnkStudentResults.Click();

            string strTDOrgnisationGroup = testdataStudentResults[4];
            string strTDStudentName = testdataStudentResults[5];
            string strTDInstructorName = testdataStudentResults[6];
            string strTDCourseName = testdataStudentResults[7];
            string strTDSyllabusName = testdataStudentResults[8];
            string strTDEventName = testdataStudentResults[9];
            string strTDScore = testdataStudentResults[10];
            string strTDScoreAssesmentCriteria = testdataStudentResults[11];
            string strTDStrength = testdataStudentResults[12];
            string strTDWeakness = testdataStudentResults[13];
            string strTDOverallComments = testdataStudentResults[14];
            string strTDPrivateComments = testdataStudentResults[15];
            string strTDServiceName = testdataStudentResults[16];
            string strTDCountryName = testdataStudentResults[17];
            string strTDStudentPosition = testdataStudentResults[18];
            string strTDStudentSurname = testdataStudentResults[19];
            string strTDResultAwarded = testdataStudentResults[20];
            studentResults.SearchStudent(strTDStudentSurname);
            studentResults.SearchCourseEvent(strTDEventName);

            string style = studentResults.VerifyEventIcon(strTDEventName);

            if (style.Contains("background-color: rgb(70, 136, 71)") == false)
            {
                studentResults.DeleteWriteup();

            }
            WebDriverWait wait = new WebDriverWait(PropertiesCollection.driver, TimeSpan.FromSeconds(10));
            
            Console.WriteLine("The number of rows:" + PropertiesCollection.driver.FindElements(By.XPath("//*[@id=\"documentsGrid\"]/div/div[6]/div/div/div[1]/div/table/tbody/tr/td[1]")).Count);
            int Count = PropertiesCollection.driver.FindElements(By.XPath("//*[@id=\"documentsGrid\"]/div/div[6]/div/div/div[1]/div/table/tbody/tr/td[1]")).Count;

            for (int i = 0; i <= Count; i++)
            {
                System.Threading.Thread.Sleep(2000);
                if (PropertiesCollection.driver.FindElements(By.XPath("//*[@id=\"documentsGrid\"]/div/div[6]/div/div/div[1]/div/table/tbody/tr[1]/td[5]/div/dx-button")).Count != 0)
                {
                    IWebElement btnRemove = PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"documentsGrid\"]/div/div[6]/div/div/div[1]/div/table/tbody/tr[1]/td[5]/div/dx-button"));
                    btnRemove.Click();
                    System.Threading.Thread.Sleep(2000);
                    IWebElement btnYesPopup = PropertiesCollection.driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[3]/div/div[2]/div[1]/div/div"));
                    btnYesPopup.Click();
                    System.Threading.Thread.Sleep(2000);
                }
            }

            int count = 0;
            foreach (var file in fileList_InvalidFiles)
            {
                IWebElement btnAddDocument = wait.Until(ExpectedConditions.ElementToBeClickable(PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"psr-file-uploader\"]/div/div/div/div[1]/div[1]/div/span"))));
                btnAddDocument.Click();
                System.Threading.Thread.Sleep(2000);
                AutoIt.AutoItX.ControlFocus("Open", "", "Edit1");
                AutoIt.AutoItX.ControlSetText("Open", "", "Edit1", System.Configuration.ConfigurationManager.AppSettings["MaliciousFileUploadLocation"] + folderLocation + file);
                AutoIt.AutoItX.ControlClick("Open", "", "Button1");
                System.Threading.Thread.Sleep(3000);
                PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"psr-file-uploader\"]/div/div/div/div[3]/div/div[2]/div/div")).Click();
                System.Threading.Thread.Sleep(2000);

                IList<IWebElement> fileNames = PropertiesCollection.driver.FindElements(By.XPath("//*[@id=\"documentsGrid\"]/div/div[6]/div/div/div[1]/div/table/tbody/tr/td[1]"));
                System.Threading.Thread.Sleep(3000);

                if (count <= fileNames.Count)
                {
                    if (count == 0)
                    {
                        string filename = fileNames.First().Text;
                        System.Threading.Thread.Sleep(2000);
                        try
                        {
                            Assert.AreEqual(file, filename);
                            PropertiesCollection.test.Log(Status.Pass, "File: " + filename + "is successfully uploaded");
                        }
                        catch
                        {
                            PropertiesCollection.test.Log(Status.Fail, "File: " + filename + "is not successfully uploaded");
                        }
                    }
                    else
                    {
                        string filename = fileNames.ElementAt(count).Text;
                        try
                        {
                            Assert.AreEqual(file, filename);
                            PropertiesCollection.test.Log(Status.Pass, "File: " + filename + "is successfully uploaded");
                        }
                        catch
                        {
                            PropertiesCollection.test.Log(Status.Fail, "File: " + filename + "is not successfully uploaded");
                        }
                    }
                    count = count + 1;
                }              
            }

            Console.WriteLine("The number of rows:" + PropertiesCollection.driver.FindElements(By.XPath("//*[@id=\"documentsGrid\"]/div/div[6]/div/div/div[1]/div/table/tbody/tr/td[1]")).Count);
            Count = PropertiesCollection.driver.FindElements(By.XPath("//*[@id=\"documentsGrid\"]/div/div[6]/div/div/div[1]/div/table/tbody/tr/td[1]")).Count;

            for (int i = 0; i <= Count; i++)
            {
                System.Threading.Thread.Sleep(2000);

                if (PropertiesCollection.driver.FindElements(By.XPath("//*[@id=\"documentsGrid\"]/div/div[6]/div/div/div[1]/div/table/tbody/tr[1]/td[5]/div/dx-button")).Count != 0)
                {
                    IWebElement btnRemove = PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"documentsGrid\"]/div/div[6]/div/div/div[1]/div/table/tbody/tr[1]/td[5]/div/dx-button"));
                    btnRemove.Click();
                    System.Threading.Thread.Sleep(2000);
                    IWebElement btnYesPopup = PropertiesCollection.driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[3]/div/div[2]/div[1]/div/div"));
                    btnYesPopup.Click();
                    System.Threading.Thread.Sleep(2000);
                }
            }
        }

        [TestCategory("Progression")]
        [TestCategory("US_41889_KnowledgeBase")]
        [TestMethod]
        public void US_41889_PreventMaliciousFileUpload_KnowledgeBase_InvalidFiles()
        {
            PropertiesCollection.test = PropertiesCollection.extent.CreateTest("US_41889_PreventMaliciousFileUpload_KnowledgeBase_InvalidFiles");

            var connection = new ConnectToMySQL_Fetch_TestData();
            var sidebarMenu = new FpSideMenus();
            var knowledgeBase = new FpKnowledgeBasePage();
            var folderLocation = "Malicious Files\\";

            sidebarMenu.KnowledgeBaseClick();
            System.Threading.Thread.Sleep(4000);

            foreach (var file in fileList_ValidVideoFiles)
            {
                System.Threading.Thread.Sleep(4000);
                knowledgeBase.btnAddKnowledgebaseByEntry.Click();
                System.Threading.Thread.Sleep(2000);
                knowledgeBase.btnBrowse.Click();
                System.Threading.Thread.Sleep(4000);
                AutoIt.AutoItX.ControlFocus("Open", "", "Edit1");
                AutoIt.AutoItX.ControlSetText("Open", "", "Edit1", System.Configuration.ConfigurationManager.AppSettings["MaliciousFileUploadLocation"] + folderLocation + file);
                AutoIt.AutoItX.ControlClick("Open", "", "Button1");
                System.Threading.Thread.Sleep(2000);
                knowledgeBase.btnSave.Click();
                System.Threading.Thread.Sleep(2000);

                string filename = knowledgeBase.grdFileNameRow.Text;
                try
                {
                    Assert.AreEqual(file, filename);
                    PropertiesCollection.test.Log(Status.Pass, "File: " + filename + " is successfully uploaded from Location: " + System.Configuration.ConfigurationManager.AppSettings["MaliciousFileUploadLocation"] + folderLocation);
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "File: " + filename + "is not successfully uploaded");
                }

                knowledgeBase.btnDelete.Click();
                IWebElement btnOKConfirmation = PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"_0_btn_modal\"]"));
                btnOKConfirmation.Click();
            }
        }

        [TestCleanup]
        public void TestCleanup()
        {
            PropertiesCollection.driver.Close();
            PropertiesCollection.driver.Quit();
            PropertiesCollection.driver.Dispose();
            System.Threading.Thread.Sleep(30000);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            PropertiesCollection.extent.Flush();
        }
    }
}
