using Unity.Behavior;
using UnityEngine;

public class MushroomMan : Enemy
{
    
}

[BlackboardEnum]
public enum MushroomManState
{
    PTAROL, CHASE, ATTACK, STUN, HIT, DEATH
}
