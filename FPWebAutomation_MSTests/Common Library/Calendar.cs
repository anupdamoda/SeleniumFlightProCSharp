/* Helper File for Picking date from Calendar control
   Input Date parameter should be in format: 1 Nov 2019 / 30 Dec 2020
   Author: Anup Damodaran
   Dated: 20th Dec 2019 */

using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace FPWebAutomation_MSTests.Common_Library
{
    class CalendarFunctions
    {
        #region WebElements on Calendar control
        private static IWebElement btnCalendar => PropertiesCollection.driver.FindElement(By.XPath("//span[@class='k-icon k-i-calendar']"));
        private static IWebElement btnPrevious => PropertiesCollection.driver.FindElement(By.XPath("//div[@class='k-scheduler-calendar k-widget k-calendar']/*/a[@class ='k-link k-nav-prev']/span[@class='k-icon k-i-arrow-60-left']"));
        private static IWebElement btnNext => PropertiesCollection.driver.FindElement(By.XPath("//div[@class='k-scheduler-calendar k-widget k-calendar']/*/a[@class ='k-link k-nav-next']/span[@class='k-icon k-i-arrow-60-right']"));
        private static IWebElement txtMonthAndYear => PropertiesCollection.driver.FindElement(By.XPath("//div[@class='k-scheduler-calendar k-widget k-calendar']/*/a[@class='k-link k-nav-fast']"));
        private static IList<IWebElement> lstDate => PropertiesCollection.driver.FindElements(By.XPath("//table[@class='k-content k-month']/tbody/tr/td[not(@class)]/a | //table[@class='k-content k-month']/tbody/tr/td[@class!='k-other-month' and @class!='k-other-month k-weekend']/a"));
        private static IList<IWebElement> lstMonth => PropertiesCollection.driver.FindElements(By.XPath("//table[@class='k-content k-meta-view k-year']/tbody/tr[1]/td/a | //table[@class='k-content k-meta-view k-year']/tbody/tr[2]/td/a | //table[@class='k-content k-meta-view k-year']/tbody/tr[3]/td/a"));
        #endregion

        public static void PickDateInCalendarView(string newDate)
        {
            string[] newDateSplit = newDate.Split(' ');
            string date = newDateSplit[0];
            string month = newDateSplit[1];
            string year = newDateSplit[2];

            int intYear = Int32.Parse(year);

            btnCalendar.Click();
            Thread.Sleep(2000);

            txtMonthAndYear.Click();
            Thread.Sleep(1000);

            int currentYear = Int32.Parse(txtMonthAndYear.Text);

            #region Selecting Year
            if (currentYear == intYear)
            {
                goto SelectMonth;
            }
            else if (intYear < currentYear)
            {
                do
                {
                    btnPrevious.Click();
                } while (!txtMonthAndYear.Text.Equals(year));

            }
            else if (intYear > currentYear)
            {
                do
                {
                    btnNext.Click();
                } while (!txtMonthAndYear.Text.Equals(year));
            }
        #endregion Selecting year ends

        #region Selecting month
        SelectMonth:
            for (int i = 0; i < lstMonth.Count; i++)
            {
                if (lstMonth.ElementAt(i).Text.Equals(month))
                {
                    lstMonth.ElementAt(i).Click();
                    break;
                }
            }
            Thread.Sleep(1000);
            #endregion Selecting month ends

            #region Selecting Date
            for (int i = 0; i < lstDate.Count; i++)
            {
                if (lstDate.ElementAt(i).Text.Equals(date))
                {
                    lstDate.ElementAt(i).Click();
                    break;
                }
            }
            Thread.Sleep(3000);
            #endregion Selecting Date ends
        }
    }
}
