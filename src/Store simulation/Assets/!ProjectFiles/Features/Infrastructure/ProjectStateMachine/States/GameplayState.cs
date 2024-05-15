using Data.Scene;
using Extension.FinalStateMachine;
using Infrastructure.Services.AssetsAddressables;
using Infrastructure.Services.DataBase;
using Infrastructure.Services.Factory.NpcFactory;
using Infrastructure.Services.Market;
using Infrastructure.Services.Windows;
using UI.Market.Scripts;
using UnityEngine.AddressableAssets;
using UniRx;
using UnityEngine;

namespace Infrastructure.ProjectStateMachine.States
{
    public class GameplayState : IState<GameBootstrap>, IEnterable, IExitable
    {
        public GameplayState(GameBootstrap initializer, IWindowService windowService, IDataBaseService dataBaseService,
            INpcFactory npcFactory, IMarketService marketService, GameplaySceneData gameplaySceneData)
        {
            Initializer = initializer;
            _windowService = windowService;
            _dataBaseService = dataBaseService;
            _npcFactory = npcFactory;
            _marketService = marketService;
            _gameplaySceneData = gameplaySceneData;
        }

        public GameBootstrap Initializer { get; }
        private readonly IWindowService _windowService;
        private readonly IDataBaseService _dataBaseService;
        private readonly INpcFactory _npcFactory;
        private readonly IMarketService _marketService;
        private readonly GameplaySceneData _gameplaySceneData;
        private readonly CompositeDisposable _disposable = new();

        public void OnEnter()
        {
            var asyncOperation = Addressables.LoadSceneAsync(AssetsAddressableConstants.GAMEPLAY_SCENE);
            asyncOperation.ToObservable().Subscribe(_ => SimulationInitialization()).AddTo(_disposable);
        }

        public void OnExit()
        {
            _windowService.Open(WindowID.Loading);
            _windowService.Close(WindowID.Gameplay);
        }

        private void SimulationInitialization()
        {
            OpenGameplayWindow();
            InitializeMarketUI();

            Observable.Timer(System.TimeSpan.FromSeconds(0), System.TimeSpan.FromSeconds(20))
                .Subscribe(_ => _npcFactory.Spawn(_gameplaySceneData)).AddTo(_disposable);
            
            Observable.Timer(System.TimeSpan.FromSeconds(1000), System.TimeSpan.FromSeconds(1000))
                .Subscribe(_ => _marketService.OrderProducts()).AddTo(_disposable);
        }

        private void InitializeMarketUI()
        {
            MarketUI.Instance.Balance.text = _marketService.MarketData.balance.Value.ToString();
            MarketUI.Instance.Earned.text = _marketService.MarketData.earned.Value.ToString();
            MarketUI.Instance.Spent.text = _marketService.MarketData.spent.Value.ToString();
            MarketUI.Instance.ProductCount.text = _marketService.MarketData.productCount.Value.ToString();
            MarketUI.Instance.BuyerCount.text = _marketService.MarketData.buyerCount.Value.ToString();

            _marketService.MarketData.balance.Subscribe(value => MarketUI.Instance.Balance.text = $"{value}")
                .AddTo(_disposable);
            _marketService.MarketData.earned.Subscribe(value => MarketUI.Instance.Earned.text = $"{value}")
                .AddTo(_disposable);
            _marketService.MarketData.spent.Subscribe(value => MarketUI.Instance.Spent.text = $"{value}")
                .AddTo(_disposable);
            _marketService.MarketData.productCount.Subscribe(value => MarketUI.Instance.ProductCount.text = $"{value}")
                .AddTo(_disposable);
            _marketService.MarketData.buyerCount.Subscribe(value => MarketUI.Instance.BuyerCount.text = $"{value}")
                .AddTo(_disposable);
        }

        private void OpenGameplayWindow()
        {
            _windowService.Open(WindowID.Gameplay);
            _windowService.Close(WindowID.Loading);
        }
    }
}