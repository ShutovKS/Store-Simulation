using System.Collections;
using Extension.NonLinearStateMachine;
using Infrastructure.Services.CoroutineRunner;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

namespace NonPlayerCharacter.States
{
    public class ProductPurchaseState : IState
    {
        public ProductPurchaseState(NpcController initializer, GameObject instantiate, Vector3 cashRegisterPoint,
            ICoroutineRunner coroutineRunner)
        {
            _instantiate = instantiate;
            _cashRegisterPoint = cashRegisterPoint;
            _coroutineRunner = coroutineRunner;
            Initializer = initializer;
        }

        public NpcController Initializer { get; }
        private readonly GameObject _instantiate;
        private readonly Vector3 _cashRegisterPoint;
        private readonly ICoroutineRunner _coroutineRunner;

        public BoolReactiveProperty IsPurchasesPaid { get; } = new(false);
        private NavMeshAgent _agent;
        private Coroutine _purchaseCoroutine;

        public void OnEnter()
        {
            _agent = _instantiate.GetComponent<NavMeshAgent>();

            MoveToCashRegister();
        }

        public void OnUpdate()
        {
            var distanceToTarget = Vector3.Distance(_instantiate.transform.position, _cashRegisterPoint);
            if (distanceToTarget < 1 && !IsPurchasesPaid.Value && _purchaseCoroutine == null)
            {
                _purchaseCoroutine = _coroutineRunner.StartCoroutine(Purchase());
            }
        }

        public void OnExit()
        {
            _coroutineRunner.StopCoroutine(_purchaseCoroutine);
            _purchaseCoroutine = null;
        }

        private void MoveToCashRegister()
        {
            _agent.SetDestination(_cashRegisterPoint);
        }

        private IEnumerator Purchase()
        {
            yield return new WaitForSeconds(2);

            IsPurchasesPaid.Value = true;
        }
    }
}