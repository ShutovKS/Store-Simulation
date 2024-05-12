using Extension.StateMachineCore;
using UnityEngine;
using UnityEngine.AI;

namespace NonPlayerCharacter.States
{
    public class ProductSearchState : IState<NpcController>, IEnterable, ITickable
    {
        public ProductSearchState(NpcController initializer, GameObject instantiate, Vector3[] groceryOutletPoints)
        {
            _instantiate = instantiate;
            _groceryOutletPoints = groceryOutletPoints;
            Initializer = initializer;
        }

        public NpcController Initializer { get; }
        private readonly GameObject _instantiate;
        private readonly Vector3[] _groceryOutletPoints;
        private Transform _transform;
        private NavMeshAgent _agent;
        private Vector3 _target;

        public void OnEnter()
        {
            _agent = _instantiate.GetComponent<NavMeshAgent>();
            _transform = _instantiate.transform;

            MoveForProduct();
        }

        public void OnUpdate()
        {
            var distanceToTarget = Vector3.Distance(_transform.position, _target);
            if (distanceToTarget < 1)
            {
                ShoppingComplete();
            }
        }

        private void MoveForProduct()
        {
            _target = _groceryOutletPoints[Random.Range(0, _groceryOutletPoints.Length)];
            _agent.SetDestination(_target);
        }

        private void ShoppingComplete()
        {
            Initializer.StateMachine.SwitchState<ProductPurchaseState>();
        }
    }
}