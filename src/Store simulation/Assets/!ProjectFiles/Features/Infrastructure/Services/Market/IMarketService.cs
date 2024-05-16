namespace Infrastructure.Services.Market
{
    public interface IMarketService
    {
        MarketData MarketData { get; }
        Products Products { get; }

        void InitializeData();
        void PurchaseByBuyer((int id, int count)[] cart);
        void OrderProducts();
    }
}