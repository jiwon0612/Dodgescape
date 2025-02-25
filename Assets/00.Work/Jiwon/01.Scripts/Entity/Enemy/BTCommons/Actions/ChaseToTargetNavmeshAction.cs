using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ChaseToTargetNavmesh", story: "[self] chase to [target] with [mover]", category: "Action", id: "e84f395d3ea78c385c62474f76a44375")]
public partial class ChaseToTargetNavmeshAction : Action
{
    [SerializeReference] public BlackboardVariable<BTEnemy> Self;
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<EntityMover> Mover;

    protected override Status OnUpdate()
    {
        Mover.Value.SetMovement(Target.Value.position);
        return Status.Running;
    }

}

