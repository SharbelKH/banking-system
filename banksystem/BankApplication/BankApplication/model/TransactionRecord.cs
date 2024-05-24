using Microsoft.Identity.Client.NativeInterop;
using System;
using System.ComponentModel;

namespace BankApplication.model
{
    // This class will store data about a specific Transaction, storing data about which user, amount,time and type of transaction
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

