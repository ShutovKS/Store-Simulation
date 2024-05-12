using Data.Scene;
using Extension.StateMachineCore;
using Infrastructure.Services.AssetsAddressables;
using Infrastructure.Services.DataBase;
using Infrastructure.Services.Factory.NpcFactory;
using Infrastructure.Services.Windows;
using Market;
using UI.Market.Scripts;
using UnityEngine.AddressableAssets;
using UniRx;

namespace Infrastructure.ProjectStateMachine.States
{
    public class GameplayState : IState<GameBootstrap>, IEnterable, IExitable
    {
        public GameplayState(GameBootstrap initializer, IWindowService windowService, IDataBaseService dataBaseService,
            INpcFactory npcFactory, GameplaySceneData gameplaySceneData)
        {
            Initializer = initializer;
            _windowService = windowService;
            _dataBaseService = dataBaseService;
            _npcFactory = npcFactory;
            _gameplaySceneData = gameplaySceneData;
        }

        public GameBootstrap Initializer { get; }
        private readonly IWindowService _windowService;
        private readonly IDataBaseService _dataBaseService;
        private readonly INpcFactory _npcFactory;
        private readonly GameplaySceneData _gameplaySceneData;
        private readonly CompositeDisposable _disposable = new();

        private MarketCore _marketCore;

        public void OnEnter()
        {
            var asyncOperation = Addressables.LoadSceneAsync(AssetsAddressableConstants.GAMEPLAY_SCENE);
            asyncOperation.ToObservable().Subscribe(_ => SimulationInitialization()).AddTo(_disposable);
        }

        private void SimulationInitialization()
        {
            OpenGameplayWindow();

            _marketCore = new MarketCore(MarketUI.Instance, null, _disposable);

            _npcFactory.Spawn(_gameplaySceneData);
        }

        private void OpenGameplayWindow()
        {
            _windowService.Open(WindowID.Gameplay);
            _windowService.Close(WindowID.Loading);
        }

        public void OnExit()
        {
            _windowService.Open(WindowID.Loading);
            _windowService.Close(WindowID.Gameplay);
        }
    }
}