using System;

namespace Data.ForBD
{
    [Serializable]
    public class Shop
    {
        public int id;
        public int[] productListsInStock;
        public int[] productListsInPlans;
        public int balance;
        public int[] employeesId;
    }
}