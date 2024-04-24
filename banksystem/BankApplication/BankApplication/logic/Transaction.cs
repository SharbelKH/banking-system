using System;
using BankApplication.myExceptions;
namespace BankApplication.logic
{
	public class Transaction
	{
		public Transaction()
		{

		}

        public bool Transfer(string fromAccountNumber, string toAccountNumber, double amount)
        {
            // Retrieve the source and destination accounts based on the provided account numbers
            User fromAccount = GetAccountByAccountNumber(fromAccountNumber);
            User toAccount = GetAccountByAccountNumber(toAccountNumber);

            // Check if both accounts are valid
            if (!dataBaseManipulation.is_in_database(toAccountNumber))
            {
                invalid_source invalid_Source = new invalid_source();
                throw invalid_Source;
            }

            // Check if withdrawal from the source account is successful and if deposit is successful
            if (fromAccount.Withdraw(amount) && toAccount.Deposit(amount))
            {
                // If withdraw and deposit is successful then send update to database
                dataBaseManipulation.deposit(toAccountNumber, amount);
                dataBaseManipulation.withdraw(fromAccountNumber, amount);
                return true;
            }
            else
            {
                insufficientFunds insufficientFunds = new insufficientFunds();
                throw insufficientFunds;
            }
        }


        private User GetAccountByAccountNumber(string accountNumber)
        {
            // Logic to retrieve the account from the database based on the account number
            // This would involve querying the database to find the account with the given account number
            // For now, let's assume you have a method that retrieves the account based on its number
            // For demonstration purposes, let's return null
            User user = new User(accountNumber);


            return user;
        }


    }
}

