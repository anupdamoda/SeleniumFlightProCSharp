using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using System.Threading;


namespace FPWebAutomation_MSTests.PageObjects
{
    class FpBudgetAdministrationPage
    {
        /****** WebElements on Budget Administration Page ********/

        public IWebElement title => PropertiesCollection.driver.FindElement(By.XPath("//td[@class='tableColumnSubHeader'][contains(.,'Budget Administration')]"));
        public IWebElement cboOrgGroupSelector => PropertiesCollection.driver.FindElement(By.XPath("//span[@class='k-icon k-i-arrow-60-down']"));
        public IList<IWebElement> cboOrgGroupName => PropertiesCollection.driver.FindElements(By.XPath("//*[@id='BudgetListOrgGroupDropDown_listbox']/li"));
        public IWebElement btnAddBudget => PropertiesCollection.driver.FindElement(By.Id("addBudget"));

        /* For Retrieving relevant budget details on Budget Administration Page */
        public IList<IWebElement> lstEditBudget => PropertiesCollection.driver.FindElements(By.XPath("//table/tbody/tr/td[1]/div/a[1]/span[@class='k-icon k-i-edit']"));
        public IList<IWebElement> lstDeleteBudget => PropertiesCollection.driver.FindElements(By.XPath("//table/tbody/tr/td[1]/div/a[2]/span[@class='glyphicon glyphicon-trash']"));
        public IList<IWebElement> lstViewBudget => PropertiesCollection.driver.FindElements(By.XPath("//table/tbody/tr/td[1]/div/a[3]/span[@class='k-icon k-i-search']"));
        public IWebElement btnDeleteBudget => PropertiesCollection.driver.FindElement(By.Id("_0_btn_modal"));

        /* For Retrieving relevant budget names on Budget Administration Page */
        public IList<IWebElement> lblBudgetName => PropertiesCollection.driver.FindElements(By.XPath("//*[@id='BudgetListGrid']/table/tbody/tr/td[2]"));

        /****** WebElements on Add Budget/ Edit Budget Page********/
        public IWebElement txtBudgetName => PropertiesCollection.driver.FindElement(By.Name("BudgetName"));
        public IWebElement txtBudgetDesc => PropertiesCollection.driver.FindElement(By.Id("Description"));
        public IWebElement txtAllocation => PropertiesCollection.driver.FindElement(By.Id("AllocationTimeString"));
        public IWebElement btnSave => PropertiesCollection.driver.FindElement(By.Id("btnSave"));
        public IWebElement btnReturn => PropertiesCollection.driver.FindElement(By.Id("btnReturn"));
        public IWebElement txtConfirmationMessage => PropertiesCollection.driver.FindElement(By.XPath("//div[@class='k-window-content k-content']/div[1]"));
        public IWebElement btnCancel => PropertiesCollection.driver.FindElement(By.Id("_1_btn_modal"));

        /** Web Elements on Pane Selection dropdown Add Budget / Edit Budget Window**/
        public IWebElement cboPaneDropdown => PropertiesCollection.driver.FindElement(By.XPath("//div[@class='k-widget k-multiselect k-header']"));        
        public IWebElement cboPane => PropertiesCollection.driver.FindElement(By.Id("Panes"));
        public IList<IWebElement> lstPaneName => PropertiesCollection.driver.FindElements(By.XPath("//*//*[@id='Panes_listbox']/li"));

        /** Web Elements on Org Group Selector Window on Add Budget / Edit Budget Window**/
        public IWebElement btnorgGroupSelector => PropertiesCollection.driver.FindElement(By.Id("btnOrganisationGroup"));
        public IWebElement txtorgGroupName => PropertiesCollection.driver.FindElement(By.Id("txtSearch_grdOrgGroups"));
        public IWebElement btnSearchOrgGroup => PropertiesCollection.driver.FindElement(By.Id("btnSearch"));
        public IWebElement btnApplyOrgGroup => PropertiesCollection.driver.FindElement(By.Id("btnApplyOrgGroupsWindow"));
        public IWebElement txtBudgetName_ErrorMsg => PropertiesCollection.driver.FindElement(By.Id("BudgetName_validationMessage"));
        public IWebElement txtAllocation_ErrorMsg => PropertiesCollection.driver.FindElement(By.Id("AllocationTimeString_validationMessage"));
        public IList<IWebElement> cboOrgGroup => PropertiesCollection.driver.FindElements(By.XPath("//*[@id='grdOrgGroups']/div[2]/table/tbody/tr"));
        public IList<IWebElement> orgGroupName => PropertiesCollection.driver.FindElements(By.XPath("//*[@id='grdOrgGroups']/div[2]/table/tbody/tr/td[2]"));
        

