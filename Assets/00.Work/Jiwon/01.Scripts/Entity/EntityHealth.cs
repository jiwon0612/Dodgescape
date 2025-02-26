using System;
using UnityEngine;
using UnityEngine.Events;

public class EntityHealth : MonoBehaviour,IEntityComp
{
    private Entity _entity;
    
    public float maxHealth = 10f;
    private float _currentHealth;
    
    public UnityEvent OnDeathEvent;
    public UnityEvent OnEvasionEvent;
    public event Action<float, Vector2, float> OnHitEvent;
    
    public bool IsCanHit { get; set; }
    public bool IsEvasion { get; protected set; }
    
    public void Initialize(Entity entity)
    {
        _entity = entity;
        _currentHealth = maxHealth;
        IsCanHit = true;
        IsEvasion = false;
    }

    public void Reset()
    {
        _currentHealth = maxHealth;
    }

    public void TakeDamage(float damage, Vector2 knockBack, float stunDuration, Entity dealer)
    {
        if (!IsCanHit) return;

        if (IsEvasion)
        {
            OnEvasionEvent?.Invoke();
            return;
        }
        
        _currentHealth -= damage;
        OnHitEvent?.Invoke(damage, knockBack, stunDuration);
        
        if (_currentHealth <= 0)
            OnDeathEvent?.Invoke();
    }
}
