using System;
using UnityEngine;

public class EnemyAttackCompo : MonoBehaviour, IEntityComp
{
    public float attackDistance;
    public float detectDistance;
    
    [SerializeField] private string attackRangeName, detectRangeName;

    [SerializeField] private AttackDataSO _attackData;
    
    public DamageCaster caster;

    private BTEnemy _btEnemy;
    
    public void Initialize(Entity entity)
    {
        _btEnemy = entity as BTEnemy;
        Debug.Assert(_btEnemy != null, $"is Not BTEnemy{entity.gameObject.name}");
        
        caster.InitCaster(_btEnemy);
    }

    private void Start()
    {
        _btEnemy.GetBlackboardVariable<float>(detectRangeName).Value = detectDistance;
        _btEnemy.GetBlackboardVariable<float>(attackRangeName).Value = attackDistance;
        
    }

    public void Attack()
    {
        caster.CastDamage(_attackData.damage, _attackData.attackPower);
    }

#if UNITY_EDITOR

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position,detectDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,attackDistance);
        Gizmos.color = Color.white;
    }

#endif
}
