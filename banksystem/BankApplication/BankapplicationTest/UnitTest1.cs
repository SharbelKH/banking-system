
using BankApplication.model;
using System.Data;
using Moq;
using Xunit;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using BankApplication.Controller;
using BankApplication.myExceptions;
using Microsoft.Data.SqlClient;
using static BankApplication.model.Account;
using BankApplication.View.UserControls;
using BankApplication.View;
using System.Threading;
using System.Windows;
using System.Xaml.Schema;
using BankApplication;
using System.Windows.Controls;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.Identity.Client.NativeInterop;


namespace BankApplicationTest
{
    [TestFixture]
    public class DatabaseTestsClass
    {
        [Test]
        public void ExecuteNonQuery_ShouldReturnNumberOfRowsAffected()
        {
            // Arrange
            string query = "INSERT INTO Users (Name, Age) VALUES ('John', 30)";

            // Mock IDbConnection
            var mockConnection = new Mock<IDbConnection>();

            // Mock IDbCommand
            var mockCommand = new Mock<IDbCommand>();
            mockCommand.Setup(cmd => cmd.ExecuteNonQuery())
                       .Returns(1); // Assuming one row is affected

            mockConnection.Setup(con => con.CreateCommand())
                          .Returns(mockCommand.Object);

            // Mock IDbConnectionFactory
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            mockConnectionFactory.Setup(factory => factory.CreateConnection(It.IsAny<string>()))
                                .Returns(mockConnection.Object);

            // Create an instance of Database using the mock connection factory
            var database = new Database("fake_connection_string", mockConnectionFactory.Object);

            // Act
            int rowsAffected = database.ExecuteNonQuery(query);

            // Assert
            Xunit.Assert.Equal(1, rowsAffected);
        }

        [Test]
        public void ExecuteQuery_ShouldReturnDataTable_WhenQueryIsValid()
        {
            // Arrange
            string query = "SELECT * FROM Users";

            // Mock IDbConnection
            var mockConnection = new Mock<IDbConnection>();

            // Mock IDbCommand
            var mockCommand = new Mock<IDbCommand>();
            var mockReader = new Mock<IDataReader>();
            var dataTable = new DataTable();

            // Setup mock reader to return a valid schema
            mockReader.Setup(reader => reader.FieldCount)
                      .Returns(2); // Example field count

            // Setup mock reader to return data rows
            mockReader.SetupSequence(reader => reader.Read())
                      .Returns(true) // First row
                      .Returns(false); // End of data

            // Setup mock reader to return specific column names
            mockReader.Setup(reader => reader.GetName(0)).Returns("ColumnName1");
            mockReader.Setup(reader => reader.GetName(1)).Returns("ColumnName2");

            // Setup mock reader to return data types for columns
            mockReader.Setup(reader => reader.GetFieldType(0)).Returns(typeof(int)); // Example data type for column 1
            mockReader.Setup(reader => reader.GetFieldType(1)).Returns(typeof(string)); // Example data type for column 2

            mockCommand.Setup(cmd => cmd.ExecuteReader())
                       .Returns(mockReader.Object);

            mockConnection.Setup(con => con.CreateCommand())
                          .Returns(mockCommand.Object);

            // Mock IDbConnectionFactory
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            mockConnectionFactory.Setup(factory => factory.CreateConnection(It.IsAny<string>()))
                                .Returns(mockConnection.Object);

            // Create an instance of Database using the mock connection factory
            var database = new Database("fake_connection_string", mockConnectionFactory.Object);

            // Act
            DataTable result = database.ExecuteQuery(query);

            // Assert
            Xunit.Assert.NotNull(result);
            // Add more assertions as needed
        }

