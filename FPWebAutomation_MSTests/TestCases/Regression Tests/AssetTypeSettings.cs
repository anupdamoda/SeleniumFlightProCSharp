/* Regression Test Case -- FlightPro Web -> Admin > Assets > Asset Type Settings 
   Author -- Vandana Kalluru
   Dated - 11th Nov 2019*/

using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using FPWebAutomation_MSTests.Email;
using FPWebAutomation_MSTests.PageObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using System;
using System.Configuration;
using System.Threading;
using OpenQA.Selenium;
using System.Data.SqlClient;
using System.IO;

namespace FPWebAutomation_MSTests.TestCases.Regression_Tests
{
    class AssetTypeSettings
    {
        [TestClass]
        public class AssetTypeSettingsPage
        {
            public SqlConnection sqlCon;
            public SqlCommand command;
            string shortCode;
            string connectionString;
            string query;
            SqlDataReader reader;
            string TDAssetTypeLong;
            string WebAssetTypeLong;
            bool TDIsHistorical;
            string WebIsHistorical;
            Int64 assetTypeId;
            int TDAvailableStationCount;
            String WebAvailableStationCount;

            public TestContext TestContext { get; set; }

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
            }

            [TestCategory("Regression")]
            [TestMethod]
            public void AssetTypeSettings()
            {
                TC01_VerifyAssetTypeSettings();
                TC02_AddImage();
                TC03_RemoveImage();
                TC04_VerifyErrorMessage();
                TC05_NavigateToDifferentWindowWithoutSave();
            }
           
            public void TC01_VerifyAssetTypeSettings()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TC01_VerifyAssetTypeSettings");
                FpAssetTypeSettingsPage assetTypeSettings = new FpAssetTypeSettingsPage();

                assetTypeSettings.NavigateToAssetTypeSettings();

                shortCode = assetTypeSettings.txtShortCode.Text;
                WebAssetTypeLong = assetTypeSettings.txtAssetTypeLong.Text;
                WebIsHistorical = assetTypeSettings.txtIsHistorical.Text;
                WebAvailableStationCount = assetTypeSettings.txtAvailableStationCount.Text;

                connectionString = "Data Source=" + ConfigurationManager.AppSettings["SQLServerDataSource"] + ";Initial Catalog=" + ConfigurationManager.AppSettings["SQLServerInitialCatalog"] + ";Integrated Security=" + ConfigurationManager.AppSettings["SQLServerIntegratedSecurity"] + ';';
                sqlCon = new SqlConnection(connectionString);
                sqlCon.Open();

                query = "select AssetTypeId,AssetTypeLong,IsHistorical From tblAssettype where AssetType = '" + shortCode + "'";
                command = new SqlCommand(query, sqlCon);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    assetTypeId = reader.GetInt64(0);
                    TDAssetTypeLong = reader.GetString(1);
                    TDIsHistorical = reader.GetBoolean(2);
                }

                reader.Close();

