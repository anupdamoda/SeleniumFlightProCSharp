using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPWebAutomation.Database
{
    class ConnectToMySQL_Fetch_TestData
    {
        private string server;
        private string database;
        private string user;
        private string password;
        private string port;
        private string connString;
        private string sslM;
      

        //Constructor
        public ConnectToMySQL_Fetch_TestData()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            server = "10.0.2.35";
            database = "AutomationDB";
            user = "AnupD";
            password = "AnupD";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + user + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        //open connection to database
        private bool OpenConnection()
        {
        }

        //Close connection
        private bool CloseConnection()
        {
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
        public List<string>[] Select()
        {
        }

        //Count statement
        public int Count()
        {
        }

        //Backup
        public void Backup()
        {
        }

        //Restore
        public void Restore()
        {
        }


        private ConnectToMySQL_Fetch_TestData()
        {
        }

        private string databaseName = string.Empty;
        public string DatabaseName
        {
            get { return DatabaseName; }
            set { DatabaseName = value; }
        }

        public string Password { get; set; }
        private MySqlConnection connection = null;
        public MySqlConnection Connection
        {
            get { return connection; }
        }

        private static ConnectToMySQL_Fetch_TestData _instance = null;
        public static ConnectToMySQL_Fetch_TestData Instance()
        {
            if (_instance == null)
                _instance = new ConnectToMySQL_Fetch_TestData();
            return _instance;
        }

        public bool IsConnect()
        {
            if (Connection == null)
            {
                if (String.IsNullOrEmpty(databaseName))
                    return false;         

                server = "10.0.2.35";
                database = "AutomationDB";
                user = "AnupD";
                password = "AnupD";
                port = "3306";
                sslM = "none";

                connString = String.Format("server={0};port={1};user id={2}; password={3}; database={4}; SslMode={5}", server, port, user, password, database, sslM);

                connection = new MySqlConnection(connString);
                connection.Open();
            }

            return true;
        }

        
    }
}



