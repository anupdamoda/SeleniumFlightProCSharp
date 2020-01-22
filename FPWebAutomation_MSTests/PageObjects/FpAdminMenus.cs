using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FPWebAutomation_MSTests.PageObjects
{
    class FpAdminMenus

    /****** WebElements on Admin Menu ********/

    {
        public WebDriverWait wait = new WebDriverWait(PropertiesCollection.driver, new TimeSpan(0, 0, 8));

        public IWebElement LnkAdmin => PropertiesCollection.driver.FindElement(By.PartialLinkText("Admin"));
        public IWebElement LnkStripSubGroups => PropertiesCollection.driver.FindElement(By.PartialLinkText("Strip Sub Groups"));
        public IWebElement LnkCatalogueAdministration => PropertiesCollection.driver.FindElement(By.PartialLinkText("Catalogue Administration"));
        public IWebElement LnkDefinePlanningBoards => PropertiesCollection.driver.FindElement(By.PartialLinkText("Define Planning Boards"));
        public IWebElement LnkActivityTypes => PropertiesCollection.driver.FindElement(By.PartialLinkText("Activity Types"));
        public IWebElement LnkOrganisationGroupSettings => PropertiesCollection.driver.FindElement(By.PartialLinkText("Organisation Group Settings"));
        public IWebElement LnkBudgetAdministration => PropertiesCollection.driver.FindElement(By.PartialLinkText("Budget Administration"));
        public IWebElement LnkAssetTypeSettings => PropertiesCollection.driver.FindElement(By.PartialLinkText("Asset Type Settings"));
        public IWebElement LnkAssetTypeSystems => wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.PartialLinkText("Asset Type Systems")));
        public IWebElement LnkRosterAdministration => PropertiesCollection.driver.FindElement(By.PartialLinkText("Roster Administration"));
        public IWebElement LnkShiftAdministration => PropertiesCollection.driver.FindElement(By.PartialLinkText("Shift Administration"));
        public IWebElement LnkTemplates => PropertiesCollection.driver.FindElement(By.PartialLinkText("Templates"));
        public IWebElement LnkSyllabi => PropertiesCollection.driver.FindElement(By.PartialLinkText("Syllabi"));
        public IWebElement LnkCourses => PropertiesCollection.driver.FindElement(By.PartialLinkText("Courses"));
        public IWebElement LnkLicenseAdministration => PropertiesCollection.driver.FindElement(By.PartialLinkText("License Administration"));
        public IWebElement LnkLogout => PropertiesCollection.driver.FindElement(By.PartialLinkText("Logout"));


        public void AdminClick()
        {
            LnkAdmin.Click();
        }

        public void StripSubGroupsClick()
        {
            LnkStripSubGroups.Click();
        }

        public void CatalogueAdministrationClick()
        {
            LnkCatalogueAdministration.Click();
        }

        public void DefinePlanningBoardsClick()
        {
            LnkDefinePlanningBoards.Click();
        }

        public void ActivityTypesClick()
        {
            LnkActivityTypes.Click();
        }

        public void OrganisationGroupSettingsClick()
        {
            LnkOrganisationGroupSettings.Click();
        }

        public void BudgetAdministrationClick()
        {
            LnkBudgetAdministration.Click();
        }

        public void AssetTypeSettingsClick()
        {
            LnkAssetTypeSettings.Click();
        }

        public void AssetTypeSystemsClick()
        {
            LnkAssetTypeSystems.Click();
        }

        public void RosterAdministrationClick()
        {
            LnkRosterAdministration.Click();
        }

        public void ShiftAdministrationClick()
        {
            LnkShiftAdministration.Click();
        }

        public void TemplatesClick()
        {
            LnkTemplates.Click();
        }

        public void SyllabiClick()
        {
            LnkSyllabi.Click();
        }

        public void CoursesClick()
        {
            LnkCourses.Click();
        }

        public void LicenseAdministrationClick()
        {
            LnkLicenseAdministration.Click();
        }

        public void LogoutClick()
        {
            LnkLogout.Click();
        }
    }
}
