using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPWebAutomation_MSTests.PageObjects
{
    class FpPlanningBoardPage
    {
        /****** WebElements on Planning Board Page ********/

        public IWebElement Frame => PropertiesCollection.driver.FindElement(By.XPath("//iframe[@class='iframe-placeholder']"));
        public IWebElement Title => PropertiesCollection.driver.FindElement(By.XPath("//h4[@class='alert-heading'][contains(.,'Planning Board')]"));
        public IWebElement CboPlanningBoardName => PropertiesCollection.driver.FindElement(By.XPath("//lrp-planning-board-selector//kendo-dropdownlist/span[1]"));
        public IWebElement PlanningBoardSelection => PropertiesCollection.driver.FindElement(By.XPath("//lrp-planning-board-selector//kendo-dropdownlist//span[@class='k-input'][contains(., PlanningBoardName)]"));
        public IWebElement BtnClickHere => PropertiesCollection.driver.FindElement(By.XPath("//button[@type='button'][contains(.,'Click Here!')]"));
        public IWebElement TabActivityView => PropertiesCollection.driver.FindElement(By.XPath("//lrp-viewtype-selector//kendo-buttongroup//button[contains(.,'Activity View')]"));
        public IWebElement TabGroupView => PropertiesCollection.driver.FindElement(By.XPath("//lrp-viewtype-selector//kendo-buttongroup//button[contains(.,'Group View')]"));
        public IWebElement TabListView => PropertiesCollection.driver.FindElement(By.XPath("//lrp-viewtype-selector//button[contains(.,'List View')]"));
        public IWebElement GridRowSelection => PropertiesCollection.driver.FindElement(By.XPath("//daypilot-scheduler//div[3]/div[3]/div/div[2]/div[2]"));
        public IWebElement TabDaily => PropertiesCollection.driver.FindElement(By.XPath("//planning-board//*//button[contains(.,'Daily')]"));
        public IWebElement TabWeekly => PropertiesCollection.driver.FindElement(By.XPath("//planning-board//*//button[contains(.,'Weekly')]"));
        public IWebElement TabMonthly => PropertiesCollection.driver.FindElement(By.XPath("//planning-board//*//button[contains(.,'Monthly')]"));
        public IWebElement Activity => PropertiesCollection.driver.FindElement(By.XPath("//div[@aria-label='AT_Activity']"));
        public IWebElement ExpandTree => PropertiesCollection.driver.FindElement(By.XPath("//daypilot-scheduler//*[@class='scheduler_default_tree_image_expand']"));
        public IWebElement ActivitySelected => PropertiesCollection.driver.FindElement(By.XPath("//daypilot-scheduler//*[@class='scheduler_default_event_inner'][contains(.,OrganisationGroup)]"));
        public IWebElement TaskPackageSelection => PropertiesCollection.driver.FindElement(By.XPath("//scheduler-component//*//div[@class='scheduler_default_event_inner'][contains(.,'AT_ALS')]"));
        public IWebElement BtnPushToPane => PropertiesCollection.driver.FindElement(By.XPath("//button[contains(.,'Push to Pane')]"));
        public IWebElement PlanningBoardNameSelector => PropertiesCollection.driver.FindElement(By.XPath("//kendo-dropdownlist//span[@class='k-i-arrow-s k-icon']"));
        public IWebElement PlanningBoardNameList => PropertiesCollection.driver.FindElement(By.XPath("//kendo-list//li"));

        /****** WebElements on Activity Profile Popup ********/

        public IWebElement BtnActivityTypeSelector => PropertiesCollection.driver.FindElement(By.XPath("//lrp-activity-type-lookup//button"));
        public IWebElement TxtActivityName => PropertiesCollection.driver.FindElement(By.XPath("//input[@formcontrolname='activityName']"));
        public IWebElement CboStartDate => PropertiesCollection.driver.FindElement(By.XPath("//kendo-datepicker[@formcontrolname='startDate']//*//input"));
        public IWebElement CboEndDate => PropertiesCollection.driver.FindElement(By.XPath("//kendo-datepicker[@formcontrolname='endDate']//*//input"));
        public IWebElement BtnOrganisationGroupSelector => PropertiesCollection.driver.FindElement(By.XPath("//lrp-organisation-group-lookup//button"));
        public IWebElement CboStatus => PropertiesCollection.driver.FindElement(By.XPath("//kendo-dropdownlist[@formcontrolname='status']"));
        public IWebElement TxtDetails => PropertiesCollection.driver.FindElement(By.XPath("//textarea[@formcontrolname='details']"));
        public IWebElement BtnLocationSelector => PropertiesCollection.driver.FindElement(By.XPath("//lrp-location-lookup//button"));
        public IWebElement BtnSaveAll => PropertiesCollection.driver.FindElement(By.XPath("//button[contains(.,'Save All')]"));

        /****** WebElements on Activity Types Popup ********/

        public IWebElement TxtActivityCode => PropertiesCollection.driver.FindElement(By.XPath("//kendo-grid-string-filter-cell//input"));
        public IWebElement ActivitySelection => PropertiesCollection.driver.FindElement(By.XPath("//kendo-grid-list//*//input[@type='checkbox']"));
        public IWebElement BtnApply => PropertiesCollection.driver.FindElement(By.XPath("//button[contains(.,'Apply')]"));

        /****** WebElements on Organisation Groups  Popup ********/

        public IWebElement TxtOrganisationGroup => PropertiesCollection.driver.FindElement(By.XPath("//dx-tree-list//*//input[@class='dx-texteditor-input']"));
        public IWebElement OrganisationGroupSelection => PropertiesCollection.driver.FindElement(By.XPath("//div[@class='dx-treelist-content']//table[@class='dx-treelist-table dx-treelist-table-fixed']//tbody/tr[1]"));
        public IWebElement BtnOrgGroupApply => PropertiesCollection.driver.FindElement(By.XPath("//button[contains(.,'Apply')] "));
        public IWebElement TxtTaskCode => PropertiesCollection.driver.FindElement(By.XPath("//kendo-grid-filter-wrapper-cell//input"));
        public IWebElement TaskSelection => PropertiesCollection.driver.FindElement(By.XPath("//kendo-grid-list//*//input[@type='checkbox']"));
        public IWebElement BtnTaskApply => PropertiesCollection.driver.FindElement(By.XPath("//button[contains(.,'Apply')]"));

        /****** WebElements on Task Package Profile  Popup ********/

        public IWebElement BtnTaskSelector => PropertiesCollection.driver.FindElement(By.XPath("//lrp-strip-template-lookup//*[@class='k-icon k-i-find']"));
        public IWebElement BtnAssetTypeSelector => PropertiesCollection.driver.FindElement(By.XPath("//lrp-asset-type-lookup//*[@class='k-icon k-i-find']"));
        public IWebElement BtnShiftSelector => PropertiesCollection.driver.FindElement(By.XPath("//lrp-shift-type-lookup//*[@class='k-icon k-i-find']"));
        public IWebElement BtnSave => PropertiesCollection.driver.FindElement(By.XPath("//button[contains(.,'Save All')]"));
        public IWebElement TxtAssetTypeCode => PropertiesCollection.driver.FindElement(By.XPath("//kendo-grid-filter-wrapper-cell//input"));
        public IWebElement AssetTypeSelection => PropertiesCollection.driver.FindElement(By.XPath("//kendo-grid-list//*//input[@type='checkbox']"));
        public IWebElement BtnAssetTypeApply => PropertiesCollection.driver.FindElement(By.XPath("//button[contains(.,'Apply')]"));
        public IWebElement TxtShiftCode => PropertiesCollection.driver.FindElement(By.XPath("//kendo-grid-filter-wrapper-cell//input"));
        public IWebElement ShiftSelection => PropertiesCollection.driver.FindElement(By.XPath("//kendo-grid-list//*//input[@type='checkbox']"));
        public IWebElement BtnShiftApply => PropertiesCollection.driver.FindElement(By.XPath("//button[contains(.,'Apply')]"));
        public IWebElement BtnCancel => PropertiesCollection.driver.FindElement(By.XPath("//button[contains(.,'Cancel')]"));

        /****** WebElements on Pane Popup ********/
        public IWebElement TxtPaneOrganisationGroup => PropertiesCollection.driver.FindElement(By.XPath("//kendo-grid-filter-wrapper-cell//input"));
        public IWebElement PaneOrganisationGroupSelection => PropertiesCollection.driver.FindElement(By.XPath("//kendo-grid-list//*//input[@type='checkbox']"));
        public IWebElement BtnPaneApply => PropertiesCollection.driver.FindElement(By.XPath("//button[contains(.,'Apply')]"));

        /****** WebElements on Push to Pane Popup ********/
       
        public IWebElement PushtoPaneTitle => PropertiesCollection.driver.FindElement(By.XPath("//kendo-dialog-titlebar//div[@class='k-window-title k-dialog-title'][contains(.,'Pushed to Pane')]"));
		public IWebElement BtnOK => PropertiesCollection.driver.FindElement(By.XPath("//button[contains(.,'OK')]"));

        /****** WebElements on Confirmation Popup ********/

        public IWebElement BtnYes => PropertiesCollection.driver.FindElement(By.XPath("//button[contains(.,'Yes')]"));


        /* Create Activity on Planning Board Page  */

        public void CreateActivity(string ActivityCode, string ActivityName, string OrganisationGroup, string PlanningBoardName)
        {
            FpPlanningBoardPage PlanningBoard = new FpPlanningBoardPage();
            PlanningBoard.BtnActivityTypeSelector.Click();
            PlanningBoard.TxtActivityCode.SendKeys(ActivityCode);
            System.Threading.Thread.Sleep(5000);
            PlanningBoard.ActivitySelection.Click();
            System.Threading.Thread.Sleep(5000);
            PlanningBoard.BtnApply.Click();
            PlanningBoard.TxtActivityName.SendKeys(ActivityName);
            System.Threading.Thread.Sleep(6000);
            PlanningBoard.BtnOrganisationGroupSelector.Click();
            PlanningBoard.TxtOrganisationGroup.SendKeys(OrganisationGroup);
            System.Threading.Thread.Sleep(6000);
            PlanningBoard.OrganisationGroupSelection.Click();
            System.Threading.Thread.Sleep(6000);
            PlanningBoard.BtnOrgGroupApply.Click();
            PlanningBoard.BtnSaveAll.Click();
            System.Threading.Thread.Sleep(6000);
            PlanningBoard.TabDaily.Click();
            System.Threading.Thread.Sleep(30000);
        }


        public void DeleteActivity(string OrganisationGroup)
        {
            FpSideMenus SideMenu = new FpSideMenus();
            SideMenu.PlanningBoardClick();
            System.Threading.Thread.Sleep(60000);
            FpPlanningBoardPage PlanningBoard = new FpPlanningBoardPage();
            PropertiesCollection.driver.SwitchTo().Frame(PlanningBoard.Frame);
            PlanningBoard.TabDaily.Click();
            System.Threading.Thread.Sleep(10000);
            PlanningBoard.ExpandTree.Click();
            Actions action = new Actions(PropertiesCollection.driver);
            action.MoveToElement(PlanningBoard.ActivitySelected).ContextClick().Perform();
            System.Threading.Thread.Sleep(10000);
            action.MoveToElement(PlanningBoard.ActivitySelected).MoveByOffset(97, 88).Click().Perform();
            System.Threading.Thread.Sleep(3000);
            PlanningBoard.BtnYes.Click();
        }

        public void CreateTask(string TaskCode, string AssetTypeCode, string ShiftCode)
        {            
            FpSideMenus SideMenu = new FpSideMenus();
            SideMenu.PlanningBoardClick();
            System.Threading.Thread.Sleep(60000);
            FpPlanningBoardPage PlanningBoard = new FpPlanningBoardPage();
            PropertiesCollection.driver.SwitchTo().Frame(PlanningBoard.Frame);
            PlanningBoard.TabDaily.Click();
            System.Threading.Thread.Sleep(5000);
            PlanningBoard.ExpandTree.Click();
            Actions action = new Actions(PropertiesCollection.driver);
            action.MoveToElement(PlanningBoard.ActivitySelected).ContextClick().Perform();
            System.Threading.Thread.Sleep(5000);

            action.MoveToElement(PlanningBoard.ActivitySelected).MoveByOffset(97, 35).Click().Perform();
            System.Threading.Thread.Sleep(3000);

            PlanningBoard.BtnTaskSelector.Click();
            System.Threading.Thread.Sleep(15000);
            PlanningBoard.TxtTaskCode.SendKeys(TaskCode);
            System.Threading.Thread.Sleep(10000);
            PlanningBoard.TaskSelection.Click();
            System.Threading.Thread.Sleep(5000);
            PlanningBoard.BtnTaskApply.Click();

            PlanningBoard.BtnAssetTypeSelector.Click();
            PlanningBoard.TxtAssetTypeCode.SendKeys(AssetTypeCode);
            System.Threading.Thread.Sleep(5000);
            PlanningBoard.AssetTypeSelection.Click();
            System.Threading.Thread.Sleep(5000);
            PlanningBoard.BtnAssetTypeApply.Click();

            PlanningBoard.BtnShiftSelector.Click();
            PlanningBoard.TxtShiftCode.SendKeys(ShiftCode);
            System.Threading.Thread.Sleep(5000);
            PlanningBoard.ShiftSelection.Click();
            System.Threading.Thread.Sleep(5000);
            PlanningBoard.BtnShiftApply.Click();

            System.Threading.Thread.Sleep(6000);
            PlanningBoard.BtnSave.Click();
            System.Threading.Thread.Sleep(6000);
            PlanningBoard.TabDaily.Click();
            System.Threading.Thread.Sleep(30000);

            PlanningBoard.ExpandTree.Click();
            System.Threading.Thread.Sleep(5000);
        }

        public void PushTaskToPane(string TaskCode, string OrganisationGroup)
        {
            FpPlanningBoardPage PlanningBoard = new FpPlanningBoardPage();
            System.Threading.Thread.Sleep(15000);
            Actions act = new Actions(PropertiesCollection.driver);
            act.MoveToElement(PlanningBoard.TaskPackageSelection).ContextClick().Perform();
            System.Threading.Thread.Sleep(5000);

            act.MoveToElement(PlanningBoard.TaskPackageSelection).MoveByOffset(127, 10).Click().Perform();
            System.Threading.Thread.Sleep(15000);
            PlanningBoard.BtnPushToPane.Click();
            System.Threading.Thread.Sleep(5000);
            PlanningBoard.TxtPaneOrganisationGroup.SendKeys(OrganisationGroup);
            System.Threading.Thread.Sleep(5000);
            PlanningBoard.PaneOrganisationGroupSelection.Click();
            System.Threading.Thread.Sleep(5000);
            PlanningBoard.BtnPaneApply.Click();
            System.Threading.Thread.Sleep(30000);
        }
    }
}