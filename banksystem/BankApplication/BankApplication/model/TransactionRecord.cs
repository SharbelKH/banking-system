using Microsoft.Identity.Client.NativeInterop;
using System;
namespace BankApplication.model
{
	public class TransactionRecord
	{
        public int UserId { get; }
        public string TransactionType { get; }
        public string Amount { get; }
        public DateTime Timestamp { get; }


        public TransactionRecord(int userId, string amount, string transactionType, DateTime datetime)
        {
            UserId = userId;
            Amount = amount;
            TransactionType = transactionType;
            Timestamp = datetime;

        }

        public string Insert_Transaction_Into_TransactionDb_String()
        {
            string formattedDate = Timestamp.ToString("yyyy-MM-dd HH:mm:ss");
            string insertQueryintoTransaction = $"INSERT INTO Transactions (UserId, TransactionType, Amount, TimeStamp) VALUES ('{UserId}', '{TransactionType}', '{Amount}', '{formattedDate}')";

            return insertQueryintoTransaction;
        }
    }
}

