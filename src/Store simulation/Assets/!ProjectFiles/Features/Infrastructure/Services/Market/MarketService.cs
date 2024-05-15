using Infrastructure.Services.DataBase;
using UniRx;

namespace Infrastructure.Services.Market
{
    public class MarketService : IMarketService
    {
        public MarketService(IDataBaseService dataBaseService)
        {
            _dataBaseService = dataBaseService;
        }

        public MarketData MarketData { get; private set; }
        private readonly IDataBaseService _dataBaseService;
        private Products _products;

        public void Purchase()
        {
            MarketData.balance.Value += 1;
            MarketData.earned.Value += 1;
            MarketData.productCount.Value++;
            MarketData.buyerCount.Value++;

            UpdateStoreData();
        }

        public void PurchaseByBuyer(int id, int count)
        {
            if (!_products.CheckProduct(id, out var countAvailable))
            {
                return;
            }

            if (count > countAvailable)
            {
                count = countAvailable;
            }

            var price = 1 * count; // TODO: Получить цену от продукта

            MarketData.balance.Value += price;
            MarketData.earned.Value += price;
            MarketData.productCount.Value += count;
            MarketData.buyerCount.Value++;
        }

        public void InitializeData()
        {
            var storeData = _dataBaseService.GetStoreData(1);
            MarketData = new MarketData
            {
                balance = new IntReactiveProperty(storeData.Balance),
                earned = new IntReactiveProperty(storeData.TotalEarnings),
                spent = new IntReactiveProperty(storeData.TotalExpenses),
                productCount = new IntReactiveProperty(storeData.TotalProductsSold),
                buyerCount = new IntReactiveProperty(storeData.TotalCustomers)
            };
        }

        private void UpdateStoreData()
        {
            _dataBaseService.UpdatedStoreData(1, MarketData.balance.Value, MarketData.earned.Value,
                MarketData.spent.Value, MarketData.productCount.Value, MarketData.buyerCount.Value);
        }
    }
}