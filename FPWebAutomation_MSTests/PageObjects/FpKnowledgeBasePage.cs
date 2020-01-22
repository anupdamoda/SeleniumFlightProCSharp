using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPWebAutomation_MSTests.PageObjects
{
    class FpKnowledgeBasePage
    {
        string UserXPath = null;
        string KBItemXPath = null;
        WebDriverWait wait = new WebDriverWait(PropertiesCollection.driver, TimeSpan.FromSeconds(10));

        #region WebElements on Knowledge Base Page
        public IWebElement RowValue => PropertiesCollection.driver.FindElement(By.XPath("//ul[@id='pbKBTrees']//a[contains(.,'Knowledge Base By Entry')]"));
        public IWebElement btnAddKnowledgebaseByEntry => wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='btnAddRoot']")));
        public IWebElement btnManagePersonnel => PropertiesCollection.driver.FindElement(By.Id("btnManage"));
        public IWebElement txtTitleAddKnowledgebaseByEntry => PropertiesCollection.driver.FindElement(By.XPath("//textarea[@id='EntryTitle']"));
        public IWebElement dateAddKnowledgebaseByEntry => PropertiesCollection.driver.FindElement(By.XPath("//*[@id='EntryDated']"));
        public IWebElement chkRequiresAcknowledgement => PropertiesCollection.driver.FindElement(By.XPath("//*[@id='IsControlled']"));
        public IWebElement chkRestrictViewing => PropertiesCollection.driver.FindElement(By.XPath("//*[@id='IsRestricted']"));
        public IWebElement iFrame => PropertiesCollection.driver.FindElement(By.XPath("//iframe[@title='Editable area. Press F10 for toolbar.']"));
        public IWebElement txtContentAddKnowledgebaseByEntry => PropertiesCollection.driver.FindElement(By.XPath("/html/body[@autocorrect='off']"));
        public IWebElement txtVersion => PropertiesCollection.driver.FindElement(By.XPath("//*[@id='CurrentVersion']"));
        public IWebElement btnCancel => PropertiesCollection.driver.FindElement(By.XPath("//button[contains(.,'Cancel')]"));
        public IWebElement btnSave => wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(.,'Save')]")));
        public int countbtnSave => PropertiesCollection.driver.FindElements(By.XPath("//button[contains(.,'Save')]")).Count;
        public IWebElement btnEdit => PropertiesCollection.driver.FindElement(By.XPath("//button[contains(.,'Edit')]"));
        public IWebElement btnDelete => wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(.,'Delete')]")));
        public IWebElement btnMarkAsRead => PropertiesCollection.driver.FindElement(By.XPath("//button[contains(.,'Mark as Read')]"));
        public IWebElement txtKBItemTitle => PropertiesCollection.driver.FindElement(By.XPath("//*[@id='container']/div[3]/table/tbody/tr[1]/td/div/h2"));
        public IWebElement txtKBItem => PropertiesCollection.driver.FindElement(By.XPath(KBItemXPath));
        public string Title() => RowValue.Text;
        public IWebElement btnBrowse => PropertiesCollection.driver.FindElement(By.Id("btnBrowse"));
        public IWebElement btnClear => PropertiesCollection.driver.FindElement(By.Id("btnClear"));
        public IWebElement lblFileAttachmentFileType => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"fileAttachmentInvalid\"]"));
        public IWebElement lblFileAttachmentAllowableFileTypes => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"fileAttachmentInvalid\"]/u/a"));
        public IWebElement lblFileAttachmentSysAdmin => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"fileAttachmentInvalid\"]/text()[2]"));
        public IWebElement lnkAllowableFileTypes => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"fileAttachmentInvalid\"]/u/a"));
        public IWebElement grdFileNameRow => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"container\"]/div[3]/table/tbody/tr[3]"));
        public IWebElement txtboxKnowledgeBaseTitle => wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"EntryTitle\"]")));
        public IWebElement lbliFrameKnowledgeBase => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"spanDetails\"]/div"));
        public IWebElement txtAreaFrameKnowledgeBase => wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body")));
        public IWebElement btnOK => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"_0_btn_modal\"]"));
        #endregion

        #region WebElements on Manage Personnel Popup
        public IWebElement BtnExpandAll => PropertiesCollection.driver.FindElement(By.XPath("//div[@id='divKBSecurity']//div[@id='divPersonnel']//div[contains(.,'Expand All')]"));
        public IWebElement BtnApply => PropertiesCollection.driver.FindElement(By.Id("btnApplyAssignment"));
        public IWebElement BtnManagePersonnelCancel => PropertiesCollection.driver.FindElement(By.Id("btnCancelAssignment"));
        public IWebElement User => PropertiesCollection.driver.FindElement(By.XPath(UserXPath));
        #endregion

        #region WebElements on Please confirm Popup
        public IWebElement BtnOK => PropertiesCollection.driver.FindElement(By.XPath("//button[contains(.,'OK')]"));
        public IWebElement BtnCancelDelete => PropertiesCollection.driver.FindElement(By.XPath("//button[contains(.,'Cancel')]"));
        #endregion

        public void Add_Knowledgebase_Item(string Title, string Content, string Version, string Date, string RestrictViewing, string PeopleID, string PeopleIDOther)
        {
            FpSideMenus SideMenu = new FpSideMenus();
            SideMenu.KnowledgeBaseClick();
            System.Threading.Thread.Sleep(30000);
            RowValue.Click();
            System.Threading.Thread.Sleep(5000);
            btnAddKnowledgebaseByEntry.Click();
            System.Threading.Thread.Sleep(5000);
            txtTitleAddKnowledgebaseByEntry.Clear();
            txtTitleAddKnowledgebaseByEntry.SendKeys(Title);
            System.Threading.Thread.Sleep(5000);
            PropertiesCollection.driver.SwitchTo().Frame(iFrame);
            System.Threading.Thread.Sleep(5000);
            txtContentAddKnowledgebaseByEntry.SendKeys(Content);
            System.Threading.Thread.Sleep(5000);
            PropertiesCollection.driver.SwitchTo().DefaultContent();
            System.Threading.Thread.Sleep(5000);
            txtVersion.SendKeys(Version);
            System.Threading.Thread.Sleep(5000);
            dateAddKnowledgebaseByEntry.Clear();
            dateAddKnowledgebaseByEntry.SendKeys(Date);
            System.Threading.Thread.Sleep(5000);
            chkRequiresAcknowledgement.Click();
            System.Threading.Thread.Sleep(5000);
            btnManagePersonnel.Click();
            System.Threading.Thread.Sleep(5000);
            BtnExpandAll.Click();

            System.Threading.Thread.Sleep(5000);
            string PersonID = "P-" + PeopleID;
            UserXPath = "//span[@class='k-checkbox-wrapper']//input[@value='" + PersonID + "']";
            Console.WriteLine(UserXPath);
            User.Click();

            System.Threading.Thread.Sleep(15000);
            PersonID = null;
            PersonID = "P-" + PeopleIDOther;
            UserXPath = "//span[@class='k-checkbox-wrapper']//input[@value='" + PersonID + "']";
            Console.WriteLine(UserXPath);
            User.Click();

            System.Threading.Thread.Sleep(5000);
            BtnApply.Click();
            System.Threading.Thread.Sleep(15000);
            if (RestrictViewing == "Yes")
            {
                chkRestrictViewing.Click();
            }
            System.Threading.Thread.Sleep(15000);
            btnSave.Click();
            System.Threading.Thread.Sleep(15000);
        }

        public void Add_Knowledgebase_Item_AssigntoOrgGroup(string Title, string Content, string Version, string Date, string RestrictViewing, string OrgGroupID, string OrgGroupIDOther)
        {
            FpSideMenus SideMenu = new FpSideMenus();
            SideMenu.KnowledgeBaseClick();
            System.Threading.Thread.Sleep(30000);
            RowValue.Click();
            System.Threading.Thread.Sleep(5000);
            btnAddKnowledgebaseByEntry.Click();
            System.Threading.Thread.Sleep(5000);
            txtTitleAddKnowledgebaseByEntry.Clear();
            txtTitleAddKnowledgebaseByEntry.SendKeys(Title);
            System.Threading.Thread.Sleep(5000);
            PropertiesCollection.driver.SwitchTo().Frame(iFrame);
            System.Threading.Thread.Sleep(5000);
            txtContentAddKnowledgebaseByEntry.SendKeys(Content);
            System.Threading.Thread.Sleep(5000);
            PropertiesCollection.driver.SwitchTo().DefaultContent();
            System.Threading.Thread.Sleep(5000);
            txtVersion.SendKeys(Version);
            System.Threading.Thread.Sleep(5000);
            dateAddKnowledgebaseByEntry.Clear();
            dateAddKnowledgebaseByEntry.SendKeys(Date);
            System.Threading.Thread.Sleep(5000);
            chkRequiresAcknowledgement.Click();
            System.Threading.Thread.Sleep(5000);
            btnManagePersonnel.Click();
            System.Threading.Thread.Sleep(5000);
            BtnExpandAll.Click();

            string OrgID = "PG-" + OrgGroupID;
            UserXPath = "//span[@class='k-checkbox-wrapper']//input[@value='" + OrgID + "']";
            Console.WriteLine(UserXPath);
            System.Threading.Thread.Sleep(5000);
            User.Click();
            System.Threading.Thread.Sleep(15000);
            OrgID = null;
            OrgID = "PG-" + OrgGroupIDOther;
            UserXPath = "//span[@class='k-checkbox-wrapper']//input[@value='" + OrgID + "']";
            Console.WriteLine(UserXPath);
            System.Threading.Thread.Sleep(5000);
            User.Click();

            System.Threading.Thread.Sleep(5000);
            BtnApply.Click();
            System.Threading.Thread.Sleep(15000);
            if (RestrictViewing == "Yes")
            {
                chkRestrictViewing.Click();
            }
            System.Threading.Thread.Sleep(15000);
            btnSave.Click();
            System.Threading.Thread.Sleep(15000);
        }

        public void Delete_Knowledgebase_Item(string Title, string RestrictViewing, string PeopleID, string PeopleIDOther)
        {
            FpSideMenus SideMenu = new FpSideMenus();
            SideMenu.KnowledgeBaseClick();
            System.Threading.Thread.Sleep(30000);

            KBItemXPath = "//*/div/span[contains(.,'" + Title + "')]";
            Console.WriteLine(KBItemXPath);

            txtKBItem.Click();
            System.Threading.Thread.Sleep(5000);
            btnEdit.Click();
            System.Threading.Thread.Sleep(5000);
            btnManagePersonnel.Click();
            System.Threading.Thread.Sleep(5000);
            BtnExpandAll.Click();
            System.Threading.Thread.Sleep(5000);
            string PersonID = "P-" + PeopleID;
            UserXPath = "//span[@class='k-checkbox-wrapper']//input[@value='" + PersonID + "']";
            Console.WriteLine(UserXPath);
            User.Click();
            PersonID = null;
            System.Threading.Thread.Sleep(15000);
            PersonID = "P-" + PeopleIDOther;
            UserXPath = "//span[@class='k-checkbox-wrapper']//input[@value='" + PersonID + "']";
            Console.WriteLine(UserXPath);
            User.Click();

            System.Threading.Thread.Sleep(5000);
            BtnApply.Click();
            System.Threading.Thread.Sleep(5000);
            chkRequiresAcknowledgement.Click();
            System.Threading.Thread.Sleep(5000);
            if (RestrictViewing == "Yes")
            {
                chkRestrictViewing.Click();
            }
            System.Threading.Thread.Sleep(5000);
            btnSave.Click();
            System.Threading.Thread.Sleep(5000);
            btnDelete.Click();
            System.Threading.Thread.Sleep(5000);
            BtnOK.Click();
        }

        public void Delete_Knowledgebase_Item_AssignedtoOrgGroup(string Title, string RestrictViewing, string OrgGroupID)
        {
            FpSideMenus SideMenu = new FpSideMenus();
            SideMenu.KnowledgeBaseClick();
            System.Threading.Thread.Sleep(30000);

            KBItemXPath = "//*/div/span[contains(.,'" + Title + "')]";
            Console.WriteLine(KBItemXPath);

            txtKBItem.Click();
            System.Threading.Thread.Sleep(5000);
            btnEdit.Click();
            System.Threading.Thread.Sleep(5000);
            btnManagePersonnel.Click();
            System.Threading.Thread.Sleep(5000);
            BtnExpandAll.Click();
            System.Threading.Thread.Sleep(5000);
            string OrgID = "PG-" + OrgGroupID;
            UserXPath = "//span[@class='k-checkbox-wrapper']//input[@value='" + OrgID + "']";
            Console.WriteLine(UserXPath);

            User.Click();
            System.Threading.Thread.Sleep(5000);
            BtnApply.Click();
            System.Threading.Thread.Sleep(5000);
            chkRequiresAcknowledgement.Click();
            System.Threading.Thread.Sleep(5000);
            if (RestrictViewing == "Yes")
            {
                chkRestrictViewing.Click();
            }
            System.Threading.Thread.Sleep(5000);
            btnSave.Click();
            System.Threading.Thread.Sleep(5000);
            btnDelete.Click();
            System.Threading.Thread.Sleep(5000);
            BtnOK.Click();
        }
        public void Update_Knowledgebase_Item(string Title, string UpdatedVersion, string UpdatedContent)
        {
            Console.WriteLine(Title);
            FpSideMenus SideMenu = new FpSideMenus();
            System.Threading.Thread.Sleep(5000);
            SideMenu.KnowledgeBaseClick();
            System.Threading.Thread.Sleep(30000);

            KBItemXPath = "//*/div/span[contains(.,'" + Title + "')]";

            Console.WriteLine(KBItemXPath);
            txtKBItem.Click();
            System.Threading.Thread.Sleep(5000);
            btnEdit.Click();
            System.Threading.Thread.Sleep(5000);
            txtVersion.Clear();
            System.Threading.Thread.Sleep(5000);
            txtVersion.SendKeys(UpdatedVersion);
            System.Threading.Thread.Sleep(5000);
            PropertiesCollection.driver.SwitchTo().Frame(iFrame);
            System.Threading.Thread.Sleep(5000);
            txtContentAddKnowledgebaseByEntry.Clear();
            System.Threading.Thread.Sleep(5000);
            txtContentAddKnowledgebaseByEntry.SendKeys(UpdatedContent);
            System.Threading.Thread.Sleep(5000);
            PropertiesCollection.driver.SwitchTo().DefaultContent();
            System.Threading.Thread.Sleep(5000);
            btnSave.Click();
            System.Threading.Thread.Sleep(15000);
        }

        public void MarkAsRead_Knowledgebase_Item(string Title)
        {
            FpSideMenus SideMenu = new FpSideMenus();
            System.Threading.Thread.Sleep(5000);
            SideMenu.KnowledgeBaseClick();
            System.Threading.Thread.Sleep(30000);

            KBItemXPath = "//*/div/span[contains(.,'" + Title + "')]";

            txtKBItem.Click();
            System.Threading.Thread.Sleep(5000);
            btnMarkAsRead.Click();
            System.Threading.Thread.Sleep(5000);
        }
    }
}
