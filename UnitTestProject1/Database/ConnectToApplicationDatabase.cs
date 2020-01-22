using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPAPIAutomation_MSTests.Database
{
    [TestClass]
    public class ConnectToApplicationDatabase
    {
        
        

        [TestMethod]
        public void ConnectiontoSQL()
        {

            SqlConnection myConnection = new SqlConnection("Data Source=oc-svr-at1\\SQL2016;Initial Catalog=TestAutomation_SQL2016_FP_v8_01_100;User Id=fltpro4;Password=fltpro4;Integrated Security=True;");

            myConnection.Open();

            //     SqlCommand myCommand = new SqlCommand("Command String", myConnection);



            try
            {
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("select * from dbo.tblAsset where AssetTail ='QA214'",
                                                         myConnection);
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    Console.WriteLine(myReader["AssetID"].ToString());
                    Console.WriteLine(myReader["AssetStatus"].ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        

    }

 }

