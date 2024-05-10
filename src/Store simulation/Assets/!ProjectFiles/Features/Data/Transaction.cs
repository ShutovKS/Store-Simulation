using System;

namespace Data
{
    [Serializable]
    public class Transaction
    {
        public int id;
        public int buyerId;
        public int[] productId;
        public int sum;
        public DateTime dateTime;
    }
}