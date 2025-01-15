using System;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour,IEntityComp
{
    private Entity _entity;
    
    public float maxHealth = 10f;
    private float _currentHealth;

    public UnityEvent OnDeathEvent;
    public UnityEvent<float> OnHitEvent;

    public void Initialize(Entity entity)
    {
        _entity = entity;
        _currentHealth = maxHealth;
    }

    public void Reset()
    {
        _currentHealth = maxHealth;
    }

    public void TakeDamage(float damage, Vector3 originPoint, float stunDuration)
    {
        _currentHealth -= damage;
        _entity.Stun(originPoint, stunDuration);
        OnHitEvent?.Invoke(_currentHealth);
    }
}
