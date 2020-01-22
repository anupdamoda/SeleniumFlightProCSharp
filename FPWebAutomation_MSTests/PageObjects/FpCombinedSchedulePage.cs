using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPWebAutomation_MSTests.PageObjects
{
    class FpCombinedSchedulePage
    {
        /****** WebElements on Combined Schedule Page ********/

        public IWebElement frame => PropertiesCollection.driver.FindElement(By.XPath("//iframe[@class='iframe-placeholder']"));
        public IWebElement title => PropertiesCollection.driver.FindElement(By.XPath("//*[@class='report-header']"));
        public IWebElement btnAddReport => PropertiesCollection.driver.FindElement(By.XPath("//button[@class='k-button glyphicon glyphicon-plus']"));
        public IWebElement reportTitle => PropertiesCollection.driver.FindElement(By.XPath("//kendo-dropdownlist[@class = 'k-widget k-dropdown k-header']//*//span[@class = 'reportItemTemplateNameBold ng-star-inserted'][text()]"));
        public IWebElement date => PropertiesCollection.driver.FindElement(By.XPath("//input[@class = 'k-input']"));
        public IWebElement btnDay => PropertiesCollection.driver.FindElement(By.XPath("//button[contains(.,'Day')]"));
        public IWebElement btnWeek => PropertiesCollection.driver.FindElement(By.XPath("//button[contains(.,'Week')]"));
        public IWebElement btnMonth => PropertiesCollection.driver.FindElement(By.XPath("//button[contains(.,'Month')]"));
        public IWebElement btnRefresh => PropertiesCollection.driver.FindElement(By.Id("btnRefresh"));
        public IWebElement grdStripContainer => PropertiesCollection.driver.FindElement(By.Id("stripContainer"));

        /****** Strips on Combined Schedule Page ********/

        public IWebElement missionStrip => PropertiesCollection.driver.FindElement(By.XPath("//*[@id='stripContainer']//*[text()[contains(.,'AT Mission Strip')]]"));
        public IWebElement sortieStrip1 => PropertiesCollection.driver.FindElement(By.XPath("//*[@id='stripContainer']//*[text()[contains(.,'AT Sortie Group Strip1')]]"));
        public IWebElement sortieStrip2 => PropertiesCollection.driver.FindElement(By.XPath("//*[@id='stripContainer']//*[text()[contains(.,'AT Sortie Group Strip2')]]"));
        public IWebElement sortieStrip3 => PropertiesCollection.driver.FindElement(By.XPath("//*[@id='stripContainer']//*[text()[contains(.,'AT Sortie Group Strip3')]]"));
        public IWebElement formationStrip => PropertiesCollection.driver.FindElement(By.XPath("//*[@id='stripContainer']//*[text()[contains(.,'[3] AT Formation Group Strip 1')]]"));

        /****** WebElements on Add Report Setings Popup Window ********/

        public IWebElement optReportOwner => PropertiesCollection.driver.FindElement(By.XPath("//*[@id = 'peopleOwner'][@class = 'k-radio']"));
        public IWebElement txtReportName => PropertiesCollection.driver.FindElement(By.XPath("//input[@class = 'ng-untouched ng-pristine ng-invalid k-textbox']"));
        public IWebElement txtOrganisationGroups => PropertiesCollection.driver.FindElement(By.XPath("//org-groups-multiselect//*[@class = 'k-input']"));
        public IWebElement cboTasks => PropertiesCollection.driver.FindElement(By.XPath("//task-multiselect//*[@class = 'k-input']"));
        public IWebElement txtClassification => PropertiesCollection.driver.FindElement(By.XPath("//input[@class = 'k-textbox']"));
        public IWebElement cboPanes => PropertiesCollection.driver.FindElement(By.XPath("//pane-multiselect//*[@class = 'k-input']"));
        public IWebElement txtNote => PropertiesCollection.driver.FindElement(By.XPath("//textarea[@class = 'note k-textarea']"));
        public IWebElement btnApply => PropertiesCollection.driver.FindElement(By.XPath("//button//*[@class = 'glyphicon glyphicon-refresh']"));
        public IWebElement btnDiscardChanges => PropertiesCollection.driver.FindElement(By.XPath("//button//*[@class = 'glyphicon glyphicon-remove']"));
        public IWebElement btnSave => PropertiesCollection.driver.FindElement(By.XPath("//button[@class = 'k-button k-primary']"));
        public IWebElement errReportName => PropertiesCollection.driver.FindElement(By.XPath(" //span[@class = 'reportError']"));
        public IWebElement paneSelection => PropertiesCollection.driver.FindElement(By.XPath("//*[@class='k-item ng-star-inserted'][contains(.,'AT_Production Pane')]"));


        /************ Method to Create Combined Schedule Report************/

        public string CreateCombinedScheduleReport(string ReportName, string OrganisationGroup, string Classification, string Pane, string Note)
        {

            FpSideMenus SideMenu = new FpSideMenus();
            SideMenu.CombinedScheduleClick();
            System.Threading.Thread.Sleep(30000);
            FpCombinedSchedulePage CombinedSchedule = new FpCombinedSchedulePage();
            PropertiesCollection.driver.SwitchTo().Frame(CombinedSchedule.frame);
            CombinedSchedule.btnAddReport.Click();
            System.Threading.Thread.Sleep(5000);            
            CombinedSchedule.txtReportName.SendKeys(ReportName);
            CombinedSchedule.txtOrganisationGroups.SendKeys(OrganisationGroup + Keys.Enter + Keys.Tab);
            CombinedSchedule.txtClassification.SendKeys(Classification);
            CombinedSchedule.cboPanes.Click();
            System.Threading.Thread.Sleep(5000);
            Actions act = new Actions(PropertiesCollection.driver);
            act.MoveToElement(CombinedSchedule.paneSelection).Click().SendKeys(Keys.Tab).Perform();
            System.Threading.Thread.Sleep(5000);
            CombinedSchedule.txtNote.SendKeys(Note);
            CombinedSchedule.btnSave.Click();
            System.Threading.Thread.Sleep(15000);
            return CombinedSchedule.reportTitle.Text;
        }
    }
}
