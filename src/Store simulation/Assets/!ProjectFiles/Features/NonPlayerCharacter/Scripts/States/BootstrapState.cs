using Extension.StateMachineCore;

namespace NonPlayerCharacter.States
{
    public class BootstrapState : IState<NpcController>, IEnterable
    {
        public BootstrapState(NpcController initializer)
        {
            Initializer = initializer;
        }

        public NpcController Initializer { get; }

        public void OnEnter()
        {
            Initializer.StateMachine.SwitchState<ProductSearchState>();
        }
    }
}