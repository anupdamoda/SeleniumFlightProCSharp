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
    class clsShiftAdministration
    {

        public WebDriverWait wait;

        public clsShiftAdministration()
        {
            PageFactory.InitElements(PropertiesCollection.driver, this);
        }

        /***************** WebElements on Shift Administration Page *******************/

        [FindsBy(How = How.XPath, Using = "//*[@id=\"ShiftTypeListGrid\"]/div/button")]
        public IWebElement btnAddShift { get; set; }

        [FindsBy(How = How.Id, Using = "ShiftTypeName")]
        public IWebElement txtShiftName { get; set; }

        [FindsBy(How = How.Id, Using = "ShiftTypeShortName")]
        public IWebElement txtShortCode { get; set; }

        [FindsBy(How = How.Id, Using = "From")]
        public IWebElement btnStartFrom { get; set; }

        [FindsBy(How = How.Id, Using = "Duration")]
        public IWebElement txtDuration { get; set; }

        [FindsBy(How = How.Id, Using = "Active")]
        public IWebElement chkboxActive { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id=\"gridRequiredCurrencies\"]/div[1]/a")]
        public IWebElement btnAddCurrency { get; set; }


        [OpenQA.Selenium.Support.PageObjects.FindsBy(How = How.Id, Using = "//*[@id=\"form\"]/div/div[2]/fieldset/table/tbody/tr[5]/td[2]/div/div/span[2]/span/span[2]")]
        public IWebElement selColour { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id=\"form\"]/div/div[2]/fieldset/table/tbody/tr[5]/td[2]/div/div/span[2]/span/span[1]")]
        public IWebElement grdColour { get; set; }

        [FindsBy(How = How.Id, Using = "btnSave")]
        public IWebElement btnSave { get; set; }




        /***************** WebElements on Currency Popup Window - on Shift Administration Page *******************/

        // for getting list all the currencies in Currency Selection in 4th Column
        [FindsBy(How = How.XPath, Using = "//*[@id=\"gridShiftTypeCurrencies\"]/div[2]/table/tbody/tr/td[4]")]
        public IList<IWebElement> allCurrencies { get; set; }

        // for getting list all the checkboxes in Currency Selection next to Currencies
        [FindsBy(How = How.XPath, Using = "//input[@type='checkbox' and @class='chkbx']")]
        public IList<IWebElement> allCheckboxes { get; set; }


        [FindsBy(How = How.XPath, Using = "//*[@id=\"MainBody\"]/div[6]/div[1]/div/a[3]")]
        public IWebElement btnClosePopupWindow { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id=\"btnApplyCurrencyWindow\"]")]
        public IWebElement btnCurrenciesApplyPopupWindow { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id=\"btnCancelCurrencyWindow\"]")]
        public IWebElement btnCurrenciesCancelPopupWindow { get; set; }

        /*********************************************************************************************************/

        // for getting all the rows related to Shift Types on the second column
        [FindsBy(How = How.XPath, Using = "//*[@id=\"ShiftTypeListGrid\"]/table/tbody/tr/td[2]")]
        public IList<IWebElement> allShiftValue { get; set; }

        // for getting all the rows related to Edit Icons on First Column
        [FindsBy(How = How.XPath, Using = "//*[@id=\"ShiftTypeListGrid\"]/table/tbody/tr/td[1]/div/a[1]")]
        public IList<IWebElement> btnallEditIcons { get; set; }

        // for getting all the rows related to Copy Icons on First Column
        [FindsBy(How = How.XPath, Using = "//*[@id=\"ShiftTypeListGrid\"]/table/tbody/tr/td[1]/div/a[2]")]
        public IList<IWebElement> btnallDeleteIcons { get; set; }

        // for getting all the rows related to Delete Icons on First Column
        [FindsBy(How = How.XPath, Using = "//*[@id=\"ShiftTypeListGrid\"]/table/tbody/tr/td[1]/div/a[3]")]
        public IList<IWebElement> btnallCopyIcons { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id=\"_0_btn_modal\"]")]
        public IWebElement btnConfirmationOK { get; set; }

        // for getting the relevant ShiftName
        [FindsBy(How = How.XPath, Using = "//*[@id=\"ShiftTypeListGrid\"]/table/tbody/tr/td[2]")]
        public IList<IWebElement> lblShiftName { get; set; }

        // for getting the relevant Shortcode
        [FindsBy(How = How.XPath, Using = "//*[@id=\"ShiftTypeListGrid\"]/table/tbody/tr[3]/td[3]/div")]
        public IList<IWebElement> lblShortcode { get; set; }


        // for getting the relevant Start time
        [FindsBy(How = How.XPath, Using = "//*[@id=\"ShiftTypeListGrid\"]/table/tbody/tr/td[4]")]
        public IList<IWebElement> lblStartTime { get; set; }

        //for getting the relevant Duration
        [FindsBy(How = How.XPath, Using = "//*[@id=\"ShiftTypeListGrid\"]/table/tbody/tr/td[5]")]
        public IList<IWebElement> lblDuration { get; set; }

        //for getting the relevant Currencies
        [FindsBy(How = How.XPath, Using = "//*[@id=\"ShiftTypeListGrid\"]/table/tbody/tr/td[6]")]
        public IList<IWebElement> lblCurrencies { get; set; }

        //for getting the relevant Currencies
        [FindsBy(How = How.XPath, Using = "//*[@id=\"ShiftTypeListGrid\"]/table/tbody/tr/td[7]")]
        public IList<IWebElement> lblStatus { get; set; }


        public IWebElement lblErrorShiftNameUnique => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"errorMessages\"]/ul/li"));

        public IWebElement lblErrorShiftShortCodeRequired => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"ShiftTypeShortName_validationMessage\"]"));


        public clsShiftAdministration AddShiftdetails(String ShiftName, String Shortcode, String Start, String Duration, String Currencies)
        {

            var wait = new WebDriverWait(PropertiesCollection.driver, TimeSpan.FromSeconds(30));

            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//*[@id=\"ShiftTypeListGrid\"]/div/button")));


            IWebElement element = btnAddShift;
            Actions actions = new OpenQA.Selenium.Interactions.Actions(PropertiesCollection.driver);
            actions.MoveToElement(element).Perform();

            IJavaScriptExecutor executor = (IJavaScriptExecutor)PropertiesCollection.driver;
            executor.ExecuteScript("arguments[0].click();", element);


            txtShiftName.SendKeys(ShiftName);
            txtShortCode.SendKeys(Shortcode);

            btnStartFrom.SendKeys(Start);

            txtDuration.SendKeys(Keys.Control + "a");
            txtDuration.SendKeys(Keys.Control + "{DEL}");

            txtDuration.SendKeys(Duration);

            btnAddCurrency.Click();

            System.Threading.Thread.Sleep(3000);

            Console.WriteLine("The Count of Currencies" + allCurrencies.Count);

            Console.WriteLine("The Count of Checkboxes" + allCheckboxes.Count);

            for (int i = 0; i < allCurrencies.Count; i++)
            {
                if (allCurrencies.ElementAt(i).Text.Equals(Currencies))
                {
                    allCheckboxes.ElementAt(i).Click();
                    break;
                }
            }

            System.Threading.Thread.Sleep(3000);
            btnCurrenciesApplyPopupWindow.Click();
            System.Threading.Thread.Sleep(1000);

            IWebElement element1 = btnSave;

            executor.ExecuteScript("window.scrollBy(0,-200)", element1);
            System.Threading.Thread.Sleep(1000);

            btnSave.Click();
            System.Threading.Thread.Sleep(3000);
            return new clsShiftAdministration();

        }

        public clsShiftAdministration VerifyErrorMessages(String ShiftName, String Shortcode, String Start, String Duration, String Currencies)
        {

            var wait = new WebDriverWait(PropertiesCollection.driver, TimeSpan.FromSeconds(30));

            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//*[@id=\"ShiftTypeListGrid\"]/div/button")));


            IWebElement element = btnAddShift;
            Actions actions = new OpenQA.Selenium.Interactions.Actions(PropertiesCollection.driver);
            actions.MoveToElement(element).Perform();

            IJavaScriptExecutor executor = (IJavaScriptExecutor)PropertiesCollection.driver;
            executor.ExecuteScript("arguments[0].click();", element);


            txtShiftName.SendKeys(ShiftName);
            btnSave.Click();

            Assert.AreEqual(lblErrorShiftShortCodeRequired.Text, "A Short Code is required.");

            txtShortCode.SendKeys(Shortcode);

            btnSave.Click();
            System.Threading.Thread.Sleep(2000);
            Assert.AreEqual(lblErrorShiftNameUnique.Text, "The Shift Name must be unique for the Organisation Group.");
            

            System.Threading.Thread.Sleep(3000);
            return new clsShiftAdministration();

        }



        public clsShiftAdministration EditShiftdetails(string ShiftName, string UpdatedStartDate, string UpdatedDuration)
        {

            System.Threading.Thread.Sleep(3000);


            // Now please note that inside the web-table all column and rows are fixed
            // i.e size of each row for each column will be fixed 
            // i.e if size for ShiftTypes is = 4 then ,Edits ,Deletes ,Copying ,Start Dates 
            // will be same i.e =4

            // hence on that basic we can do it like below 


            for (int i = 0; i < allShiftValue.Count; i++)
            {

                if (allShiftValue.ElementAt(i).Text.Equals(ShiftName))
                {
                    btnallEditIcons.ElementAt(i).Click();
                    break;
                }
            }

            btnStartFrom.SendKeys(Keys.Control + "a");
            btnStartFrom.SendKeys(Keys.Control + "{DEL}");
            btnStartFrom.SendKeys(UpdatedStartDate);
            txtDuration.SendKeys(Keys.Control + "a");
            txtDuration.SendKeys(Keys.Control + "{DEL}");
            txtDuration.SendKeys(UpdatedDuration);

            btnSave.Click();

            return new clsShiftAdministration();
        }

        public clsShiftAdministration CopyShiftdetails(string ShiftName)
        {

            System.Threading.Thread.Sleep(3000);


            // Now please note that inside the web-table all column and rows are fixed
            // i.e size of each row for each column will be fixed 
            // i.e if size for ShiftTypes is = 4 then ,Edits ,Deletes ,Copying ,Start Dates 
            // will be same i.e =4

            // hence on that basic we can do it like below 


            for (int i = 0; i < allShiftValue.Count; i++)
            {

                if (allShiftValue.ElementAt(i).Text.Equals(ShiftName))
                {
                    btnallCopyIcons.ElementAt(i).Click();
                    break;
                }
            }
            System.Threading.Thread.Sleep(3000);
            btnSave.Click();
            return new clsShiftAdministration();
        }


        public clsShiftAdministration DeleteShiftdetails(String ShiftName)
        {

            System.Threading.Thread.Sleep(3000);
            // Now please note that inside the web-table all column and rows are fixed
            // i.e size of each row for each column will be fixed 
            // i.e if size for ShiftTypes is = 4 then ,Edits ,Deletes ,Copying ,Start Dates 
            // will be same i.e =4

            // hence on that basic we can do it like below 

            for (int i = 0; i < allShiftValue.Count; i++)
            {
                if (allShiftValue.ElementAt(i).Text.Equals(ShiftName))
                {
                    btnallDeleteIcons.ElementAt(i).Click();
                    break;
                }
            }

            System.Threading.Thread.Sleep(4000);

            try
            {
                if (btnConfirmationOK.Displayed == true && btnConfirmationOK.Enabled == true)
                {
                    btnConfirmationOK.Click();
                }
            }
            catch
            {
                return new clsShiftAdministration();
            }           
            return new clsShiftAdministration();
        }

        public string[] RetrieveShiftdetails(string Shiftname)
        {

            String[] Shiftdetails = new string[6];

            System.Threading.Thread.Sleep(3000);

            for (int i=0; i < allShiftValue.Count; i++)
            {
                if (allShiftValue.ElementAt(i).Text.Equals(Shiftname))
                {


                    Shiftdetails[0] = lblShiftName.ElementAt(i).Text;
               //    Shiftdetails[1] = lblShortcode.ElementAt(i).Text;
                    Shiftdetails[2] = lblStartTime.ElementAt(i).Text;
                     Shiftdetails[3] = lblDuration.ElementAt(i).Text;
                     Shiftdetails[4] = lblCurrencies.ElementAt(i).Text;
                     Shiftdetails[5] = lblStatus.ElementAt(i).Text;

                    break;
                }

            }

            return Shiftdetails;
            
        }
        public void NavigateShiftAdminBeforeSave(string Shiftname)
        {

            var wait = new WebDriverWait(PropertiesCollection.driver, TimeSpan.FromSeconds(30));

            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//*[@id=\"ShiftTypeListGrid\"]/div/button")));


            IWebElement element = btnAddShift;
            Actions actions = new OpenQA.Selenium.Interactions.Actions(PropertiesCollection.driver);
            actions.MoveToElement(element).Perform();

            IJavaScriptExecutor executor = (IJavaScriptExecutor)PropertiesCollection.driver;
            executor.ExecuteScript("arguments[0].click();", element);
            txtShiftName.SendKeys(Shiftname);
        }
    }
}
