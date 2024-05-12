using Data.Scene;
using Extension.StateMachineCore;
using Infrastructure.Services.Factory.NpcFactory;
using NonPlayerCharacter.States;
using UniRx;
using UnityEngine;

namespace NonPlayerCharacter
{
    public class NpcController
    {
        public readonly BoolReactiveProperty NpcFinishedShopping = new(false);

        public NpcController(GameObject instantiate, GameplaySceneData gameplaySceneData, INpcFactory npcFactory)
        {
            NpcData = new NpcData();
            StateMachine = new StateMachine<NpcController>(
                new BootstrapState(this),
                new ProductSearchState(this, instantiate, gameplaySceneData.GroceryOutletPoints),
                new ProductPurchaseState(this, instantiate, gameplaySceneData.CashRegisterPoint),
                new ExitState(this, instantiate, gameplaySceneData.NpcSpawnPoint)
            );

            StateMachine.SwitchState<BootstrapState>();

            NpcFinishedShopping.Subscribe(finishedShopping =>
            {
                if (finishedShopping)
                {
                    StateMachine.TickStop();
                    npcFactory.DestroyNpc(this);
                }
            }).AddTo(instantiate);
        }

        public readonly StateMachine<NpcController> StateMachine;
        public readonly NpcData NpcData;
    }
}