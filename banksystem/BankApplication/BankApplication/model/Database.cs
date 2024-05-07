using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace BankApplication.model
{
    public class Database : IDisposable, IDatabase
    {
        // Connection string to your database
        private string connectionString;
        private SqlConnection connection;

        public Database(string connectionString)
        {
            this.connectionString = connectionString;
            this.connection = new SqlConnection(this.connectionString);
            this.connection.Open();
        }

        // Execute a query that returns data
        public DataTable ExecuteQuery(string query)
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                DataTable dataTable = new DataTable();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    dataTable.Load(reader);
                }
                return dataTable;
            }
        }

        // Execute a query that doesn't return data (e.g., INSERT, UPDATE, DELETE)
        public int ExecuteNonQuery(string query)
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                return command.ExecuteNonQuery();
            }
        }

        // Execute a query that returns a single value (e.g., COUNT, MAX, MIN)
        public object ExecuteScalar(string query)
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                return command.ExecuteScalar();
            }
        }

        // Dispose method to close the connection when the Database object is disposed
        public void Dispose()
        {
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }
}
