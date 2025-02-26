using System;
using UnityEditor;
using UnityEngine;

public class OverlapDamageCaster : DamageCaster
{
    [SerializeField] private OverlapType overlapType;
    [SerializeField] private Vector3 damageBoxSize;
    [SerializeField] private float damageRadius;

    private Collider[] _hitResults;

    public override void InitCaster(Entity entity)
    {
        base.InitCaster(entity);
        _hitResults = new Collider[maxHitCount];
    }

    public override bool CastDamage(float damage, float knockBackPower)
    {
        int cnt = overlapType switch
        {
            OverlapType.Box => Physics.OverlapBoxNonAlloc(transform.position, damageBoxSize, _hitResults,
                transform.rotation, whatIsTarget),
            OverlapType.Circle => Physics.OverlapSphereNonAlloc(transform.position, damageRadius, _hitResults,
                whatIsTarget),
            _ => 0
        };

        for (int i = 0; i < cnt; i++)
        {
            Vector3 direction = (_hitResults[i].transform.position - _entity.transform.position).normalized;
            
            Vector2 knockBack = new Vector2(direction.x, direction.z) * knockBackPower;
            
            if (_hitResults[i].TryGetComponent(out EntityHealth health))
            {
                health.TakeDamage(damage, knockBack, .5f, _entity);
            }
        }

        return cnt > 0;
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        switch (overlapType)
        {
            case OverlapType.Box:
                Gizmos.color = Color.green; 
                Gizmos.matrix = transform.localToWorldMatrix;
                Gizmos.DrawWireCube(Vector3.zero, damageBoxSize);
                Gizmos.color = Color.white;
                break;
            case OverlapType.Circle:
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(transform.position, damageRadius);
                Gizmos.color = Color.white;
                break;
        }
    }

#endif
}

public enum OverlapType
{
    Circle, Box
}
