using System;

namespace Data.ForBD
{
    [Serializable]
    public class Buyer
    {
        public int id;
        public string name;
        public int balance;
        public int[] desiredProducts;
    }
}