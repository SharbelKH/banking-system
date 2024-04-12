using System;
namespace BankApplication.logic
{
	public class User
	{
		public int userId { get; }
		public string firstName { get; }
		public string lastName { get; }
		private string password;
		public string email { get; }
		public string phoneNumber { }
		private List<Account> accounts;
		

		public User(int userId, string firstName, string lastName, string email, string phoneNumber, srting password)
		{
            userId = _nextUserId++;
            firstName = firstName;
            lastName = lastName;
			password = password;
            email = email;
            phoneNumber = phoneNumber;
            accounts = new List<Account>();
        }


        public void AddAccount(string accountType)
        {
            Account newAccount = Account.CreateAccount(accountType);
            accounts.Add(newAccount);
        }

    }
}

