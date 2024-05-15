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
        public Products Products { get; private set; }

        private readonly IDataBaseService _dataBaseService;

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

            var productDatas = _dataBaseService.GetAllProducts();
            Products = new Products(productDatas);
        }

        public void PurchaseByBuyer((int id, int count)[] cart)
        {
            foreach (var product in cart)
            {
                PurchaseByBuyer(product.id, product.count);
            }

            MarketData.buyerCount.Value++;
            
            UpdateStoreData();
        }

        private void PurchaseByBuyer(int id, int count)
        {
            var productData = _dataBaseService.GetProductById(id);
            var isProductToStock = _dataBaseService.CheckProductToStockById(id);

            if (isProductToStock == false)
            {
                var isProductToPurchase = _dataBaseService.CheckProductToPurchaseById(id);

                if (isProductToPurchase)
                {
                    _dataBaseService.UpdateProductToPurchaseById(id, count);
                }
                else
                {
                    _dataBaseService.AddProductToPurchaseById(id, count);
                }

                return;
            }

            var countAvailable = _dataBaseService.GetProductToStockById(id).Quantity;

            if (count > countAvailable)
            {
                var purchaseQuantity = count - countAvailable;
                count = countAvailable;

                var isProductToPurchase = _dataBaseService.CheckProductToPurchaseById(id);

                if (isProductToPurchase)
                {
                    _dataBaseService.UpdateProductToPurchaseById(id, purchaseQuantity);
                }
                else
                {
                    _dataBaseService.AddProductToPurchaseById(id, purchaseQuantity);
                }
            }

            var price = productData.SellingPrice * count;

            MarketData.productCount.Value += count;
            MarketData.balance.Value += price;
            MarketData.earned.Value += price;
        }

        private void UpdateStoreData()
        {
            _dataBaseService.UpdatedStoreData(1, MarketData.balance.Value, MarketData.earned.Value,
                MarketData.spent.Value, MarketData.productCount.Value, MarketData.buyerCount.Value);
        }
    }
}