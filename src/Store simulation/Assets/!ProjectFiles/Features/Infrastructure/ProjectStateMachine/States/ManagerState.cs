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
                .Subscribe(_ => Initializer.StateMachine.SwitchState<GameMainMenuState>()).AddTo(_disposable);

            _managerUI.CategoryButton.OnClickAsObservable().Subscribe(_ => OpenCategory()).AddTo(_disposable);
            _managerUI.CategoryProductButton.OnClickAsObservable().Subscribe(_ => OpenCategoryProduct())
                .AddTo(_disposable);
            _managerUI.EmployeesButton.OnClickAsObservable().Subscribe(_ => OpenEmployees()).AddTo(_disposable);
            _managerUI.ProductsButton.OnClickAsObservable().Subscribe(_ => OpenProducts()).AddTo(_disposable);
            _managerUI.ProductToPurchaseButton.OnClickAsObservable().Subscribe(_ => OpenProductToPurchase())
                .AddTo(_disposable);
            _managerUI.ProductToStockButton.OnClickAsObservable().Subscribe(_ => OpenProductToStock())
                .AddTo(_disposable);
            _managerUI.StoreButton.OnClickAsObservable().Subscribe(_ => OpenStore()).AddTo(_disposable);
            _managerUI.TransactionsButton.OnClickAsObservable().Subscribe(_ => OpenTransactions()).AddTo(_disposable);

            _windowService.Close(WindowID.Loading);
        }

        private void OpenCategory()
        {
            OpenItems(() => _dataBaseService.GetAllCategories(), item => new object[]
                {
                    item.Name
                }
            );
        }

        private void OpenCategoryProduct()
        {
            OpenItems(() => _dataBaseService.GetAllCategoryProducts(), item => new object[]
                {
                    item.CategoryId,
                    item.ProductId
                }
            );
        }

        private void OpenEmployees()
        {
            OpenItems(() => _dataBaseService.GetAllEmployees(), item => new object[]
                {
                    item.Id,
                    item.Name,
                    item.Position,
                    item.Salary,
                    item.HireDate,
                    item.MovingSpeed,
                    item.ServiceSpeed
                }
            );
        }

        private void OpenProducts()
        {
            OpenItems(() => _dataBaseService.GetAllProducts(), item => new object[]
                {
                    item.Id,
                    item.Name,
                    item.Description,
                    item.PurchasePrice,
                    item.SellingPrice,
                    item.Category
                }
            );
        }

        private void OpenProductToPurchase()
        {
            OpenItems(() => _dataBaseService.GetAllProductsToPurchase(), item => new object[]
                {
                    item.ProductId,
                    item.ProductName,
                    item.Quantity
                }
            );
        }

        private void OpenProductToStock()
        {
            OpenItems(() => _dataBaseService.GetAllProductToStock(), item => new object[]
                {
                    item.ProductId,
                    item.ProductName,
                    item.Quantity
                }
            );
        }

        private void OpenStore()
        {
            OpenItems(() => _dataBaseService.GetAllStores(), item => new object[]
                {
                    item.Id,
                    item.Name,
                    item.Address,
                    item.EmployeeId,
                    item.Balance,
                    item.TotalEarnings,
                    item.TotalExpenses,
                    item.TotalProductsSold,
                    item.TotalCustomers,
                }
            );
        }

        private void OpenTransactions()
        {
            OpenItems(() => _dataBaseService.GetAllTransactions(), item => new object[]
                {
                    item.Id,
                    item.StoreId,
                    item.TransactionDateTime,
                    item.Type,
                    item.TransactionAmount
                }
            );
        }

        private void OpenItems<T>(Func<T[]> getData, Func<T, object[]> mapItem)
        {
            CloseWindow();

            _managerUI.TableUI.SetActive(true);

            var items = getData();

            foreach (var arg in items)
            {
                _managerUI.TableUI.AddItem(mapItem(arg));
            }
        }

        private void CloseWindow()
        {
            _managerUI.TableUI.Clear();
            _managerUI.TableUI.SetActive(false);
        }
    }
}