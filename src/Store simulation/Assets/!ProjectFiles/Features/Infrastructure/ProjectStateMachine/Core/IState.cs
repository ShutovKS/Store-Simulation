namespace Infrastructure.ProjectStateMachine.Core
{
    public interface IState<out TInitializer>
    {
        TInitializer Initializer { get; }
    }
}