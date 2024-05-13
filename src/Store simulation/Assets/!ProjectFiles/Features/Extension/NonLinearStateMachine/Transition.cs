using System;

namespace Extension.NonLinearStateMachine
{
    public class Transition
    {
        public Transition(IState to, Func<bool> condition)
        {
            To = to;
            Condition = condition;
        }

        public Func<bool> Condition { get; }
        public IState To { get; }
    }
}