using UnityEngine;

public class EntityShooter : MonoBehaviour, IEntityComp
{
    public Bullet bulletPrefab;
    
    private Entity _entity;
    
    public void Initialize(Entity entity)
    {
        _entity = entity;
    }

    public void SetBulletDirAndFire(Vector3 direction)
    {
        bulletPrefab.InitAndFire(_entity,direction);
    }

    public void SetBulletTargetAndFire(Transform target)
    {
        bulletPrefab.InitAndFire(_entity,target);
    }
}
