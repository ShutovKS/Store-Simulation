using Infrastructure.ProjectStateMachine.Core;
using Infrastructure.Services.AssetsAddressables;
using Infrastructure.Services.Windows;
using UnityEngine.AddressableAssets;

namespace Infrastructure.ProjectStateMachine.States
{
    public class GameplayState : IState<GameBootstrap>, IEnterableWithArg<bool>, IExitable
    {
        public GameBootstrap Initializer { get; }
        private readonly IWindowService _windowService;
        private readonly IAssetsAddressablesProvider _assetsAddressablesProvider;

        public GameplayState(GameBootstrap initializer,
            IWindowService windowService,
            IAssetsAddressablesProvider assetsAddressablesProvider)
        {
            Initializer = initializer;
            _windowService = windowService;
            _assetsAddressablesProvider = assetsAddressablesProvider;
        }

        public void OnEnter(bool isTest)
        {
            var sceneForLoad = isTest
                ? AssetsAddressableConstants.EMPTY_2D_SCENE
                : AssetsAddressableConstants.EMPTY_2D_SCENE;

            var asyncOperation = Addressables.LoadSceneAsync(sceneForLoad);

            asyncOperation.Completed += _ => { OpenGameplayWindow(); };
        }

        private async void OpenGameplayWindow()
        {
            await _windowService.Open(WindowID.Gameplay);

            CloseLoadingWindow();
        }

        private void CloseLoadingWindow()
        {
            _windowService.Close(WindowID.Loading);
        }

        public void OnExit()
        {
        }
    }
}