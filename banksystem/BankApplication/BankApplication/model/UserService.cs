using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.model
{
    public class UserService
    {
        public User CreateUser(int id, string name, string phoneNumber, string address, string password, int balance, DateTime dateOfBirth)
        {
            if (!User.IsUserOldEnough(dateOfBirth))
            {
                throw new InvalidOperationException("User must be 18 years or older to create an account.");
            }
            var transactionRecords = new ObservableCollection<TransactionRecord>();
            return new User(id, name, phoneNumber, address, password, balance, transactionRecords, dateOfBirth);
        }
    }

}
