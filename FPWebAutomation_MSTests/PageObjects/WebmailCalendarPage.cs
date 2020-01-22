using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPWebAutomation_MSTests.PageObjects
{
    class WebmailCalendarPage
    {
        private String DayXPath = null;
        private String MonthXPath = null;
        private String YearXPath = null;
        private String CalendarXPath = null;

        /***************** WebElements on Webmail Calendar Page *******************/

        public IWebElement BtnDay => PropertiesCollection.driver.FindElement(By.XPath("//*[@id='primaryContainer']//span[contains(.,'Day')]"));
        public IWebElement BtnMonth => PropertiesCollection.driver.FindElement(By.XPath("//*[@id='primaryContainer']//span[contains(.,'Month')]"));
        public IWebElement CalendarDownIcon => PropertiesCollection.driver.FindElement(By.XPath("//*[@class='_fc_3 owaimg ms - Icon--chevronDown ms - icon - font - size - 21 ms - fcl - ns - b']"));
        public IWebElement MissionStrip => PropertiesCollection.driver.FindElement(By.XPath("//*[@id='primaryContainer']//span[contains(.,'FlightPro Mission')]"));
        public IWebElement TaskStrip => PropertiesCollection.driver.FindElement(By.XPath("//*[@id='primaryContainer']//span[contains(.,'FlightPro Task')]"));
        public IWebElement BriefStrip => PropertiesCollection.driver.FindElement(By.XPath("//*[@id='primaryContainer']//span[contains(.,'FlightPro Brief')]"));
        public IWebElement StickyNoteStrip => PropertiesCollection.driver.FindElement(By.XPath("//*[@id='primaryContainer']//span[contains(.,'FlightPro Sticky Note')]"));
        public IWebElement FormationStrip => PropertiesCollection.driver.FindElement(By.XPath("//*[@id='primaryContainer']//span[contains(.,'FlightPro Formation Group')]"));
        public IWebElement SortieStrip => PropertiesCollection.driver.FindElement(By.XPath("//*[@id='primaryContainer']//span[contains(.,'FlightPro Sortie Group')]"));
        public IWebElement UnavailabilityStrip => PropertiesCollection.driver.FindElement(By.XPath("//*[@id='primaryContainer']//span[contains(.,'FlightPro Unavailability')]"));

        public IWebElement CboNew => PropertiesCollection.driver.FindElement(By.XPath("//*[@id='_ariaId_29'][contains(.,'New')]"));
        public IWebElement AddIcon => PropertiesCollection.driver.FindElement(By.XPath("//div[@class='_wx_e']//*//span[@class='image owaimg ms-Icon--plus ms-icon-font-size-16-circle ms-fcl-tp-b ms-bcl-tp-b ms-bg-transparent ms-icon-font-circle _fce_t']"));


        /***************** WebElements on Webmail Calendar  *******************/
        public IWebElement DaySelection => PropertiesCollection.driver.FindElement(By.XPath(DayXPath));
        public IWebElement MonthSelection => PropertiesCollection.driver.FindElement(By.XPath(MonthXPath));
        public IWebElement YearSelection => PropertiesCollection.driver.FindElement(By.XPath(YearXPath));

        public IWebElement Calendar => PropertiesCollection.driver.FindElement(By.XPath(CalendarXPath));
        public IWebElement PreviousYearSelection => PropertiesCollection.driver.FindElement(By.XPath("//div[@class='_dx_f']//*//span[@class='_fc_3 owaimg ms-Icon--chevronLeft ms-icon-font-size-21 ms-fcl-ns-b']"));
        public IWebElement NextYearSelection => PropertiesCollection.driver.FindElement(By.XPath("//div[@class='_dx_f']//*//span[@class='_fc_3 owaimg ms-Icon--chevronRight ms-icon-font-size-21 ms-fcl-ns-b']"));

        /***************** WebElements on Webmail Calendar Details Window *******************/
        public IWebElement Title => PropertiesCollection.driver.FindElement(By.XPath("//input[@aria-label='Add a title for the event']"));
        public IWebElement CboCategorize => PropertiesCollection.driver.FindElement(By.XPath("//span[contains(.,'Categorize')]"));

        public IWebElement CategorySelection => PropertiesCollection.driver.FindElement(By.XPath("//span[contains(.,'FlightPro Unavailability (S...')]"));

        public IWebElement BtnSave => PropertiesCollection.driver.FindElement(By.XPath("//span[contains(.,'Save')]"));

        public void DateClick(int Day, String Month, int Year, String CalendarValue)
        {
            DayXPath = "//*//abbr[@role='gridcell'][@class='_dx_m ms-font-weight-regular'][contains(., '" + Day +"')]";
            MonthXPath = "//span[@role='gridcell'][contains(., '"+ Month +"')]";
            Console.WriteLine(MonthXPath);
            YearXPath = "//*//abbr[@role='gridcell'][contains(., '"+ Year +"')]";
            System.Threading.Thread.Sleep(5000);
            Console.WriteLine(CalendarValue);
            CalendarXPath = "//span[contains(.,'"+ CalendarValue +"')]";
            Console.WriteLine(CalendarXPath);
            System.Threading.Thread.Sleep(5000);
            Calendar.Click();            
            if (Year > DateTime.Now.Year) { NextYearSelection.Click(); }
            if (Year < DateTime.Now.Year) { PreviousYearSelection.Click(); }
            System.Threading.Thread.Sleep(5000);
            MonthSelection.Click();
            System.Threading.Thread.Sleep(5000);
            DaySelection.Click();
            System.Threading.Thread.Sleep(5000);
        }

    }
    
}