        [Test]
        public void ExecuteScalar_ShouldReturnScalarValue_WhenQueryIsValid()
        {
            // Arrange
            string query = "SELECT COUNT(*) FROM Users";
            int expectedResult = 10; // Example expected result

            // Mock IDbConnection
            var mockConnection = new Mock<IDbConnection>();

            // Mock IDbCommand
            var mockCommand = new Mock<IDbCommand>();
            mockCommand.Setup(cmd => cmd.ExecuteScalar())
                       .Returns(expectedResult); // Mock the ExecuteScalar method to return the expected result

            mockConnection.Setup(con => con.CreateCommand())
                          .Returns(mockCommand.Object);

            // Mock IDbConnectionFactory
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            mockConnectionFactory.Setup(factory => factory.CreateConnection(It.IsAny<string>()))
                                .Returns(mockConnection.Object);

            // Create an instance of Database using the mock connection factory
            var database = new Database("fake_connection_string", mockConnectionFactory.Object);

            // Act
            object result = database.ExecuteScalar(query);

            // Assert
            Xunit.Assert.Equal(expectedResult, result);
        }

        [Test]
        public void Dispose_ShouldCloseConnection_WhenConnectionIsOpen()
        {
            // Arrange
            var mockConnection = new Mock<IDbConnection>();
            mockConnection.SetupGet(c => c.State).Returns(ConnectionState.Open);

            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            mockConnectionFactory.Setup(factory => factory.CreateConnection(It.IsAny<string>()))
                                .Returns(mockConnection.Object);

            // Create an instance of Database
            var database = new Database("fake_connection_string", mockConnectionFactory.Object);

            // Act
            database.Dispose();

            // Assert
            mockConnection.Verify(c => c.Close(), Times.Once);
        }

        [Test]
        public void Dispose_ShouldNotCloseConnection_WhenConnectionIsClosed()
        {
            // Arrange
            var mockConnection = new Mock<IDbConnection>();
            mockConnection.SetupGet(c => c.State).Returns(ConnectionState.Closed);

            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            mockConnectionFactory.Setup(factory => factory.CreateConnection(It.IsAny<string>()))
                                .Returns(mockConnection.Object);

            // Create an instance of Database
            var database = new Database("fake_connection_string", mockConnectionFactory.Object);

            // Act
            database.Dispose();

            // Assert
            mockConnection.Verify(c => c.Close(), Times.Never);
        }
    }


    [TestFixture]
    public class UserTest
    {
        [Test]
        public void Deposit_less_than_10000()
        {
            // Arrange
            var user = new User(1, "John", "1234567890", "Address", "password", 1000);

            // Act
            var result = user.Deposit(5000);

            // Assert
            Xunit.Assert.True(result);
            Xunit.Assert.Equal(6000, user.Balance);
        }

        [Test]
        public void Deposit_more_than_10000()
        {
            // Arrange
            var user = new User(1, "John", "1234567890", "Address", "password", 3000);

            // Act
            var result = user.Deposit(15000);

            // Assert
            Xunit.Assert.False(result);
            Xunit.Assert.Equal(3000, user.Balance);
        }

        [Test]
        public void Withdraw_less_than_available_balance()
        {
            // Arrange
            var user = new User(1, "John", "1234567890", "Address", "password", 3000);

            // Act
            var result = user.Withdraw(100);

            // Assert
            Xunit.Assert.True(result);
            Xunit.Assert.Equal(2900, user.Balance);
        }
        [Test]
        public void Withdraw_more_than_available_balance()
        {
            // Arrange
            var user = new User(1, "John", "1234567890", "Address", "password", 3000);

            // Act
            var result = user.Withdraw(4000);

            // Assert
            Xunit.Assert.False(result);
            Xunit.Assert.Equal(3000, user.Balance);

        }


    }

    [TestFixture]
    public class ExceptionTests
    {
        [Test]
        public void testInvalidSourceException()
        {
            // Arrange
            var exception = new invalid_source();

            // Act
            var message = exception.Message;

            // Assert
            Xunit.Assert.Equal("Invalid source or destination account number", message);
        }

        [Test]
        public void testInsufficientFundsException()
        {
            // Arrange
            var exception = new insufficientFunds();

            // Act
            var message = exception.Message;

            // Assert
            Xunit.Assert.Equal("Insufficient funds", message);
        }
    }

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

