using Unity.Behavior;
using UnityEngine;

public class MushroomMan : BTEnemy
{
    
}

[BlackboardEnum]
public enum MushroomManState
{
    PTAROL, CHASE, ATTACK, STUN, HIT, DEATH
}
