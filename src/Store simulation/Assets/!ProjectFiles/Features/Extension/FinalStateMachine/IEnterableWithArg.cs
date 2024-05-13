namespace Extension.FinalStateMachine
{
    public interface IEnterableWithArg<in TArg>
    {
        void OnEnter(TArg arg0);
    }
}