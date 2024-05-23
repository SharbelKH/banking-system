using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.model
{
    public class ApplicationUser
    {
        public static User LoggedInUser { get; set; } = new User(0, "a", "b", "c", "d", 0, new ObservableCollection<TransactionRecord>()); // Initialize with a default User object
    }
}
