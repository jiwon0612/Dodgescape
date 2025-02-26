using UnityEngine;

public class PlayerMoveState : EntityState
{
    private Player _player;
    private EntityMover _mover;

    private float angle;
    private float _rotationSpeed = 250f;

    public PlayerMoveState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _player = entity as Player;
        _mover = _player.GetComp<EntityMover>();
    }

    public override void Update()
    {
        base.Update();
        Vector2 input = _player.PlayerInput.MoveDirection;
        float xMove = input.x;
        float zMove = input.y;

        Vector3 moveDirection = new Vector3(xMove, 0, zMove);

        if (moveDirection.magnitude > 1)
            moveDirection.Normalize();

        _mover.SetMovement(moveDirection);

        if (Mathf.Approximately(xMove, 0) && Mathf.Approximately(zMove, 0))
        {
            _player.ChangeState("Idle");
        }
        else
        {
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);

            _renderer.SetRotation(targetRotation, _rotationSpeed);
        }
    }
}
