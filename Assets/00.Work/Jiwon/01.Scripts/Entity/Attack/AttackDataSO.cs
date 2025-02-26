using UnityEngine;

[CreateAssetMenu(fileName = "AttackData", menuName = "SO/AttData")]
public class AttackDataSO : ScriptableObject
{
    public float damage;
    public float movement;
    public float attackPower;
    public float stunDuration;
}
