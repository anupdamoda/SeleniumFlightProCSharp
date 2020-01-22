using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FPWebAutomation_MSTests.PageObjects
{
    class FpSideMenus
    
        #region WebElements on Side Menu
    {
        public IWebElement LnkSummary => PropertiesCollection.driver.FindElement(By.Id("href12003"));
        public IWebElement LnkCalendar => PropertiesCollection.driver.FindElement(By.Id("href12024"));
        public IWebElement LnkControlHoursReport => PropertiesCollection.driver.FindElement(By.Id("href12005"));
        public IWebElement LnkCurrencyHistoryReport => PropertiesCollection.driver.FindElement(By.Id("href12006"));
        public IWebElement LnkEventsReport => PropertiesCollection.driver.FindElement(By.Id("href12025"));
        public IWebElement LnkRoster => PropertiesCollection.driver.FindElement(By.Id("href12008"));
        public IWebElement LnkDailyScheduling => PropertiesCollection.driver.FindElement(By.Id("href12009"));
        public IWebElement LnkDutyTimes => PropertiesCollection.driver.FindElement(By.Id("href12010"));
        public IWebElement LnkControlHours => PropertiesCollection.driver.FindElement(By.Id("href12011"));
        public IWebElement LnkProgrammeViewer => PropertiesCollection.driver.FindElement(By.Id("href12021"));
        public IWebElement LnkPlanningBoard => PropertiesCollection.driver.FindElement(By.Id("href12103"));
        public IWebElement LnkKnowledgeBase => PropertiesCollection.driver.FindElement(By.Id("href12022"));
        public IWebElement LnkStatusBoard => PropertiesCollection.driver.FindElement(By.Id("href12023"));
        public IWebElement LnkCombinedSchedule => PropertiesCollection.driver.FindElement(By.Id("href12104"));
        public IWebElement lnkStudentResults => PropertiesCollection.driver.FindElement(By.Id("href12058"));
        #endregion

        public void SummaryClick()
        {
            LnkSummary.Click();
        }

        public void CalendarClick()
        {
            LnkCalendar.Click();
        }

        public void ControlHoursReportClick()
        {
            LnkControlHoursReport.Click();
        }

        public void CurrencyHistoryReportClick()
        {
            LnkCurrencyHistoryReport.Click();
        }

        public void EventsReportClick()
        {
            LnkEventsReport.Click();
        }

        public void RosterClick()
        {
            LnkRoster.Click();
        }

        public void DailySchedulingClick()
        {
            LnkDailyScheduling.Click();
        }

        public void DutyTimesClick()
        {
            LnkDutyTimes.Click();
        }

        public void ControlHoursClick()
        {
            LnkControlHours.Click();
        }

        public void ProgrammeViewerClick()
        {
            LnkProgrammeViewer.Click();
        }

        public void PlanningBoardClick()
        {
            LnkPlanningBoard.Click();
        }

        public void KnowledgeBaseClick()
        {
            LnkKnowledgeBase.Click();
        }

        public void StatusBoardClick()
        {
            LnkStatusBoard.Click();
        }

        public void CombinedScheduleClick()
        {
            LnkCombinedSchedule.Click();
        }

        public void StudentResultsClick()
        {
            lnkStudentResults.Click();
        }
    }
}
 