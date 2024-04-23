using System;
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
            Account fromAccount = GetAccountByAccountNumber(fromAccountNumber);
            Account toAccount = GetAccountByAccountNumber(toAccountNumber);

            // Check if both accounts are valid
            if (fromAccount == null || toAccount == null)
            {
                Console.WriteLine("Invalid source or destination account number.");
                return false;
            }

            // Check if withdrawal from the source account is successful
            if (fromAccount.Withdraw(amount))
            {
                // If withdrawal is successful, deposit the amount into the destination account
                toAccount.Deposit(amount);
                return true;
            }
            else
            {
                Console.WriteLine("Transfer failed: Insufficient funds.");
                return false;
            }
        }


        private Account GetAccountByAccountNumber(string accountNumber)
        {
            // Logic to retrieve the account from the database based on the account number
            // This would involve querying the database to find the account with the given account number
            // For now, let's assume you have a method that retrieves the account based on its number
            // For demonstration purposes, let's return null
            return null;
        }


    }
}

