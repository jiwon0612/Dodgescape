using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ChaseToTargetNavmesh", story: "[self] chase to [target] with [mover]", category: "Action", id: "94a395b74d65235a982fbee11a464a22")]
public partial class ChaseToTargetNavmeshAction : Action
{
    [SerializeReference] public BlackboardVariable<BTEnemy> Self;
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<EntityNavMeshMover> Mover;

    protected override Status OnUpdate()
    {
        Mover.Value.SetMovement(Target.Value.position);
        return Status.Running;
    }
}

