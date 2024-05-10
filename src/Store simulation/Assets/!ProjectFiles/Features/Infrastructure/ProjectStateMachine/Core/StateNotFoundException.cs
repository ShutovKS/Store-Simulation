using System;

namespace Infrastructure.ProjectStateMachine.Core
{
    public class StateNotFoundException : Exception
    {
        public StateNotFoundException(string message) : base(message)
        {
        }
    }
}