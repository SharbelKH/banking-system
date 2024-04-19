using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Drawing;
using System.Net;

namespace BankApplication.logic
{
    public class User
	{
        private static int _nextUserId = 1;

        public string UserId { get; private set; } = string.Empty;
        public string Name { get; private set; } = string.Empty;
        public string Password { get; private set; } = string.Empty;
		public string Email { get; private set; } = string.Empty;
        public string PhoneNumber { get; private set; } = string.Empty;
        public string Address { get; private set; } = string.Empty;

        private List<Account> accounts;

        //public User(int userId, string Name, string email, string phoneNumber, string password)
        //{
        //          //userId = _nextUserId++;
        //          this.userId = _nextUserId++;
        //          this.Name = Name;
        //	this.password = password;
        //          this.email = email;
        //          this.phoneNumber = phoneNumber;
        //          accounts = new List<Account>();


        //      }

        // The new constructor has the following implementation:
        // When we log in, the accountId that are used for login are used to instanciate a class User
        // Which will then get all the data from the database such as name, phonenumber etc.
        public User(string accountId)
        {
            string ConString = OurSqlConnectionString.ConString;
            this.UserId = accountId;
            this.Email = "test@hotmail.com";
            this.accounts = new List<Account>();
            
            try
            {
                using (SqlConnection Con = new SqlConnection(ConString))
                {
                    string query = "SELECT Name, PhoneNumber, Address, Password FROM Account WHERE Id = @AccountId";
                    using (SqlCommand command = new SqlCommand(query, Con))
                    {
                        command.Parameters.AddWithValue("@AccountId", accountId);
                        Con.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Extract values from the SqlDataReader and store in variables
                                this.Name = reader["Name"].ToString();
                                this.PhoneNumber = reader["Phonenumber"].ToString();
                                this.Address = reader["Address"].ToString();
                                this.Password = reader["Password"].ToString();

                            }
                           
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Some exception window maybe?
            }
        }
        public void AddAccount(string accountType)
            {
                Account newAccount = AccountFactory.CreateAccount(accountType);
                accounts.Add(newAccount);
            }

        }
}