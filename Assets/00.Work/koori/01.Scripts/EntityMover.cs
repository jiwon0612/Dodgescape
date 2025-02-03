using System;
using UnityEngine;

public class EntityMover : MonoBehaviour, IEntityComp
{
    [Header("Move values")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _originalGravityScale;

    [SerializeField] private Transform _groundTrm;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private Vector3 _groundCheckSize;

    public bool IsGrounded { get; private set; }
    public event Action<bool> OnGroundStateChange;

    public Vector3 Velocity => _rbCompo.linearVelocity;

    public float SpeedMultiplier { get; set; } = 1f;
    public bool CanManualMove { get; set; } = true;

    public float gravityScale { get; private set; }


    private Entity _entity;
    private Rigidbody _rbCompo;

    private Vector3 _movement;

    public void Initialize(Entity entity)
    {
        _entity = entity;
        _rbCompo = _entity.GetComponent<Rigidbody>();

        _rbCompo.useGravity = false;
    }

    public void SetGravityScale(float value)
    {
        gravityScale = value;
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
        AddForceToEntity(new Vector3(force.x,0 , force.y));
        
    }

    public void SetMovement(Vector3 movement)
    {
        _movement = movement;
    }

    private void FixedUpdate()
    {
        CheckGround();
        MoveCharacter();
    }

    private void CheckGround()
    {
        bool before = IsGrounded;
        IsGrounded = Physics.OverlapBox(
            _groundTrm.position, _groundCheckSize / 2, Quaternion.identity, _whatIsGround).Length > 0;

        if (before != IsGrounded)
            OnGroundStateChange?.Invoke(IsGrounded);
    }

    private void MoveCharacter()
    {
        _rbCompo.AddForce(Physics.gravity * _originalGravityScale * _rbCompo.mass * gravityScale, ForceMode.Force);

        if (CanManualMove)
        {
            _rbCompo.linearVelocity = new Vector3(
                _movement.x * _moveSpeed * SpeedMultiplier,
                _rbCompo.linearVelocity.y,
                _movement.z * _moveSpeed * SpeedMultiplier);
        }
    }

    private void OnDrawGizmos()
    {
        if (_groundTrm == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_groundTrm.position, _groundCheckSize / 2);
    }
}