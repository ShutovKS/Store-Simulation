namespace Extension.NonLinearStateMachine
{
    public interface IState
    {
        void OnEnter();
        void OnUpdate();
        void OnExit();
    }
}