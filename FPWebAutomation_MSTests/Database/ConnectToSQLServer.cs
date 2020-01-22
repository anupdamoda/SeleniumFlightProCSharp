using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPWebAutomation_MSTests.Database
{
    class ConnectToSQLServer
    {
        public SqlConnection sqlCon;

        /* Constructor */
        public ConnectToSQLServer()
        {
            Initialize();
        }

        /* Connect to SQL Server */
        private void Initialize()
        {

            string connectionString = "Data Source=" + ConfigurationManager.AppSettings["SQLServerDataSource"] + ";Initial Catalog=" + ConfigurationManager.AppSettings["SQLServerInitialCatalog"] + ";User Id=" + ConfigurationManager.AppSettings["SQLServerUserId"] + ";Password=" + ConfigurationManager.AppSettings["SQLServerPassword"];
            sqlCon = new SqlConnection(connectionString);
        }


        /* Open Database conenction */
        private bool OpenConnection()
        {
            try
            {
                sqlCon.Open();
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
                sqlCon.Close();
                return true;
            }
            catch (SqlException excep)
            {
                Console.WriteLine(excep.Message);
                return false;
            }

        }



        /* Delete statement */
        public bool Delete(string strtblname, string strtblcolumn, string strwherecondn)
        {
            string str = "DELETE from " + strtblname + " WHERE " + strtblcolumn + "='" + strwherecondn + "';";
            Console.WriteLine(str);
            if (this.OpenConnection() == true)
            {
                SqlCommand sqlcmd = new SqlCommand(str, sqlCon);
                sqlcmd.ExecuteNonQuery();

                try
                {
                    this.CloseConnection();
                    return true;
                }
                catch (SqlException excep)
                {
                    Console.WriteLine(excep.Message);
                    return false;
                }
            }
            else
            {
                this.CloseConnection();
                return false;
            }
        }

        /* Select statement to select a specific double value */
        public string Select(string tblname, string tblselectcolumn, string tblcolumn, string wherecondn)
        {
            string str = "SELECT " + tblselectcolumn + " from " + tblname + " WHERE " + tblcolumn + "='" + wherecondn + "';";
            string result = null;

            Console.WriteLine(str);
            if (this.OpenConnection() == true)
            {
                try
                {
                    SqlCommand sqlcmd = new SqlCommand(str, sqlCon);
                    result = sqlcmd.ExecuteScalar().ToString();
                    Console.WriteLine(result);
                    this.CloseConnection();
                    return result;
                }
                catch (SqlException excep)
                {
                    Console.WriteLine(excep.Message);
                    return result;
                }
            }
            else
            {
                this.CloseConnection();
                return result;
            }
        }

        public bool Delete_Int(string strtblname, string strtblcolumn,  Int64 strwherecondn)
        {
            string str = "DELETE from " + strtblname + " WHERE " + strtblcolumn + "='" + strwherecondn + "';";
            Console.WriteLine(str);
            if (this.OpenConnection() == true)
            {
                SqlCommand sqlcmd = new SqlCommand(str, sqlCon);
                sqlcmd.ExecuteNonQuery();

                try
                {
                    this.CloseConnection();
                    return true;
                }
                catch (SqlException excep)
                {
                    Console.WriteLine(excep.Message);
                    return false;
                }
            }
            else
            {
                this.CloseConnection();
                return false;
            }
        }

        /* Select  statement */
        public Int64 Select(string strtblname, string strtblcolumn, string strwherecondn)
        {
            string strValue;
            Int64 csVal = 0;
            sqlCon.Open();
            SqlCommand cm = new SqlCommand("Select * from " + strtblname + " WHERE " + strtblcolumn + "='" + strwherecondn + "';",sqlCon);
            Console.WriteLine("Select * from " + strtblname + " WHERE " + strtblcolumn + "='" + strwherecondn + "';");
            
            SqlDataReader reader = cm.ExecuteReader();

            if (reader.Read())
            {
                csVal = reader.GetInt64(reader.GetOrdinal("RosterID"));
                Console.WriteLine("csVal" + csVal);
                return csVal;
                //    csVal = System.Convert.ToInt16(strValue);
            }
            else
            {
                return csVal;
            }
            sqlCon.Close();
            reader.Close();

            this.CloseConnection();

            if (cm.Connection.State == ConnectionState.Open)
            {
                cm.Connection.Close();
            }

            //string strValue = null; 

            //var str = "Select * from " + strtblname + " WHERE " + strtblcolumn + "='" + strwherecondn + "';";
            //Console.WriteLine(str);
            //if (this.OpenConnection() == true)
            //{
            //    SqlCommand sqlcmd = new SqlCommand(str, sqlCon);
            //    sqlcmd.ExecuteNonQuery();
            //    SqlDataReader dataReader = sqlcmd.ExecuteReader();
            //    Console.WriteLine(dataReader.HasRows);
            //    switch (strtblname)

            //    {
            //        case "tblRoster":
            //            Console.WriteLine(strtblname);
            //            while (dataReader.Read())
            //            {
            //                Console.WriteLine(dataReader.GetOrdinal("RosterID"));
            //               strValue = dataReader.GetString(dataReader.GetOrdinal("RosterID"));

            //            }                    
            //            break;     
            //    }
            //    //close Data Reader
            //    dataReader.Close();

            //    //close Connection
            //    this.CloseConnection();

            //return list to be displayed


        }

        /* Select statement to select a specific double value */
        public string Select(string tblname, string tblselectcolumn, string tblcolumn, string wherecondn)
        {
            string str = "SELECT " + tblselectcolumn + " from " + tblname + " WHERE " + tblcolumn + "='" + wherecondn + "';";
            string result = null;

            Console.WriteLine(str);
            if (this.OpenConnection() == true)
            {
                try
                {
                    SqlCommand sqlcmd = new SqlCommand(str, sqlCon);
                    result = sqlcmd.ExecuteScalar().ToString();
                    Console.WriteLine(result);
                    this.CloseConnection();
                    return result;
                }
                catch (SqlException excep)
                {
                    Console.WriteLine(excep.Message);
                    return result;
                }
            }
            else
            {
                this.CloseConnection();
                return result;
            }
        }

            

       
        

        /* Assign Planning Board To Security Group */
        public bool AssignPlanningBoardToSecurityGroup(string strPlanningBoardName, string strSecurityGroupName)
        {
            string str = "DECLARE @boardName nvarchar(50) = '" + strPlanningBoardName + "'; DECLARE @securityGroupName nvarchar(50) = '" + strSecurityGroupName + "';  DECLARE @newID bigint; DECLARE @base bigint = (SELECT StaticConfigValInt FROM tblStaticConfig WHERE StaticConfigID = 50) *10000000000; DECLARE @boardID bigint = 0; SELECT @boardID = (SELECT TOP(1) PlanningBoardID FROM tblPlanningBoard WHERE  Name = @boardName); DECLARE @securityGroupID bigint = 0; SELECT @securityGroupID = (SELECT TOP(1) SecurityGroupID FROM tblSecurityGroup WHERE SecurityGroupName = @securityGroupName); IF @boardID > 0 AND @securityGroupID > 0 BEGIN IF NOT EXISTS (SELECT 1 FROM tblSecurityGroupObjectData WHERE SecurityGroupID = @securityGroupID AND LinkID = @boardID AND SecurityObjectID = 220) BEGIN EXEC FPNextRecordID  'tblSecurityGroupObjectData', 'SecurityGroupObjectDataID', 1, 1, @newID OUT PRINT 'NewID: ' + CAST(@newID AS nvarchar); INSERT INTO tblSecurityGroupObjectData (SecurityGroupObjectDataID, SecurityGroupID, SecurityObjectID, SecurityLevel, LinkID, LastUpdated, LoginName) VALUES (@base + @newID, @securityGroupID, 220, 4, @boardID, GETUTCDATE(), 'Automation') END ELSE PRINT 'Security already exists for ' + @boardName + ' (' + CAST(@boardID as nvarchar) + ') and ' + @securityGroupName + ' (' +  CAST(@securityGroupID as nvarchar) + ').' END ELSE PRINT 'Planning Board and/or Security Group not found.' ";

            if (this.OpenConnection() == true)
            {
                SqlCommand sqlcmd = new SqlCommand(str, sqlCon);
                sqlcmd.ExecuteNonQuery();

                try
                {
                    this.CloseConnection();
                    return true;
                }
                catch (SqlException excep)
                {
                    Console.WriteLine(excep.Message);
                    return false;
                }
            }
            else
            {
                this.CloseConnection();
                return false;
            }

        }

        /* Run SQL */
        public bool RunSQL(string str)
        {
            if (this.OpenConnection() == true)
            {
                SqlCommand sqlcmd = new SqlCommand(str, sqlCon);
                sqlcmd.ExecuteNonQuery();

                try
                {
                    this.CloseConnection();
                    return true;
                }
                catch (SqlException excep)
                {
                    Console.WriteLine(excep.Message);
                    return false;
                }
            }
            else
            {
                this.CloseConnection();
                return false;
            }
        }
    }
}





