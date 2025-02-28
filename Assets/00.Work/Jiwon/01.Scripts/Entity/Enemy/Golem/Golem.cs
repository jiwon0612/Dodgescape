using System;
using Unity.Behavior;
using UnityEngine;

public class Golem : BTEnemy
{
    [field : SerializeField] public GameEventChannelSO cameraChannel { get;private set; }
    
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

    protected override void AfterInit()
    {
        base.AfterInit();
        _animationTrigger.OnAttackTrigger += _attackCompo.Attack;
        _animationTrigger.OnAttackTrigger2 += _attackCompo.Attack2;
        _animationTrigger.OnAttackTrigger3 += _attackCompo.Attack3;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        _animationTrigger.OnAttackTrigger -= _attackCompo.Attack;
        _animationTrigger.OnAttackTrigger2 -= _attackCompo.Attack2;
        _animationTrigger.OnAttackTrigger3 -= _attackCompo.Attack3;
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
