#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

#endregion

namespace Extension.NonLinearStateMachine
{
    public class StateMachine
    {
        private static readonly List<Transition> emptyTransitions = new(0);

        private readonly List<Transition> _anyTransitions = new();
        private readonly Dictionary<Type, List<Transition>> _transitions = new();

        private CancellationTokenSource _cancellationTick = new();
        private List<Transition> _currentTransitions = new();
        private IState _currentState;

        public void TickStop()
        {
            _cancellationTick.Cancel();
        }

        public async UniTaskVoid Tick()
        {
            _cancellationTick = new CancellationTokenSource();

            while (!_cancellationTick.Token.IsCancellationRequested)
            {
                var transition = GetTransition();
                if (transition != null)
                {
                    SetState(transition.To);
                }

                _currentState?.OnUpdate();

                await UniTask.Yield();
            }
        }

        public void SetState(IState state)
        {
            if (state == _currentState)
            {
                return;
            }

            TickStop();

            _currentState?.OnExit();

            _currentState = state;

            _transitions.TryGetValue(_currentState.GetType(), out _currentTransitions);

            _currentTransitions ??= emptyTransitions;

            _currentState.OnEnter();

            Tick();
            
            Debug.Log($"Current state: {_currentState.GetType().Name}");
        }

        public void AddTransition(IState to, IState from, Func<bool> predicate)
        {
            if (_transitions.TryGetValue(from.GetType(), out var transitions) == false)
            {
                transitions = new List<Transition>();
                _transitions[from.GetType()] = transitions;
            }

            transitions.Add(new Transition(to, predicate));
        }

        public void AddAnyTransition(IState state, Func<bool> predicate)
        {
            _anyTransitions.Add(new Transition(state, predicate));
        }

        private Transition GetTransition()
        {
            foreach (var transition in _anyTransitions.Where(transition => transition.Condition()))
            {
                return transition;
            }

            foreach (var transition in _currentTransitions.Where(transition => transition.Condition()))
            {
                return transition;
            }

            return null;
        }
    }
}