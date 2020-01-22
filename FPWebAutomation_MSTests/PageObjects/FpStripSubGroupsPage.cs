using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace FPWebAutomation_MSTests.PageObjects
{
    class FpStripSubGroupsPage
    {
        /****** WebElements on Strip Sub Groups Page ********/

        public IWebElement Title => PropertiesCollection.driver.FindElement(By.XPath("//td[@class='tableColumnSubHeader'][contains(.,'Strip Sub Group Administration')]"));
        private static IWebElement btnAddStripSubGroup => PropertiesCollection.driver.FindElement(By.XPath("//*[@id='StripSubGroupListGrid']/div/button"));

        /****** List of WebElements on Strip Sub Groups Page ********/
        private static IList<IWebElement> lstShortCode => PropertiesCollection.driver.FindElements(By.XPath("//*[@id='StripSubGroupListGrid']/table/tbody/tr/td[2]/div"));
        private static IList<IWebElement> lstSubGroupName => PropertiesCollection.driver.FindElements(By.XPath("//*[@id='StripSubGroupListGrid']/table/tbody/tr/td[3]"));
        private static IList<IWebElement> lstEditSubGroup => PropertiesCollection.driver.FindElements(By.XPath("//*[@id='StripSubGroupListGrid']/table/tbody/tr/td[1]/div/a/span"));

        /****** List of WebElements on Add / Edit Strip Sub Groups Page ********/
        private static IWebElement txtShortCode => PropertiesCollection.driver.FindElement(By.Id("StripSubGroupShortName"));
        private static IWebElement txtSubGroupName => PropertiesCollection.driver.FindElement(By.Id("StripSubGroupName"));
        private static IWebElement btnColor => PropertiesCollection.driver.FindElement(By.XPath("//div[@class='editor-field']/span[1]/span/span[2]/span"));
        private static IWebElement grdColorContainer => PropertiesCollection.driver.FindElement(By.XPath("//div[@class='k-animation-container']"));
        private static IWebElement btnSave => PropertiesCollection.driver.FindElement(By.Id("btnSave"));
        private static IWebElement btnReturn => PropertiesCollection.driver.FindElement(By.Id("btnReturn"));
        private static IWebElement txtToolTip_ShortCode => PropertiesCollection.driver.FindElement(By.Id("StripSubGroupShortName_validationMessage"));
        private static IWebElement txtToolTip_SubGroupName => PropertiesCollection.driver.FindElement(By.Id("StripSubGroupName_validationMessage"));
        private static IWebElement chkActive => PropertiesCollection.driver.FindElement(By.Id("Active"));

        /****** List of WebElements on Confirmation window in Add / Edit Strip Sub Groups Page ********/
        private static IWebElement btnConfirmationOk => PropertiesCollection.driver.FindElement(By.Id("_0_btn_modal"));
        private static IWebElement btnConfirmationCancel => PropertiesCollection.driver.FindElement(By.Id("_1_btn_modal"));
        private static IWebElement txtConfirmationText => PropertiesCollection.driver.FindElement(By.XPath("//div[@class='k-widget k-window']/div[2]/div[1]"));

        public static void ClickAddStripSubGroup()
        {
            btnAddStripSubGroup.Click();
        }

        public static void EnterShortCode(string shortCode)
        {
            txtShortCode.SendKeys(shortCode);
        }
        public static void ClearShortCode()
        {
            txtShortCode.Clear();
        }
        public static void ClearSubGroupName()
        {
            txtSubGroupName.Clear();
        }
        public static void ClickSubGroupName()
        {
            txtSubGroupName.Click();
        }
        public static void ClickShortCode()
        {
            txtShortCode.Click();
        }
        public static void EnterSubGroupName(string subGroupName)
        {
            txtSubGroupName.SendKeys(subGroupName);
        }
        public static void ClickSave()
        {
            btnSave.Click();
        }
        public static void ClickReturn()
        {
            btnReturn.Click();
        }
        public static void ClickColorContainer()
        {
            btnColor.Click();
        }
        public static void ClickConfirmationOk()
        {
            btnConfirmationOk.Click();
        }
        public static void ClickConfirmationCancel()
        {
            btnConfirmationCancel.Click();
        }
        public static string GetToolTip_ShortCode()
        {
            return txtToolTip_ShortCode.Text;
        }
        public static string GetToolTip_SubGroupName()
        {
            return txtToolTip_SubGroupName.Text;
        }
        public static string GetConfirmationText()
        {
            return txtConfirmationText.Text;
        }
        public static void DeSelectActive()
        {
            if (chkActive.Selected == true)
                chkActive.Click();
        }
        public static void NavigateToStripSubGroupPage()
        {
            FpAdminMenus adminMenus = new FpAdminMenus();
            adminMenus.AdminClick();
            Thread.Sleep(1000);
            adminMenus.StripSubGroupsClick();
            Thread.Sleep(4000);
        }
        public static void ClickEditStripSubGroup(string shortCode)
        {
            for(int i =0; i <lstShortCode.Count(); i++)
            {
                if(lstShortCode.ElementAt(i).Text.Equals(shortCode))
                {
                    lstEditSubGroup.ElementAt(i).Click();
                }
            }
        }

        public static void AddStripSubGroup(string shortCode, string subGroupName)
        {
            NavigateToStripSubGroupPage();

            ClickAddStripSubGroup();
            Thread.Sleep(2000);

            EnterShortCode(shortCode);
            EnterSubGroupName(subGroupName);

            ClickColorContainer();
            int height = grdColorContainer.Size.Height;
            int width = grdColorContainer.Size.Width;
            Actions actions = new Actions(PropertiesCollection.driver);
            actions.MoveToElement(grdColorContainer).MoveByOffset((-width / 3), 2).Click().Perform();
            Thread.Sleep(2000);

            ClickSave();
        }

        public static void EditStripSubGroup(string shortCode, string newShortCode, string newSubGroupName)
        {
            NavigateToStripSubGroupPage();

            ClickEditStripSubGroup(shortCode);
            Thread.Sleep(2000);

            ClearShortCode();
            EnterShortCode(newShortCode);
            ClearSubGroupName();
            EnterSubGroupName(newSubGroupName);

            ClickSave();
        }

        public static void DeactivateStripSubGroup(string shortCode)
        {
            NavigateToStripSubGroupPage();

            ClickEditStripSubGroup(shortCode);
            Thread.Sleep(2000);

            DeSelectActive();

            ClickSave();

            Thread.Sleep(1000);
        }

        public static string[] FetchSubGroupData(string shortCode)
        {
            string[] subGroupData = new string[2];
            for (int i = 0; i < lstShortCode.Count; i++)
            {
                if (lstShortCode.ElementAt(i).Text.Equals(shortCode))
                {
                    subGroupData[0] = lstShortCode.ElementAt(i).Text;
                    subGroupData[1] = lstSubGroupName.ElementAt(i).Text;
                    break;
                }
            }
            return subGroupData;
        }
    }
}
