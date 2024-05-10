using System;

namespace Data
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