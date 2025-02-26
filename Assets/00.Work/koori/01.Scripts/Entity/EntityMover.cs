using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class EntityMover : MonoBehaviour, IEntityComp
{
    [Header("Move values")]
    [SerializeField] protected float _moveSpeed = 5f;

    public Vector3 Velocity => _rbCompo.linearVelocity;

    public float SpeedMultiplier { get; set; } = 1f;
    public bool CanManualMove { get; set; } = true;

    protected Entity _entity;
    protected Rigidbody _rbCompo;

    protected Vector3 _movement;

    public virtual void Initialize(Entity entity)
    {
        _entity = entity;
        _rbCompo = _entity.GetComponent<Rigidbody>();
    }

    public virtual void AddForceToEntity(Vector3 force, ForceMode mode = ForceMode.Impulse)
    {
        _rbCompo.AddForce(force, mode);
    }

    public virtual void StopImmediately(bool isYAxisToo = false)
    {
        if (isYAxisToo)
            _rbCompo.linearVelocity = Vector3.zero;
        else
            _rbCompo.linearVelocity = new Vector3(0, _rbCompo.linearVelocity.y, 0);
        _movement = Vector3.zero;
    }

    public virtual void KnockBack(Vector2 force, float time)
    {
        CanManualMove = false;
        StopImmediately(true);
        AddForceToEntity(new Vector3(force.x, 0, force.y));
        DOVirtual.DelayedCall(time, () => CanManualMove = true);
    }

    public virtual void SetMovement(Vector3 movement)
    {
        _movement = movement;
    }

    protected virtual void FixedUpdate()
    {
        MoveCharacter();
    }

    protected virtual void MoveCharacter()
    {
        if (CanManualMove)
        {
            _rbCompo.linearVelocity = new Vector3(
                _movement.x * _moveSpeed * SpeedMultiplier,
                _rbCompo.linearVelocity.y,
                _movement.z * _moveSpeed * SpeedMultiplier);
        }
    }
}