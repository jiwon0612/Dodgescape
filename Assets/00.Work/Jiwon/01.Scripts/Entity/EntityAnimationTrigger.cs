using System;
using Unity.VisualScripting;
using UnityEngine;

public class EntityAnimationTrigger : MonoBehaviour, IEntityComp
{
    public event Action OnAttackTrigger;
    public event Action OnAnimationEndTrigger;
    
    private Entity _entity;
    
    public void Initialize(Entity entity)
    {
        _entity = entity;
    }

    public void AttackTrigger() => OnAttackTrigger.Invoke();
    public void AnimationEndTrigger() => OnAnimationEndTrigger.Invoke();
}
