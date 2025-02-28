using System;
using Unity.Behavior;
using UnityEngine;

public class Golem : BTEnemy
{
    private BlackboardVariable<GolemState> _state;
    
    private GolemStateChangeEvent _stateChannel;
    private GolemAnimationTrigger _animationTrigger;
    private GolemAttackCompo _attackCompo;

    protected override void InitComp()
    {
        base.InitComp();
        _animationTrigger = GetComp<GolemAnimationTrigger>();
        _attackCompo = GetComp<GolemAttackCompo>();
    }

    private void Start()
    {
        BlackboardVariable<GolemStateChangeEvent> stateChangeEvent
            = GetBlackboardVariable<GolemStateChangeEvent>("StateChannel");
        _stateChannel = stateChangeEvent.Value;
        Debug.Assert(_stateChannel != null, $"stateChannel is null {gameObject.name}");
        
        _state = GetBlackboardVariable<GolemState>("GolemState");
    }

    public override void ApplyDamage(float damage, Vector2 knockBack, float stunDuration)
    {
        
        if (_state.Value == GolemState.Idle  || _state.Value == GolemState.Chase)
            base.ApplyDamage(damage, knockBack, stunDuration);
    }

    public override void Death()
    {
        base.Death();
        _stateChannel.SendEventMessage(GolemState.Death);
    }
}

[BlackboardEnum]
public enum GolemState
{
    Idle, Chase, Attack1, Attack2, Jump, Hit, Death
}
