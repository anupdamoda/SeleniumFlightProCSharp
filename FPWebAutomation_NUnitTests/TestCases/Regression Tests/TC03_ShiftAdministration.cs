using FPWebAutomation.Database;
using FPWebAutomation.Libraries;
using FPWebAutomation.PageObjects;
using MySql.Data.MySqlClient;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPWebAutomation.TestCases
{
    class TC03_ShiftAdministration
    {


        [SetUp]
        public void Initialize()
        {
            PropertiesCollection.driver = new ChromeDriver(@ConfigurationManager.AppSettings["ChromeDriverPath"]);
            PropertiesCollection.driver.Manage().Window.Maximize();
            

            /*
            var dbCon = ConnectToMySQL_Fetch_TestData.Instance();
            dbCon.DatabaseName = "AutomationDB";
            if (dbCon.IsConnect())
            {
                //suppose col0 and col1 are defined as VARCHAR in the DB
                string query = "SELECT col0,col1 FROM automation.automation_shiftadministration";
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string someStringFromColumnZero = reader.GetString(0);
                    string someStringFromColumnOne = reader.GetString(1);
                    Console.WriteLine(someStringFromColumnZero + "," + someStringFromColumnOne);
                }
                dbCon.Close();
            }
            */
        }

        [Test]
        public void TC03_FPWeb_Admin_Rostering_ShiftAdministration()
        {

            clsLoginPage loginPage = new clsLoginPage();
            loginPage.LoginPage();
            

            var TopbarMenu = new clsMainPage_TopbarMenu();

            TopbarMenu.NavigatetoShiftAdministration();

            var ShiftAdministration = new clsShiftAdministration();

            ShiftAdministration.AddShiftdetails("Night","N","08:00","9:00","BFM");

            TopbarMenu.NavigatetoShiftAdministration();

            ShiftAdministration.EditShiftdetails("Night", "12:00");

            TopbarMenu.NavigatetoShiftAdministration();

            ShiftAdministration.CopyShiftdetails("Night","Night-Copy","11:00");

            TopbarMenu.NavigatetoShiftAdministration();

            ShiftAdministration.DeleteShiftdetails("Night");


          //   TopbarMenu.Logout();


        }


        [TearDown]
        public void TearDown()
        {
           
        }



    }

}

