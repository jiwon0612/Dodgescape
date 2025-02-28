using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EntityNavMeshMover : EntityMover
{
    [SerializeField] private float toleranceValue = 0.5f;
    
    private NavMeshAgent _navMeshAgent;

    public override void Initialize(Entity entity)
    {
        base.Initialize(entity);
        _navMeshAgent = entity.GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = _moveSpeed;
    }

    public override void KnockBack(Vector2 force, float time)
    {
        CanManualMove = false;
        _rbCompo.isKinematic = false;
        StopImmediately();
        AddForceToEntity(new Vector3(force.x, 0, force.y));
        DOVirtual.DelayedCall(time, () =>
        {
            StopImmediately();
            CanManualMove = true;
            _rbCompo.isKinematic = true;
        });
    }

    public override void SetMovement(Vector3 movement)
    {
        _navMeshAgent.isStopped = false;
        base.SetMovement(movement);
    }

    public override void StopImmediately(bool isYAxisToo = false)
    {
        if (!_rbCompo.isKinematic)
            base.StopImmediately(isYAxisToo);
        else
            _movement = Vector3.zero;
        _navMeshAgent.ResetPath();
        _navMeshAgent.isStopped = true;
    }

    protected override void MoveCharacter()
    {
        if (CanManualMove)
        {
            _navMeshAgent.SetDestination(_movement);
        }
    }

    public bool GetIsSuccessToPoint()
    {
        return Vector3.Distance(transform.position, _movement) <= toleranceValue;
    }

#if UNITY_EDITOR

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, toleranceValue);
        Gizmos.color = Color.white;
    }

#endif
}
