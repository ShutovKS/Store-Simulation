using UI.Market.Scripts;
using UniRx;

namespace Market
{
    public class MarketCore
    {
        public MarketCore(MarketUI marketUI, MarketData marketData, CompositeDisposable disposable)
        {
            _marketUI = marketUI;
            _marketData = marketData;
            _disposable = disposable;

            InitializeData();
            // InitializeUI();
        }

        public Products Products { get; private set; }

        private readonly MarketUI _marketUI;
        private readonly MarketData _marketData;
        private readonly CompositeDisposable _disposable;

        private void InitializeUI()
        {
            _marketData.balance.Subscribe(value => _marketUI.Balance.text = $"{value}").AddTo(_disposable);
            _marketData.earned.Subscribe(value => _marketUI.Earned.text = $"{value}").AddTo(_disposable);
            _marketData.spent.Subscribe(value => _marketUI.Spent.text = $"{value}").AddTo(_disposable);
            _marketData.productCount.Subscribe(value => _marketUI.ProductCount.text = $"{value}").AddTo(_disposable);
            _marketData.buyerCount.Subscribe(value => _marketUI.BuyerCount.text = $"{value}").AddTo(_disposable);
        }

        private void InitializeData()
        {
            Products = new Products();
        }
    }
}