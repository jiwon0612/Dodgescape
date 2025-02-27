using UnityEngine;

public class EntityShooter : MonoBehaviour, IEntityComp
{
    public Bullet bulletPrefab;
    
    private Entity _entity;

    public void Initialize(Entity entity)
    {
        _entity = entity;
    }

    public void BulletDirAndFire(Vector3 direction)
    {
        Bullet bullet = Instantiate(bulletPrefab);
        bullet.InitAndFire(_entity, direction);
    }

    public void BulletTargetAndFire(Transform target)
    {
        Bullet bullet = Instantiate(bulletPrefab);
        bullet.InitAndFire(_entity, target);
    }
}
