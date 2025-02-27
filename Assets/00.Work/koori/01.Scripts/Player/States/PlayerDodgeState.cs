using UnityEngine;

public class PlayerDodgeState : EntityState
{
    private Player _player;
    private EntityMover _mover;
    private EntityHealth _health;

    public PlayerDodgeState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _player = entity as Player;
        _mover = entity.GetComp<EntityMover>();
        _health = entity.GetComp<EntityHealth>();
    }

    public override void Enter()
    {
        base.Enter();
        _mover.StopImmediately(true);
        _mover.CanManualMove = false;
        _health.IsEvasion = true;
        
        if (_player.PlayerInput.MoveDirection == Vector2.zero)
            _mover.AddForceToEntity(_player.transform.right * _player.dodgeSpeed);
        else
            _mover.AddForceToEntity(new Vector3(_player.PlayerInput.MoveDirection.x,
                0,_player.PlayerInput.MoveDirection.y) * _player.dodgeSpeed);
    }

    public override void Update()
    {
        base.Update();
        if (_isTriggerCall)
        {
            _player.ChangeState("Idle");
        }
    }

    public override void Exit()
    {
        _mover.StopImmediately(true);
        _mover.CanManualMove = true;
        _health.IsEvasion = false;
        base.Exit();
    }
}