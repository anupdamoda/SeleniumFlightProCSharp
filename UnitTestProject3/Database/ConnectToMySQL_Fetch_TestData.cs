using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* Purpose: Connect to MySQL Database (Test Data Database)
 * Author: Anup Damodaran
 * Date first created :23 /05/ 2019
 * 
 * 
 */

namespace FPWebAutomation_MSTests.Database
{
    class ConnectToMySQL_Fetch_TestData
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string user;
        private string password;
        private string port;
        private string connString;
        private string sslM;
        private string port_id;


        //Constructor
        public ConnectToMySQL_Fetch_TestData()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {

            string connectionString = "server=" + ConfigurationManager.AppSettings["TestDataDBServer"] + ";user=" + ConfigurationManager.AppSettings["TestDataDBUsername"] + ";database=" + ConfigurationManager.AppSettings["TestDataDBDatabase"] + ";password=" + ConfigurationManager.AppSettings["TestDataDBPassword"];
            connection = new MySqlConnection(connectionString);
        }

        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        internal static void select()
        {
            throw new NotImplementedException();
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }



        //Insert statement
        public void Insert()
        {
        }

        //Update statement
        public void Update()
        {
        }

        //Delete statement
        public void Delete()
        {
        }

        //Select statement
        public string[] Select(string strtblname, string strTestCaseNo, string strTestType)
        {

            string query = "SELECT * FROM " + strtblname + " where TestCaseNo='" + strTestCaseNo + "' and TestType='" + strTestType + "'";

            //Create a list to store the result
            String[] TestData = new string[16];

            //Open connection
            if (this.OpenConnection() == true)
            {

                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                Console.WriteLine(dataReader.HasRows);

                switch (strtblname)

                {


                    case "automation_shiftadministration":

                        Console.WriteLine(strtblname);


                        while (dataReader.Read())
                        {
                            Console.WriteLine(dataReader.GetOrdinal("TestCaseNo"));
                            Console.WriteLine(dataReader.GetOrdinal("TestCaseName"));
                            Console.WriteLine(dataReader.GetOrdinal("Environment"));
                            Console.WriteLine(dataReader.GetOrdinal("OrganisationGroup"));
                            Console.WriteLine(dataReader.GetOrdinal("ShiftName"));
                            Console.WriteLine(dataReader.GetOrdinal("ShortCode"));
                            Console.WriteLine(dataReader.GetOrdinal("StartTime"));
                            Console.WriteLine(dataReader.GetOrdinal("Duration"));
                            Console.WriteLine(dataReader.GetOrdinal("Currencies"));
                            TestData[0] = dataReader.GetString(dataReader.GetOrdinal("TestCaseNo"));
                            TestData[1] = dataReader.GetString(dataReader.GetOrdinal("TestCaseName"));
                            TestData[2] = dataReader.GetString(dataReader.GetOrdinal("Environment"));
                            TestData[3] = dataReader.GetString(dataReader.GetOrdinal("OrganisationGroup"));
                            TestData[4] = dataReader.GetString(dataReader.GetOrdinal("ShiftName"));
                            TestData[5] = dataReader.GetString(dataReader.GetOrdinal("ShortCode"));
                            TestData[6] = dataReader.GetString(dataReader.GetOrdinal("StartTime"));
                            TestData[7] = dataReader.GetString(dataReader.GetOrdinal("Duration"));
                            TestData[8] = dataReader.GetString(dataReader.GetOrdinal("Currencies"));
                            TestData[9] = dataReader.GetString(dataReader.GetOrdinal("Active"));
                        }

                        break;


                    case "automation_rosteradministration":

                        Console.WriteLine(strtblname);

                        while (dataReader.Read())
                        {
                            Console.WriteLine(dataReader.GetOrdinal("TestCaseNo"));
                            Console.WriteLine(dataReader.GetOrdinal("TestCaseName"));
                            Console.WriteLine(dataReader.GetOrdinal("Environment"));
                            Console.WriteLine(dataReader.GetOrdinal("OrganisationGroup"));
                            Console.WriteLine(dataReader.GetOrdinal("RosterName"));
                            Console.WriteLine(dataReader.GetOrdinal("Pane"));
                            Console.WriteLine(dataReader.GetOrdinal("TimeZoneorLocation"));
                            Console.WriteLine(dataReader.GetOrdinal("TimeZone"));
                            Console.WriteLine(dataReader.GetOrdinal("Location"));
                            Console.WriteLine(dataReader.GetOrdinal("People"));
                            Console.WriteLine(dataReader.GetOrdinal("Shiftdetails"));


                            TestData[0] = dataReader.GetString(dataReader.GetOrdinal("TestCaseNo"));
                            TestData[1] = dataReader.GetString(dataReader.GetOrdinal("TestCaseName"));
                            TestData[2] = dataReader.GetString(dataReader.GetOrdinal("Environment"));
                            TestData[3] = dataReader.GetString(dataReader.GetOrdinal("OrganisationGroup"));
                            TestData[4] = dataReader.GetString(dataReader.GetOrdinal("RosterName"));
                            TestData[5] = dataReader.GetString(dataReader.GetOrdinal("Pane"));
                            TestData[6] = dataReader.GetString(dataReader.GetOrdinal("TimeZoneorLocation"));
                            TestData[7] = dataReader.GetString(dataReader.GetOrdinal("TimeZone"));
                            TestData[8] = dataReader.GetString(dataReader.GetOrdinal("Location"));
                            TestData[9] = dataReader.GetString(dataReader.GetOrdinal("People"));
                            TestData[10] = dataReader.GetString(dataReader.GetOrdinal("Shiftdetails"));
                            TestData[11] = dataReader.GetString(dataReader.GetOrdinal("Active"));

                        }

                        break;


                    case "automation_studentresults":

                        Console.WriteLine(strtblname);


                        while (dataReader.Read())
                        {
                            Console.WriteLine(dataReader.GetOrdinal("TestCaseNo"));
                            Console.WriteLine(dataReader.GetOrdinal("TestCaseName"));
                            Console.WriteLine(dataReader.GetOrdinal("Environment"));
                            Console.WriteLine(dataReader.GetOrdinal("TestType"));
                            Console.WriteLine(dataReader.GetOrdinal("OrganisationGroup"));
                            Console.WriteLine(dataReader.GetOrdinal("StudentName"));
                            Console.WriteLine(dataReader.GetOrdinal("InstructorName"));
                            Console.WriteLine(dataReader.GetOrdinal("CourseName"));
                            Console.WriteLine(dataReader.GetOrdinal("SyllabusName"));
                            Console.WriteLine(dataReader.GetOrdinal("EventName"));
                            Console.WriteLine(dataReader.GetOrdinal("Score"));
                            Console.WriteLine(dataReader.GetOrdinal("ScoreAssesmentCriteria"));
                            Console.WriteLine(dataReader.GetOrdinal("Strength"));
                            Console.WriteLine(dataReader.GetOrdinal("Weakness"));
                            Console.WriteLine(dataReader.GetOrdinal("OverallComments"));
                            Console.WriteLine(dataReader.GetOrdinal("PrivateComments"));
                            TestData[0] = dataReader.GetString(dataReader.GetOrdinal("TestCaseNo"));
                            TestData[1] = dataReader.GetString(dataReader.GetOrdinal("TestCaseName"));
                            TestData[2] = dataReader.GetString(dataReader.GetOrdinal("Environment"));
                            TestData[3] = dataReader.GetString(dataReader.GetOrdinal("TestType"));
                            TestData[4] = dataReader.GetString(dataReader.GetOrdinal("OrganisationGroup"));
                            TestData[5] = dataReader.GetString(dataReader.GetOrdinal("StudentName"));
                            TestData[6] = dataReader.GetString(dataReader.GetOrdinal("InstructorName"));
                            TestData[7] = dataReader.GetString(dataReader.GetOrdinal("CourseName"));
                            TestData[8] = dataReader.GetString(dataReader.GetOrdinal("SyllabusName"));
                            TestData[9] = dataReader.GetString(dataReader.GetOrdinal("EventName"));
                            TestData[10] = dataReader.GetString(dataReader.GetOrdinal("Score"));
                            TestData[11] = dataReader.GetString(dataReader.GetOrdinal("ScoreAssesmentCriteria"));
                            TestData[12] = dataReader.GetString(dataReader.GetOrdinal("Strength"));
                            TestData[13] = dataReader.GetString(dataReader.GetOrdinal("Weakness"));
                            TestData[14] = dataReader.GetString(dataReader.GetOrdinal("OverallComments"));
                            TestData[15] = dataReader.GetString(dataReader.GetOrdinal("PrivateComments"));
                        }

                        break;

                }
                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return TestData;

            }
            else
            {
                return TestData;
            }

        }



    }

}



