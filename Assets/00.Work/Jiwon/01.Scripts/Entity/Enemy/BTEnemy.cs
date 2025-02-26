using System;
using Unity.Behavior;
using UnityEngine;

public class BTEnemy : Entity
{
    [field : SerializeField] public EntityFinderSO PlayerFinder { get; protected set; }
    
    protected BehaviorGraphAgent _btAgent;
    protected EntityNavMeshMover _navMeshMover;
    protected EntityHealth _health;
    
    public bool IsDead { get; protected set; }

    protected override void InitComp()
    {
        _btAgent = GetComponent<BehaviorGraphAgent>();
        IsDead = false;
        base.InitComp();
    }

    protected override void AfterInit()
    {
        base.AfterInit();
        _navMeshMover = GetComp<EntityNavMeshMover>();
        _health = GetComp<EntityHealth>();
        
        _health.OnHitEvent += ApplyDamage;
        _health.OnDeathEvent.AddListener(Death);
    }

    protected virtual void OnDestroy()
    {
        _health.OnHitEvent -= ApplyDamage;
        _health.OnDeathEvent.RemoveListener(Death);
    }

    public override void ApplyDamage(float damage, Vector2 knockBack, float stunDuration)
    {
        _navMeshMover.KnockBack(knockBack, stunDuration);
    }

    public BlackboardVariable<T> GetBlackboardVariable<T>(string key)
    {
        if (_btAgent.GetVariable(key, out BlackboardVariable<T> result))
        {
            return result;
        }
        return default;
    }

    public virtual void Death()
    {
        IsDead = true;
    }
}

[BlackboardEnum]
public enum EnemyState
{
    PTAROL, CHASE, ATTACK, STUN, HIT, DEATH
}
    
