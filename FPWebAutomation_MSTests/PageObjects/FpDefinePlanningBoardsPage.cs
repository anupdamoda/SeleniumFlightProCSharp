using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FPWebAutomation_MSTests.PageObjects
{
    class FpDefinePlanningBoardsPage
    {
        /****** WebElements on Define Planning Boards Page ********/

        public IWebElement frame => PropertiesCollection.driver.FindElement(By.XPath("//iframe[@class='iframe-placeholder']"));
        public IWebElement title => PropertiesCollection.driver.FindElement(By.XPath("//planning-board-admin//div//div//h4[contains(.,'Planning Board Administration')]"));
        public IWebElement btnOrgGroupSelector => PropertiesCollection.driver.FindElement(By.XPath("//lrp-secure-organisation-group-lookup//button"));
        public IWebElement btnAddPlanningBoard => PropertiesCollection.driver.FindElement(By.XPath("//button[@class='k-button'][contains(.,'Add Planning Board')]"));
        public IWebElement txtIsPlanningBoardPresent = PropertiesCollection.driver.FindElement(By.XPath("//span[contains(.,strTDPlanningBoardName)]"));
        
        /****** WebElements on Organisation Groups Popup ********/
        public IWebElement txtOrganisationGroup => PropertiesCollection.driver.FindElement(By.XPath("//kendo-grid-string-filter-cell//*//input"));
        public IWebElement cboOrganisationGroupSelection => PropertiesCollection.driver.FindElement(By.XPath("//kendo-grid-list//*//table/tbody/tr[1]/td[1]/input"));
        public IWebElement btnApply => PropertiesCollection.driver.FindElement(By.XPath("//button[contains(.,'Apply')]"));

        /*  For getting list all Planning Board  Names */
        public IList<IWebElement> txtAllPlanningBoardName => PropertiesCollection.driver.FindElements(By.XPath("//kendo-grid/*/table/tbody/tr/td[2]"));

        /*  For getting relevant Planning Board Name */
        public IList<IWebElement> lblPlanningBoardName => PropertiesCollection.driver.FindElements(By.XPath("//kendo-grid/*/table/tbody/tr/td[2]"));
        public IList<IWebElement> btnDeletePlanningBoard => PropertiesCollection.driver.FindElements(By.XPath("//kendo-grid/*/table/tbody/tr/td[1]/button[2]"));
        public IList<IWebElement> btnEditPlanningBoard => PropertiesCollection.driver.FindElements(By.XPath("//kendo-grid/*/table/tbody/tr/td[1]/button[1]"));
        public IList<IWebElement> txtSupportingOrgGroup => PropertiesCollection.driver.FindElements(By.XPath("//kendo-grid/div/table/tbody/tr/td[4]"));

        /****** WebElements on Add Planning Boards Page ********/

        public IWebElement txtName => PropertiesCollection.driver.FindElement(By.XPath("//*[@formcontrolname='name']"));
        public IWebElement btnSave => PropertiesCollection.driver.FindElement(By.XPath("//button[@type='submit'][contains(.,'Save')]"));

        /****** WebElements on Add Planning Boards Page ********/
        public IWebElement btnSelectOrgGroup => PropertiesCollection.driver.FindElement(By.XPath("//span[@class='k-icon k-i-find']"));
        public IWebElement ckbxSelectOrgGroup => PropertiesCollection.driver.FindElement(By.XPath("//kendo-grid-list/div/div[1]/table/tbody/tr/td[1]/input"));
        public IWebElement txtSelectOrgGroup => PropertiesCollection.driver.FindElement(By.XPath("//kendo-grid-string-filter-cell/kendo-grid-filter-wrapper-cell/input"));
        //public IWebElement filterOrgGroup => PropertiesCollection.driver.FindElement(By.XPath("//kendo-grid-string-filter-cell/kendo-grid-filter-wrapper-cell/input"));
        public IWebElement btnApplyInAddPlanningBoard => PropertiesCollection.driver.FindElement(By.XPath("//kendo-dialog/div[2]/div/ng-component/div/div[2]/button[2]"));
        public IWebElement chkSupportingOrgGroups => PropertiesCollection.driver.FindElement(By.XPath("//div/input[@formcontrolname='displaySupportingOrganisationGroups']"));
        public IWebElement txtErrorMessage => PropertiesCollection.driver.FindElement(By.XPath("//div[@class='ng-star-inserted']"));
        public IWebElement btnReturn => PropertiesCollection.driver.FindElement(By.XPath("//ng-component/form/div/div[1]/button[1]"));
        public IWebElement txtConfirmationMsg => PropertiesCollection.driver.FindElement(By.XPath("//div[@class='k-content k-window-content k-dialog-content']"));
        public IWebElement btnConfirmationCancel => PropertiesCollection.driver.FindElement(By.XPath("//ng-component/kendo-dialog/div[2]/kendo-dialog-actions/button[2]"));

        /****** WebElements on Delete Planning Boards Popup ********/
        public IWebElement btnConfirmationOK => PropertiesCollection.driver.FindElement(By.XPath("//button[@class='k-button k-primary ng-star-inserted'][contains(.,'OK')]"));



        /* Add Planning Board  */

        public void AddPlanningBoard(string PlanningBoardName, string OrganisationGroup, string SelectOrgGroup)
        {
            FpAdminMenus AdminMenu = new FpAdminMenus();
            AdminMenu.AdminClick();
            AdminMenu.DefinePlanningBoardsClick();
            Thread.Sleep(10000);

            PropertiesCollection.driver.SwitchTo().Frame(frame);
            btnOrgGroupSelector.Click();
            Thread.Sleep(3000);
            txtOrganisationGroup.SendKeys(OrganisationGroup);
            Thread.Sleep(3000);
            cboOrganisationGroupSelection.Click();
            Thread.Sleep(5000);
            btnApply.Click();
            btnAddPlanningBoard.Click();
            Thread.Sleep(5000);
            txtName.SendKeys(PlanningBoardName);

            btnSelectOrgGroup.Click();
            Thread.Sleep(3000);
            txtSelectOrgGroup.SendKeys(SelectOrgGroup);
            Thread.Sleep(5000);
            ckbxSelectOrgGroup.Click();
            Thread.Sleep(5000);
            btnApplyInAddPlanningBoard.Click();
            Thread.Sleep(5000);

            btnSave.Click();
            Thread.Sleep(8000);
        }


        /****** Retrieve Planning Boards Details ********/

        public string[] RetrievePlanningBoarddetails(string PlanningBoardName)
        {
            string[] PlanningBoarddetails = new string[4];

            Thread.Sleep(3000);

            for (int i = 0; i < txtAllPlanningBoardName.Count; i++)
            {
                if (txtAllPlanningBoardName.ElementAt(i).Text.Equals(PlanningBoardName))
                {
                    PlanningBoarddetails[0] = lblPlanningBoardName.ElementAt(i).Text;
                    break;
                }
            }

            return PlanningBoarddetails;
        }

        /****** Delete  Planning Board ********/

        public FpDefinePlanningBoardsPage DeletePlanningBoard(String PlanningBoardName, String OrganisationGroup)
        {
            FpAdminMenus AdminMenu = new FpAdminMenus();
            AdminMenu.AdminClick();
            AdminMenu.DefinePlanningBoardsClick();
            Thread.Sleep(10000);
            
            PropertiesCollection.driver.SwitchTo().Frame(frame);
            Thread.Sleep(3000);
            btnOrgGroupSelector.Click();
            Thread.Sleep(3000);
            txtOrganisationGroup.SendKeys(OrganisationGroup);
            Thread.Sleep(2000);
            cboOrganisationGroupSelection.Click();
            Thread.Sleep(3000);
            btnApply.Click();
            Thread.Sleep(10000);

            for (int i = 0; i < txtAllPlanningBoardName.Count; i++)
            {
                if (txtAllPlanningBoardName.ElementAt(i).Text.Equals(PlanningBoardName))
                {
                    btnDeletePlanningBoard.ElementAt(i).Click();
                    break;
                }
            }

            Thread.Sleep(4000);

            try
            {
                if (btnConfirmationOK.Displayed == true && btnConfirmationOK.Enabled == true)
                {
                    btnConfirmationOK.Click();
                }
            }
            catch
            {
                return new FpDefinePlanningBoardsPage();
            }
            
            return new FpDefinePlanningBoardsPage();
        }

        /****** Edit  Planning Board ********/

        public string EditPlanningBoard(string PlanningBoardName, string OrganisationGroup)
        {
            string SupportingOrgGroup = "";
            btnOrgGroupSelector.Click();
            Thread.Sleep(3000);
            txtOrganisationGroup.SendKeys(OrganisationGroup);
            Thread.Sleep(2000);
            cboOrganisationGroupSelection.Click();
            Thread.Sleep(3000);
            btnApply.Click();
            Thread.Sleep(5000);

            for (int i = 0; i < txtAllPlanningBoardName.Count; i++)
            {
                if (txtAllPlanningBoardName.ElementAt(i).Text.Equals(PlanningBoardName))
                {
                    btnEditPlanningBoard.ElementAt(i).Click();
                    break;
                }
            }

            Thread.Sleep(4000);
            chkSupportingOrgGroups.Click();
            Thread.Sleep(2000);
            btnSave.Click();
            Thread.Sleep(5000);

            for (int i = 0; i < txtAllPlanningBoardName.Count; i++)
            {
                if (txtAllPlanningBoardName.ElementAt(i).Text.Equals(PlanningBoardName))
                {
                    SupportingOrgGroup = txtSupportingOrgGroup.ElementAt(i).Text;
                    break;
                }
            }

            return SupportingOrgGroup;
        }
    }
}
