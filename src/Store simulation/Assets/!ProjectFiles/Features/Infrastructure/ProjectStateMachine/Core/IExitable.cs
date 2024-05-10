namespace Infrastructure.ProjectStateMachine.Core
{
    public interface IExitable
    {
        void OnExit();
    }
}