namespace Extension.NonLinearStateMachine
{
    public interface IState
    {
        void OnEnter();
        void Tick();
        void OnExit();
    }
}