                query = "select AvailableStations from tblAssetTypeSetting where AssetTypeID = '" + assetTypeId + "'";
                command = new SqlCommand(query, sqlCon);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    TDAvailableStationCount = reader.GetInt32(0);
                }

                reader.Close();
                sqlCon.Close();

                try
                {
                    int count = Int32.Parse(WebAvailableStationCount);
                    Assert.AreEqual(count, TDAvailableStationCount);
                    PropertiesCollection.test.Log(Status.Pass, "Validation for available station count has passed");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Validation for available station count has not passed");
                }
                try
                {
                    Assert.AreEqual(TDAssetTypeLong, WebAssetTypeLong);
                    PropertiesCollection.test.Log(Status.Pass, "Validation for Asset type long name has passed");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Validation for Asset type long name has not passed");
                }
                try
                {
                    bool historical;
                    if (WebIsHistorical == "Yes")
                    {
                        historical = false;
                    }
                    else
                    {
                        historical = true;
                    }
                    Assert.AreEqual(historical, TDIsHistorical);
                    PropertiesCollection.test.Log(Status.Pass, "Validation for Is historical field has passed");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Validation for Is historical field has not passed");
                }
            }

            public void TC02_AddImage()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TC02_AddImage");
                FpAssetTypeSettingsPage assetTypeSettings = new FpAssetTypeSettingsPage();
                String IsImageAdded = assetTypeSettings.AddImage();

                try
                {
                    Assert.AreEqual(IsImageAdded, "Yes");
                    PropertiesCollection.test.Log(Status.Pass, "Adding the image is successful");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Adding the image is not successful");
                }

            }

            public void TC03_RemoveImage()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TC03_RemoveImage");
                FpAssetTypeSettingsPage assetTypeSettings = new FpAssetTypeSettingsPage();

                assetTypeSettings.NavigateToAssetTypeSettings();

                String isImagePresent = assetTypeSettings.txtImage.Text;

                if (isImagePresent == "Yes")
                {
                    String isImageRemoved = assetTypeSettings.RemoveImage();

                    try
                    {
                        Assert.AreEqual(isImageRemoved, "No");
                        PropertiesCollection.test.Log(Status.Pass, "Removing the image is successful");
                    }
                    catch
                    {
                        PropertiesCollection.test.Log(Status.Fail, "Removing the image is not successful");
                    }
                }
                else
                {
                    PropertiesCollection.test.Log(Status.Pass, "There is no image to be removed");
                }

            }

            public void TC04_VerifyErrorMessage()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TC04_VerifyErrorMessage");
                FpAssetTypeSettingsPage assetTypeSettings = new FpAssetTypeSettingsPage();

                assetTypeSettings.NavigateToAssetTypeSettings();

                assetTypeSettings.btnEditAssetTypeSettings.Click();

                Thread.Sleep(500);

                assetTypeSettings.txtAvailableStations.Clear();
                assetTypeSettings.txtAvailableStations.SendKeys("abcd");
                assetTypeSettings.txtAvailableStations.SendKeys(Keys.Return);

                IWebElement element = assetTypeSettings.txtErrorMessage;

                if (element.Displayed)
                {
                    PropertiesCollection.test.Log(Status.Pass, "Validation for error message has passed");
                }
                else
                {
                    PropertiesCollection.test.Log(Status.Fail, "Validation for error message has failed");
                }

                Thread.Sleep(1500);

                assetTypeSettings.txtAvailableStations.Clear();
                assetTypeSettings.txtAvailableStations.SendKeys("2");
                assetTypeSettings.btnSave.Click();
            }

            public void TC05_NavigateToDifferentWindowWithoutSave()
            {
                PropertiesCollection.test = PropertiesCollection.extent.CreateTest("TC05_NavigateToDifferentWindowWithoutSave");
                FpAssetTypeSettingsPage assetTypeSettings = new FpAssetTypeSettingsPage();
                FpSideMenus sideMenus = new FpSideMenus();
                assetTypeSettings.NavigateToAssetTypeSettings();

                assetTypeSettings.btnEditAssetTypeSettings.Click();

                Thread.Sleep(3000);

                assetTypeSettings.txtAvailableStations.Clear();
                assetTypeSettings.txtAvailableStations.SendKeys("3");
                assetTypeSettings.btnSave.Click();

                Thread.Sleep(5000);

                assetTypeSettings.btnEditAssetTypeSettings.Click();

                Thread.Sleep(3000);

                assetTypeSettings.txtAvailableStations.Clear();

                assetTypeSettings.txtAvailableStations.SendKeys("4");

                String mainWindow = PropertiesCollection.driver.CurrentWindowHandle;

                sideMenus.lnkStudentResults.Click();

                String childWindow = PropertiesCollection.driver.CurrentWindowHandle;

                PropertiesCollection.driver.SwitchTo().Window(childWindow);

                Thread.Sleep(5000);

                String errorText = assetTypeSettings.txtConfirmationMessage.Text;

                try
                {
                    String ExpectedErrorMessage = "Any unsaved changes will be lost. Are you sure?";
                    Assert.AreEqual(ExpectedErrorMessage, errorText);
                    PropertiesCollection.test.Log(Status.Pass, "Validation for message received when navigating to a different window without save has passed");
                }
                catch
                {
                    PropertiesCollection.test.Log(Status.Fail, "Validation for message received when navigating to a different window without save has not passed");
                }
                assetTypeSettings.btnCancel.Click();

                PropertiesCollection.driver.SwitchTo().Window(mainWindow);

                assetTypeSettings.btnSave.Click();

            }

            [TestCleanup]
            public void TestCleanup()
            {
                var status = TestContext.CurrentTestOutcome;

                if (TestContext.CurrentTestOutcome != UnitTestOutcome.Passed)
                {
                    PropertiesCollection.test.Log(Status.Fail, "Test failed and aborted");
                }
                PropertiesCollection.driver.Close();
                PropertiesCollection.driver.Quit();
                PropertiesCollection.driver.Dispose();
                Thread.Sleep(5000);
            }

            [ClassCleanup]
            public static void ClassCleanup()
            {
                PropertiesCollection.extent.Flush();
                var email = new SendEmail();
                //email.Email();
            }
        }
    }
}
