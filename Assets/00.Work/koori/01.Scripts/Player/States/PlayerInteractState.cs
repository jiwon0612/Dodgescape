using UnityEngine;

public class PlayerInteractState : EntityState
{
    private Player _player;
    private EntityMover _mover;
    public PlayerInteractState(Entity entity, AnimParamSO animParam) : base(entity, animParam)
    {
        _player = entity as Player;
        _mover = entity.GetComp<EntityMover>();
    }
    public override void Enter()
    {
        base.Enter();
        _mover.StopImmediately();
        _mover.CanManualMove = false;
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

        base.Exit();
    }
}
