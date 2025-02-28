using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private AttackDataSO _attackData;
    [SerializeField] private EntityFinderSO _entityFinder;
    [SerializeField] private GameEventChannelSO _playerEventChannel;
    [SerializeField] private GameEventChannelSO _cameraShakeChannel;

    private List<int> hitEntities = new List<int>();
    private ParticleSystem _particle;

    private bool _canAttack = false;

    private void Awake()
    {
        _particle = GetComponentInChildren<ParticleSystem>();
    }
    
    private void OnEnable()
    {
        _playerEventChannel.AddListener<AttackEvent>(OnAttackEvent);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EntityHealth entity))
        {
            if (hitEntities.Contains(entity.GetInstanceID())) return;
            if (!_canAttack) return;

            Vector3 dir = (entity.transform.position - transform.position).normalized;
            Vector2 knockBack = new Vector2(dir.x, dir.z) * _attackData.attackPower;

            entity.TakeDamage(_attackData.damage, knockBack, _attackData.stunDuration, _entityFinder.target);
            hitEntities.Add(entity.GetInstanceID());
            _particle.Play();
        }
    }

    private void OnDisable()
    {
        _playerEventChannel.RemoveListener<AttackEvent>(OnAttackEvent);
    }

    private void OnAttackEvent(AttackEvent attackEvent)
    {
        _canAttack = attackEvent.isAttacking;

        if (attackEvent.isAttacking)
            hitEntities.Clear();
    }
}
