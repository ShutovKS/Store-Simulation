namespace Infrastructure.ProjectStateMachine.Core
{
    public interface IEnterableWithArg<in TArg>
    {
        void OnEnter(TArg arg0);
    }
}