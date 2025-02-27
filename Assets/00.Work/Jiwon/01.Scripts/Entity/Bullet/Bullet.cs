using System;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [Header("Settings")] 
    [SerializeField] protected float speed;
    [SerializeField] protected float lifeTime;
    [SerializeField] protected AttackDataSO attackData;

    protected float _currentTime;
    
    protected Rigidbody _rbCompo;
    protected DamageCaster _damageCaster;

    public bool IsFire { get; protected set; }
    
    private void Awake()
    {
        _rbCompo = GetComponent<Rigidbody>();
        _damageCaster = GetComponentInChildren<DamageCaster>();
    }

    public virtual void InitAndFire(Entity dealer, Vector3 direction)
    {
        _rbCompo.excludeLayers = dealer.gameObject.layer;
        IsFire = true;
    }

    public virtual void InitAndFire(Entity dealer, Transform target)
    {
        _rbCompo.excludeLayers = dealer.gameObject.layer;
        IsFire = true;
    }

    protected virtual void Update()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime >= lifeTime)
        {
            _damageCaster.CastDamage(attackData.damage, attackData.attackPower, attackData.stunDuration);
            IsFire = false;
            Destroy(gameObject);
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        _damageCaster.CastDamage(attackData.damage, attackData.attackPower, attackData.stunDuration);
        IsFire = false;
        Destroy(gameObject);
    }
}