using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class EntityNavMeshMover : EntityMover
{
    private NavMeshAgent _navMeshAgent;

    [SerializeField] private Transform test;
    
    public override void Initialize(Entity entity)
    {
        base.Initialize(entity);
        _navMeshAgent = entity.GetComponent<NavMeshAgent>();
        SetMovement(test.position);
    }

    public override void KnockBack(Vector2 force, float time)
    {
        CanManualMove = false;
        StopImmediately();
        AddForceToEntity(new Vector3(force.x, 0, force.y));
        DOVirtual.DelayedCall(time, () =>
        {
            StopImmediately();
            CanManualMove = true;
        });
    }

    protected override void MoveCharacter()
    {
        if (CanManualMove)
        {
            _navMeshAgent.isStopped = false;
            _navMeshAgent.SetDestination(_movement);
        }
        else
            _navMeshAgent.isStopped = true;
    }
}
