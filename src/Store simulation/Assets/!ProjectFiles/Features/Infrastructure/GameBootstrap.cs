using Data.Scene;
using Extension.FinalStateMachine;
using Infrastructure.ProjectStateMachine.States;
using Infrastructure.Services.AssetsAddressables;
using Infrastructure.Services.DataBase;
using Infrastructure.Services.Factory.NpcFactory;
using Infrastructure.Services.Market;
using Infrastructure.Services.Windows;

namespace Infrastructure
{
    public class GameBootstrap
    {
        public GameBootstrap(
            IAssetsAddressablesProvider assetsAddressablesProvider,
            IDataBaseService dataBaseService,
            INpcFactory npcFactory,
            IMarketService marketService,
            IWindowService windowService,
            GameplaySceneData gameplaySceneData
        )
        {
            StateMachine = new StateMachine<GameBootstrap>(new BootstrapState(this),
                new InitializationState(this, assetsAddressablesProvider, dataBaseService, marketService),ц
                new ResourcesLoadingState(this, windowService),
                new GameMainMenuState(this, windowService),
                new GameplayState(this, windowService, dataBaseService, npcFactory, marketService, gameplaySceneData)
            );

            StateMachine.SwitchState<BootstrapState>();
        }

        public readonly StateMachine<GameBootstrap> StateMachine;
    }
}