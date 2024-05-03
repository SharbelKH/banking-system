using System;
using Moq;
using NUnit.Framework;
using System.Data.SqlClient;
using BankApplication.logic;
using BankApplication.myExceptions;
using Microsoft.Data.SqlClient;
using NUnit.Framework;
using Microsoft.Identity.Client.NativeInterop;
using Account = BankApplication.logic.Account;

namespace BankApplicationTest
{
    [TestFixture]
    public class TransactionTests
    {
        [Test]
        public void transferTestsShouldReturnTrue()
        {
            // Arrange
            Transaction transaction = new Transaction();

            // Act
            bool transferResult = transaction.Transfer("1000", "1001", 1);

            // Assert
            Assert.That(transferResult, "This Transfer should be allowed!");
        }

        [Test]
        public void transferShouldThrowInvalid_SourceException()
        {
            // Arrange
            Transaction transaction = new Transaction();

            // Act & Assert
            Assert.Throws<invalid_source>(() => transaction.Transfer("1000", "123", 1));

        }

        [Test]
        public void transferShouldThrowinsufficientFundsException()
        {
            // Arrange
            Transaction transaction = new Transaction();
            // Act & Assert
            Assert.Throws<insufficientFunds>(() => transaction.Transfer("1000", "1001", 100000));

        }
    }

    [TestFixture]
    public class UserTests
    {
        [Test]
        public void DepositShouldReturnTrue()
        {
            // Arrange 
            User user = new User("1000");

            // Act
            int balance_before = int.Parse(user.Balance);
            bool result = user.Withdraw(1);
            bool balance = balance_before - 1 == int.Parse(user.Balance);


            // Assert
            Assert.That(result, "You should be able to Withdraw 100 from " + balance_before);
            Assert.That(balance, $"Your balance is {user.Balance}, it should be {balance_before - 1}!");
            }
    }

    [TestFixture]
    public class AccountFactoryTests
    {
        [Test]
        public void CreateAccountTest()
        {
            // Arrange
            Account savingsAccount = AccountFactory.CreateAccount("Savings");
            Account retirementAccount = AccountFactory.CreateAccount("Retirement");
            Account checkingAccount = AccountFactory.CreateAccount("Checking");


            // Act
            bool savingsResult = savingsAccount.accountType == "Savings";
            bool retirementResult = retirementAccount.accountType == "Retirement";
            bool checkingResult = checkingAccount.accountType == "Checking";

            // Assert
            Assert.That(savingsResult, "This should be a 'savings' account!");
            Assert.That(retirementResult, "This should be a 'retirement' account!");
            Assert.That(checkingResult, "This should be a 'checkings' account!");
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
            Assert.That(result, "You should be able to Deposit 100 to Savings account!");
            Assert.That(balance, "Your balance is not 100 which it should be!");
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
            Assert.That(result, "You should be able to Withdraw 100 from 200");
            Assert.That(balance, $"Your balance is {account.balance}, it should be 100!");
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
            Assert.That(result, "You should not be able to Withdraw 100 from your balance: 50");
            Assert.That(balance, $"Your balance is {account.balance}, it should be 50");
        }

        [Test]
        public void SavingsAccount_Constructor_SetsAccountType()
        {
            // Arrange & Act
            Account savingsAccount = AccountFactory.CreateAccount("Savings");

            bool accountType = "Savings" == savingsAccount.accountType;

            // Assert
            Assert.That(accountType, $"Your accountType = {savingsAccount.accountType}, it should be 'Savings'!");
        }

        [Test]
        public void CheckingAccount_Constructor_SetsAccountType()
        {
            // Arrange & Act
            Account checkingAccount = AccountFactory.CreateAccount("Checking");

            bool accountType = "Checking" == checkingAccount.accountType;

            // Assert
            Assert.That(accountType, $"Your accountType = {checkingAccount.accountType}, it should be 'Checking'!");
        }

        [Test]
        public void RetirementAccount_Constructor_SetsAccountType()
        {
            // Arrange & Act
            Account retirementAccount = AccountFactory.CreateAccount("Retirement"); ;
            bool accountType = "Retirement" == retirementAccount.accountType;

            // Assert
            Assert.That(accountType, $"Your accountType = {retirementAccount.accountType}, it should be 'Retirement'!");
        }
    }
}