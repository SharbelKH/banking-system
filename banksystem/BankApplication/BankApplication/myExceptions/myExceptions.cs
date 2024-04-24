using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.myExceptions
{
    public class invalid_source : Exception
    {
        public invalid_source() : base("Invalid source or destination account number") { }
    }
    public class insufficientFunds : Exception
    {
        public insufficientFunds() : base("Insufficient funds") { }
    }
}
