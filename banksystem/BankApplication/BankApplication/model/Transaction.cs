using System;
using BankApplication.myExceptions;
namespace BankApplication.model
{
	public class Transaction
	{
		public Transaction()
		{

		}


        public bool Transfer(string fromAccountNumber, string toAccountNumber, double amount)
        {
            // Logic to transfer funds between accounts...

            // Insert transaction record into database
            string connectionString = "YourDatabaseConnectionString";
            string insertQuery = "INSERT INTO Transactions (FromAccountNumber, ToAccountNumber, Amount, Timestamp) VALUES (@FromAccountNumber, @ToAccountNumber, @Amount, @Timestamp)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@FromAccountNumber", fromAccountNumber);
                    command.Parameters.AddWithValue("@ToAccountNumber", toAccountNumber);
                    command.Parameters.AddWithValue("@Amount", amount);
                    command.Parameters.AddWithValue("@Timestamp", DateTime.Now);
                    command.ExecuteNonQuery();
                }
            }

            return true; // Transaction successful
        }


        public List<TransactionRecord> GetTransactionHistory(string userId)
        {
            List<TransactionRecord> transactionHistory = new List<TransactionRecord>();
            string connectionString = "YourDatabaseConnectionString";
            string query = "SELECT FromAccountNumber, ToAccountNumber, Amount, Timestamp FROM Transactions WHERE UserId = @UserId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Read transaction data from the database
                            string fromAccountNumber = reader.GetString(0);
                            string toAccountNumber = reader.GetString(1);
                            double amount = reader.GetDouble(2);
                            DateTime timestamp = reader.GetDateTime(3);

                            // Create TransactionRecord object and add it to the list
                            TransactionRecord transaction = new TransactionRecord(fromAccountNumber, toAccountNumber, amount, timestamp);
                            transactionHistory.Add(transaction);
                        }
                    }
                }
            }

            return transactionHistory;
        }


    }
}

