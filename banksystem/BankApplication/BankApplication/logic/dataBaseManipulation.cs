using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client.NativeInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankApplication.logic
{
    public static class dataBaseManipulation
    {
        private static string ConString = OurSqlConnectionString.ConString;

        public static void deposit(string toAccount, double amount)
        {
            try
            {
                using (SqlConnection Con = new SqlConnection(ConString))
                {
                    string query = "UPDATE Account SET Balance = Balance + @deposit WHERE Id = @AccountId";
                    using (SqlCommand command = new SqlCommand(query, Con))
                    {
                        command.Parameters.AddWithValue("@AccountId", toAccount);
                        command.Parameters.AddWithValue("@deposit", amount);
                        Con.Open();
                        command.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                // Some Exception?
            }
        }

        public static bool is_in_database(string accountId)
        {
            using (SqlConnection Con = new SqlConnection(ConString))
            {
                string query = "SELECT COUNT(*) FROM Account WHERE ID = @AccountId";
                using (SqlCommand command = new SqlCommand(query, Con))
                {
                    command.Parameters.AddWithValue("@AccountId", accountId);
                    Con.Open();
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }
        public static void withdraw(string fromAccount, double amount)
        {
            try
            {
                using (SqlConnection Con = new SqlConnection(ConString))
                {
                    string query = "UPDATE Account SET Balance = Balance - @withdraw WHERE Id = @AccountId";
                    using (SqlCommand command = new SqlCommand(query, Con))
                    {
                        command.Parameters.AddWithValue("@AccountId", fromAccount);
                        command.Parameters.AddWithValue("@withdraw", amount);
                        Con.Open();
                        command.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                // Some Exception?
            }
        }
    }
}
