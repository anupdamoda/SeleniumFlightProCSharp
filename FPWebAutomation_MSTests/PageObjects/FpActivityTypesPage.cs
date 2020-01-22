using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FPWebAutomation_MSTests.PageObjects
{
    class FpActivityTypesPage
    {
        /****** WebElements on Activity Types Page ********/

        public IWebElement frame => PropertiesCollection.driver.FindElement(By.XPath("//iframe[@class='iframe-placeholder']"));
        public IWebElement title => PropertiesCollection.driver.FindElement(By.XPath("//activity-types-admin//div//div//h4[contains(.,'Activity Type Administration')]"));
        public IWebElement btnAddActivityType => PropertiesCollection.driver.FindElement(By.XPath("//button[@class='k-button k-button-icontext'][contains(.,'Add Activity Type')]"));
        public IList<IWebElement> btnDeleteActivityType => PropertiesCollection.driver.FindElements(By.XPath("//kendo-grid/*/table/tbody/tr/td[1]/span[@title='Deletes the record. This button is disabled if the record has been used']"));
        public IList<IWebElement> btnEditActivityType => PropertiesCollection.driver.FindElements(By.XPath("//kendo-grid/div/table/tbody/tr/td[1]/button"));
        
        /*  For getting list all Activity Short Codes */
        public IList<IWebElement> txtAllActivityShortCode => PropertiesCollection.driver.FindElements(By.XPath("//kendo-grid/*/table/tbody/tr/td[2]"));

        /*  For getting list all Activty Names */
        public IList<IWebElement> txtAllActivityName => PropertiesCollection.driver.FindElements(By.XPath("//kendo-grid/*/table/tbody/tr/td[3]"));

        /*  For getting relevant Activity Short Code */
        public IList<IWebElement> lblActivityShortCode => PropertiesCollection.driver.FindElements(By.XPath("//kendo-grid/*/table/tbody/tr/td[2]"));

        /*  For getting relevant Activty Name */
        public IList<IWebElement> lblActivityName => PropertiesCollection.driver.FindElements(By.XPath("//kendo-grid/*/table/tbody/tr/td[3]"));


        /****** WebElements on Add Activity Types Page ********/
        public IWebElement txtShortCode => PropertiesCollection.driver.FindElement(By.XPath("//*[@formcontrolname='shortCode']"));
        public IWebElement txtName => PropertiesCollection.driver.FindElement(By.XPath("//*[@formcontrolname='name']"));
        public IWebElement btnSave => PropertiesCollection.driver.FindElement(By.XPath("//button[@type='submit'][contains(.,'Save')]"));
        public IWebElement txtErrorMessage => PropertiesCollection.driver.FindElement(By.XPath("//div[@class='alert alert-danger col-sm-6']/div"));
        public IWebElement btnReturn => PropertiesCollection.driver.FindElement(By.XPath("//div/button[1][contains(.,'Return to Activity Type List')]"));
        public IWebElement txtConfirmationMsg => PropertiesCollection.driver.FindElement(By.XPath("//div[@class='k-content k-window-content k-dialog-content']"));
        public IWebElement btnCancel => PropertiesCollection.driver.FindElement(By.XPath("//div[2]/kendo-dialog-actions/button[@class='k-button ng-star-inserted']"));

        public IWebElement colourContainer => PropertiesCollection.driver.FindElement(By.XPath("//div[@class='dx-colorview-container-cell dx-colorview-palette-cell']"));

        /****** WebElements on Delete Activity Types Popup ********/
        public IWebElement btnConfirmationOK => PropertiesCollection.driver.FindElement(By.XPath("//button[@class='k-button k-primary ng-star-inserted'][contains(.,'OK')]"));

        /****** WebElements on Add colors popup ********/
        public IWebElement txtColour => PropertiesCollection.driver.FindElement(By.XPath("//label[@class='dx-colorview-label-hex']/*/div[@class='dx-texteditor-container']/input"));
        public IWebElement btnOK => PropertiesCollection.driver.FindElement(By.XPath("//div[@class='dx-toolbar-after']/div/div/div/div/span"));
        public IWebElement btnColour => PropertiesCollection.driver.FindElement(By.XPath("//div[@class='dx-button-content']/div"));

        /* Add Activity Type */

        public void AddActivitydetails(string ActivityShortCode, string ActivityName, string Colour)
        {      
            FpAdminMenus AdminMenu = new FpAdminMenus();
            AdminMenu.AdminClick();
            AdminMenu.ActivityTypesClick();
            Thread.Sleep(6000);       
            
            PropertiesCollection.driver.SwitchTo().Frame(frame);
            btnAddActivityType.Click();
            Thread.Sleep(6000);

            txtShortCode.SendKeys(ActivityShortCode);
            txtName.SendKeys(ActivityName);

            btnColour.Click();
            Thread.Sleep(5000);

            int height = colourContainer.Size.Height;
            int width = colourContainer.Size.Width;
            Console.WriteLine(height);
            Console.WriteLine(width);
            Actions actions = new Actions(PropertiesCollection.driver);
            actions.MoveToElement(colourContainer).MoveByOffset((-width / 3), 2).Click().Perform();
            Thread.Sleep(3000);
            btnOK.Click();
            Thread.Sleep(5000);
            btnSave.Click();
        }


        public string[] RetrieveActivitydetails(string ActivityName)
        {
            string[] Activitydetails = new string[4];
            Thread.Sleep(3000);

            for (int i = 0; i < txtAllActivityName.Count; i++)
            {
                if (txtAllActivityName.ElementAt(i).Text.Equals(ActivityName))
                {
                    Activitydetails[1] = lblActivityName.ElementAt(i).Text;
                    break;
                }
            }
            return Activitydetails;           
        }
               
        /****** Delete  ActivityType  ********/

        public FpActivityTypesPage DeleteActivity(String ActivityName)
        {
            Thread.Sleep(5000);            
            
            for (int i = 0; i < txtAllActivityName.Count; i++)
            {
                if (txtAllActivityName.ElementAt(i).Text.Equals(ActivityName))
                {
                    btnDeleteActivityType.ElementAt(i).Click();
                    break;
                }
            }

            Thread.Sleep(5000);
         
            try
            {
                if (btnConfirmationOK.Displayed == true && btnConfirmationOK.Enabled == true)
                {
                    btnConfirmationOK.Click();
                }
            }
            catch
            {
                return new FpActivityTypesPage();
            }

            return new FpActivityTypesPage();
        }

    }
}
