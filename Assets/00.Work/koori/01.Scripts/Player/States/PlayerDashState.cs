using UnityEngine;

public class PlayerDashState : EntityState
{
    private Player _player;
    private EntityMover _mover;
    private EntityHealth _health;

    public PlayerDashState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
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
        _health.IsCanHit = false;
    }
    public override void Update()
    {
        base.Update();
        if (_isTriggerCall)
        {
            _player.ChangeState("Idle");
        }
        _mover.AddForceToEntity(_player.transform.forward * _player.dashSpeed);
    }

    public override void Exit()
    {
        _mover.StopImmediately(true);
        _mover.CanManualMove = true;
        _health.IsCanHit = true;

        base.Exit();
    }
}
