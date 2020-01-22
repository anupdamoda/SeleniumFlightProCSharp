using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPWebAutomation_MSTests.PageObjects
{
    class FpTemplatesPage
    {
        /****** WebElements on Templates Page ********/

        public IWebElement Title => PropertiesCollection.driver.FindElement(By.XPath("//td[@class='tableColumnSubHeader'][contains(.,'Templates')]"));
        public IWebElement btnAddTemplate => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"btnAddTemplate\"]"));
        public IWebElement txtGroup => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"group\"]"));
        public IWebElement txtDescription => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"description\"]"));
        public IWebElement txtShortcode => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"code\"]"));
        public IWebElement txtURL => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"url\"]"));
        public IWebElement chkboxSecurityToken => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"includeSecurityTokens\"]"));
        public IWebElement chkboxActive => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"active\"]"));
        public IWebElement txtSequence => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"grdTemplates\"]/div[3]/table/tbody/tr/td[2]/span[1]/span/input[1]"));
        public IWebElement btnSave => PropertiesCollection.driver.FindElement(By.CssSelector(".k-button.k-grid-save-changes"));
        public IWebElement btnCancel => PropertiesCollection.driver.FindElement(By.ClassName("k-button k-grid-cancel-changes"));



        public IWebElement grdShortCode => PropertiesCollection.driver.FindElement(By.XPath(""));

        // var thisButton = cy.get(".k-button .k-grid-cancel-changes");


        public void addTemplates( )
        {
            System.Threading.Thread.Sleep(2000);
            btnAddTemplate.Click();
            txtSequence.SendKeys("13");
            txtShortcode.SendKeys("ShortCode");
            txtDescription.SendKeys("Description");
            txtGroup.SendKeys("Group");
            txtURL.SendKeys("URL");
            System.Threading.Thread.Sleep(2000);
          //  chkboxSecurityToken.Click();
            System.Threading.Thread.Sleep(2000);
          //  chkboxActive.Click();
            btnSave.Click();
        }

        /*
        public void retriveTemplatedetails()
        {

            String[] Shiftdetails = new string[6];

            System.Threading.Thread.Sleep(3000);

            for (int i = 0; i < allShiftValue.Count; i++)
            {
                if (allShiftValue.ElementAt(i).Text.Equals(Shiftname))
                {


                    Shiftdetails[0] = lblShiftName.ElementAt(i).Text;
                    //    Shiftdetails[1] = lblShortcode.ElementAt(i).Text;
                    Shiftdetails[2] = lblStartTime.ElementAt(i).Text;
                    Shiftdetails[3] = lblDuration.ElementAt(i).Text;
                    Shiftdetails[4] = lblCurrencies.ElementAt(i).Text;
                    Shiftdetails[5] = lblStatus.ElementAt(i).Text;

                    break;
                }

            }

            return Shiftdetails;


        }
        */
        public void deletetemplates()
        {

        }

        public void editTemplates()
        {

            


        }

    }
}
