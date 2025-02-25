using Unity.Behavior;
using UnityEngine;

public class BTEnemy : Entity
{
    [field : SerializeField] public EntityFinderSO PlayerFinder { get; protected set; }
    
    protected BehaviorGraphAgent _btAgent;

    protected override void InitComp()
    {
        base.InitComp();
        _btAgent = GetComponent<BehaviorGraphAgent>();
    }

    public BlackboardVariable<T> GetBlackboardVariable<T>(string key)
    {
        if (_btAgent.GetVariable(key, out BlackboardVariable<T> result))
        {
            return result;
        }
        return default;
    }
}

[BlackboardEnum]
public enum EnemyState
{
    PTAROL, CHASE, ATTACK, STUN, HIT, DEATH
}
    
