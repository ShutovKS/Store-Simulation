namespace Extension.StateMachineCore
{
    public interface IEnterableWithArg<in TArg>
    {
        void OnEnter(TArg arg0);
    }
}