using System.Collections;
using Extension.NonLinearStateMachine;
using Infrastructure.Services.CoroutineRunner;
using Infrastructure.Services.Market;
using UnityEngine;
using UnityEngine.AI;

namespace NonPlayerCharacter.States
{
    public class ProductPurchaseState : IState
    {
        public ProductPurchaseState(NpcController initializer, GameObject instantiate, Vector3 cashRegisterPoint,
            IMarketService marketService, ICoroutineRunner coroutineRunner)
        {
            _instantiate = instantiate;
            _cashRegisterPoint = cashRegisterPoint;
            _marketService = marketService;
            _coroutineRunner = coroutineRunner;
            Initializer = initializer;
        }

        public NpcController Initializer { get; }
        private readonly GameObject _instantiate;
        private readonly Vector3 _cashRegisterPoint;
        private readonly IMarketService _marketService;
        private readonly ICoroutineRunner _coroutineRunner;

        public bool IsPurchasesPaid { get; private set; }
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
            if (distanceToTarget < 1 && !IsPurchasesPaid && _purchaseCoroutine == null)
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

            _marketService.Purchase();
            IsPurchasesPaid = true;
        }
    }
}