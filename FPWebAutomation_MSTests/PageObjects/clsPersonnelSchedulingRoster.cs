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
    class clsPersonnelSchedulingRoster
    {
        public WebDriverWait wait;

        public clsPersonnelSchedulingRoster()
        {
            PageFactory.InitElements(PropertiesCollection.driver, this);
        }

        /***************** WebElements on Personnel Scheduling - Rostering Page *******************/
        public IWebElement btnShiftfirst => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"divMainBody\"]/div[2]/div[2]/div[4]/div/div[1]/button"));
        public IWebElement btndatepicker => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"divMainBody\"]/div[2]/div[2]/div[1]/div[5]/span/span/span/span"));
        public IWebElement btnleftbutton => PropertiesCollection.driver.FindElement(By.XPath(".//div[@data-role='calendar']/div[@class='k-header']/a[@role='button']/span[@class='k-icon k-i-arrow-60-left']"));
        public IWebElement linkCalendarToday => PropertiesCollection.driver.FindElement(By.ClassName("k-link k-nav-today"));
        public IWebElement grdRosterViewlastcell => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"29\"]"));
        public IWebElement btnRemoveShortcode => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"MainBody\"]/div[8]/div/div[2]/div/div[1]"));
        public IList<IWebElement> grdShiftName => PropertiesCollection.driver.FindElements(By.XPath("//*[@id=\"rosterViewTable\"]/tbody/tr/td[3]"));
        public IWebElement btnEditRostering => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"btnEditRostering\"]"));
        public IWebElement btnlockExitEditRostering => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"btnEditRostering\"]/i"));


        public clsPersonnelSchedulingRoster ScheduleRoster(String ShiftName, String Shortcode, String Start, String Duration, String Currencies)
        {

            var wait = new WebDriverWait(PropertiesCollection.driver, TimeSpan.FromSeconds(30));
            /********* Click on Edit button if the Exit Edit button is enabled********/

            System.Threading.Thread.Sleep(6000);

            if (btnlockExitEditRostering.Displayed == true)
            {
                btnEditRostering.Click();
            }
            /*************************************************************************/

            System.Threading.Thread.Sleep(15000);
            new WebDriverWait(PropertiesCollection.driver, TimeSpan.FromSeconds(30)).Until(ExpectedConditions.ElementToBeClickable(btnEditRostering));
            btnEditRostering.Click();

            System.Threading.Thread.Sleep(15000);
            new WebDriverWait(PropertiesCollection.driver, TimeSpan.FromSeconds(30)).Until(ExpectedConditions.ElementToBeClickable(btndatepicker));
            btndatepicker.Click();

            wait.Until(ExpectedConditions.ElementToBeClickable(btnShiftfirst));
            System.Threading.Thread.Sleep(15000);
            // linkCalendarToday.Click();

            //  System.Threading.Thread.Sleep(2000);
            // btnleftbutton.Click();

            btnShiftfirst.Click();
            System.Threading.Thread.Sleep(15000);
            grdRosterViewlastcell.Click();          
            System.Threading.Thread.Sleep(15000);
            btnEditRostering.Click();

            return new clsPersonnelSchedulingRoster();
        }

        public clsPersonnelSchedulingRoster RemoveRoster(String Shortcode)
        {
            var wait = new WebDriverWait(PropertiesCollection.driver, TimeSpan.FromSeconds(75));

            System.Threading.Thread.Sleep(4000);

            if (btnlockExitEditRostering.Displayed == true)
            {
                btnEditRostering.Click();
            }

            /**********************************************************/

            System.Threading.Thread.Sleep(2000);
            new WebDriverWait(PropertiesCollection.driver, TimeSpan.FromSeconds(30)).Until(ExpectedConditions.ElementToBeClickable(btnEditRostering));
            btnEditRostering.Click();

            System.Threading.Thread.Sleep(3000);
            grdRosterViewlastcell.Click();

            System.Threading.Thread.Sleep(3000);
            Actions a = new Actions(PropertiesCollection.driver);
            a.MoveToElement(btndatepicker).Perform();

            System.Threading.Thread.Sleep(20000);

            a.MoveToElement(btnRemoveShortcode).Perform();
            System.Threading.Thread.Sleep(75000);

            
         //   wait.Until(ExpectedConditions.ElementToBeClickable(btnRemoveShortcode));
            btnRemoveShortcode.Click();

            System.Threading.Thread.Sleep(15000);
            btnEditRostering.Click();

            return new clsPersonnelSchedulingRoster();
        }
        internal static void ScheduleRoster()
        {
            throw new NotImplementedException();
        }


       
    }
}
