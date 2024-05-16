using System;

namespace Extension.FinalStateMachine
{
    public class StateNotFoundException : Exception
    {
        public StateNotFoundException(string message) : base(message)
        {
        }
    }
}