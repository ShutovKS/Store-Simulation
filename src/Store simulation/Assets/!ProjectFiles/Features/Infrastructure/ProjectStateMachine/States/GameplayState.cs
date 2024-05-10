using Infrastructure.ProjectStateMachine.Core;
using Infrastructure.Services.AssetsAddressables;
using Infrastructure.Services.Windows;
using UniRx;
using UnityEngine.AddressableAssets;

namespace Infrastructure.ProjectStateMachine.States
{
    public class GameplayState : IState<GameBootstrap>, IEnterable, IExitable
    {
        public GameplayState(GameBootstrap initializer,
            IWindowService windowService,
            IAssetsAddressablesProvider assetsAddressablesProvider)
        {
            Initializer = initializer;
            _windowService = windowService;
            _assetsAddressablesProvider = assetsAddressablesProvider;
        }

        public GameBootstrap Initializer { get; }
        private readonly IWindowService _windowService;
        private readonly IAssetsAddressablesProvider _assetsAddressablesProvider;
        private readonly CompositeDisposable _disposable = new();

        public void OnEnter()
        {
            var asyncOperation = Addressables.LoadSceneAsync(AssetsAddressableConstants.EMPTY_2D_SCENE);
            asyncOperation.ToObservable().Subscribe(_ => OpenGameplayWindow()).AddTo(_disposable);
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