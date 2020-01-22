using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPWebAutomation_MSTests.PageObjects
{
    class MainPage_SidebarMenu
    {
        private IWebDriver driver;

        public MainPage_SidebarMenu()
        {
            PageFactory.InitElements(PropertiesCollection.driver, this);
        }

        #region Page Elements

        // Capturing the object properties of Summary link of Side Menu
        [FindsBy(How = How.Id, Using = "href12003")]
        public IWebElement Summary_link { get; set; }

        // Capturing the object properties of Calendar link of Side Menu
        [FindsBy(How = How.Id, Using = "href12024")]
        public IWebElement Calendar_link { get; set; }

        /***************** WebElements under My Reports ***************/
        // Capturing the object properties of Control Hours Report link of Side Menu
        [FindsBy(How = How.Id, Using = "href12005")]
        public IWebElement ControlHoursReport_link { get; set; }

        // Capturing the object properties of Currency History Report link of Side Menu
        [FindsBy(How = How.Id, Using = "href12006")]
        public IWebElement CurrencyHistoryReport_link { get; set; }

        // Capturing the object properties of Events Report link of Side Menu
        [FindsBy(How = How.Id, Using = "href12025")]
        public IWebElement EventsReport_link { get; set; }

        /***************** WebElements under Personnel Scheduling ***************/
        // Capturing the object properties of Roster link of Side Menu
        [FindsBy(How = How.Id, Using = "href12008")]
        public IWebElement Roster_link { get; set; }

        // Capturing the object properties of Daily Schedule link of Side Menu
        [FindsBy(How = How.Id, Using = "href12009")]
        public IWebElement DailyScheduling_link { get; set; }

        // Capturing the object properties of Duty Times link of Side Menu
        [FindsBy(How = How.Id, Using = "href12006")]
        public IWebElement DutyTimes_link { get; set; }

        // Capturing the object properties of Control Hours link of Side Menu
        [FindsBy(How = How.Id, Using = "href12011")]
        public IWebElement ControlHours_link { get; set; }

        /***************** WebElements under Common ****************************/

        // Capturing the object properties of Programme Viewer link of Side Menu
        [FindsBy(How = How.Id, Using = "href12021")]
        public IWebElement ProgrammeViewer_link { get; set; }

        // Capturing the object properties of Planning Board link of Side Menu
        [FindsBy(How = How.Id, Using = "href12103")]
        public IWebElement PlanningBoard_link { get; set; }

        // Capturing the object properties of Knowledge base link of Side Menu
        [FindsBy(How = How.Id, Using = "href12022")]
        public IWebElement KnowledgeBase_link { get; set; }

        // Capturing the object properties of Status Board link of Side Menu
        [FindsBy(How = How.Id, Using = "href12023")]
        public IWebElement StatusBoard_link { get; set; }

        // Capturing the object properties of Combined Schedule link of Side Menu
        [FindsBy(How = How.Id, Using = "href12104")]
        public IWebElement CombinedSchedule_link { get; set; }

        /***************** WebElements under Training *************************/
        // Capturing the object properties of Student Results link of Side Menu
        [FindsBy(How = How.Id, Using = "href12058")]
        public IWebElement StudentResults_link { get; set; }
        #endregion

        public void NavigatetoStudentResults()
        {
            StudentResults_link.Click();
        }
    }
}
