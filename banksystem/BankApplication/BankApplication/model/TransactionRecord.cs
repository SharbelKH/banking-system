using System;
namespace BankApplication.model
{
	public class TransactionRecord
	{
        
            public string FromAccountNumber { get; }
            public string ToAccountNumber { get; }
            public double Amount { get; }
            public DateTime Timestamp { get; }

            public TransactionRecord(string fromAccountNumber, string toAccountNumber, double amount, DateTime timestamp)
            {
                FromAccountNumber = fromAccountNumber;
                ToAccountNumber = toAccountNumber;
                Amount = amount;
                Timestamp = timestamp;
            }
        
    }
}

