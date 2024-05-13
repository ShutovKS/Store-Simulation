namespace Infrastructure.Services.Market
{
    public interface IMarketService
    {
        MarketData MarketData { get; }

        void Purchase();
        void PurchaseByBuyer(int id, int count);
    }
}