namespace Extension.StateMachineCore
{
    public interface IState<out TInitializer>
    {
        TInitializer Initializer { get; }
    }
}