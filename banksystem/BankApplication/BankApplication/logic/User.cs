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

			accounts.Add(newAccount);
		}

	}
}

