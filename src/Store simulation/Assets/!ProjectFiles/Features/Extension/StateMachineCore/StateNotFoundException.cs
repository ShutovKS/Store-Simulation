using System;

namespace Extension.StateMachineCore
{
    public class StateNotFoundException : Exception
    {
        public StateNotFoundException(string message) : base(message)
        {
        }
    }
}