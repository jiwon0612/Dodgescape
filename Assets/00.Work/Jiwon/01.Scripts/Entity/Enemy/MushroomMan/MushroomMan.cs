using System;
using Unity.Behavior;
using UnityEngine;

public class MushroomMan : BTEnemy
{
    private EnemyStateChangeEvent _stateChannel;
    
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
    }
}
