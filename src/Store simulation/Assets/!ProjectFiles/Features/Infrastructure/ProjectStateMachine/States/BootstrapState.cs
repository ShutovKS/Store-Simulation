using Extension.FinalStateMachine;
using Zenject;

namespace Infrastructure.ProjectStateMachine.States
{
    public class BootstrapState : IState<GameBootstrap>, IInitializable
    {
        public BootstrapState(GameBootstrap initializer)
        {
            Initializer = initializer;
        }

        public GameBootstrap Initializer { get; }

        public void Initialize()
        {
            Initializer.StateMachine.SwitchState<InitializationState>();
        }
    }
}