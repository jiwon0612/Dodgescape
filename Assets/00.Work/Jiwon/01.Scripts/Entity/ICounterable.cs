using UnityEngine;

public interface ICounterable
{
    public bool IsCanCounter { get; }
    
    public Transform TargetTrm { get; }
    
    public void ApplyCounter(float damage, Vector2 direction, Vector2 knockBackForce, Entity dealer);
}
