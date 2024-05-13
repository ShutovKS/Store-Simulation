using System;
using Data.Scene;
using Extension.NonLinearStateMachine;
using Infrastructure.Services.Factory.NpcFactory;
using NonPlayerCharacter.States;
using UniRx;
using UnityEngine;

namespace NonPlayerCharacter
{
    public class NpcController
    {
        public NpcController(GameObject instantiate, GameplaySceneData gameplaySceneData, INpcFactory npcFactory)
        {
            var productSearchState = new ProductSearchState(this, instantiate, gameplaySceneData.GroceryOutletPoints);
            var productPurchaseState = new ProductPurchaseState(this, instantiate, gameplaySceneData.CashRegisterPoint);
            var exitState = new ExitState(this, instantiate, gameplaySceneData.NpcSpawnPoint);

            At(productPurchaseState, productSearchState, IsAllProductSearch());
            At(exitState, productPurchaseState, IsPurchasesPaid());

            _stateMachine.SetState(productSearchState);

            NpcFinishedShopping.Subscribe(finishedShopping =>
            {
                if (finishedShopping)
                {
                    _stateMachine.TickStop();
                    npcFactory.DestroyNpc(this);
                }
            }).AddTo(instantiate);

            return;

            Func<bool> IsAllProductSearch() => () => productSearchState.NumberPlacesVisited >= 3;
            Func<bool> IsPurchasesPaid() => () => productPurchaseState.IsPurchasesPaid;
        }

        public readonly BoolReactiveProperty NpcFinishedShopping = new(false);
        public readonly NpcData NpcData = new();
        private readonly StateMachine _stateMachine = new();

        private void At(IState to, IState from, Func<bool> condition)
        {
            _stateMachine.AddTransition(to, from, condition);
        }

        private void AtAny(IState to, Func<bool> condition)
        {
            _stateMachine.AddAnyTransition(to, condition);
        }
    }
}