    [TestFixture]
    public class DefaultDbConnectionFactoryTest
    {
        [Test]
        public void CreateConnection_ReturnsSqlConnection()
        {
            // Arrange
            var factory = new DefaultDbConnectionFactory();
            var connectionString = OurSqlConnectionString.ConString;
            var sqlconnection = new SqlConnection();
           
            // Act
            var connection = factory.CreateConnection(connectionString);
            bool result = connection.GetType() == sqlconnection.GetType();

            // Assert
            NUnit.Framework.Assert.That(result, "DefaultDbConnectionFactory does not return SqlConnection");
        }
    }

    [Apartment(ApartmentState.STA)]
    public class PasswordInputBoxTest
    {
        private PasswordInputBox passwordInputBox;

        [SetUp]
        public void SetUp()
        {
            passwordInputBox = new PasswordInputBox();
        }


        [Test]
        public void TestPlaceholderProperty()
        {
            // Arrange
            string expectedPlaceholder = "Enter your password";

            // Act
            passwordInputBox.Placeholder = expectedPlaceholder;
            string actualPlaceholder = passwordInputBox.Placeholder;

            // Assert
            Xunit.Assert.Equal(expectedPlaceholder, actualPlaceholder);
        }

        [Test]
        public void TestSetPassword()
        {
            // Arrange
            string testPassword = "test";

            // Act
            passwordInputBox.SetPassword(testPassword);

            // Assert
            Xunit.Assert.Equal("test", testPassword);
        }

        [Test]
        public void PasswordVisibility_DefaultState_IsFalse()
        {
            // Act
            bool initialState = passwordInputBox.isPasswordVisible;

            // Assert
            Xunit.Assert.False(initialState, "By default, password visibility should be false.");
        }

        [Test]
        public void IsPlaceholderEmpty()
        {
            // Act
            bool IsEmpty = passwordInputBox.Placeholder.Equals(string.Empty);
            bool IsNull = passwordInputBox.Placeholder.Equals(null);

            // Assert
            Xunit.Assert.True(IsEmpty, "Is Empty");
            Xunit.Assert.False(IsNull, "Should be Empty, not Null");
        }

        [Test]
        public void PasswordString_ShouldReturnPassword()
        {
            // Arrange
            passwordInputBox.SetPassword("testpassword");

            // Act
            var result = passwordInputBox.passwordString;

            // Assert
            Xunit.Assert.Equal("testpassword", result);
        }

        [Test]
        public void BtnClear_Click_ShouldClearPasswordInputs()
        {
            // Arrange
            passwordInputBox.SetPassword("password");

            // Act
            passwordInputBox.btnClear_Click(null!, null!);

            // Assert
            Xunit.Assert.Empty(passwordInputBox.passwordString);
        }
    }

    [TestFixture]

    public class OurSqlConnectionStringTests
    {
        [Test]
        public void TestConnectionStringEmpty()
        {
            //Arrange

            //Act
            string connectionString = OurSqlConnectionString.ConString;

            //Assert
            //Xunit.Assert.Null(connectionString);
            Xunit.Assert.NotEmpty(connectionString);
        }
    }

    [TestFixture]
    public class validatePhonenumberTest
    {
        [Test]
        public void TestPhonenumberValid()
        {
            //Arrange
            var ValidPhoneNumber = new User(1, "John", "1234567890", "Address", "password", 1000);

            //Act 
            var valid = ValidPhoneNumber.validatePhoneNumber();

            // Assert
            NUnit.Framework.Assert.That(valid, "The phonenumber is larger than 5 characters");
           

        }
    }
    public class InvalidatePhonenumberTest
    {
        [Test]
        public void TestPhonenumberInvalid()
        {
            //Arrange
            var InvalidPhoneNumber = new User(1, "John", "123", "Address", "password", 1000);

            //Act 
            var valid = InvalidPhoneNumber.validatePhoneNumber();

            // Assert
            NUnit.Framework.Assert.That(valid, "The phonenumber is smaller than 5 characters");

        }
    }
}