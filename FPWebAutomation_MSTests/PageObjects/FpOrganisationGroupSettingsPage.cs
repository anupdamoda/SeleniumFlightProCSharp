using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace FPWebAutomation_MSTests.PageObjects
{
    class FpOrganisationGroupSettingsPage
    {
        /****** WebElements on Organisation Group Settings Page ********/

        private static IWebElement title => PropertiesCollection.driver.FindElement(By.XPath("//td[@class='tableColumnSubHeader'][contains(.,'Organisation Group Settings')]"));
        private static IList<IWebElement> lstAllOrgGroupName => PropertiesCollection.driver.FindElements(By.XPath("//*[@id='OrganisationSettingsListGrid']/table/tbody/tr/td[2]"));
        private static IList<IWebElement> lstEditOrgGroup => PropertiesCollection.driver.FindElements(By.XPath("//*[@id='OrganisationSettingsListGrid']/table/tbody/tr/td[1]/div/a/span"));
        private static IList<IWebElement> lstBreakDuration => PropertiesCollection.driver.FindElements(By.XPath("//*[@id='OrganisationSettingsListGrid']/table/tbody/tr/td[4]"));
        private static IList<IWebElement> lstMaxConsecutiveTasking=> PropertiesCollection.driver.FindElements(By.XPath("//*[@id='OrganisationSettingsListGrid']/table/tbody/tr/td[5]"));
        private static IList<IWebElement> lstStandDuration => PropertiesCollection.driver.FindElements(By.XPath("//*[@id='OrganisationSettingsListGrid']/table/tbody/tr/td[6]"));
        private static IList<IWebElement> lstCurrencyAudit => PropertiesCollection.driver.FindElements(By.XPath("//*[@id='OrganisationSettingsListGrid']/table/tbody/tr/td[7]"));
        private static IList<IWebElement> lstEventAcknowledgement => PropertiesCollection.driver.FindElements(By.XPath("//*[@id='OrganisationSettingsListGrid']/table/tbody/tr/td[8]"));

        /****** WebElements on Edit Organisation Group Settings Page ********/
        private static IWebElement optDecimalHours => PropertiesCollection.driver.FindElement(By.XPath("//*[@id='OrganisationGroupTable']//*//span[1]/label[@class='k-radio-label']"));
        private static IWebElement optHoursAndMinutes => PropertiesCollection.driver.FindElement(By.XPath("//*[@id='OrganisationGroupTable']//*//span[2]/label[@class='k-radio-label']"));
        private static IWebElement txtBreakDuration => PropertiesCollection.driver.FindElement(By.Id("BreakDurationTimeString"));
        private static IWebElement txtMaxConsecutiveTasking => PropertiesCollection.driver.FindElement(By.Id("MaxConsecutiveTaskingTimeString"));
        private static IWebElement txtStandDown => PropertiesCollection.driver.FindElement(By.Id("StandDownTimeString"));
        private static IWebElement chkCurrencyAudit => PropertiesCollection.driver.FindElement(By.Id("CurrencyAudit"));
        private static IWebElement chkEventAcknowledgement => PropertiesCollection.driver.FindElement(By.Id("EventAcknowledgement"));
        private static IWebElement txtAcknowledgementTime => PropertiesCollection.driver.FindElement(By.Id("AcknowledgementTime"));
        private static IWebElement txtAcknowledgementDays => PropertiesCollection.driver.FindElement(By.Id("AcknowledgementFor"));
        private static IWebElement btnSave => PropertiesCollection.driver.FindElement(By.Id("btnSave"));
        private static IWebElement btnReturn => PropertiesCollection.driver.FindElement(By.Id("btnReturn"));

        /****** WebElements on Edit Organisation Group Settings Page - Add People Groups********/
        private static IWebElement btnAddPeopleGroups => PropertiesCollection.driver.FindElement(By.Id("btnOrgGroups"));
        private static IList<IWebElement> lstPeopleGroupName => PropertiesCollection.driver.FindElements(By.XPath("//*[@id='grdOrgGroups']/div[2]/table/tbody/tr/td[2]"));
        private static IList<IWebElement> lstSelectPeopleGroup => PropertiesCollection.driver.FindElements(By.XPath("//*[@id='grdOrgGroups']/div[2]/table/tbody/tr/td[1]/input"));
        private static IWebElement btnApplyPeopleGroups => PropertiesCollection.driver.FindElement(By.Id("btnApplyOrgGroupsWindow"));

        /****** WebElements on Conformation popup for Currency Audits ********/
        private static IWebElement btnOk => PropertiesCollection.driver.FindElement(By.Id("_0_btn_modal"));
        private static IWebElement btnCancel => PropertiesCollection.driver.FindElement(By.Id("_1_btn_modal"));

        /****** WebElements on Conformation popup for Return Button ********/
        private static IWebElement btnReturnOk => PropertiesCollection.driver.FindElement(By.Id("_0_btn_modal"));
        private static IWebElement btnReturnCancel => PropertiesCollection.driver.FindElement(By.Id("_1_btn_modal"));
        private static IWebElement txtConfirmationMsg => PropertiesCollection.driver.FindElement(By.XPath("//*[@class='k-window-content k-content']/div"));

        /****** WebElements  for error messages on Edit Org Group Settins Page ********/
        private static IWebElement txtErrorAckTime => PropertiesCollection.driver.FindElement(By.Id("AcknowledgementTime_validationMessage"));
        private static IWebElement txtErrorAckFor => PropertiesCollection.driver.FindElement(By.Id("AcknowledgementFor_validationMessage"));

        public static void NavigateToOrgGroupSettingsPage()
        {
            Thread.Sleep(3000);
            FpAdminMenus adminMenus = new FpAdminMenus();
            adminMenus.AdminClick();
            Thread.Sleep(3000);
            adminMenus.OrganisationGroupSettingsClick();            
        }
        public bool IsTitleDisplayed()
        {
            return(title.Displayed);            
        }
        public static void ClickOptDecimalHours()
        {
            optDecimalHours.Click();
        }
        public static void ClickOptHoursAndMinutes()
        {
            optHoursAndMinutes.Click();
        }
        public static void ClearBreakDuration()
        {
            txtBreakDuration.Clear();
        }           
        public static void EnterBreakDuration(string breakDuration)
        {            
            txtBreakDuration.SendKeys(breakDuration);
        }
        public static void ClearMaxConsecutiveTasking()
        {
            txtMaxConsecutiveTasking.Clear();
        }
        public static void EnterMaxConsecutiveTasking(string maxConsecutiveTasking)
        {
            txtMaxConsecutiveTasking.SendKeys(maxConsecutiveTasking);
        }
        public static void ClearStandDown()
        {
            txtStandDown.Clear();
        }
        public static void EnterStandDown(string standDown)
        {
            txtStandDown.SendKeys(standDown);
        }
        public static void ClickCurrencyAudit()
        {
            chkCurrencyAudit.Click();
                       
        }
        public static void ClickEventAcknowledgement()
        {
             chkEventAcknowledgement.Click();                     
        }
        public static void ClearAcknowledgementTime()
        {
            txtAcknowledgementTime.Clear();
        }
        public static void ClearAcknowledgementDays()
        {
            txtAcknowledgementDays.Clear();
        }
        public static void EnterAcknowledgementTime(string acknowledgementTime)
        {            
            txtAcknowledgementTime.SendKeys(acknowledgementTime);
        }
        public static void EnterAcknowledgementDays(string acknowledgementDays)
        {
            txtAcknowledgementDays.SendKeys(acknowledgementDays);
        }
        public static void ClickSave()
        {
            btnSave.Click();
        }
        public static void ClickReturn()
        {
            btnReturn.Click();
        }
        public static void ClickAddPeopleGroupsButton()
        {
            btnAddPeopleGroups.Click();
        }
        public static void ClickApplyAddPeopleGroups()
        {
            btnApplyPeopleGroups.Click();
        }
        public static void ClickEditOrgGroup(string orgGroup)
        {
            for (int i = 0; i < lstAllOrgGroupName.Count; i++)
            {
                if (lstAllOrgGroupName.ElementAt(i).Text.Equals(orgGroup))
                {
                    lstEditOrgGroup.ElementAt(i).Click();
                    break;
                }
            }
        }
        public static void SelectPeopleGroup(string peopleGroup)
        {
            for (int i = 0; i < lstPeopleGroupName.Count(); i++)
            {
                if (lstPeopleGroupName.ElementAt(i).Text.Equals(peopleGroup))
                {
                    if (lstSelectPeopleGroup.ElementAt(i).Selected)
                    {                       
                        break;
                    }
                    lstSelectPeopleGroup.ElementAt(i).Click();
                }
            }
        }
        public static void ClickConfirmationOK()
        {
            btnOk.Click();
        }
        public static void ClickConfirmationCancel()
        {
            btnCancel.Click();
        }
        public static string[] FetchValuesAfterEdit(string orgGroup)
        {
            string[] OrgGroupData = new string[5];
            for (int i = 0; i < lstAllOrgGroupName.Count; i++)
            {
                if (lstAllOrgGroupName.ElementAt(i).Text.Equals(orgGroup))
                {
                    OrgGroupData[0] = lstBreakDuration.ElementAt(i).Text;
                    OrgGroupData[1] = lstMaxConsecutiveTasking.ElementAt(i).Text;
                    OrgGroupData[2] = lstStandDuration.ElementAt(i).Text;
                    OrgGroupData[3] = lstCurrencyAudit.ElementAt(i).Text;
                    OrgGroupData[4] = lstEventAcknowledgement.ElementAt(i).Text;
                    break;
                }
            }
            return OrgGroupData;
        }
        public static string GetConfirmationText()
        {
            string text = txtConfirmationMsg.Text;
            return text;
        }        
        public static string GetErrorMessageForAckTime()
        {
            string errorMsg = txtErrorAckTime.Text;
            return errorMsg;
        }
        public static string GetErrorMessageForAckFor()
        {
            string errorMsg = txtErrorAckFor.Text;
            return errorMsg;
        }
        public static void ClickBreakDuration()
        {
            txtBreakDuration.Click();
        }

        public static void EditOrgGroupSettings(string orgGroup, string durationFormat, string breakDuration, string maxConsecutiveTasking, string standDown, string acknowledgementTime, string acknowledgementFor, string peopleGroup)
        {
            NavigateToOrgGroupSettingsPage();
            Thread.Sleep(5000);
            ClickEditOrgGroup(orgGroup);
            Thread.Sleep(5000);
            ClickOptDecimalHours();
            ClearBreakDuration();
            EnterBreakDuration(breakDuration);
            ClearMaxConsecutiveTasking();
            EnterMaxConsecutiveTasking(maxConsecutiveTasking);
            ClearStandDown();
            EnterStandDown(standDown);

            if (chkEventAcknowledgement.Selected)
            {

            }
            else
            {
                ClickEventAcknowledgement();
            }
            
            Thread.Sleep(1000);
            ClearAcknowledgementTime();
            EnterAcknowledgementTime(acknowledgementTime);
            ClearAcknowledgementDays();
            EnterAcknowledgementDays(acknowledgementFor);

            ClickAddPeopleGroupsButton();
            Thread.Sleep(3000);
            SelectPeopleGroup(peopleGroup);
            Thread.Sleep(1000);
            ClickApplyAddPeopleGroups();
            Thread.Sleep(4000);


            if (chkCurrencyAudit.Selected)
            {
                ClickSave();
            }
            else
            {
                ClickCurrencyAudit();
                ClickSave();
                Thread.Sleep(2000);
                ClickConfirmationOK();
                Thread.Sleep(5000);
            }            
        }
    }
}
