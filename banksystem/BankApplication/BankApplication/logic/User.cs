using System;
namespace BankApplication.logic
{
   
    public class User
	{
        private static int _nextUserId = 1;

        public int userId { get; private set; }
		public string firstName { get; private set; }
		public string lastName { get; private set; }
		private string password;
		public string email { get; private set; }
		public string phoneNumber { get; private set; }
		private List<Account> accounts;
		

		public User(int userId, string firstName, string lastName, string email, string phoneNumber, string password)
		{
            //userId = _nextUserId++;
            this.userId = _nextUserId++;
            this.firstName = firstName;
            this.lastName = lastName;
			this.password = password;
            this.email = email;
            this.phoneNumber = phoneNumber;
            accounts = new List<Account>();
        }


        public void AddAccount(string accountType)
        {
            Account newAccount = AccountFactory.CreateAccount(accountType);
            accounts.Add(newAccount);
        }

    }
}

