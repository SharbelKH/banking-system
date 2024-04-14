using System;
namespace BankApplication.logic
{
    public class Account
	{
		// A uniqe accountId should be given and saved to the database 
		public int accountId { get; private set; }
        public string accountType { get; private set; }
        public double balance { get; private set; }
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
                return true;
            }
            else
            {
                return false;
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




        

    }
}

