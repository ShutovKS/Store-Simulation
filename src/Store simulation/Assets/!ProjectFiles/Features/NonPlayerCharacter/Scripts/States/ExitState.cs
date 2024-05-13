using Extension.NonLinearStateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace NonPlayerCharacter.States
{
    public class ExitState : IState
    {
        public ExitState(NpcController initializer, GameObject instantiate, Vector3 exitPoint)
        {
            _instantiate = instantiate;
            _exitPoint = exitPoint;
            Initializer = initializer;
        }

        public NpcController Initializer { get; }
        private readonly GameObject _instantiate;
        private readonly Vector3 _exitPoint;
        private NavMeshAgent _agent;

        public void OnEnter()
        {
            _agent = _instantiate.GetComponent<NavMeshAgent>();

            MoveToExit();
        }

        public void OnUpdate()
        {
            var distanceToTarget = Vector3.Distance(_instantiate.transform.position, _exitPoint);
            if (distanceToTarget < 1)
            {
                Initializer.NpcFinishedShopping.Value = true;
            }
        }

        public void OnExit()
        {
        }

        private void MoveToExit()
        {
            _agent.SetDestination(_exitPoint);
        }
    }
}