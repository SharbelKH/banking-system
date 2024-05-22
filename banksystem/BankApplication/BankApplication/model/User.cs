using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Drawing;
using System.Net;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace BankApplication.model
{
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

        public User(int Id, string name, string phoneNumber, string address, string password, int balance, DateTime dateOfBirth)
        {
            this.Id = Id;
            this.Name = name;
            this.PhoneNumber = phoneNumber;
            this.Address = address;
            this.Password = password;
            this.Balance = balance;
            this.DateOfBirth = dateOfBirth;
        }
       
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

        public event PropertyChangedEventHandler PropertyChanged;
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

    }
}