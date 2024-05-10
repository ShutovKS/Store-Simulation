using Infrastructure.ProjectStateMachine.Core;
using Infrastructure.ProjectStateMachine.States;
using Infrastructure.Services.AssetsAddressables;
using Infrastructure.Services.Windows;

namespace Infrastructure
{
    public class GameBootstrap
    {
        public GameBootstrap(IWindowService windowService,
            IAssetsAddressablesProvider assetsAddressablesProvider)
        {
            StateMachine = new StateMachine<GameBootstrap>(new BootstrapState(this),
                new InitializationState(this),
                new ResourcesLoadingState(this, windowService),
                new GameMainMenuState(this, windowService),
                new GameplayState(this, windowService, assetsAddressablesProvider)
            );
            
            StateMachine.SwitchState<BootstrapState>();
        }

        public readonly StateMachine<GameBootstrap> StateMachine;
    }
}