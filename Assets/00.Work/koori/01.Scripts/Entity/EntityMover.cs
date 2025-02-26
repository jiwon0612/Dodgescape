using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class EntityMover : MonoBehaviour, IEntityComp
{
    [Header("Move values")]
    [SerializeField] private float _moveSpeed = 5f;

    public Vector3 Velocity => _rbCompo.linearVelocity;

    public float SpeedMultiplier { get; set; } = 1f;
    public bool CanManualMove { get; set; } = true;

    private Entity _entity;
    private Rigidbody _rbCompo;

    private Vector3 _movement;

    public void Initialize(Entity entity)
    {
        _entity = entity;
        _rbCompo = _entity.GetComponent<Rigidbody>();
    }

    public void AddForceToEntity(Vector3 force, ForceMode mode = ForceMode.Impulse)
    {
        _rbCompo.AddForce(force, mode);
    }

    public void StopImmediately(bool isYAxisToo = false)
    {
        if (isYAxisToo)
            _rbCompo.linearVelocity = Vector3.zero;
        else
            _rbCompo.linearVelocity = new Vector3(0, _rbCompo.linearVelocity.y, 0);
        _movement = Vector3.zero;
    }

    public void KnockBack(Vector2 force, float time)
    {
        CanManualMove = false;
        StopImmediately(true);
        AddForceToEntity(new Vector3(force.x, 0, force.y));
        DOVirtual.DelayedCall(time, () => CanManualMove = true);
    }

    public void SetMovement(Vector3 movement)
    {
        _movement = movement;
    }

    private void FixedUpdate()
    {
        MoveCharacter();
    }

    private void MoveCharacter()
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