using Extension.FinalStateMachine;
using Infrastructure.Services.AssetsAddressables;
using Infrastructure.Services.Windows;
using UI.MainMenuScreen.Scripts;
using UniRx;
using UnityEngine.AddressableAssets;

namespace Infrastructure.ProjectStateMachine.States
{
    public class GameMainMenuState : IState<GameBootstrap>, IEnterable, IExitable
    {
        public GameMainMenuState(GameBootstrap initializer, IWindowService windowService)
        {
            _windowService = windowService;
            Initializer = initializer;
        }

        public GameBootstrap Initializer { get; }
        private readonly IWindowService _windowService;
        private readonly CompositeDisposable _disposable = new();

        public void OnEnter()
        {
            var asyncOperation = Addressables.LoadSceneAsync(AssetsAddressableConstants.EMPTY_2D_SCENE);
            asyncOperation.ToObservable().Subscribe(_ => OpenMainMenuWindow()).AddTo(_disposable);
        }

        private async void OpenMainMenuWindow()
        {
            var mainMenuScreen = await _windowService.OpenAndGetComponent<MainMenuScreen>(WindowID.MainMenu);

            mainMenuScreen.StartGameButton.OnClickAsObservable().Subscribe(_ =>
                Initializer.StateMachine.SwitchState<GameplayState>()).AddTo(_disposable);

            _windowService.Close(WindowID.Loading);
        }

        public void OnExit()
        {
            _disposable.Clear();

            _windowService.Close(WindowID.MainMenu);
            _windowService.Open(WindowID.Loading);
        }
    }
}