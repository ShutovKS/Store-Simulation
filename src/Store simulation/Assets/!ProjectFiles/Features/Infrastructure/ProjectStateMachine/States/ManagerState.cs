using System;
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
        private Action _onCloseWindow;

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

            _managerUI.CategoryButton.OnClickAsObservable().Subscribe(_ => OpenCategoryWindow()).AddTo(_disposable);
            _managerUI.CategoryProductButton.OnClickAsObservable().Subscribe(_ => OpenCategoryProduct()).AddTo(_disposable);
            _managerUI.EmployeesButton.OnClickAsObservable().Subscribe(_ => OpenEmployees()).AddTo(_disposable);
            _managerUI.ProductsButton.OnClickAsObservable().Subscribe(_ => OpenProducts()).AddTo(_disposable);
            _managerUI.ProductToPurchaseButton.OnClickAsObservable().Subscribe(_ => OpenProductToPurchase()).AddTo(_disposable);
            _managerUI.ProductToStockButton.OnClickAsObservable().Subscribe(_ => OpenProductToStock()).AddTo(_disposable);
            _managerUI.StoreButton.OnClickAsObservable().Subscribe(_ => OpenStore()).AddTo(_disposable);
            _managerUI.TransactionsButton.OnClickAsObservable().Subscribe(_ => OpenTransactions()).AddTo(_disposable);

            _windowService.Close(WindowID.Loading);
        }

        private void OpenCategoryWindow()
        {
            _onCloseWindow?.Invoke();

            var allCategories = _dataBaseService.GetAllCategories();

            _managerUI.CategoryUI.SetActive(true);
            for (var i = 0; i < allCategories.Length; i++)
            {
                var category = allCategories[i];
                _managerUI.CategoryUI.AddItem(i + 1, category.Name);
            }

            _onCloseWindow = () =>
            {
                _managerUI.CategoryUI.Clear();
                _managerUI.CategoryUI.SetActive(false);
                _onCloseWindow = null;
            };
        }

        private void OpenCategory()
        {
            _onCloseWindow?.Invoke();

            _onCloseWindow = () => { _onCloseWindow = null; };
        }

        private void OpenCategoryProduct()
        {
            _onCloseWindow?.Invoke();

            _onCloseWindow = () => { _onCloseWindow = null; };
        }

        private void OpenEmployees()
        {
            _onCloseWindow?.Invoke();

            _onCloseWindow = () => { _onCloseWindow = null; };
        }

        private void OpenProducts()
        {
            _onCloseWindow?.Invoke();

            _onCloseWindow = () => { _onCloseWindow = null; };
        }

        private void OpenProductToPurchase()
        {
            _onCloseWindow?.Invoke();

            _onCloseWindow = () => { _onCloseWindow = null; };
        }

        private void OpenProductToStock()
        {
            _onCloseWindow?.Invoke();

            _onCloseWindow = () => { _onCloseWindow = null; };
        }

        private void OpenStore()
        {
            _onCloseWindow?.Invoke();

            _onCloseWindow = () => { _onCloseWindow = null; };
        }

        private void OpenTransactions()
        {
            _onCloseWindow?.Invoke();

            _onCloseWindow = () => { _onCloseWindow = null; };
        }
    }
}