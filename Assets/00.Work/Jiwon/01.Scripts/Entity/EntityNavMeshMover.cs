using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class EntityNavMeshMover : EntityMover
{
    private NavMeshAgent _navMeshAgent;

    public Transform test;
    
    public override void Initialize(Entity entity)
    {
        base.Initialize(entity);
        _navMeshAgent = entity.GetComponent<NavMeshAgent>();
        SetMovement(test.position);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            SetMovement(test.position);
        }
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
        base.StopImmediately(isYAxisToo);
        _navMeshAgent.isStopped = true;
        _movement = transform.position;
    }

    protected override void MoveCharacter()
    {
        if (CanManualMove)
        {
            _navMeshAgent.SetDestination(_movement);
        }
    }
}
