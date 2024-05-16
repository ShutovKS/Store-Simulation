namespace Infrastructure.Services.DataBase
{
    public interface IDataBaseService
    {
        void ConnectToDataBase();

        CategoryData[] GetAllCategories();

        CategoryProductData[] GetAllCategoryProducts();

        EmployeeData[] GetAllEmployees();
        EmployeeData GetEmployeeByStoreId(int storeId);

        ProductData[] GetAllProducts();
        ProductData GetProductById(int productId);

        ProductCountData[] GetAllProductToStock();
        ProductCountData GetProductToStockById(int id);
        void UpdateProductToStockById(int id, int quantity);
        bool CheckProductToStockById(int id);
        void AddProductToStockById(int id, int quantity);
        void RemoveProductToStockById(int id);

        ProductCountData[] GetAllProductsToPurchase();
        ProductCountData GetProductToPurchaseById(int id);
        void UpdateProductToPurchaseById(int id, int quantity);
        bool CheckProductToPurchaseById(int id);
        void AddProductToPurchaseById(int id, int quantity);
        void RemoveProductToPurchaseById(int id);

        StoreData[] GetAllStores();
        StoreData GetStoreData(int storeId);
        void UpdatedStoreData(int storeId, int balance, int totalEarnings, int totalExpenses, int totalProductsSold,
            int totalCustomers);

        TransactionData[] GetAllTransactions();
        TransactionData GetTransactionById(int id);
        void AddTransaction(int storeId, TransactionType type, int totalPrice);
    }
}