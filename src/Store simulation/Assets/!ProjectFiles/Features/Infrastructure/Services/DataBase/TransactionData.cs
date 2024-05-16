using System;

namespace Infrastructure.Services.DataBase
{
    public struct TransactionData
    {
        public int Id;
        public int StoreId;
        public DateTime TransactionDateTime;
        public TransactionType Type;
        public int TransactionAmount;
        
    }

    public enum TransactionType
    {
        purchase,
        sale
    }
}