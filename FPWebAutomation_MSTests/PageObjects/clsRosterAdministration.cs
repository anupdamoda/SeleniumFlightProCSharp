using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPWebAutomation_MSTests.PageObjects
{
    class clsRosterAdministration
    {

        public WebDriverWait wait;

        public clsRosterAdministration()
        {
            PageFactory.InitElements(PropertiesCollection.driver, this);
        }

        /***************** WebElements on Roster Administration Page *******************/

        public IWebElement btnAddRoster => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"RosterMaintenanceGrid\"]/div/button"));
        public IWebElement txtRosterName => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"RosterName\"]"));
        public IWebElement btnTzLocationPicker => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"timezone-location-picker\"]/button"));
        public IWebElement btnTimeZone => PropertiesCollection.driver.FindElement(By.XPath("//*[contains(@id,'btnTimeZone')]"));
        public IList<IWebElement> lstTimeZones => PropertiesCollection.driver.FindElements(By.XPath("//*[@id=\"grdTimeZone\"]/div[2]/table/tbody/tr/td[2]"));
        public IWebElement btnApplyTimeZoneWindow => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"btnApplyTimeZoneWindow\"]"));
        public IWebElement btnLocation => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"btnLocations6d0a\"]"));
        public IWebElement txtLocationSelectio => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"txtSearch_grdLocation\"]"));
        public IWebElement btnLocationSelection => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"btnSearch\"]"));
        public IWebElement btnAddPeople => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"gridRosterPerson\"]/div[1]/a"));
        public IWebElement txtPeopleSelection => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"txtSearch_grdPeople\"]"));
        private IWebElement drpdwnPane => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"form\"]/div/table/tbody/tr[5]/td[2]/span/span/span[2]"));
        private IWebElement lstboxPanelist => PropertiesCollection.driver.FindElement(By.XPath("//ul[@id='PaneID_listbox']/li"));
        public IWebElement chkSelectAll => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"SelectAll\"]"));
        public IWebElement btnApplyPeopleWindow => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"btnApplyPeopleWindow\"]"));
        public IWebElement btnAddshift => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"gridRosterShiftType\"]/div[1]/a"));
        public IList<IWebElement> allShiftNames => PropertiesCollection.driver.FindElements(By.XPath("//*[@id=\"AddShiftGrid\"]/div[2]/table/tbody/tr/td[3]"));
        public IList<IWebElement> allchkboxes => PropertiesCollection.driver.FindElements(By.XPath("//*[@id=\"AddShiftGrid\"]/div[2]/table/tbody/tr/td[2]/input"));
        public IWebElement btnApplyShiftTypeWindow => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"btnApplyShiftTypeWindow\"]"));
        public IWebElement btnSave => PropertiesCollection.driver.FindElement(By.Id("btnSave"));
        public IWebElement btnConfirmationOK => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"_0_btn_modal\"]"));
        public IList<IWebElement> allRosterValue => PropertiesCollection.driver.FindElements(By.XPath("//*[@id=\"RosterMaintenanceGrid\"]/table/tbody/tr/td[2]"));
        public IList<IWebElement> btnallEditIcons => PropertiesCollection.driver.FindElements(By.XPath("//*[@id=\"RosterMaintenanceGrid\"]/table/tbody/tr/td[1]/div/a[1]"));
        public IList<IWebElement> btnallDeleteIcons => PropertiesCollection.driver.FindElements(By.XPath("//*[@id=\"RosterMaintenanceGrid\"]/table/tbody/tr/td[1]/div/a[2]"));
        public IList<IWebElement> btnDeleteDisabled => PropertiesCollection.driver.FindElements(By.ClassName("deleteButton k-button k-state-disabled href="));
        public IList<IWebElement> lblRosterName => PropertiesCollection.driver.FindElements(By.XPath("//*[@id=\"RosterMaintenanceGrid\"]/table/tbody/tr/td[2]"));
        public IList<IWebElement> lblPane => PropertiesCollection.driver.FindElements(By.XPath("//*[@id=\"RosterMaintenanceGrid\"]/table/tbody/tr/td[3]"));
        public IList<IWebElement> lblShiftTypes => PropertiesCollection.driver.FindElements(By.XPath("//*[@id=\"RosterMaintenanceGrid\"]/table/tbody/tr/td[4]"));
        public IList<IWebElement> lblStatus => PropertiesCollection.driver.FindElements(By.XPath("//*[@id=\"RosterMaintenanceGrid\"]/table/tbody/tr/td[5]"));
        public IWebElement btnDeleteRosterShiftType => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"gridRosterShiftType\"]/div[3]/table/tbody/tr[1]/td[2]/div/a"));
        public IWebElement lblErrorRosterNameRequired => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"RosterName_validationMessage\"]"));
        public IWebElement lblErrorPaneIDRequired => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"PaneID_validationMessage\"]"));

        public clsRosterAdministration AddRosterdetails(String RosterName, String Pane, String TimeZoneorLocation, String TimeZone, String Location, String People, String Shiftdetails, String Status)
        {

            var wait = new WebDriverWait(PropertiesCollection.driver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//*[@id=\"RosterMaintenanceGrid\"]/div/button")));
            IWebElement element = btnAddRoster;
            Actions actions = new OpenQA.Selenium.Interactions.Actions(PropertiesCollection.driver);
            actions.MoveToElement(element).Perform();
            IJavaScriptExecutor executor = (IJavaScriptExecutor)PropertiesCollection.driver;
            executor.ExecuteScript("arguments[0].click();", element);
            txtRosterName.SendKeys(RosterName);
            System.Threading.Thread.Sleep(5000);
            wait.Until(ExpectedConditions.ElementToBeClickable(drpdwnPane));
            drpdwnPane.Click();
            IList<IWebElement> options = PropertiesCollection.driver.FindElements(By.XPath("//ul[@id='PaneID_listbox']/li"));
            System.Threading.Thread.Sleep(5000);
            // Loop through the options and select the one that matches

            for (int i = 0; i < options.Count; i++)
            {
                if (options.ElementAt(i).Text.Equals(Pane))
                {
                    wait.Until(ExpectedConditions.ElementToBeClickable(options.ElementAt(i)));
                    options.ElementAt(i).Click();
                    break;
                }
            }            

            System.Threading.Thread.Sleep(2000);
            btnAddPeople.Click();
            System.Threading.Thread.Sleep(2000);
            wait.Until(ExpectedConditions.ElementToBeClickable(txtPeopleSelection));
            txtPeopleSelection.SendKeys(People);
            System.Threading.Thread.Sleep(2000);
            chkSelectAll.Click();
            btnApplyPeopleWindow.Click();
            System.Threading.Thread.Sleep(10000);
            btnAddshift.Click();

            System.Threading.Thread.Sleep(8000);
            for (int i = 0; i < allShiftNames.Count; i++)
            {

                if (allShiftNames.ElementAt(i).Text.Equals(Shiftdetails))
                {
                    allchkboxes.ElementAt(i).Click();
                    break;
                }
            }

            wait.Until(ExpectedConditions.ElementToBeClickable(btnApplyShiftTypeWindow));
            System.Threading.Thread.Sleep(8000);
            btnApplyShiftTypeWindow.Click();
            System.Threading.Thread.Sleep(3000);
            IWebElement element1 = btnSave;
            executor.ExecuteScript("window.scrollBy(0,-200)", element1);
            Actions a = new Actions(PropertiesCollection.driver);
            a.MoveToElement(btnSave).Perform();
            wait.Until(ExpectedConditions.ElementToBeClickable(btnSave));          
            btnSave.Click();
            System.Threading.Thread.Sleep(3000);
            return new clsRosterAdministration();
        }

        public clsRosterAdministration VerifyErrorMessages(String RosterName, String Pane, String TimeZoneorLocation, String TimeZone, String Location, String People, String Shiftdetails, String Status)
        {

            var wait = new WebDriverWait(PropertiesCollection.driver, TimeSpan.FromSeconds(30));

            //wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//*[@id=\"ShiftTypeListGrid\"]/div/button")));
            System.Threading.Thread.Sleep(4000);
            btnAddRoster.Click();
            IWebElement element = btnSave;
            Actions actions = new OpenQA.Selenium.Interactions.Actions(PropertiesCollection.driver);
            actions.MoveToElement(element).Perform();

            IJavaScriptExecutor executor = (IJavaScriptExecutor)PropertiesCollection.driver;
            executor.ExecuteScript("arguments[0].click();", element);


            System.Threading.Thread.Sleep(2000);

            Assert.AreEqual(lblErrorRosterNameRequired.Text, "A Roster Name is required.");          
            Assert.AreEqual(lblErrorPaneIDRequired.Text, "A Pane is required.");


            System.Threading.Thread.Sleep(3000);
            return new clsRosterAdministration();

        }

        public clsRosterAdministration EditRosterdetails(String RosterName, string shiftdetails)
        {
            var wait = new WebDriverWait(PropertiesCollection.driver, TimeSpan.FromSeconds(30));
            System.Threading.Thread.Sleep(3000);
            for (int i = 0; i < allRosterValue.Count; i++)
            {
                if (allRosterValue.ElementAt(i).Text.Equals(RosterName))
                {
                    btnallEditIcons.ElementAt(i).Click();
                    break;
                }
            }
            btnDeleteRosterShiftType.Click();
            System.Threading.Thread.Sleep(2000);
            if (btnConfirmationOK.Displayed == true && btnConfirmationOK.Enabled == true)
            {
                btnConfirmationOK.Click();
            }
            System.Threading.Thread.Sleep(2000);
            btnAddshift.Click();

            System.Threading.Thread.Sleep(8000);
            for (int i = 0; i < allShiftNames.Count; i++)
            {
                if (allShiftNames.ElementAt(i).Text.Equals(shiftdetails))
                {
                    allchkboxes.ElementAt(i).Click();
                    break;
                }
            }

            IJavaScriptExecutor executor = (IJavaScriptExecutor)PropertiesCollection.driver;
            wait.Until(ExpectedConditions.ElementToBeClickable(btnApplyShiftTypeWindow));
            System.Threading.Thread.Sleep(8000);
            btnApplyShiftTypeWindow.Click();
            System.Threading.Thread.Sleep(3000);
            IWebElement element1 = btnSave;
            executor.ExecuteScript("window.scrollBy(0,-200)", element1);
            Actions a = new Actions(PropertiesCollection.driver);
            a.MoveToElement(btnSave).Perform();
            wait.Until(ExpectedConditions.ElementToBeClickable(btnSave));
            btnSave.Click();
            System.Threading.Thread.Sleep(3000);
            return new clsRosterAdministration();

            if (btnConfirmationOK.Displayed == true)
            {
                btnConfirmationOK.Click();
            }

            return new clsRosterAdministration();
        }

        public clsRosterAdministration DeleteRosterdetails(String RosterName)
        {
            System.Threading.Thread.Sleep(3000);
            for (int i = 0; i < allRosterValue.Count; i++)
            {

                if (allRosterValue.ElementAt(i).Text.Equals(RosterName))
                {
                    btnallDeleteIcons.ElementAt(i).Click();
                    break;
                }
            }

            if (btnConfirmationOK.Displayed == true)
            { 
            btnConfirmationOK.Click();
            }

            return new clsRosterAdministration();
        }


        public string[] RetrieveRosterdetails(string Rostername)
        {
            String[] Rosterdetails = new string[4];
            System.Threading.Thread.Sleep(3000);
            for (int i = 0; i < allRosterValue.Count; i++)
            {
                if (allRosterValue.ElementAt(i).Text.Equals(Rostername))
                {
                    Rosterdetails[0] = lblRosterName.ElementAt(i).Text;
                    Rosterdetails[1] = lblPane.ElementAt(i).Text;
                    Rosterdetails[2] = lblShiftTypes.ElementAt(i).Text;
                    Rosterdetails[3] = lblStatus.ElementAt(i).Text;
                    break;
                }
            }
            return Rosterdetails;
        }
        public void NavigateRosterAdminBeforeSave(string Rostertname)
        {

            var wait = new WebDriverWait(PropertiesCollection.driver, TimeSpan.FromSeconds(30));

            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//*[@id=\"ShiftTypeListGrid\"]/div/button")));


            IWebElement element = btnAddRoster;
            Actions actions = new OpenQA.Selenium.Interactions.Actions(PropertiesCollection.driver);
            actions.MoveToElement(element).Perform();

            IJavaScriptExecutor executor = (IJavaScriptExecutor)PropertiesCollection.driver;
            executor.ExecuteScript("arguments[0].click();", element);
            txtRosterName.SendKeys(Rostertname);
        }
    }

}
