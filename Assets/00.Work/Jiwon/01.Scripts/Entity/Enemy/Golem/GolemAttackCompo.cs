using Unity.Cinemachine;
using UnityEngine;

public class GolemAttackCompo : EnemyAttackCompo
{
    [Header("GolemSetting")]
    
    public AttackDataSO attackData2;
    public AttackDataSO attackData3;

    public DamageCaster caster2;
    public DamageCaster caster3;
    
    private ParticleSystem _particle;
    private Golem _golem;

    public override void Initialize(Entity entity)
    {
        base.Initialize(entity);
        caster2.InitCaster(entity);
        caster3.InitCaster(entity);
        
        _golem = entity as Golem;
        
        _particle = GetComponentInChildren<ParticleSystem>();
    }

    public void Attack2()
    {
        caster2.CastDamage(attackData2.damage, attackData2.attackPower, attackData2.stunDuration);
    }

    public void Attack3()
    {
        CamShakeEvent shakeEvt = CameraEvents.CameraShake;
        shakeEvt.shakePower = 1.5f;
        shakeEvt.shakeDirection = Vector3.down;
        shakeEvt.shakeTime = 0.25f;
        shakeEvt.impulseShapes = CinemachineImpulseDefinition.ImpulseShapes.Explosion;
        
        _golem.cameraChannel.RaiseEvent(shakeEvt);
        
        caster3.CastDamage(attackData3.damage, attackData3.attackPower, attackData3.stunDuration);
        _particle.Play();
    }
}
