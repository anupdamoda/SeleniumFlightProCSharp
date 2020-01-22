using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPWebAutomation
{
    public class PropertiesCollection
    {

        public static IWebDriver driver { get; set; }

         WebDriverWait wait30 = new WebDriverWait(PropertiesCollection.driver, TimeSpan.FromSeconds(30));

    }
}
