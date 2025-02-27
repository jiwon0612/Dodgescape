using System;
using Unity.Behavior;
using UnityEngine;

public class MushroomMan : BTEnemy
{
    private EnemyStateChangeEvent _stateChannel;
    private EntityAnimationTrigger _animationTrigger;
    private EnemyAttackCompo _attackCompo;

    protected override void InitComp()
    {
        base.InitComp();
        _animationTrigger = GetComp<EntityAnimationTrigger>();
        _attackCompo = GetComp<EnemyAttackCompo>();
    }

    protected override void AfterInit()
    {
        base.AfterInit();
        _animationTrigger.OnAttackTrigger += HandleAttack;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        _animationTrigger.OnAttackTrigger -= HandleAttack;
    }

    private void HandleAttack()
    {
        _attackCompo.Attack();
    }

    private void Start()
    {
        BlackboardVariable<EnemyStateChangeEvent> stateChannel =
            GetBlackboardVariable<EnemyStateChangeEvent>("StateChannel");
        _stateChannel = stateChannel.Value;
        Debug.Assert(_stateChannel != null, $"StateChannel is null {gameObject.name}");
    }

    public override void ApplyDamage(float damage, Vector2 knockBack, float stunDuration)
    {
        base.ApplyDamage(damage, knockBack, stunDuration);
        _stateChannel.SendEventMessage(EnemyState.HIT);
    }

    public override void Death()
    {
        base.Death();
        _stateChannel.SendEventMessage(EnemyState.DEATH);
        _health.IsCanHit = false;
    }
}
