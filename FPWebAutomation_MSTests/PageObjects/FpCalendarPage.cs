using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPWebAutomation_MSTests.PageObjects
{
    class FpCalendarPage
    {
        #region WebElements on Calendar Page
        public IWebElement UserName => PropertiesCollection.driver.FindElement(By.Id("PeopleFullName"));
        public IWebElement Grid => PropertiesCollection.driver.FindElement(By.XPath("//*[@id='schdObligations']/table/tbody/tr[2]/td[2]/div/table/tbody/tr[5]/td"));
        public IWebElement StripDescription => PropertiesCollection.driver.FindElement(By.XPath("//*[@class = 'overflowtext']"));
        public IWebElement BtnToday => PropertiesCollection.driver.FindElement(By.XPath("//*[@class='k-state-default k-header k-nav-today']"));
        public IWebElement BtnDelete => PropertiesCollection.driver.FindElement(By.XPath("//*[@class = 'k-icon k-i-close']"));
        #endregion

        #region WebElements on People Unavailability Popup
        public IWebElement TxtDescription => PropertiesCollection.driver.FindElement(By.Id("unavailability-description"));
        public IWebElement BtnSave => PropertiesCollection.driver.FindElement(By.Id("btnSave"));
        public IWebElement BtnCancel => PropertiesCollection.driver.FindElement(By.Id("btnCancel"));
        #endregion
        
        #region WebElements on Calendar Delete Event Popup Page
        public IWebElement BtnDeleteEvent => PropertiesCollection.driver.FindElement(By.XPath("//*[@class='k-button k-primary k-scheduler-delete']"));
        public IWebElement lblPeopleUnavailability => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"wndPopup-0_wnd_title\"]"));
        #endregion

        public string GetLoggedInUserName() => UserName.Text;
        public string GetPeopleUnavailabilityDescription() => StripDescription.Text;

        public string CreatePeopleUnavailability(string Description)
        {
            FpSideMenus SideMenu = new FpSideMenus();
            SideMenu.CalendarClick();
            System.Threading.Thread.Sleep(30000);
            FpCalendarPage CalendarPage = new FpCalendarPage();
            CalendarPage.BtnToday.Click();
            System.Threading.Thread.Sleep(5000);
            Actions actions = new Actions(PropertiesCollection.driver);
            actions.MoveToElement(CalendarPage.Grid).Perform();
            actions.DoubleClick(CalendarPage.Grid).Perform();
            System.Threading.Thread.Sleep(30000);

            if (CalendarPage.TxtDescription.Text == Description)
            {
                CalendarPage.BtnSave.Click();
                System.Threading.Thread.Sleep(75000);
            }
            else
            {
                CalendarPage.TxtDescription.SendKeys(Description);
                CalendarPage.BtnSave.Click();
                System.Threading.Thread.Sleep(75000);
            }
            return CalendarPage.GetPeopleUnavailabilityDescription();
        }
    }
}
