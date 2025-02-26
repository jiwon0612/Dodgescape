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
        _health.IsCanHit = false;
    }

    public override void Update()
    {
        base.Update();
        if (_isTriggerCall)
        {
            _player.ChangeState("Idle");
        }

        _mover.KnockBack(-_player.transform.forward * _player.dodgeSpeed, 0.1f);
    }

    public override void Exit()
    {
        _mover.StopImmediately(true);
        _mover.CanManualMove = true;
        _health.IsEvasion = false;
        _health.IsCanHit = true;
        base.Exit();
    }
}