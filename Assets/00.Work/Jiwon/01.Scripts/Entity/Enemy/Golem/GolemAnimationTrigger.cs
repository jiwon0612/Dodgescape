using System;
using UnityEngine;

public class GolemAnimationTrigger : EntityAnimationTrigger
{
    public event Action OnAttackTrigger2;
    
    public event Action OnAttackTrigger3;
    
    public void AttackTrigger2() => OnAttackTrigger2?.Invoke();
    public void AttackTrigger3() => OnAttackTrigger3?.Invoke();
    
}
