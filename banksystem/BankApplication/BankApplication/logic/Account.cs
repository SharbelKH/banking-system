using System;
namespace BankApplication.logic
{


	public class Account
	{
		// A uniqe accountId should be given and saved to the database 
		public int accountId{get; }
        public string accountType { get; }
        public double balance { get; }


        public Account(string accountType)
		{
			this.accountType = accountType;
            // A uniqe accountId should be given and saved to the database 
        }

        public bool Deposit(double amount)
		{
			if (amount <= 10000)
			{
                balance += amount;
            }
			
		}

		public bool Withdraw(double amount)
		{
			if (amount <= balance)
			{
				balance -= amount;
				return true;
			}
			else
			{
				return false;

			}
		}

	}




    public class SavingsAccount : Account
    {
        public SavingsAccount() : base("Savings") { }
    }



    public class CheckingAccount : Account
    {
        public CheckingAccount() : base("Checking") { }
    }



    public class RetirementAccount : Account
    {
        public RetirementAccount() : base("Retirement") { }
    }




    public void CreateAccount(string accountType)
    {
        Account = newAccount;
        switch (accountType.ToLower())

        {
            case "savings":
                newAccount = new SavingsAccount();
                break;

            case "retirement":
                newAccount = new RetirementAccount();
                break;

            case "checking":
                newAccount = new CheckingAccount();
                break;

            default:
                throw new ArgumentException("Invalid account type. ");
        }

        
    }

}

