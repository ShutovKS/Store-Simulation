using System.Collections;
using Extension.NonLinearStateMachine;
using Infrastructure.Services.CoroutineRunner;
using UnityEngine;
using UnityEngine.AI;

namespace NonPlayerCharacter.States
{
    public class ProductSearchState : IState
    {
        public ProductSearchState(NpcController initializer, GameObject instantiate, Vector3[] groceryOutletPoints,
            ICoroutineRunner coroutineRunner)
        {
            _instantiate = instantiate;
            _groceryOutletPoints = groceryOutletPoints;
            _coroutineRunner = coroutineRunner;
            Initializer = initializer;
        }

        public NpcController Initializer { get; }
        private readonly GameObject _instantiate;
        private readonly Vector3[] _groceryOutletPoints;
        private readonly ICoroutineRunner _coroutineRunner;

        public int NumberPlacesVisited { get; private set; }

        private Transform _transform;
        private NavMeshAgent _agent;
        private Vector3 _target;
        private int _currentPointIndex;
        private Coroutine _coroutine;

        public void OnEnter()
        {
            _agent = _instantiate.GetComponent<NavMeshAgent>();
            _transform = _instantiate.transform;

            _coroutine = _coroutineRunner.StartCoroutine(SearchNewProduct());
        }

        public void OnUpdate()
        {
            var distanceToTarget = Vector3.Distance(_transform.position, _target);
            if (distanceToTarget < 1 && _coroutine == null)
            {
                NumberPlacesVisited++;

                _coroutine = _coroutineRunner.StartCoroutine(SearchNewProduct());
            }
        }

        public void OnExit()
        {
            _coroutineRunner.StopCoroutine(_coroutine);
            _coroutine = null;
        }

        private IEnumerator SearchNewProduct()
        {
            yield return new WaitForSeconds(3);

            MoveForProduct();

            _coroutine = null;
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