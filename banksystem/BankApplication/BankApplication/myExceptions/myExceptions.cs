using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.myExceptions
{
    // Exception that triggers when user sends money to invalid account
    public class invalid_source : Exception
    {
        public invalid_source() : base("Invalid source or destination account number") { }
    }

    // Exception that triggers when user has insufficient funds
    public class insufficientFunds : Exception
    {
        public insufficientFunds() : base("Insufficient funds") { }
    }
    public class negativeValueTransaction : Exception
    {
        public negativeValueTransaction() : base("Cannot make transaction with less than 0kr") { }
    }
}
