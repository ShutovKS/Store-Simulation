using Extension.FinalStateMachine;
using Infrastructure.Services.AssetsAddressables;
using Infrastructure.Services.DataBase;
using Infrastructure.Services.Market;

namespace Infrastructure.ProjectStateMachine.States
{
    public class InitializationState : IState<GameBootstrap>, IEnterable
    {
        public InitializationState(GameBootstrap initializer, IAssetsAddressablesProvider assetsAddressablesProvider,
            IDataBaseService dataBaseService, IMarketService marketService)
        {
            _assetsAddressablesProvider = assetsAddressablesProvider;
            _dataBaseService = dataBaseService;
            _marketService = marketService;
            Initializer = initializer;
        }

        private readonly IAssetsAddressablesProvider _assetsAddressablesProvider;
        private readonly IDataBaseService _dataBaseService;
        private readonly IMarketService _marketService;
        public GameBootstrap Initializer { get; }

        public void OnEnter()
        {
            InitializeGame();

            ChangeStateToLoading();
        }

        private void InitializeGame()
        {
            _assetsAddressablesProvider.Initialize();
            _dataBaseService.ConnectToDataBase();
            _marketService.InitializeData();
        }

        private void ChangeStateToLoading()
        {
            Initializer.StateMachine.SwitchState<ResourcesLoadingState>();
        }
    }
}