        /** Web Elements on People Selector Window on Add Budget / Edit Budget window **/
        public IWebElement txtPeopleName => PropertiesCollection.driver.FindElement(By.Id("txtSearch_grdPeople"));
        public IWebElement btnPeople => PropertiesCollection.driver.FindElement(By.Id("btnPerson"));
        public IWebElement btnPeopleSearch => PropertiesCollection.driver.FindElement(By.Id("btnSearch"));
        public IWebElement txtPeopleSelection => PropertiesCollection.driver.FindElement(By.XPath("//*[@id='grdPeople']/div[2]/table/tbody/tr"));
        public IWebElement btnApplyPeopleSearch => PropertiesCollection.driver.FindElement(By.Id("btnApplyPeopleWindow"));

        /** Web Elements on Calendar **/
        public IWebElement btnPeriodFrom => PropertiesCollection.driver.FindElement(By.XPath("//span[@class='k-icon k-i-calendar']"));
        public IWebElement btnPeriodTo => PropertiesCollection.driver.FindElement(By.XPath("//span[@aria-controls='DateTo_dateview']/span[@class='k-icon k-i-calendar']"));
        public IWebElement txtDateFrom => PropertiesCollection.driver.FindElement(By.Id("DateFrom"));
        public IWebElement txtDateTo => PropertiesCollection.driver.FindElement(By.Id("DateTo"));

        /** Web Elements on Sub Groups dropdown selection on Add Budget / Edit Budget window **/
        public IWebElement btnSubGroupSelector => PropertiesCollection.driver.FindElement(By.XPath("//tbody/tr[9]/td[2]/div/div[@class='k-multiselect-wrap k-floatwrap']"));
        public IList<IWebElement> cboSubGroupName => PropertiesCollection.driver.FindElements(By.XPath("//*[@id='SubGroups_listbox']/li"));

        /** Web Elements on Asset types selection on Add Budget / Edit Budget window **/
        public IWebElement cboAssetTypesAir => PropertiesCollection.driver.FindElement(By.Name("checkedNodes"));

        /** Web Elements on Strip types selection on Add Budget / Edit Budget window **/
        public IWebElement btnStripTypeSelector => PropertiesCollection.driver.FindElement(By.XPath("//table/tbody/tr[11]/td[2]/div/div[@class='k-widget k-multiselect k-header']"));
        public IWebElement cboStripType => PropertiesCollection.driver.FindElement(By.Name("StripTypes"));

        /** Web Elements on Strip types selection on View Budget window **/
        public IWebElement btnSearchBudget => PropertiesCollection.driver.FindElement(By.Id("btnSearch"));
        public IWebElement btnReturnBudget => PropertiesCollection.driver.FindElement(By.Id("btnReturn"));

        public void NavigateToBudgetAdminPage()
        {
            Thread.Sleep(5000);
            FpAdminMenus adminMenus = new FpAdminMenus();
            adminMenus.AdminClick();
            Thread.Sleep(3000);
            adminMenus.BudgetAdministrationClick();
            Thread.Sleep(6000);
        }

        public void AddBudget(string strBudgetName, string strDescription, string strOrgGroup, string strPerson, string strPane, string strSubGroup, string strAssetType, string strStripType, string strDateFrom, string strDateTo, string strAllocation)
        {
            NavigateToBudgetAdminPage();
            cboOrgGroupSelector.Click();
            
            Thread.Sleep(3000);

            for (int i = 0; i < cboOrgGroupName.Count; i++)
            {
                if (cboOrgGroupName.ElementAt(i).Text.Equals(strOrgGroup))
                {
                    cboOrgGroupName.ElementAt(i).Click();
                    break;
                }
            }

            Thread.Sleep(2000);
            btnAddBudget.Click();
            Thread.Sleep(5000);

            txtBudgetName.SendKeys(strBudgetName);
            Thread.Sleep(2000);
            txtBudgetDesc.SendKeys(strDescription);
            Thread.Sleep(2000);
            
            btnorgGroupSelector.Click();
            Thread.Sleep(2000);            

            for (int i = 0; i < orgGroupName.Count; i++)
            {
                if (orgGroupName.ElementAt(i).Text.Equals(strOrgGroup))
                {
                    cboOrgGroup.ElementAt(i).Click();
                    break;
                }
            }

            Thread.Sleep(3000);
            btnApplyOrgGroup.Click();
            Thread.Sleep(5000);

            btnPeople.Click();
            Thread.Sleep(3000);
            txtPeopleName.SendKeys(strPerson);
            Thread.Sleep(3000);
            btnPeopleSearch.Click();
            Thread.Sleep(3000);
            txtPeopleSelection.Click();
            Thread.Sleep(3000);
            btnApplyPeopleSearch.Click();
            Thread.Sleep(3000);

            cboPaneDropdown.Click();
            Thread.Sleep(4000);

            for (int i = 0; i < lstPaneName.Count; i++)
            {
                if (lstPaneName.ElementAt(i).Text.Equals(strPane))
                {
                    lstPaneName.ElementAt(i).Click();
                    break;
                }
            }
            Thread.Sleep(4000);

            txtBudgetDesc.Click();
            Thread.Sleep(2000);

            btnSubGroupSelector.Click();
            Thread.Sleep(2000);

            for (int i = 0; i < cboSubGroupName.Count; i++)
            {
                if (cboSubGroupName.ElementAt(i).Text.Equals(strSubGroup))
                {
                    cboSubGroupName.ElementAt(i).Click();
                    break;
                }
            }
            Thread.Sleep(4000);

            txtBudgetDesc.Click();
            Thread.Sleep(2000);

            cboAssetTypesAir.Click();
            Thread.Sleep(3000);

            txtDateFrom.SendKeys(strDateFrom);
            Thread.Sleep(3000);
            txtDateTo.SendKeys(strDateTo);
            Thread.Sleep(3000);

            txtAllocation.SendKeys(strAllocation);
            Thread.Sleep(3000);

            btnSave.Click();
            Thread.Sleep(5000);
        }

