using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Drawing;
using System.Net;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace BankApplication.model
{
    // User class is storing data about a specific user in the database
    // Data stored will be id,name,pn,balance, transactions, etc
    public class User : INotifyPropertyChanged
    {

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string PhoneNumber { get; private set; }
		public string Address { get; private set; }
        public string Password { get; private set; }
        public int Balance { get; set; }
        public DateTime DateOfBirth { get; private set; }
        //private List<Account> accounts;

        public ObservableCollection<TransactionRecord> _transactions;
        public ObservableCollection<TransactionRecord> Transactions
        {
            get { return _transactions; }
            set
            {
                if (_transactions != value)
                {
                    _transactions = value;
                    OnPropertyChanged(nameof(Transactions));
                }
            }
        }


        //private List<Account> accounts;

        public User(int Id, string name, string phoneNumber, string address, string password, int balance, ObservableCollection<TransactionRecord> transactionList,DateTime dateOfBirth)
        {
            this.Id = Id;
            this.Name = name;
            this.PhoneNumber = phoneNumber;
            this.Address = address;
            this.Password = password;
            this.Balance = balance;
            this.DateOfBirth = dateOfBirth;
            _transactions = transactionList;
        }

        // Deposit is updating the users balance
        public bool Deposit(int amount)
        {
            if (amount <= 10000)
            {
                Balance += amount;
                OnPropertyChanged(nameof(Balance));
                return true;
            }
            else
            {
                return false;
            }
        }

        // Deposit is updating the users balance
        public bool Withdraw(int amount)
        {

            if (amount <= Balance)
            {
                Balance -= amount;
                OnPropertyChanged(nameof(Balance));
                return true;
            }
            else
            {
                return false;
            }
        }

        // Methodcall to add a transaction to the users list of transactions
        public void addTransaction(TransactionRecord transaction)
        {
            _transactions.Add(transaction);
        }

        // Events which triggers on updatedProperties
        public event PropertyChangedEventHandler ?PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public static bool IsUserOldEnough(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > today.AddYears(-age)) age--;
            return age >= 18;
        }


        // Feature to validate the users PhoneNumber
        public bool ValidatePhoneNumber()
        {
            if (PhoneNumber.Length < 5)
                return false;
            else
                return true; 
        }

    }
}