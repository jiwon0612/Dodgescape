using UnityEngine;

public class PlayerAttackState : EntityState
{
    private Player _player;
    private EntityMover _mover;

    public PlayerAttackState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _player = entity as Player;
        _mover = entity.GetComp<EntityMover>();
    }

    public override void Enter()
    {
        base.Enter();
        _mover.StopImmediately(true);
        _mover.CanManualMove = false;
    }
    public override void Exit()
    {
        _mover.StopImmediately(true);
        _mover.CanManualMove = true;
        _player.ChangeState("Idle");

        base.Exit();
    }
}
