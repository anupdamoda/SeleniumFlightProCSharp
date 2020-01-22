using System;
using System.Configuration;
using System.Data.SqlClient;

/* Purpose: Connect to SQL Server Database (Test Data Database)
 * Author: Anitha Joshua
 * Date first created :12 /12/ 2019
 */

namespace FPWebAutomation_MSTests.Database
{
    class ConnectToSQLServer_Fetch_TestData
    {
        private SqlConnection connection;


        /* Constructor */
        public ConnectToSQLServer_Fetch_TestData()
        {
            Initialize();
        }

        /* Connect to SQL Server */
        private void Initialize()
        {

            string connectionString = "Data Source=" + ConfigurationManager.AppSettings["SQLServerTestDataSource"] + ";Initial Catalog=" + ConfigurationManager.AppSettings["SQLServerTestInitialCatalog"] + ";Integrated Security=" + ConfigurationManager.AppSettings["SQLServerIntegratedSecurity"] + ';';
            connection = new SqlConnection(connectionString);
        }

        /* Open Database conenction */
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (SqlException exception)
            {
                Console.WriteLine(exception.Message);
                return false;
            }
        }

        /* Close Database conenction */
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (SqlException excep)
            {
                Console.WriteLine(excep.Message);
                return false;
            }

        }

        
        /* Get Login Credentials */
        public string[] GetLoginDetails(string Login)
        {
            String[] TestData = new string[3];
            try
            {

                string connectionString = "server=" + ConfigurationManager.AppSettings["TestDataServer"] + ";user=" + ConfigurationManager.AppSettings["TestDataUsername"] + ";database=" + ConfigurationManager.AppSettings["TestDataDatabase"] + ";password=" + ConfigurationManager.AppSettings["TestDataPassword"];
                var conn = new SqlConnection(connectionString);
                conn.Open();
                string str = "Select Username, Password from automation_login where Login = '" + Login + "';";
                SqlCommand cmd = new SqlCommand(str, conn);
                SqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    TestData[1] = dataReader.GetString(dataReader.GetOrdinal("Username"));
                    TestData[2] = dataReader.GetString(dataReader.GetOrdinal("Password"));
                }
                dataReader.Close();
                conn.Close();
                return TestData;
            }
            catch
            {
                return TestData;
            }
        }


        /*Select statement */
        public string[] Select(string strtblname, string strTestCaseNo, string strTestType)
        {

            string query = "SELECT * FROM " + strtblname + " where TestCaseNo='" + strTestCaseNo + "' and TestType='" + strTestType + "'";
            Console.WriteLine(query);

           /* Create a list to store the result */
            String[] TestData = new string[25];


            /* Open connection */
            if (this.OpenConnection() == true)
            {
                SqlCommand sqlcmd = new SqlCommand(query, connection);
                SqlDataReader dataReader = sqlcmd.ExecuteReader();

                Console.WriteLine(dataReader.HasRows);

                switch (strtblname)

                {
                    case "automation_shiftadministration":
                        { 
                        Console.WriteLine(strtblname);

                        while (dataReader.Read())
                        {
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
                         }

                    case "automation_rosteradministration":

                        Console.WriteLine(strtblname);

                        while (dataReader.Read())
                        {

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
                            TestData[16] = dataReader.GetString(dataReader.GetOrdinal("ServiceName"));
                            TestData[17] = dataReader.GetString(dataReader.GetOrdinal("CountryName"));
                            TestData[18] = dataReader.GetString(dataReader.GetOrdinal("StudentPosition"));
                            TestData[19] = dataReader.GetString(dataReader.GetOrdinal("StudentSurname"));
                            TestData[20] = dataReader.GetString(dataReader.GetOrdinal("ResultAwarded"));


                        }

                        break;

                    case "automation_combinedschedule":

                        Console.WriteLine(strtblname);


                        while (dataReader.Read())
                        {
                            TestData[0] = dataReader.GetString(dataReader.GetOrdinal("TestCaseNo"));
                            TestData[1] = dataReader.GetString(dataReader.GetOrdinal("TestCaseName"));
                            TestData[2] = dataReader.GetString(dataReader.GetOrdinal("Environment"));
                            TestData[3] = dataReader.GetString(dataReader.GetOrdinal("ReportName"));
                            TestData[4] = dataReader.GetString(dataReader.GetOrdinal("OrganisationGroup"));
                            TestData[5] = dataReader.GetString(dataReader.GetOrdinal("Classification"));
                            TestData[6] = dataReader.GetString(dataReader.GetOrdinal("Pane"));
                            TestData[7] = dataReader.GetString(dataReader.GetOrdinal("Note"));
                            TestData[8] = dataReader.GetString(dataReader.GetOrdinal("Date"));
                        }

                        break;

                    case "automation_activitytype":

                        while (dataReader.Read())
                        {
                            TestData[0] = dataReader.GetString(dataReader.GetOrdinal("TestCaseNo"));
                            TestData[1] = dataReader.GetString(dataReader.GetOrdinal("TestCaseName"));
                            TestData[2] = dataReader.GetString(dataReader.GetOrdinal("Environment"));
                            TestData[3] = dataReader.GetString(dataReader.GetOrdinal("ActivityShortCode"));
                            TestData[4] = dataReader.GetString(dataReader.GetOrdinal("ActivityName"));
                            TestData[5] = dataReader.GetString(dataReader.GetOrdinal("Colour"));
                            TestData[6] = dataReader.GetString(dataReader.GetOrdinal("EditTestData"));
                        }

                        break;

                    case "automation_defineplanningboard":

                        Console.WriteLine(strtblname);
                        while (dataReader.Read())
                        {
                            TestData[0] = dataReader.GetString(dataReader.GetOrdinal("TestCaseNo"));
                            TestData[1] = dataReader.GetString(dataReader.GetOrdinal("TestCaseName"));
                            TestData[2] = dataReader.GetString(dataReader.GetOrdinal("Environment"));
                            TestData[3] = dataReader.GetString(dataReader.GetOrdinal("PlanningBoardName"));
                            TestData[4] = dataReader.GetString(dataReader.GetOrdinal("SecurityGroup"));
                            TestData[5] = dataReader.GetString(dataReader.GetOrdinal("OrganisationGroup"));
                            TestData[6] = dataReader.GetString(dataReader.GetOrdinal("SelectOrgGroup"));
                            TestData[7] = dataReader.GetString(dataReader.GetOrdinal("EditTestData"));
                        }

                        break;

                    case "automation_planningboard":

                        Console.WriteLine(strtblname);
                        while (dataReader.Read())
                        {
                            TestData[0] = dataReader.GetString(dataReader.GetOrdinal("TestCaseNo"));
                            TestData[1] = dataReader.GetString(dataReader.GetOrdinal("TestCaseName"));
                            TestData[2] = dataReader.GetString(dataReader.GetOrdinal("Environment"));
                            TestData[3] = dataReader.GetString(dataReader.GetOrdinal("ActivityCode"));
                            TestData[4] = dataReader.GetString(dataReader.GetOrdinal("ActivityName"));
                            TestData[5] = dataReader.GetString(dataReader.GetOrdinal("OrganisationGroup"));
                            TestData[6] = dataReader.GetString(dataReader.GetOrdinal("TaskCode"));
                            TestData[7] = dataReader.GetString(dataReader.GetOrdinal("AssetTypeCode"));
                            TestData[8] = dataReader.GetString(dataReader.GetOrdinal("ShiftCode"));
                        }
                        break;

                    case "automation_strips":

                        Console.WriteLine(strtblname);

                        while (dataReader.Read())
                        {
                            
                            TestData[0] = dataReader.GetString(dataReader.GetOrdinal("TestCaseNo"));
                            TestData[1] = dataReader.GetString(dataReader.GetOrdinal("TestCaseName"));
                            TestData[2] = dataReader.GetString(dataReader.GetOrdinal("Environment"));
                            TestData[3] = dataReader.GetString(dataReader.GetOrdinal("TestType"));
                            TestData[4] = dataReader.GetString(dataReader.GetOrdinal("AssetRegistration"));
                            TestData[5] = dataReader.GetString(dataReader.GetOrdinal("AssetTypeID"));
                            TestData[6] = dataReader.GetString(dataReader.GetOrdinal("CallSign"));
                            TestData[7] = dataReader.GetString(dataReader.GetOrdinal("LocationFromID"));
                            TestData[8] = dataReader.GetString(dataReader.GetOrdinal("LocationToID"));
                            TestData[9] = dataReader.GetString(dataReader.GetOrdinal("PlannedStartTime"));
                            TestData[10] = dataReader.GetString(dataReader.GetOrdinal("PlannedEndTime"));
                            TestData[11] = dataReader.GetString(dataReader.GetOrdinal("PaneId"));
                            TestData[12] = dataReader.GetString(dataReader.GetOrdinal("PersonID1"));
                            TestData[13] = dataReader.GetString(dataReader.GetOrdinal("SlotNumber1"));
                            TestData[14] = dataReader.GetString(dataReader.GetOrdinal("StripTask"));
                            TestData[15] = dataReader.GetString(dataReader.GetOrdinal("StripType"));
                            TestData[16] = dataReader.GetString(dataReader.GetOrdinal("WeatherStateID"));
                            TestData[17] = dataReader.GetString(dataReader.GetOrdinal("PersonID2"));
                            TestData[18] = dataReader.GetString(dataReader.GetOrdinal("SlotNumber2"));
                            TestData[19] = dataReader.GetString(dataReader.GetOrdinal("GroupHeaderAssetTypeID"));
                            TestData[20] = dataReader.GetString(dataReader.GetOrdinal("UnavailabilitySubTypeID"));
                            TestData[21] = dataReader.GetString(dataReader.GetOrdinal("StripID"));
                        }

                        break;

                    case "automation_summary_knowledgebase":

                        Console.WriteLine(strtblname);

                        while (dataReader.Read())
                        {
                            TestData[0] = dataReader.GetString(dataReader.GetOrdinal("TestCaseNo"));
                            TestData[1] = dataReader.GetString(dataReader.GetOrdinal("TestCaseName"));
                            TestData[2] = dataReader.GetString(dataReader.GetOrdinal("Environment"));
                            TestData[3] = dataReader.GetString(dataReader.GetOrdinal("Title"));
                            TestData[4] = dataReader.GetString(dataReader.GetOrdinal("Content"));
                            TestData[5] = dataReader.GetString(dataReader.GetOrdinal("Version"));
                            TestData[6] = dataReader.GetString(dataReader.GetOrdinal("Date"));
                            TestData[7] = dataReader.GetString(dataReader.GetOrdinal("RestrictViewing"));
                            TestData[8] = dataReader.GetString(dataReader.GetOrdinal("YourDate"));
                            TestData[9] = dataReader.GetString(dataReader.GetOrdinal("YourVersion"));
                            TestData[10] = dataReader.GetString(dataReader.GetOrdinal("Status"));
                            TestData[11] = dataReader.GetString(dataReader.GetOrdinal("User"));
                            TestData[12] = dataReader.GetString(dataReader.GetOrdinal("UpdatedVersion"));
                            TestData[13] = dataReader.GetString(dataReader.GetOrdinal("UpdatedContent"));
                            TestData[14] = dataReader.GetString(dataReader.GetOrdinal("ThirdUser"));
                            TestData[15] = dataReader.GetString(dataReader.GetOrdinal("ThirdLogin"));
                            TestData[16] = dataReader.GetString(dataReader.GetOrdinal("OtherUser"));
                            TestData[17] = dataReader.GetString(dataReader.GetOrdinal("OtherLogin"));
                            TestData[18] = dataReader.GetString(dataReader.GetOrdinal("UpdatedStatus"));
                            TestData[20] = dataReader.GetString(dataReader.GetOrdinal("Login"));
                        }

                        break;

                    case "automation_budgetadministration":

                        Console.WriteLine(strtblname);

                        while (dataReader.Read())
                        {
                            TestData[0] = dataReader.GetString(dataReader.GetOrdinal("TestCaseNo"));
                            TestData[1] = dataReader.GetString(dataReader.GetOrdinal("TestCaseName"));
                            TestData[2] = dataReader.GetString(dataReader.GetOrdinal("Environment"));
                            TestData[3] = dataReader.GetString(dataReader.GetOrdinal("BudgetName"));
                            TestData[4] = dataReader.GetString(dataReader.GetOrdinal("Description"));
                            TestData[5] = dataReader.GetString(dataReader.GetOrdinal("OrgGroup"));
                            TestData[6] = dataReader.GetString(dataReader.GetOrdinal("Person"));
                            TestData[7] = dataReader.GetString(dataReader.GetOrdinal("Pane"));
                            TestData[8] = dataReader.GetString(dataReader.GetOrdinal("SubGroup"));
                            TestData[9] = dataReader.GetString(dataReader.GetOrdinal("AssetType"));
                            TestData[10] = dataReader.GetString(dataReader.GetOrdinal("StripType"));
                            TestData[11] = dataReader.GetString(dataReader.GetOrdinal("DateFrom"));
                            TestData[12] = dataReader.GetString(dataReader.GetOrdinal("DateTo"));
                            TestData[13] = dataReader.GetString(dataReader.GetOrdinal("Allocation"));
                            TestData[14] = dataReader.GetString(dataReader.GetOrdinal("TestType"));
                        }

                        break;

                    case "automation_assettypesystems":
                        while (dataReader.Read())
                        {
                            TestData[0] = dataReader.GetString(dataReader.GetOrdinal("TestCaseNo"));
                            TestData[1] = dataReader.GetString(dataReader.GetOrdinal("TestCaseName"));
                            TestData[2] = dataReader.GetString(dataReader.GetOrdinal("Environment"));
                            TestData[3] = dataReader.GetString(dataReader.GetOrdinal("TestType"));
                            TestData[4] = dataReader.GetString(dataReader.GetOrdinal("station"));
                            TestData[5] = dataReader.GetString(dataReader.GetOrdinal("itemcode"));
                            TestData[6] = dataReader.GetString(dataReader.GetOrdinal("itemname"));
                            TestData[7] = dataReader.GetString(dataReader.GetOrdinal("ItemQty"));
                            TestData[8] = dataReader.GetString(dataReader.GetOrdinal("CatalogueItem"));
                            TestData[9] = dataReader.GetString(dataReader.GetOrdinal("catalogueitemcode"));
                            TestData[10] = dataReader.GetString(dataReader.GetOrdinal("assettype"));
                            TestData[11] = dataReader.GetString(dataReader.GetOrdinal("systemname"));
                        }

                        break;

                    case "automation_organisationgroupsettings":
                        while (dataReader.Read())
                        {
                            TestData[0] = dataReader.GetString(dataReader.GetOrdinal("TestCaseNo"));
                            TestData[1] = dataReader.GetString(dataReader.GetOrdinal("TestCaseName"));
                            TestData[2] = dataReader.GetString(dataReader.GetOrdinal("Environment"));
                            TestData[3] = dataReader.GetString(dataReader.GetOrdinal("organisationgroup"));
                            TestData[4] = dataReader.GetString(dataReader.GetOrdinal("durationformat"));
                            TestData[5] = dataReader.GetString(dataReader.GetOrdinal("breakduration"));
                            TestData[6] = dataReader.GetString(dataReader.GetOrdinal("maxconsecutivetasking"));
                            TestData[7] = dataReader.GetString(dataReader.GetOrdinal("standdown"));
                            TestData[8] = dataReader.GetString(dataReader.GetOrdinal("acknowledgementtime"));
                            TestData[9] = dataReader.GetString(dataReader.GetOrdinal("acknowledgementfor"));
                            TestData[10] = dataReader.GetString(dataReader.GetOrdinal("peoplegroup"));
                            TestData[11] = dataReader.GetString(dataReader.GetOrdinal("testtype"));
                        }

                        break;

                    case "automation_stripsubgroup":
                        while (dataReader.Read())
                        {
                            TestData[0] = dataReader.GetString(dataReader.GetOrdinal("TestCaseNo"));
                            TestData[1] = dataReader.GetString(dataReader.GetOrdinal("TestCaseName"));
                            TestData[2] = dataReader.GetString(dataReader.GetOrdinal("Environment"));
                            TestData[3] = dataReader.GetString(dataReader.GetOrdinal("Shortcode"));
                            TestData[4] = dataReader.GetString(dataReader.GetOrdinal("SubGroupName"));
                            TestData[5] = dataReader.GetString(dataReader.GetOrdinal("EditGroupName"));
                            TestData[6] = dataReader.GetString(dataReader.GetOrdinal("EditShortCode"));
                            TestData[7] = dataReader.GetString(dataReader.GetOrdinal("TestType"));
                        }

                        break;

                    case "automation_calendar":
                        while (dataReader.Read())
                        {
                            TestData[0] = dataReader.GetString(dataReader.GetOrdinal("TestCaseNo"));
                            TestData[1] = dataReader.GetString(dataReader.GetOrdinal("TestCaseName"));
                            TestData[2] = dataReader.GetString(dataReader.GetOrdinal("Environment"));
                            TestData[3] = dataReader.GetString(dataReader.GetOrdinal("UnavailabilityType"));
                            TestData[4] = dataReader.GetString(dataReader.GetOrdinal("Details"));
                            TestData[5] = dataReader.GetString(dataReader.GetOrdinal("UnavailabilityStartTime"));
                            TestData[6] = dataReader.GetString(dataReader.GetOrdinal("UnavailabilityEndTime"));
                            TestData[7] = dataReader.GetString(dataReader.GetOrdinal("UnavailabilityDescription"));
                            TestData[8] = dataReader.GetString(dataReader.GetOrdinal("NewUnavailabilityType"));
                            TestData[9] = dataReader.GetString(dataReader.GetOrdinal("NewUnavailabilityDetails"));
                            TestData[10] = dataReader.GetString(dataReader.GetOrdinal("NewUnavailabilityDescription"));
                            TestData[11] = dataReader.GetString(dataReader.GetOrdinal("TestType"));
                            TestData[12] = dataReader.GetString(dataReader.GetOrdinal("username"));
                        }

                        break;
                }
                dataReader.Close();
                this.CloseConnection();
                return TestData;                      
                
            }
            else
            {
                this.CloseConnection();
                return TestData;               
            }
        }
    }

}



