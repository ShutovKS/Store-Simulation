namespace Extension.FinalStateMachine
{
    public interface IState<out TInitializer>
    {
        TInitializer Initializer { get; }
    }
}