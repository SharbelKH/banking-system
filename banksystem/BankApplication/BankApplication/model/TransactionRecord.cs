using Microsoft.Identity.Client.NativeInterop;
using System;
namespace BankApplication.model
{
	public class TransactionRecord
	{
        public string FromAccountNumber { get; }
        public string ToAccountNumber { get; }
        public string Amount { get; }
        public DateTime Timestamp { get; }
        public string TransactionType { get; }


        public TransactionRecord(string fromAccountNumber, string toAccountNumber, string amount, string transactionType)
        {
            FromAccountNumber = fromAccountNumber;
            ToAccountNumber = toAccountNumber;
            Amount = amount;
            TransactionType = transactionType;
            Timestamp = DateTime.Now;

        }

        public string Insert_Transaction_Into_TransferDb_String()
        {
            string formattedDate = Timestamp.ToString("yyyy-MM-dd HH:mm:ss");
            string insertQueryintoTransfer = $"INSERT INTO Transfer (FromAccountNumber, ToAccountNumber, Amount, Timestamp, TypeOfTransfer) VALUES ('{FromAccountNumber}', '{ToAccountNumber}', '{Amount}', '{formattedDate}', {TransactionType})";

            return insertQueryintoTransfer;
        }
    }
}

