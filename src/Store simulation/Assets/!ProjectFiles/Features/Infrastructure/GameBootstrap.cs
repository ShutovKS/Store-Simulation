using Infrastructure.ProjectStateMachine.Core;

namespace Infrastructure
{
    public class GameBootstrap
    {
        public GameBootstrap()
        {
            StateMachine = new StateMachine<GameBootstrap>();

            // StateMachine.SwitchState<>();
        }

        public readonly StateMachine<GameBootstrap> StateMachine;
    }
}