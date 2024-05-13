using Extension.NonLinearStateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace NonPlayerCharacter.States
{
    public class ProductSearchState : IState
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

        public int NumberPlacesVisited { get; private set; }

        private Transform _transform;
        private NavMeshAgent _agent;
        private Vector3 _target;
        private int _currentPointIndex;

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
                NumberPlacesVisited++;
                MoveForProduct();
            }
        }

        public void OnExit()
        {
        }

        private void MoveForProduct()
        {
            while (true)
            {
                _currentPointIndex = Random.Range(0, _groceryOutletPoints.Length);

                if (_currentPointIndex != _groceryOutletPoints.Length)
                {
                    _target = _groceryOutletPoints[_currentPointIndex];
                    _agent.SetDestination(_target);
                }
                else
                {
                    continue;
                }

                break;
            }
        }
    }
}