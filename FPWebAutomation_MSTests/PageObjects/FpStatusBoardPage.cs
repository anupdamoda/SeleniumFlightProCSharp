using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPWebAutomation_MSTests.PageObjects
{
    class FpStatusBoardPage
    {
        /****** WebElements on Status Board Page ********/

        //public IWebElement Frame => PropertiesCollection.driver.FindElement(By.XPath("//iframe[@class='iframe-placeholder']"));
        public IWebElement StatusBoard => PropertiesCollection.driver.FindElement(By.XPath("//div[@id='divStatusBoardDetail']"));
    }
}
