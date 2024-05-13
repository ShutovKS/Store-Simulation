using Extension.NonLinearStateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace NonPlayerCharacter.States
{
    public class ProductPurchaseState : IState
    {
        public ProductPurchaseState(NpcController initializer, GameObject instantiate, Vector3 cashRegisterPoint)
        {
            _instantiate = instantiate;
            _cashRegisterPoint = cashRegisterPoint;
            Initializer = initializer;
        }

        public NpcController Initializer { get; }
        private readonly GameObject _instantiate;
        private readonly Vector3 _cashRegisterPoint;
        
        public bool IsPurchasesPaid { get; private set; }
        private NavMeshAgent _agent;

        public void OnEnter()
        {
            _agent = _instantiate.GetComponent<NavMeshAgent>();

            MoveToCashRegister();
        }

        public void OnUpdate()
        {
            var distanceToTarget = Vector3.Distance(_instantiate.transform.position, _cashRegisterPoint);
            if (distanceToTarget < 1)
            {
                IsPurchasesPaid = true;
            }
        }

        public void OnExit()
        {
        }

        private void MoveToCashRegister()
        {
            _agent.SetDestination(_cashRegisterPoint);
        }
    }
}