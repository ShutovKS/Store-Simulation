using Data.ForBD;

namespace Infrastructure.Services.DataBase
{
    public class ReadyData : IDataBaseService
    {
        private Buyer[] _buyers =
        {
            new()
            {
                id = 0,
                name = "Покупатель 0",
                balance = 100,
                desiredProducts = new int[]
                {
                }
            },
            new()
            {
                id = 1,
                name = "Покупатель 0",
                balance = 57,
                desiredProducts = new int[]
                {
                }
            },
        };

        private Employee[] _employees =
        {
            new()
            {
                id = 0,
                name = "Сотрудник",
                jobTitle = "Продавец",
                salary = 100,
                movementSpeed = 10,
                serviceSpeed = 10
            }
        };

        private Product[] _products =
        {
            new()
            {
                id = 0,
                name = "Product 0",
                description = null,
                price = 5,
                productCategoryId = 0
            },
            new()
            {
                id = 1,
                name = "Product 1",
                description = null,
                price = 5,
                productCategoryId = 0
            },
            new()
            {
                id = 2,
                name = "Product 2",
                description = null,
                price = 9,
                productCategoryId = 1
            },
            new()
            {
                id = 3,
                name = "Product 3",
                description = null,
                price = 7,
                productCategoryId = 1
            },
        };

        private ProductCategory[] _productCategories =
        {
            new()
            {
                id = 0,
                name = "ProductCategory 0"
            },
            new()
            {
                id = 0,
                name = "ProductCategory 1"
            },
        };

        private Shop _shop = new()
        {
            id = 0,
            productListsInStock = new int[]
            {
                0, 1, 2
            },
            productListsInPlans = new int[]
            {
            },
            balance = 1000,
            employeesId = new int[]
            {
            }
        };

        private ProductList[] _productLists =
        {
            new()
            {
                id = 0,
                productId = 0,
                count = 5
            },
            new()
            {
                id = 1,
                productId = 1,
                count = 1
            },
            new()
            {
                id = 2,
                productId = 2,
                count = 8
            },
        };

        private Transaction[] _transactions;

        public Shop GetShopInfo() => _shop;
    }
}