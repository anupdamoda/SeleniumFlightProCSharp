using AutoIt;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Threading;

namespace FPWebAutomation_MSTests.PageObjects
{
    class FpAssetTypeSettingsPage
    {
        /****** WebElements on Asset Type Settings Page ********/

        public IWebElement title => PropertiesCollection.driver.FindElement(By.XPath("//td[@class='tableColumnSubHeader']//div//h4[contains(.,'Asset Types')]"));
        public IWebElement txtShortCode => PropertiesCollection.driver.FindElement(By.XPath("//div[@class='badge badge-cell badge-normal-font-weight'][1]"));
        public IWebElement txtAssetTypeLong => PropertiesCollection.driver.FindElement(By.XPath("//*[@id='AssetTypeListGrid']/table/tbody/tr[1]/td[3]"));
        public IWebElement txtIsHistorical => PropertiesCollection.driver.FindElement(By.XPath("//*[@id='AssetTypeListGrid']/table/tbody/tr[1]/td[6]"));
        public IWebElement txtAvailableStationCount => PropertiesCollection.driver.FindElement(By.XPath("//*[@id='AssetTypeListGrid']/table/tbody/tr[1]/td[4]"));
        public IWebElement txtImage => PropertiesCollection.driver.FindElement(By.XPath("//*[@id='AssetTypeListGrid']/table/tbody/tr[1]/td[5]"));
        public IWebElement btnEditAssetTypeSettings => PropertiesCollection.driver.FindElement(By.XPath("//*[@id='AssetTypeListGrid']/table/tbody/tr[1]/td[1]/div/a/span"));

        /****** WebElements on Edit Asset Type Settings Page ********/

        public IWebElement btnAddImage => PropertiesCollection.driver.FindElement(By.Id("btnAdd"));
        public IWebElement btnRemoveImage => PropertiesCollection.driver.FindElement(By.Id("btnClear"));
        public IWebElement btnSave => PropertiesCollection.driver.FindElement(By.Id("btnSave"));
        public IWebElement btnReturn => PropertiesCollection.driver.FindElement(By.Id("btnReturn"));
        public IWebElement txtAvailableStations => PropertiesCollection.driver.FindElement(By.Id("AvailableStations"));

        /****** WebElements on Error Message Popup ********/

        public IWebElement txtErrorMessage => PropertiesCollection.driver.FindElement(By.XPath("//*[@id='AvailableStations_validationMessage'][contains(.,' The field Available Stations must be a number.')]"));

        /****** WebElements on Confirmation Popup ********/

        public IWebElement txtConfirmationMessage => PropertiesCollection.driver.FindElement(By.XPath("//*[@id='MainBody']/div[5]/div[2]/div[1]"));
        public IWebElement btnCancel => PropertiesCollection.driver.FindElement(By.Id("_1_btn_modal"));
        public IWebElement btnOK => PropertiesCollection.driver.FindElement(By.Id("_0_btn_modal"));

        public void NavigateToAssetTypeSettings()
        {
            Thread.Sleep(5000);
            FpAdminMenus adminMenus = new FpAdminMenus();
            adminMenus.AdminClick();
            Thread.Sleep(1000);
            adminMenus.AssetTypeSettingsClick();
            Thread.Sleep(3000);
        }

        public String AddImage()
        {
            NavigateToAssetTypeSettings();

            btnEditAssetTypeSettings.Click();

            Thread.Sleep(3000);

            IWebElement element = btnAddImage;
            Actions actions = new Actions(PropertiesCollection.driver);
            actions.MoveToElement(element).Perform();
            actions.ClickAndHold().Release().Perform();

            string currentDir = System.IO.Directory.GetCurrentDirectory();
            var configFile = currentDir.Replace("bin\\Debug", "figther aircraft.jpg");

            AutoItX.WinWaitActive("Open");
            AutoItX.ControlFocus("Open", "", "Edit1");
            AutoItX.ControlSetText("Open", "", "Edit1", configFile);
            AutoItX.ControlClick("Open", "", "Button1");

            Thread.Sleep(1500);
            btnSave.Click();
            Thread.Sleep(5000);

            String isImageAdded = txtImage.Text;

            return isImageAdded;

        }

        public String RemoveImage()
        {            
            btnEditAssetTypeSettings.Click();

            Thread.Sleep(1500);

            IWebElement element = btnRemoveImage;
            Actions actions = new Actions(PropertiesCollection.driver);
            actions.MoveToElement(element).Perform();
            actions.ClickAndHold().Release().Perform();

            btnSave.Click();
            Thread.Sleep(3000);

            String isImageRemoved = txtImage.Text;

            return isImageRemoved;
        }
    }
}
