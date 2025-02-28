using UnityEngine;

public class GolemAttackCompo : EnemyAttackCompo
{
    [Header("GolemSetting")]
    
    public AttackDataSO attackData2;
    public AttackDataSO attackData3;

    public DamageCaster caster2;
    public DamageCaster caster3;
    
    private ParticleSystem _particle;

    public override void Initialize(Entity entity)
    {
        base.Initialize(entity);
        caster2.InitCaster(entity);
        caster3.InitCaster(entity);
        
        _particle = GetComponentInChildren<ParticleSystem>();
    }

    public void Attack2()
    {
        caster2.CastDamage(attackData2.damage, attackData2.attackPower, attackData2.stunDuration);
    }

    public void Attack3()
    {
        caster3.CastDamage(attackData3.damage, attackData3.attackPower, attackData3.stunDuration);
        _particle.Play();
    }
}
