using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPWebAutomation.PageObjects
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


        [FindsBy(How = How.Id, Using = "//*[@id=\"form\"]/div/div[2]/fieldset/table/tbody/tr[5]/td[2]/div/div/span[2]/span/span[2]")]
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



        public clsShiftAdministration AddShiftdetails(String ShiftName,String Shortcode,String Start,String Duration, String Currencies)
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

            txtDuration.SendKeys(Keys.Control+"a");
            txtDuration.SendKeys(Keys.Control+"{DEL}");
            
            txtDuration.SendKeys(Duration);

            btnAddCurrency.Click();

            System.Threading.Thread.Sleep(1000);

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

            System.Threading.Thread.Sleep(1000);

            
            btnCurrenciesApplyPopupWindow.Click();

            btnSave.Click();

            System.Threading.Thread.Sleep(3000);
       

            return new clsShiftAdministration();

        }


        public clsShiftAdministration EditShiftdetails(String ShiftName, String UpdatedStartDate)
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
            btnSave.Click();

            return new clsShiftAdministration();
        }

        public clsShiftAdministration CopyShiftdetails(String ShiftName, String UpdatedShiftName , String UpdatedStartDate)
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

            txtShiftName.SendKeys(UpdatedShiftName);
            btnStartFrom.SendKeys(UpdatedStartDate);

            
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

            btnConfirmationOK.Click();

            return new clsShiftAdministration();
        }

    }
}
