using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.model
{
    class Transfer
    {
        public int FromUserId { get; }
        public int ToUserId { get; }
        public string Amount { get; }
        public DateTime Timestamp { get; }


        public Transfer(int fromUserId, int toUserId,string amount,DateTime timestamp)
        {
            FromUserId = fromUserId;
            ToUserId = toUserId;
            Amount = amount;
            Timestamp = timestamp;

        }

        public string Insert_Transaction_Into_TransfersDb_String()
        {
            string formattedDate = Timestamp.ToString("yyyy-MM-dd HH:mm:ss");
            string insertQueryintoTransfer = $"INSERT INTO Transfers (FromUserId, ToUserId, Amount, TimeStamp) VALUES ('{FromUserId}', '{ToUserId}', '{Amount}', '{formattedDate}')";

            return insertQueryintoTransfer;
        }
    }
}
