using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace BankApplication.model
{
    public class Database : IDisposable, IDatabase
    {
        private string connectionString;
        private IDbConnection connection;
        private readonly IDbConnectionFactory connectionFactory;

        public Database(string connectionString, IDbConnectionFactory connectionFactory)
        {
            this.connectionString = connectionString;
            this.connectionFactory = connectionFactory;
            this.connection = connectionFactory.CreateConnection(connectionString);
            this.connection.Open();
        }

        public DataTable ExecuteQuery(string query)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = query;
                DataTable dataTable = new DataTable();
                using (var reader = command.ExecuteReader())
                {
                    dataTable.Load(reader);
                }
                return dataTable;
            }
        }

        public int ExecuteNonQuery(string query)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = query;
                return command.ExecuteNonQuery();
            }
        }

        public object ExecuteScalar(string query)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = query;
                object? result = command.ExecuteScalar();
                if (result == null)
                {
                    throw new InvalidOperationException("ExecuteScalar returned null.");
                }

                return result;
            }
        }

        public void Dispose()
        {
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }
}
