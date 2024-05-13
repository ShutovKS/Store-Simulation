namespace Infrastructure.Services.Market
{
    public class MarketService : IMarketService
    {
        public MarketData MarketData { get; } = new();
        private Products _products;

        public void Purchase()
        {
            MarketData.balance.Value += 1;
            MarketData.earned.Value += 1;
            MarketData.productCount.Value++;
            MarketData.buyerCount.Value++;
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
    }
}