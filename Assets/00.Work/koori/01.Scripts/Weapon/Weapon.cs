using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private AttackDataSO _attackData;
    [SerializeField] private EntityFinderSO _entityFinder;

    private List<int> hitEntities = new List<int>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EntityHealth entity))
        {
            if (hitEntities.Contains(entity.GetInstanceID())) return;

            Vector3 dir = (entity.transform.position - transform.position).normalized;
            Vector2 knockBack = new Vector2(dir.x, dir.z) * _attackData.attackPower;

            entity.TakeDamage(_attackData.damage, knockBack, _attackData.stunDuration, _entityFinder.target);
            hitEntities.Add(entity.GetInstanceID());
        }
    }

    private void OnEndAttack()
    {
        hitEntities.Clear();
    }
}
