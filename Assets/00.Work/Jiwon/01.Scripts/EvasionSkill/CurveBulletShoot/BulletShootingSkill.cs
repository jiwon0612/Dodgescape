using UnityEngine;

public class BulletShootingSkill : EvasionSkill
{
    [Header("SkillSetting")]
    [SerializeField] private int fireAmount;

    private EntityShooter _shooter;

    public override void InitSkill(Entity entity, EntityEvasionSkillCompo compo)
    {
        base.InitSkill(entity, compo);
        _shooter = entity.GetComp<EntityShooter>();
    }

    public override void UseSkill(Entity target)
    {
        base.UseSkill(target);
        for (int i = 0; i < fireAmount; i++)
        {
            _shooter.BulletTargetAndFire(target.transform);
        }
    }
}
