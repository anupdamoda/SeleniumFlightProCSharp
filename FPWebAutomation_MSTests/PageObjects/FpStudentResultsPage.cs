using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FPWebAutomation_MSTests.PageObjects
{
    class FpStudentResultsPage
    {
        public WebDriverWait wait = new WebDriverWait(PropertiesCollection.driver, TimeSpan.FromSeconds(60));


        
        #region WebElements on Student Results Page

        public IWebElement UserName => PropertiesCollection.driver.FindElement(By.XPath("//dx-button[@id='people-selector-button']"));
        public IWebElement btnPeopleSelector => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"people-selector-button\"]/div"));
        public IWebElement txtPeopleSelector => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"text-search\"]/div/input"));
        public IWebElement btnPeopleSearch => PropertiesCollection.driver.FindElement(By.XPath("/html/body/div[2]/div/div[2]/div/div[1]/app-text-search/div/dx-button/div"));
        public IWebElement firstgrdPeopleSelector => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"peopleGrid\"]/div/div[6]/div/div/div[1]/div/table/tbody/tr[1]/td[2]"));
        public IWebElement btnApplyCategoriesWindow => PropertiesCollection.driver.FindElement(By.XPath("//*[contains(text(),'Apply')]"));
        public IWebElement txtboxCourseName => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"results\"]/div/div[4]/div/div/div[3]/div/div/div/div/input"));
        public IList<IWebElement> grdresults => PropertiesCollection.driver.FindElements(By.XPath("//*[@id=\"results\"]/div/div[6]/div/table/tbody/tr/td[1]"));
        public IWebElement btnAdditionalCategories => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"category-selector-button\"]/div"));
        public IList<IWebElement> allselects => PropertiesCollection.driver.FindElements(By.XPath("//*[@id=\"categoryGrid\"]/div/div[6]/div/div/div[1]/div/table/tbody/tr/td[1]/div/div/div/span"));
        public IList<IWebElement> allCategories => PropertiesCollection.driver.FindElements(By.XPath("//*[@id=\"categoryGrid\"]/div/div[6]/div/div/div[1]/div/table/tbody/tr/td[4]"));
        public IWebElement btnApply => PropertiesCollection.driver.FindElement(By.XPath("/html/body/div[2]/div/div[2]/div/div[2]/dx-button[2]/div"));
        public IWebElement txtboxOverallComments => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"student-results-shell\"]/div/div[2]/div/app-event-info-summary/div/div[2]/dx-tab-panel/div[2]/div/div/div[1]/dxi-item/div/app-event-student-details/div/div[4]/div[1]/app-student-details-overall-comments/div/div/dx-drawer/div/div[2]/div/div/div/textarea"));
        public IWebElement txtboxPrivateComments => wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"student-results-shell\"]/div/div[2]/div/app-event-info-summary/div/div[2]/dx-tab-panel/div[2]/div/div/div[1]/dxi-item/div/app-event-student-details/div/div[4]/div[2]/app-student-details-private-comments/div/div/dx-drawer/div/div[2]/div/div/div/textarea")));
        public IWebElement btnSave => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"save-button\"]"));
        public IWebElement dropdownScore => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"student-results-shell\"]/div/div[2]/div/app-event-info-summary/div/div[2]/dx-tab-panel/div[2]/div/div/div[1]/dxi-item/div/app-event-student-details/div/div[2]/div/app-student-details-result-awarded/div/div/div/div[2]/div[2]/div/div[1]/span[1]/dx-select-box/div/div/input"));
        public IWebElement txtboxscore => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"student-results-shell\"]/div/div[2]/div/app-event-info-summary/div/div[2]/dx-tab-panel/div[2]/div/div/div[1]/dxi-item/div/app-event-student-details/div/div[2]/div/app-student-details-result-awarded/div/div/div/div[2]/div[2]/div/div[1]/span[1]/dx-select-box/div[1]/div/input"));
        public IWebElement txtResultAwarded => PropertiesCollection.driver.FindElement(By.XPath("//dxi-item/div/app-event-student-details/div/div[2]/div/app-student-details-result-awarded/div/div/div/div[1]/div[2]/dx-select-box/div/div/div"));
        public IWebElement colourResultAwarded => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"student-results-shell\"]/div/div[2]/div/app-event-info-summary/div/div[2]/dx-tab-panel/div[2]/div/div/div[1]/dxi-item/div/app-event-student-details/div/div[2]/div/app-student-details-result-awarded/div/div/div/div[1]/div[2]/dx-select-box/div/div/div"));
        public IList<IWebElement> allIcons => PropertiesCollection.driver.FindElements(By.XPath("//*[@id=\"results\"]/div/div[6]/div/table/tbody/tr/td[2]/div/app-sr-icons/div/div"));
        public IWebElement txtServiceName => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"student-results-shell\"]/div/div[2]/div/app-event-info-summary/div/div[1]/app-event-info-summary-header/div/div/div[1]/div[2]/span[2]"));
        public IWebElement txtGroupName => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"student-results-shell\"]/div/div[2]/div/app-event-info-summary/div/div[1]/app-event-info-summary-header/div/div/div[1]/div[3]/span[2]"));
        public IWebElement txtEventName => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"student-results-shell\"]/div/div[2]/div/app-event-info-summary/div/div[1]/app-event-info-summary-header/div/div/div[2]/div[3]/span[2]"));
        public IWebElement txtCourseName => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"student-results-shell\"]/div/div[2]/div/app-event-info-summary/div/div[1]/app-event-info-summary-header/div/div/div[2]/div[1]/span[2]"));
        public IWebElement txtInstructorName => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"people-selector-button\"]/div/span"));
        public IList<IWebElement> gridResults => PropertiesCollection.driver.FindElements(By.XPath("//*[@id=\"results\"]/div/div[6]/div/table/tbody/tr[4]/td[1]/div[2]/div"));
        public IWebElement txtStudentName => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"student-results-shell\"]/div/div[2]/div/app-event-info-summary/div/div[1]/app-event-info-summary-header/div/div/div[1]/div[1]/span[2]"));
        public IWebElement txtCountryName => PropertiesCollection.driver.FindElement(By.XPath("//*[@id=\"student-results-shell\"]/div/div[2]/div/app-event-info-summary/div/div[1]/app-event-info-summary-header/div/div/div[2]/div[2]/span[2]"));
        public IWebElement btnDeleteWriteup => wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"delete-write-up-button\"]/div")));
        public IWebElement btnYesConfirmation => wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//span[text()='Yes']")));
        #endregion

        public void SearchStudent(string People)
        {
            var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(PropertiesCollection.driver, TimeSpan.FromSeconds(60));

            System.Threading.Thread.Sleep(60000);
            PropertiesCollection.driver.SwitchTo().Frame("myFrame");
            System.Threading.Thread.Sleep(2000);

            wait.Until(ExpectedConditions.ElementToBeClickable(btnPeopleSelector));
            btnPeopleSelector.Click();
            System.Threading.Thread.Sleep(2000);
            wait.Until(ExpectedConditions.ElementToBeClickable(txtPeopleSelector));
            txtPeopleSelector.Click();
           
            txtPeopleSelector.SendKeys(People);
            wait.Until(ExpectedConditions.ElementToBeClickable(btnPeopleSearch));
           
            btnPeopleSearch.Click();
            System.Threading.Thread.Sleep(5000);
           
            firstgrdPeopleSelector.Click();
           
            wait.Until(ExpectedConditions.ElementToBeClickable(btnApply));
            btnApply.Click();
        }

        public void SearchCourseEvent(string CourseEvent)
        {
            System.Threading.Thread.Sleep(10000);
            txtboxCourseName.Click();
            System.Threading.Thread.Sleep(10000);
            txtboxCourseName.SendKeys(CourseEvent);
            System.Threading.Thread.Sleep(15000);

            for (int i = 0; i < grdresults.Count; i++)
            {

                if (grdresults.ElementAt(i).Text.Contains(CourseEvent))
                {
                    grdresults.ElementAt(i).Click();
                    break;
                }
            }

            System.Threading.Thread.Sleep(15000);
        }

        public string[] ValidateHeaderFields()
        {
            List<string> headerfields = new List<string>
            {
                txtStudentName.Text,
                txtServiceName.Text,
                txtGroupName.Text,
                txtInstructorName.Text,
                txtCourseName.Text,
                txtCountryName.Text,
                txtEventName.Text
            };

            return headerfields.ToArray();
        }

        public void AddAdditionalCategories(string Category, string strength, string weakness)
        {
            btnAdditionalCategories.Click();
            System.Threading.Thread.Sleep(6000);

            Console.WriteLine($"All the Selects {allselects}");
            Console.WriteLine($"All the Categories {allCategories}");

            for (int i = 0; i < allCategories.Count; i++)
            {
                if (allCategories.ElementAt(i).Text.Equals(Category))
                {
                    allselects.ElementAt(i).Click();
                    break;
                }
            }

            System.Threading.Thread.Sleep(6000);
            btnApplyCategoriesWindow.Click();
        }

       
        public void EnterPrivate_OverallComments (string overallcomments, string privateComments)
        {
            txtboxOverallComments.SendKeys(OpenQA.Selenium.Keys.Control);        
            txtboxOverallComments.SendKeys(OpenQA.Selenium.Keys.Delete);
            txtboxOverallComments.SendKeys(overallcomments);
            txtboxPrivateComments.SendKeys(OpenQA.Selenium.Keys.Delete);
            txtboxPrivateComments.SendKeys(privateComments);
        }

        public void ScoringEvent (string score)
        {
            System.Threading.Thread.Sleep(10000);
            Actions act = new Actions(PropertiesCollection.driver);
            dropdownScore.Click();
            System.Threading.Thread.Sleep(10000);
            txtboxscore.SendKeys(score);
            System.Threading.Thread.Sleep(10000);
            txtboxscore.SendKeys(OpenQA.Selenium.Keys.Enter);
            System.Threading.Thread.Sleep(10000);
            btnSave.Click();
            System.Threading.Thread.Sleep(10000);

        }

        public string VerifyEventIcon(string courseEvent)
        {
            var styleString = "na";         

            for (int i = 0; i < gridResults.Count; i++)
            {
                if (gridResults.ElementAt(i).Text.Contains(courseEvent))
                {
                    styleString = allIcons.ElementAt(i).GetAttribute("style");
                    Console.WriteLine(styleString);
                    break;
                }
            }

            return styleString;
        }

        public string VerifyResultAwarded()
        {
            var resultAwarded = txtResultAwarded.Text;
            Console.WriteLine("Result Awarded: " + resultAwarded);
            return resultAwarded;
        }

        public String VerifyResultAwardedBGColour()
        {
            string resultAwardedBGcolour = null;
            resultAwardedBGcolour = colourResultAwarded.GetAttribute("style");
            Console.WriteLine("Result Awarded Colour: " + resultAwardedBGcolour);
            return resultAwardedBGcolour;
        }

        public string GetTextPrivateComments()
        {
            System.Threading.Thread.Sleep(10000);
            string PrivComments = txtboxPrivateComments.GetAttribute("value");

            System.Threading.Thread.Sleep(3000);
            Console.WriteLine("Private Comments: " + PrivComments);
            return PrivComments;
        }

        public string GetTextOverallComments()
        {
            System.Threading.Thread.Sleep(10000);
            string OverallComments = txtboxOverallComments.GetAttribute("value");

            System.Threading.Thread.Sleep(3000);            
            Console.WriteLine("Overall Comments: " + OverallComments);
            return OverallComments;
        }


        public void DeleteWriteup()
        {
            System.Threading.Thread.Sleep(5000);
            btnDeleteWriteup.Click();
            System.Threading.Thread.Sleep(5000);
            btnYesConfirmation.Click(); 
        }
    }
}
