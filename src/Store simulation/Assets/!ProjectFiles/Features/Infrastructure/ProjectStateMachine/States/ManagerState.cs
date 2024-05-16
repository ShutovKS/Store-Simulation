using Cysharp.Threading.Tasks;
using Extension.FinalStateMachine;
using Infrastructure.Services.AssetsAddressables;
using Infrastructure.Services.DataBase;
using Infrastructure.Services.Windows;
using UI.Manager;
using UniRx;
using UnityEngine.AddressableAssets;

namespace Infrastructure.ProjectStateMachine.States
{
    public class ManagerState : IState<GameBootstrap>, IEnterable, IExitable
    {
        public ManagerState(GameBootstrap initializer, IWindowService windowService, IDataBaseService dataBaseService)
        {
            _windowService = windowService;
            _dataBaseService = dataBaseService;
            Initializer = initializer;
        }

        public GameBootstrap Initializer { get; }
        private readonly IWindowService _windowService;
        private readonly IDataBaseService _dataBaseService;
        private readonly CompositeDisposable _disposable = new();
        private ManagerUI _managerUI;

        public void OnEnter()
        {
            var asyncOperation = Addressables.LoadSceneAsync(AssetsAddressableConstants.EMPTY_2D_SCENE);
            asyncOperation.ToObservable().Subscribe(_ => OpenManagerWindow()).AddTo(_disposable);
        }

        public void OnExit()
        {
            _windowService.Close(WindowID.Manager);
            _windowService.Open(WindowID.Loading);

            _disposable.Clear();
        }

        private async void OpenManagerWindow()
        {
            _managerUI = await _windowService.OpenAndGetComponent<ManagerUI>(WindowID.Manager);

            _managerUI.BackButton.OnClickAsObservable()
                .Subscribe(_ => Initializer.StateMachine.SwitchState<GameMainMenuState>())
                .AddTo(_disposable);

            _windowService.Close(WindowID.Loading);
        }
    }
}