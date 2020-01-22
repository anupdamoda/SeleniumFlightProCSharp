using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPWebAutomation.Libraries
{
    class Lib_Logout
    {
        public static void Logout()

        {

            var logout = new PageObjects.clsMainPage_TopbarMenu();
           // PageFactory.InitElements(dr, logout);
            logout.linkLogout.Click();


            // Close the driver
            //dr.Quit();

        }


    }
}
