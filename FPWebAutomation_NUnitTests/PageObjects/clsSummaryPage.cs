using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPWebAutomation.PageObjects
{
    class clsSummaryPage
    {

        private IWebDriver driver;

      //  FPWeb_SummaryPage()
     //   {
     //       PageFactory.InitElements(driver, this);
      //  }


        /***************** WebElements under People Selector *******************/


        // Capturing the object properties of People Selector button on Summary Page
        [FindsBy(How = How.Id, Using = "btnPeopleFullName")]
        public IWebElement btn_PeopleSearch { get; set; }

        // Capturing the object properties of People Selector text
        [FindsBy(How = How.XPath, Using = "//input[@type='text' and @placeholder='Type part of Surname, Given Names, Nickname or Organisation/People Group and press Enter to search.']")]
        public IWebElement txtSearch_grdPeople { get; set; }

        // Capturing the object properties of People Selector search button
        [FindsBy(How = How.Id, Using = "btnSearch")]
        public IWebElement btn_Search { get; set; }

        // Capturing the object properties of People Selector table results
        [FindsBy(How = How.XPath, Using = "//*[@id=\"grdPeople\"]/div[2]/table")]
        public IWebElement grdPeopleSelectorResult { get; set; }


        // Capturing the object properties of Apply button
        [FindsBy(How = How.Id, Using = "btnApplyPeopleWindow")]
        public IWebElement btnApply { get; set; }


        [FindsBy(How = How.XPath, Using = "//*[@id=\"summary-content\"]/div[2]/div[2]/div/div[3]/div/table/tbody/tr/td[2]/div/span/span/span[2]")]
        public IWebElement EventRole_dropdown { get; set; }


        [FindsBy(How = How.XPath, Using = "//*[@id=\"EventRoleMask_listbox\"]")]
        public IWebElement Student_Selection { get; set; }



        [FindsBy(How = How.XPath, Using = "//*[@id=\"EventRoleMask_listbox\"]/li[3]")]
        public IWebElement Instructor_Selection { get; set; }


    }
}
