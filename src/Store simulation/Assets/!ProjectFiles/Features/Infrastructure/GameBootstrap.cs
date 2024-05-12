using Data.Scene;
using Extension.StateMachineCore;
using Infrastructure.ProjectStateMachine.States;
using Infrastructure.Services.AssetsAddressables;
using Infrastructure.Services.DataBase;
using Infrastructure.Services.Factory.NpcFactory;
using Infrastructure.Services.Windows;

namespace Infrastructure
{
    public class GameBootstrap
    {
        public GameBootstrap(
            IAssetsAddressablesProvider assetsAddressablesProvider,
            IDataBaseService dataBaseService,
            INpcFactory npcFactory,
            IWindowService windowService,
            GameplaySceneData gameplaySceneData
        )
        {
            StateMachine = new StateMachine<GameBootstrap>(new BootstrapState(this),
                new InitializationState(this, assetsAddressablesProvider),
                new ResourcesLoadingState(this, windowService),
                new GameMainMenuState(this, windowService),
                new GameplayState(this, windowService, dataBaseService, npcFactory, gameplaySceneData)
            );

            StateMachine.SwitchState<BootstrapState>();
        }

        public readonly StateMachine<GameBootstrap> StateMachine;
    }
}