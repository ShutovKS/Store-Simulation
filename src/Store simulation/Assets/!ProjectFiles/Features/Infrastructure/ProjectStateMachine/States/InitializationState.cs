using Infrastructure.ProjectStateMachine.Core;
using Infrastructure.Services.AssetsAddressables;

namespace Infrastructure.ProjectStateMachine.States
{
    public class InitializationState : IState<GameBootstrap>, IEnterable
    {
        public InitializationState(GameBootstrap initializer, IAssetsAddressablesProvider assetsAddressablesProvider)
        {
            _assetsAddressablesProvider = assetsAddressablesProvider;
            Initializer = initializer;
        }

        private readonly IAssetsAddressablesProvider _assetsAddressablesProvider;
        public GameBootstrap Initializer { get; }

        public void OnEnter()
        {
            InitializeGame();

            ChangeStateToLoading();
        }

        private void InitializeGame()
        {
            _assetsAddressablesProvider.Initialize();
        }

        private void ChangeStateToLoading()
        {
            Initializer.StateMachine.SwitchState<ResourcesLoadingState>();
        }
    }
}