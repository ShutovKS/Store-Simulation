using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Extension.StateMachineCore
{
    public class StateMachine<TInitializer>
    {
        public StateMachine(params IState<TInitializer>[] states)
        {
            _states = new Dictionary<Type, IState<TInitializer>>(states.Length);

            foreach (var state in states)
            {
                _states.Add(state.GetType(), state);
            }
        }

        private readonly Dictionary<Type, IState<TInitializer>> _states;
        private IState<TInitializer> _currentState;
        private CancellationTokenSource _cancellationTick = new();

        public void SwitchState<TState>() where TState : IState<TInitializer>
        {
            ExitCurrentState();
            ChangeState<TState>();
            EnterNewState();
            Tick();
        }

        public void SwitchState<TState, TParameter>(TParameter arg0) where TState : IState<TInitializer>
        {
            ExitCurrentState();
            ChangeState<TState>();
            EnterNewState(arg0);
            Tick();
        }

        private void ExitCurrentState()
        {
            _cancellationTick.Cancel();

            if (_currentState is IExitable state)
            {
                state.OnExit();
            }
        }

        private void EnterNewState()
        {
            if (_currentState is IEnterable state)
            {
                state.OnEnter();
            }
        }

        private void EnterNewState<TParameter>(TParameter arg0)
        {
            if (_currentState is IEnterableWithArg<TParameter> state)
            {
                state.OnEnter(arg0);
            }
        }

        private async UniTaskVoid Tick()
        {
            _cancellationTick = new CancellationTokenSource();

            if (_currentState is not ITickable state)
            {
                return;
            }

            while (!_cancellationTick.Token.IsCancellationRequested)
            {
                try
                {
                    state.OnUpdate();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"[StateMachine] Tick: " + e);
                }

                await UniTask.Yield();
            }
        }

        private void ChangeState<TState>() where TState : IState<TInitializer>
        {
            if (_states.TryGetValue(typeof(TState), out var state))
            {
                _currentState = state;
            }
            else throw new StateNotFoundException($"{typeof(TState)} состояние не найдено в state machine.");
        }
    }
}