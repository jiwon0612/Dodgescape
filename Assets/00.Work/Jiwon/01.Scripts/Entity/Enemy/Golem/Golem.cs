using Unity.Behavior;
using UnityEngine;

public class Golem : BTEnemy
{
    
}

[BlackboardEnum]
public enum GolemState
{
    Idle, Chase, Attack1, Attack2, Jump, Hit, Death
}
