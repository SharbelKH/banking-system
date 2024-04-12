using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BankApplication.logic.Account;

namespace BankApplication.logic
{
    public static class AccountFactory
    {
        public static Account CreateAccount(string accountType)
        {
            Account newAccount;
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
            return newAccount;


        }
    }
}
