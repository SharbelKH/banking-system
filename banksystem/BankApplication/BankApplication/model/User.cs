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

        //private List<Account> accounts;

        public User(int Id, string name, string phoneNumber, string address, string password, int balance)
        {
            this.Id = Id;
            this.Name = name;
            this.PhoneNumber = phoneNumber;
            this.Address = address;
            this.Password = password;
            this.Balance = balance;
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

        public bool validatePhoneNumber()
        {
            if (PhoneNumber.Length < 5)
                return false;
            else
                return true; 
        }
    }
}