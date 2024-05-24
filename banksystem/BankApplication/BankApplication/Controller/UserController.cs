using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.model;
using User = BankApplication.model.User;
using BankApplication.myExceptions;

namespace BankApplication.Controller
{
    public class UserController
    {
        private Database db;

        public UserController(Database db)
        {
            this.db = db;
        }

        public bool AuthenticateUser(string phoneNumber, string password)
        {
            // Query the database to authenticate the user
            string query = $"SELECT COUNT(*) FROM Account WHERE PhoneNumber = '{phoneNumber}' AND Password = '{password}'";
            int count = (int)db.ExecuteScalar(query);

            // If count is greater than 0, the user is authenticated
            return count > 0;
        }

        public bool CreateUser(string fullName, string phoneNumber, string address, string password, DateTime dateOfBirth)
        {
            // Check if the user already exists (optional, depending on your requirements)
            string checkQuery = $"SELECT COUNT(*) FROM Account WHERE PhoneNumber = '{phoneNumber}'";
            int count = (int)db.ExecuteScalar(checkQuery);
            if (count > 0)
            {
                // User with the given phone number already exists
                return false;
            }

            // If the user does not exist, insert the new user into the database
            string insertQuery = $"INSERT INTO Account (Name, PhoneNumber, Address, Password, Balance) " +
                                 $"VALUES ('{fullName}', '{phoneNumber}', '{address}', '{password}', '{0}', '{dateOfBirth.ToString("yyyy-MM-dd")}')";

            int rowsAffected = db.ExecuteNonQuery(insertQuery);

            // Check if the user was successfully inserted
            return rowsAffected > 0;
        }

        public User getUser(string phoneNumber)
        {
            // Query the database to get user information based on the phoneNumber
            string query = $"SELECT * FROM Account WHERE PhoneNumber = '{phoneNumber}'";
            DataTable userData = db.ExecuteQuery(query);

            // Check if user data exists
            if (userData.Rows.Count > 0)
            {
                // Extract user data from the DataTable and create a User object
                DataRow row = userData.Rows[0];
                int id = Convert.ToInt32(row["Id"]);

                string name = row["Name"]?.ToString() ?? throw new Exception("Name column value is null");
                string address = row["Address"]?.ToString() ?? throw new Exception("Address column value is null");
                string password = row["Password"]?.ToString() ?? throw new Exception("Name column value is null");

                int balance = Convert.ToInt32(row["Balance"]);
                DateTime dateOfBirth = row["DateOfBirth"] != DBNull.Value ? Convert.ToDateTime(row["DateOfBirth"]) : throw new Exception("DateOfBirth column value is null");

                // Create and return a new User object
                return new User(id, name, phoneNumber, address, password, balance, dateOfBirth);
            }
            else
            {
                // User not found
                throw new Exception("User Not found!");
            }
        }
        public bool DepositFunds(string amount, string phoneNumber)
        {
            // If input is not valid
            if (!int.TryParse(amount, out int res) || res > 10000)
            {
                throw new Exception("Deposit not sucessfull!");
            }

          
            string query = $"UPDATE Account SET Balance = Balance + {amount} WHERE PhoneNumber = {phoneNumber}";
            int rowsAffected = db.ExecuteNonQuery(query);

            if (rowsAffected > 0)
            {
                // Deposit successful therefore update the class ammount aswell
                //ApplicationUser.LoggedInUser.Balance += int.Parse(amount);
                ApplicationUser.LoggedInUser.Deposit(int.Parse(amount));
                return true;
            }
            else
            {
                // Deposit failed
                return false;
            }
        }

        public bool WithdrawFunds(string amount)
        {
            // If input is not valid
            if (!int.TryParse(amount, out int res))
            {
                throw new Exception("Withdraw failed!");
            }


            string query = $"UPDATE Account SET Balance = Balance - {amount} WHERE Id = {ApplicationUser.LoggedInUser.Id}";
            int rowsAffected = db.ExecuteNonQuery(query);

            if (rowsAffected > 0)
            {
                // Withdraw successful therefore update the class ammount aswell
                ApplicationUser.LoggedInUser.Withdraw(int.Parse(amount));
                return true;
            }
            else
            {
                // Withdraw failed
                return false;
            }
        }
        public bool IsInDatabase(string phoneNumber)
        {
            // Query the database to check if a user with the given phone number exists
            string query = $"SELECT COUNT(*) FROM Account WHERE PhoneNumber = '{phoneNumber}'";
            int count = (int)db.ExecuteScalar(query);

            // If count is greater than 0, user exists in the database; otherwise, user doesn't exist
            return count > 0;
        }

        public bool TransferFunds(string toAccountNumber, string amount)
        {
            // If the recieving acount is not in database
            if (!IsInDatabase(toAccountNumber))
            {
                invalid_source invalid_Source = new invalid_source();
                throw invalid_Source;
            }
            // If the users balance is less than the amount user is sending
            else if (ApplicationUser.LoggedInUser.Balance < int.Parse(amount))
            {
                insufficientFunds insufficientFunds = new insufficientFunds();
                throw insufficientFunds;
            }

            else
            {
                // Updates the class Balance for the loggedInUser
                ApplicationUser.LoggedInUser.Withdraw(int.Parse(amount));
                // Update database
                WithdrawFunds(amount);
                DepositFunds(amount, toAccountNumber);
                return true;
            }
                
        }
    }
}
