using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Protractor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPWebAutomation_MSTests
{
    public class PropertiesCollection
    {

        public static IWebDriver driver { get; set; }

   //     public static WebDriverWait wait30 = new WebDriverWait(PropertiesCollection.driver, TimeSpan.FromSeconds(30));

        public static NgWebDriver ngDriver { get; set; }
        public static ExtentReports extent;
        public static ExtentHtmlReporter htmlReporter;
        public static ExtentTest test;
     //   public static TestContext TestContext { get; set; }

    }
}
