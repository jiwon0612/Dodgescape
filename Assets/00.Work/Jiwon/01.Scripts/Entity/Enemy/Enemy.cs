using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Entity
{
    [field : SerializeField] public float DetectionDistance {get; private set; }
    
    private NavMeshAgent agent;

    
}
