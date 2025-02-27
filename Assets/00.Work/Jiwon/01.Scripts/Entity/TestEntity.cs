using System;
using UnityEngine;

public class TestEntity : Entity
{
    [SerializeField] private Transform _target;
    private EntityShooter _shooter;
    
    protected override void AfterInit()
    {
        base.AfterInit();
        _shooter = GetComp<EntityShooter>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            _shooter.BulletTargetAndFire(_target);
        }
    }

    public override void ApplyDamage(float damage, Vector2 knockBack, float stunDuration)
    {
        
    }
}
