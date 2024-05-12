using Data.Scene;
using Extension.StateMachineCore;
using NonPlayerCharacter.States;
using UnityEngine;

namespace NonPlayerCharacter
{
    public class NpcController
    {
        public NpcController(GameObject instantiate, GameplaySceneData gameplaySceneData)
        {
            NpcData = new NpcData();
            StateMachine = new StateMachine<NpcController>(
                new BootstrapState(this),
                new ProductSearchState(this, instantiate, gameplaySceneData.GroceryOutletPoints)
            );

            StateMachine.SwitchState<BootstrapState>();
        }

        public readonly StateMachine<NpcController> StateMachine;
        public readonly NpcData NpcData;
    }
}