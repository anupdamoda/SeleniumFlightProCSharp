using System.Data.SqlClient;

using System.IO;

namespace StartupScript
{
    class StratupScript
    {
        static void Main(string[] args)
        {
            SqlConnection sqlCon = new SqlConnection("Data Source=OC-SVR-AT1\\SQL2016;Initial Catalog=TestAutomation_SQL2016_FP_v8_01_100;Integrated Security=True;");

            string str = "USE master;";
            string str1 = "DECLARE @kill varchar(8000); SET @kill = ''; SELECT @kill = @kill + 'kill ' + CONVERT(varchar(5), spid) + ';' FROM master..sysprocesses WHERE dbid = db_id('TestAutomation_SQL2016_FP_v8_01_100'); EXEC(@kill);";
            string str2 = "BACKUP DATABASE [TestAutomation_SQL2016_FP_v8_01_100] TO DISK = N'C:\\Program Files\\Microsoft SQL Server\\MSSQL13.SQL2016\\MSSQL\\Backup\\TestAutomation_SQL2016_FP_v8_01_100.bak' WITH INIT";
            string str3 = "BACKUP DATABASE [TestAutomation_SQL2016_FP_TestDataDB] TO DISK = N'C:\\Program Files\\Microsoft SQL Server\\MSSQL13.SQL2016\\MSSQL\\Backup\\TestAutomation_SQL2016_FP_TestDataDB.bak' WITH INIT";
            string str4 = "RESTORE DATABASE[TestAutomation_SQL2016_FP_v8_01_100] FROM DISK = N'C:\\Program Files\\Microsoft SQL Server\\MSSQL13.SQL2016\\MSSQL\\Backup\\TestAutomation_SQL2016_FP_TestDataDB.bak' WITH REPLACE";
            string str5 = "USE[TestAutomation_SQL2016_FP_v8_01_100]";
            string str6 = "DELETE FROM[dbo].[tblSite]";
            string str7 = "INSERT [dbo].[tblSite] ([SiteID], [TCPPortNumber], [UDPPortNumber], [SiteColour], [HeartbeatSeconds], [SiteName], [IPV4Address], [IPV6Address], [SiteGroupID], [DBConnectionString], [AgentIPV4Address], [AgentIPV6Address], [AgentTCPPortNumber], [ExternalServiceURL], [RemoteFileDirectory], [RepositoryDatabase], [IsActive], [ExchangeSvrUrl], [ExchangeSvrGroupID], [LastUpdated], [LoginName], [MSMQQueueName], [SSLEncryptionRequired], [SSLClientCertificateRequired], [SSLCheckForCertificateRevocation], [SSLAllowedPolicyErrors], [FlightProAPIURL], [UndoStripUrl]) VALUES(17000, 7001, 4001, 3854756, 30, N'FlightPro Demo v.8', N'oc-svr-at1', NULL, 0, N'Provider=sqloledb;Data Source=oc-svr-at1\\sql2016;Initial Catalog=TestAutomation_SQL2016_FP_v8_01_100;User Id=fltpro4;Password=fltpro4', N'oc-svr-at1', NULL, 7101, NULL, NULL, NULL, 1, NULL, 1, NULL, N'Obfs', N'OS:oc-svr-at1\\private$\fpapi_v8', 0, 0, 0, 7, N'http://oc-svr-at1/FlightPro_WEB_v8/API', N'')";

            sqlCon.Open();

            SqlCommand cmd = new SqlCommand(str, sqlCon);
            SqlCommand cmd1 = new SqlCommand(str1, sqlCon);
            SqlCommand cmd2 = new SqlCommand(str2, sqlCon);
            SqlCommand cmd3 = new SqlCommand(str3, sqlCon);
            SqlCommand cmd4 = new SqlCommand(str4, sqlCon);
            SqlCommand cmd5 = new SqlCommand(str5, sqlCon);
            SqlCommand cmd6 = new SqlCommand(str6, sqlCon);
            SqlCommand cmd7 = new SqlCommand(str7, sqlCon);

            cmd.ExecuteNonQuery();
            cmd1.ExecuteNonQuery();
            cmd2.ExecuteNonQuery();
            cmd3.ExecuteNonQuery();
            cmd4.ExecuteNonQuery();
            cmd5.ExecuteNonQuery();
            cmd6.ExecuteNonQuery();
            cmd7.ExecuteNonQuery();

            sqlCon.Close();
        }
    }
}