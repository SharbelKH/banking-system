
using BankApplication.model;
using System.Data;
using Moq;
using Xunit;
using Microsoft.EntityFrameworkCore;
using BankApplication.Controller;
using BankApplication.model;


namespace BankApplicationTest
{
    [TestFixture]
    public class DatabaseTests
    {
       private DbContextOptions<BankDbContext> CreateInMemoryOptions()
       {
            return new DbContextOptionsBuilder<BankDbContext>()
                .UseInMemoryDatabase("TestDB") // use a unique name for the in-memory database
                .Options;
       }

       [Test]
       public void AddUser_ShouldAddNewUser()
        {
            var options = CreateInMemoryOptions();

            using (var context = new BankDbContext(options))
            {
                var repository = new UserRepository(context);
                repository.AddUser("numbertest", "password123");

                var user = context.Users.FirstOrDefault(u => u.phonenumber == "numbertest");

                Xunit.Assert.NotNull(user);
                Xunit.Assert.Equal("numbertest", user.phonenumber);
            }
        }

        [Test]
        public void AuthenticateUser_ShouldReturnTrueForValidCredentials()
        {
            var options = CreateInMemoryOptions();

            using (var context = new BankDbContext(options))
            {
                var repository = new UserRepository(context);
                repository.AddUser("numbertest", "password123");

                var isAutenticated = repository.AuthenticateUser1("numbertest", "password123");

                Xunit.Assert.True(isAutenticated);
            }
        }

        [Test]
        public void AuthenticateUser_ShouldReturnFalseForValidCredentials()
        {
            var options = CreateInMemoryOptions();

            using (var context = new BankDbContext(options))
            {
                var repository = new UserRepository(context);
                repository.AddUser("numbertest", "password123");

                var isAutenticated = repository.AuthenticateUser1("numbertest", "wrongpassword");

                Xunit.Assert.False(isAutenticated);
            }
        }
    }

    [TestFixture]
    public class AccountTests
    {
        [Test]
        public void Account_Deposit_AddsToBalance()
        {
            // Arrange
            Account account = new Account("Savings");

            // Act
            bool result = account.Deposit(100);
            bool balance = 100 == (int)account.balance;

            // Assert
            NUnit.Framework.Assert.That(result, "You should be able to Deposit 100 to Savings account!");
            NUnit.Framework.Assert.That(balance, "Your balance is not 100 which it should be!");
        }

        [Test]
        public void Account_Withdraw_Successful()
        {
            // Arrange
            Account account = new Account("Checking");
            account.Deposit(200);

            // Act 
            bool result = account.Withdraw(100);
            bool balance = 100 == (int)account.balance;


            // Assert
            NUnit.Framework.Assert.That(result, "You should be able to Withdraw 100 from 200");
            NUnit.Framework.Assert.That(balance, $"Your balance is {account.balance}, it should be 100!");
        }

        [Test]
        public void Account_Withdraw_InsufficientFunds()
        {
            // Arrange
            Account account = new Account("Retirement");
            account.Deposit(50);

            // Act, result will be false since we cant withdraw 100 from 50
            bool result = !account.Withdraw(100);
            bool balance = 50 == (int)account.balance;

            // Assert
            NUnit.Framework.Assert.That(result, "You should not be able to Withdraw 100 from your balance: 50");
            NUnit.Framework.Assert.That(balance, $"Your balance is {account.balance}, it should be 50");
        }

        [Test]
        public void SavingsAccount_Constructor_SetsAccountType()
        {
            // Arrange & Act
            Account savingsAccount = AccountFactory.CreateAccount("Savings");

            bool accountType = "Savings" == savingsAccount.accountType;

            // Assert
            NUnit.Framework.Assert.That(accountType, $"Your accountType = {savingsAccount.accountType}, it should be 'Savings'!");
        }

        [Test]
        public void CheckingAccount_Constructor_SetsAccountType()
        {
            // Arrange & Act
            Account checkingAccount = AccountFactory.CreateAccount("Checking");

            bool accountType = "Checking" == checkingAccount.accountType;

            // Assert
            NUnit.Framework.Assert.That(accountType, $"Your accountType = {checkingAccount.accountType}, it should be 'Checking'!");
        }

        [Test]
        public void RetirementAccount_Constructor_SetsAccountType()
        {
            // Arrange & Act
            Account retirementAccount = AccountFactory.CreateAccount("Retirement"); ;
            bool accountType = "Retirement" == retirementAccount.accountType;

            // Assert
            NUnit.Framework.Assert.That(accountType, $"Your accountType = {retirementAccount.accountType}, it should be 'Retirement'!");
        }
    }
}