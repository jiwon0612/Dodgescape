using UnityEngine;

public abstract class DamageCaster : MonoBehaviour
{
    [SerializeField] protected int maxHitCount = 3;
    [SerializeField] protected LayerMask whatIsTarget;
    
    protected Entity _entity;

    public virtual void InitCaster(Entity entity)
    {
        _entity = entity;
    }

    public abstract bool CastDamage(float damage, Vector2 knockBack);
}
