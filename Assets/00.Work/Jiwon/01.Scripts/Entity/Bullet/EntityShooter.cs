using UnityEngine;

public class EntityShooter : MonoBehaviour, IEntityComp
{
    public Bullet bullet;
    
    private Entity _entity;
    
    public void Initialize(Entity entity)
    {
        _entity = entity;
    }
}
