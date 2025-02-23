using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Move", story: "[self] move with [mover]", category: "Action", id: "32b75b49d00beb325d5bb1fb087d8965")]
public partial class MoveAction : Action
{
    [SerializeReference] public BlackboardVariable<BTEnemy> Self;
    [SerializeReference] public BlackboardVariable<EntityMover> Mover;

    protected override Status OnStart()
    {
        EntityNavMeshMover navMeshMover = Mover.Value as EntityNavMeshMover;
        
        Mover.Value.SetMovement(navMeshMover.test.position);
        return Status.Success;
    }

}

