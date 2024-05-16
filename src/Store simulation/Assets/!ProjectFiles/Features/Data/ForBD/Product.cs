using System;

namespace Data.ForBD
{
    [Serializable]
    public class Product
    {
        public int id;
        public string name;
        public string description;
        public int price;
        public int productCategoryId;
    }
}