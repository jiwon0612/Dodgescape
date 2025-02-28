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

        AttackEvent attackEvent = PlayerEvent.AttackEvent;
        attackEvent.isAttacking = true;
        _player.PlayerEventChannel.RaiseEvent(attackEvent);
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

        AttackEvent attackEvent = PlayerEvent.AttackEvent;
        attackEvent.isAttacking = false;
        _player.PlayerEventChannel.RaiseEvent(attackEvent);

        base.Exit();
    }
}
