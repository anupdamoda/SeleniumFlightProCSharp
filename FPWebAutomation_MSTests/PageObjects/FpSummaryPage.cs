using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPWebAutomation_MSTests.PageObjects
{
    class FpSummaryPage
    {
        #region WebElements on Summary Page
        public WebDriverWait wait = new WebDriverWait(PropertiesCollection.driver, TimeSpan.FromSeconds(60));

        public IWebElement UserName => PropertiesCollection.driver.FindElement(By.Id("PeopleFullName"));
        private IWebElement LoggedInUser => PropertiesCollection.driver.FindElement(By.XPath("//*[@id='MainBody']/div[2]/div/div[2]/ul/li[2]/a"));
        public IWebElement LblNoData => PropertiesCollection.driver.FindElement(By.XPath("//label[@id='lblnodata']"));
        public IWebElement BtnPeopleSelector => PropertiesCollection.driver.FindElement(By.XPath("//button[@id='btnPeopleFullName']"));
        public string PeopleSelectorUserName() => UserName.Text;
        public string GetLoggedInUserName() => LoggedInUser.Text;
        #endregion
        #region WebElements on People Selection popup
        public IWebElement TxtPeopleSearch => PropertiesCollection.driver.FindElement(By.XPath("//input[@id='txtSearch_grdPeople']"));
        public IWebElement PersonSelection => PropertiesCollection.driver.FindElement(By.XPath("//*[@id='grdPeople']/div[2]/table/tbody/tr[1]"));
        public IWebElement BtnSearch => PropertiesCollection.driver.FindElement(By.XPath("//button[@id='btnSearch']"));
        public IWebElement BtnCancel => PropertiesCollection.driver.FindElement(By.XPath("//button[contains(.,'Cancel')]"));
        public IWebElement BtnApply => PropertiesCollection.driver.FindElement(By.XPath("//button[contains(.,'Apply')]"));

        /****** WebElements on Knowledgebase Section of the Summary Page ********/
        public IWebElement KnowledgebaseTitle => PropertiesCollection.driver.FindElement(By.XPath("//*[@id='summary - content']//*//h5[contains(.,'Knowledge Base')]"));
        public IWebElement KnowledgebaseTableData => PropertiesCollection.driver.FindElement(By.XPath("/div[@id='grdKnowledgeBaseSummary']//div[@class='k-grid-content']"));
        public IWebElement KnowledgebaseGridFirstItem => PropertiesCollection.driver.FindElement(By.XPath(" //*[@id='grdKnowledgeBaseSummary']/div[2]/table/tbody/tr/td[2]"));
        public IWebElement KnowledgebaseTableHeader => PropertiesCollection.driver.FindElement(By.XPath("///div[@id='grdKnowledgeBaseSummary']//div[@class='k-grid-header']"));
        public IList<IWebElement> allKnowledgebaseItemvalue => PropertiesCollection.driver.FindElements(By.XPath("//*[@id='grdKnowledgeBaseSummary']/div[2]/table/tbody/tr/td[2]"));
        public IList<IWebElement> allKnowledgebaseStatusvalue => PropertiesCollection.driver.FindElements(By.XPath("//*[@id='grdKnowledgeBaseSummary']/div[2]/table/tbody/tr/td[8]"));
        public IList<IWebElement> lblItem => PropertiesCollection.driver.FindElements(By.XPath("//*[@id='grdKnowledgeBaseSummary']/div[2]/table/tbody/tr/td[2]"));
        public IList<IWebElement> lblNowDated => PropertiesCollection.driver.FindElements(By.XPath("//*[@id='grdKnowledgeBaseSummary']/div[2]/table/tbody/tr/td[3]"));
        public IList<IWebElement> lblYourDate => PropertiesCollection.driver.FindElements(By.XPath("//*[@id='grdKnowledgeBaseSummary']/div[2]/table/tbody/tr/td[4]"));
        public IList<IWebElement> lblNowVersioned => PropertiesCollection.driver.FindElements(By.XPath("//*[@id='grdKnowledgeBaseSummary']/div[2]/table/tbody/tr/td[5]"));
        public IList<IWebElement> lblYourVersion => PropertiesCollection.driver.FindElements(By.XPath("//*[@id='grdKnowledgeBaseSummary']/div[2]/table/tbody/tr/td[6]"));
        public IList<IWebElement> lblDaysOld => PropertiesCollection.driver.FindElements(By.XPath("//*[@id='grdKnowledgeBaseSummary']/div[2]/table/tbody/tr/td[7]"));
        public IList<IWebElement> lblStatus => PropertiesCollection.driver.FindElements(By.XPath("//*[@id='grdKnowledgeBaseSummary']/div[2]/table/tbody/tr/td[8]"));
        public IWebElement CountNever => PropertiesCollection.driver.FindElement(By.XPath("//div[@id='kbSummaryCounter']//span[@class='counter badge badge-yellow']"));
        public IWebElement CountOld => PropertiesCollection.driver.FindElement(By.XPath("//div[@id='kbSummaryCounter']//span[@class='counter badge badge-error']"));
        public IWebElement lblWorkflowTitle => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"wfSummaryCounter\"]/div/h5"));
        public IWebElement lblWorkflowicon => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"workflowsSummarySection\"]/div[1]"));
        public int countlblWorkflowTitle => PropertiesCollection.driver.FindElements(By.XPath("//*[@id=\"wfSummaryCounter\"]/div/h5")).Count;
        public int countlblWorkflowicon => PropertiesCollection.driver.FindElements(By.XPath("//*[@id=\"workflowsSummarySection\"]/div[1]/span")).Count;
        public IWebElement btnSettings => wait.Until(ExpectedConditions.ElementToBeClickable(PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"showExtraOptions\"]"))));
        public IWebElement btnSave => wait.Until(ExpectedConditions.ElementToBeClickable(PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"saveDisplaySettings\"]"))));
        public IList<IWebElement> countDisplaySetting => PropertiesCollection.driver.FindElements(By.XPath("//*[@id=\"optionDisplaySettings_taglist\"]/li/span[2]"));
        public IList<IWebElement> DisplaySetting => PropertiesCollection.driver.FindElements(By.XPath("//*[@id=\"optionDisplaySettings_taglist\"]/li"));

        #region WebElements for Workflow Grid
        public IWebElement txtboxWorkFlowTitle => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"workflowDrawer\"]/form/div[1])"));
        public IWebElement txtboxWorkFlowTitle2 => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"workflowFormTitle\"]"));
        public IWebElement txtboxWorkFlowSteps => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"workflowDrawer\"]/form/div[2])"));
        public IWebElement txtboxWorkFlowType => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"workflowDrawer\"]/form/div[3]"));
        public IWebElement txtboxWorkFlowStatus => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"workflowDrawer\"]/form/div[4]"));
        public IWebElement txtboxWorkFlowLinkedTo => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"workflowDrawer\"]/form/div[5]"));
        public IWebElement txtboxWorkFlowDueDate => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"workflowDrawer\"]/form/div[6]"));
        public IWebElement txtboxWorkFlowDueTime => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"workflowDrawer\"]/form/div[7]"));
        public IWebElement txtboxWorkFlowNotes => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"workflowDrawer\"]/form/div[10]"));
        public IList<IWebElement> grdWorkflow => PropertiesCollection.driver.FindElements(By.XPath("//*[@id=\"grdWorkflowsSummary\"]/div[2]/table/tbody/tr"));
        public IList<IWebElement> grdWorkflowTypes => PropertiesCollection.driver.FindElements(By.XPath("//*[@id=\"grdWorkflowsSummary\"]/div[2]/table/tbody/tr/td[1]"));
        public IList<IWebElement> grdLinkedTo => PropertiesCollection.driver.FindElements(By.XPath("//*[@id=\"grdWorkflowsSummary\"]/div[2]/table/tbody/tr/td[2]"));
        public IList<IWebElement> grdTitleDescription => PropertiesCollection.driver.FindElements(By.XPath("//*[@id=\"grdWorkflowsSummary\"]/div[2]/table/tbody/tr/td[3]"));
        public IList<IWebElement> grdNotes => PropertiesCollection.driver.FindElements(By.XPath("//*[@id=\"grdWorkflowsSummary\"]/div[2]/table/tbody/tr/td[4]"));
        public IList<IWebElement> grdDue => PropertiesCollection.driver.FindElements(By.XPath("//*[@id=\"grdWorkflowsSummary\"]/div[2]/table/tbody/tr/td[5]"));
        public IWebElement btnHeaderOK => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"workflowDrawer\"]/form/div[11]/button"));

        public IWebElement firstrowfirstcolumn => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"grdWorkflowsSummary\"]/div[2]/table/tbody/tr[1]/td[1]"));
        #endregion


        #endregion

        public string[] RetrieveKnowledgebaseGriddetails(string Item)
        {
            Console.WriteLine(Item);
            String[] Knowledgebasedetails = new string[7];

            System.Threading.Thread.Sleep(3000);

            for (int i = 0; i < allKnowledgebaseItemvalue.Count; i++)
            {
                if (allKnowledgebaseItemvalue.ElementAt(i).Text.Equals(Item))
                {
                    Knowledgebasedetails[0] = lblItem.ElementAt(i).Text;
                    Knowledgebasedetails[1] = lblNowDated.ElementAt(i).Text;
                    Knowledgebasedetails[2] = lblYourDate.ElementAt(i).Text;
                    Knowledgebasedetails[3] = lblNowVersioned.ElementAt(i).Text;
                    Knowledgebasedetails[4] = lblYourVersion.ElementAt(i).Text;
                    Knowledgebasedetails[5] = lblDaysOld.ElementAt(i).Text;
                    Knowledgebasedetails[6] = lblStatus.ElementAt(i).Text;
                    break;
                }
            }
            return Knowledgebasedetails;
        }

        public int RetrieveKnowledgebaseOldCount()
        {
            int Old = 0;
            System.Threading.Thread.Sleep(3000);
            for (int i = 0; i < allKnowledgebaseStatusvalue.Count; i++)
            {
                if (allKnowledgebaseStatusvalue.ElementAt(i).Text.Equals("Old"))
                {
                    Old++;
                }
            }
            return (Old);
        }

        public int RetrieveKnowledgebaseNeverCount()
        {
            int Never = 0;
            System.Threading.Thread.Sleep(3000);
            for (int i = 0; i < allKnowledgebaseStatusvalue.Count; i++)
            {
                if (allKnowledgebaseStatusvalue.ElementAt(i).Text.Equals("Never"))
                {
                    Never++;
                }
            }
            return Never;
        }

        public int RetriveWorkflowCount()
        {
            int CountWorkflow;
            CountWorkflow = grdWorkflow.Count();
            return CountWorkflow;
        }

        
    }
}
