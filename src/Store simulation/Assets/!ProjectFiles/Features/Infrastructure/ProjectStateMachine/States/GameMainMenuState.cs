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

            mainMenuScreen.RunSimulationButton.OnClickAsObservable().Subscribe(_ =>
                Initializer.StateMachine.SwitchState<GameplayState>()).AddTo(_disposable);
            mainMenuScreen.ExitButton.OnClickAsObservable().Subscribe(_ => Quit()).AddTo(_disposable);
            // mainMenuScreen.StatisticsButton.OnClickAsObservable().Subscribe(_ =>
                // Initializer.StateMachine.SwitchState<StatisticsState>()).AddTo(_disposable);

            _windowService.Close(WindowID.Loading);
        }

        public void OnExit()
        {
            _disposable.Clear();

            _windowService.Close(WindowID.MainMenu);
            _windowService.Open(WindowID.Loading);
        }

        private static void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBGL
            // Link to the website page where you want to go after clicking the exit button
            const string WEBPLAYER_QUIT_URL = "https://github.com/ShutovKS";
            UnityEngine.Application.OpenURL(WEBPLAYER_QUIT_URL);
#else
            UnityEngine.Application.Quit();
#endif
        }
    }
}