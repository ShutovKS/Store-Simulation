using System;

namespace Data
{
    [Serializable]
    public class ProductCategory
    {
        public int id;
        public string name;
    }

    [Serializable]
    public class Buyer
    {
        public int id;
        public string name;
        public int balance;
        public int[] desiredProducts;
    }

    [Serializable]
    public class Product
    {
        public int id;
        public string name;
        public string description;
        public int price;
        public int productCategoryId;
    }

    [Serializable]
    public class Employee

    {
        public int id;
        public string name;
        public string jobTitle;
        public int salary;
        public float movementSpeed;
        public float serviceSpeed;
    }

    [Serializable]
    public class Transaction
    {
        public int id;
        public int buyerId;
        public int[] productId;
        public int sum;
        public DateTime dateTime;
    }

    [Serializable]
    public class Shop
    {
        public int id;
        public int[] productsInStock;
        public int[] productsInPlans;
        public int balance;
        public int[] employeesId;
    }
}