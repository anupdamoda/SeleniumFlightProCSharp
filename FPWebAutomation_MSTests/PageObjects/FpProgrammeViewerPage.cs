using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPWebAutomation_MSTests.PageObjects
{
    class FpProgrammeViewerPage
    {
        /****** WebElements on Programme Viewer Page ********/
        public IWebElement Grid => PropertiesCollection.driver.FindElement(By.Id("mainGrid"));
    }
}