        public void ViewBudget(string orgGroupName, string BudgetName)
        {
            NavigateToBudgetAdminPage();
            cboOrgGroupSelector.Click();

            Thread.Sleep(3000);

            for (int i = 0; i < cboOrgGroupName.Count; i++)
            {
                if (cboOrgGroupName.ElementAt(i).Text.Equals(orgGroupName))
                {
                    cboOrgGroupName.ElementAt(i).Click();
                    break;
                }
            }

            Thread.Sleep(4000);

            for (int i = 0; i < lblBudgetName.Count; i++)
            {
                if (lblBudgetName.ElementAt(i).Text.Equals(BudgetName))
                {
                    lstViewBudget.ElementAt(i).Click();
                    break;
                }
            }

            Thread.Sleep(4000);

            btnSearchBudget.Click();
            Thread.Sleep(4000);

            btnReturnBudget.Click();
            Thread.Sleep(4000);
        }


        public void EditBudget(string orgGroupName, string BudgetName, string BudgetDesc, string Allocation)
        {
            NavigateToBudgetAdminPage();
            cboOrgGroupSelector.Click();

            Thread.Sleep(5000);

            for (int i = 0; i < cboOrgGroupName.Count; i++)
            {
                if (cboOrgGroupName.ElementAt(i).Text.Equals(orgGroupName))
                {
                    cboOrgGroupName.ElementAt(i).Click();
                    break;
                }
            }

            Thread.Sleep(4000);

            for (int i = 0; i < lblBudgetName.Count; i++)
            {
                if (lblBudgetName.ElementAt(i).Text.Equals(BudgetName))
                {
                    lstEditBudget.ElementAt(i).Click();
                    break;
                }
            }

            Thread.Sleep(4000);

            txtBudgetDesc.Clear();
            Thread.Sleep(2000);

            txtBudgetDesc.SendKeys(BudgetDesc);
            Thread.Sleep(2000);

            txtAllocation.Clear();
            Thread.Sleep(2000);
            txtAllocation.SendKeys(Allocation);
            Thread.Sleep(2000);

            btnSave.Click();
        }

        public void DeleteBudget(string OrgGroupName, string BudgetName)
        {
            NavigateToBudgetAdminPage();
            cboOrgGroupSelector.Click();

            Thread.Sleep(5000);

            for (int i = 0; i < cboOrgGroupName.Count; i++)
            {
                if (cboOrgGroupName.ElementAt(i).Text.Equals("AT_Org Group1"))
                {
                    cboOrgGroupName.ElementAt(i).Click();
                    break;
                }
            }

            Thread.Sleep(4000);

            string mainWindow = PropertiesCollection.driver.CurrentWindowHandle;
            Thread.Sleep(4000);

            for (int i = 0; i < lblBudgetName.Count; i++)
            {
                if (lblBudgetName.ElementAt(i).Text.Equals(BudgetName))
                {
                    lstDeleteBudget.ElementAt(i).Click();
                    break;
                }
            }

            string childWindow = PropertiesCollection.driver.CurrentWindowHandle;

            PropertiesCollection.driver.SwitchTo().Window(childWindow);
            Thread.Sleep(5000);

            btnDeleteBudget.Click();
            Thread.Sleep(3000);
        }
               
        public string[] RetrieveBudgetDetails(string BudgetName)
        {
            string[] Budgetdetails = new string[4];

            Thread.Sleep(3000);

            for (int i = 0; i < lblBudgetName.Count; i++)
            {
                if (lblBudgetName.ElementAt(i).Text.Equals(BudgetName))
                {
                    Budgetdetails[0] = lblBudgetName.ElementAt(i).Text;
                    break;
                }
            }
            return Budgetdetails;
        }
    }
}
