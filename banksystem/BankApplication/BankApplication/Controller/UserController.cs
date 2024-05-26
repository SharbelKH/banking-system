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
using System.Collections.ObjectModel;

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

        // Fetch user from the database and return the datatype User
        public User getUser(string phoneNumber)
        {
            // Query the database to get user information based on the phoneNumber from table Account
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

                // Query the table Transactions to fetch the users Transactions
                ObservableCollection<TransactionRecord> transactionRecords = new ObservableCollection<TransactionRecord>();

                string transactionQuery = $"SELECT * FROM Transactions WHERE UserId = '{id}'";
                DataTable transactionData = db.ExecuteQuery(transactionQuery);
                foreach (DataRow transactionrow in transactionData.Rows)
                {
                    // Get values from each row
                    int userId = Convert.ToInt32(transactionrow["UserId"]);
                    string transactionType = transactionrow["TransactionType"].ToString() ?? throw new Exception("transactionType column value is null");
                    string _amount = transactionrow["Amount"].ToString() ?? throw new Exception("Amount column value is null");
                    DateTime Timestamp = Convert.ToDateTime(transactionrow["Timestamp"]);

                    // Instanciate a TransactionRecord and add it to the userclass
                    TransactionRecord record = new TransactionRecord(userId,_amount, transactionType, Timestamp);
                    transactionRecords.Add(record);
                }

                // Create and return a new User object

                return new User(id, name, phoneNumber, address, password, balance, transactionRecords, dateOfBirth);

            }
            else
            {
                // User not found
                throw new Exception("User Not found!");
            }
        }
        public bool DepositFunds(string amount, string phoneNumber)
        {
            // Check if amount is positive
            if (int.Parse(amount) <= 0)
            {
                throw new negativeValueTransaction();
            }
            // If input is not valid
            else if (!int.TryParse(amount, out int res))
            {
                throw new Exception("Deposit not sucessfull!");
            }
            if (int.Parse(amount) > 10000)
            {
                throw new Exception("Cannot deposit more than 10 000kr!");

            }


            string query = $"UPDATE Account SET Balance = Balance + {amount} WHERE PhoneNumber = {phoneNumber}";
            int rowsAffected = db.ExecuteNonQuery(query);

            if (rowsAffected > 0)
            {
                // Deposit successful therefore update the class ammount aswell
                ApplicationUser.LoggedInUser.Deposit(int.Parse(amount));
                // Insert the transaction into the Transfer database
                TransactionRecord transaction = new TransactionRecord(ApplicationUser.LoggedInUser.Id, amount, "Deposit",DateTime.Now);
                // Update the users transactions
                ApplicationUser.LoggedInUser.addTransaction(transaction);
                db.ExecuteNonQuery(transaction.Insert_Transaction_Into_TransactionDb_String());
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
            // Check if amount is positive
            if (int.Parse(amount) <= 0)
            {
                throw new negativeValueTransaction();
            }
            // If input is not valid
            else if (!int.TryParse(amount, out int res) )
            {
                throw new Exception("Withdraw failed!");
            }
            if (int.Parse(amount) > ApplicationUser.LoggedInUser.Balance)
            {
                throw new insufficientFunds();

            }

            // Updating the Account Table with Balance
            string query = $"UPDATE Account SET Balance = Balance - {amount} WHERE Id = {ApplicationUser.LoggedInUser.Id}";
            int rowsAffected = db.ExecuteNonQuery(query);

            if (rowsAffected > 0)
            {
                // Withdraw successful therefore update the class ammount aswell
                ApplicationUser.LoggedInUser.Withdraw(int.Parse(amount));
                // Instanciate a new transaction and add it to the class user
                TransactionRecord transaction = new TransactionRecord(ApplicationUser.LoggedInUser.Id,amount,"Withdraw", DateTime.Now);
                ApplicationUser.LoggedInUser.addTransaction(transaction);
                // Update the databasetable 'Transaction' with the new transaction
                db.ExecuteNonQuery(transaction.Insert_Transaction_Into_TransactionDb_String());
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

        // Makes a Transfer, updates both the database and the LoggedInUser
        public bool TransferFunds(string toAccountNumber, string amount)
        {
            // Check if amount is positive
            if (int.Parse(amount) <= 0)
            {
                throw new negativeValueTransaction();
            }

            // If the recieving acount is not in database
            else if (!IsInDatabase(toAccountNumber))
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
                // Get the receiving user from the database:
                User receivingUser = getUser(toAccountNumber);

                // Updates the class Balance for the loggedInUser
                ApplicationUser.LoggedInUser.Withdraw(int.Parse(amount));

                // Depositquery to database
                string depositQuery = $"UPDATE Account SET Balance = Balance + {amount} WHERE PhoneNumber = {toAccountNumber}";
                db.ExecuteNonQuery(depositQuery);


                // Withdrawquery
                string Withdrawquery = $"UPDATE Account SET Balance = Balance - {amount} WHERE Id = {ApplicationUser.LoggedInUser.Id}";
                db.ExecuteNonQuery(Withdrawquery);

                // Insert the transaction into the Transaction database with 'Transfer'
                TransactionRecord transferTransaction = new TransactionRecord(ApplicationUser.LoggedInUser.Id, amount, "Transfer", DateTime.Now);
                ApplicationUser.LoggedInUser.addTransaction(transferTransaction);

                // Insert the transaction into the Transaction database with 'Received'
                TransactionRecord receivedTransaction = new TransactionRecord(receivingUser.Id, amount, "Received", DateTime.Now);

                // Insert the Transfer from accounts into table 'Transfers'
                Transfer transfer = new Transfer(ApplicationUser.LoggedInUser.Id, receivingUser.Id, amount, DateTime.Now);

                // Transaction Table
                db.ExecuteNonQuery(transferTransaction.Insert_Transaction_Into_TransactionDb_String());
                db.ExecuteNonQuery(receivedTransaction.Insert_Transaction_Into_TransactionDb_String());

                // Transfer Table
                db.ExecuteNonQuery(transfer.Insert_Transaction_Into_TransfersDb_String());
                
                //WithdrawFunds(amount);
                //DepositFunds(amount, toAccountNumber);
                return true;
            }
                
        }
    }
}
