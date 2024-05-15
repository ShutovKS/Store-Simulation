namespace Infrastructure.Services.DataBase
{
    public interface IDataBaseService
    {
        void ConnectToDataBase();
        StoreData GetStoreData(int storeId);
        void UpdatedStoreData(int storeId, int balance, int totalEarnings, int totalExpenses, int totalProductsSold, int totalCustomers);
    }
}