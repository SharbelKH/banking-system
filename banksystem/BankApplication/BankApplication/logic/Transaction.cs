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
            User fromAccount = new User(fromAccountNumber);
            User toAccount = new User(toAccountNumber);

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
    }
}

