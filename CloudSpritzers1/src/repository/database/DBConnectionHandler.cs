using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using DotNetEnv;
using Microsoft.Data.SqlClient;
namespace CloudSpritzers1.src.repository.database
{
    public class DBConnectionHandler
    {
        private static readonly DBConnectionHandler _instance = new DBConnectionHandler();
        public static DBConnectionHandler Instance => _instance;

        private readonly string _connectionString;
        // https://learn.microsoft.com/en-us/dotnet/framework/data/adonet/sql-server-connection-pooling
        // Seems like ado.net pools connections by default, sorry Maria
        public SqlConnection Connection => new SqlConnection(_connectionString);
        
        private DBConnectionHandler()
        {
            _connectionString = BuildConnectionString();
        }

        private string BuildConnectionString()
        {
            string server = Env.GetString("DB_SERVER");
            if (server == null)
                throw new SqlConnectionException("DB_SERVER environment variable is not set.");

            string database = Env.GetString("DB_NAME");
            if (database == null)
                throw new SqlConnectionException("DB_NAME environment variable is not set.");
            
            string user = Env.GetString("DB_USER");
            if (user == null)
                throw new SqlConnectionException("DB_USER environment variable is not set.");
            
            string pass = Env.GetString("DB_PASS");
            if (pass == null)
                throw new SqlConnectionException("DB_PASS environment variable is not set.");

            //string connectionString = $"Server={server};Database={database};User Id={user};Password={pass};TrustServerCertificate=True;";
            string connectionString =  $"Server={server};Database={database};Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;";
            return connectionString;
        }
    }
}
