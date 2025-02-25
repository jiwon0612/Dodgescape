using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Stop", story: "stop with [mover]", category: "Action", id: "b621f8a07a71b5138b8eb9cd1fe283af")]
public partial class StopAction : Action
{
    [SerializeReference] public BlackboardVariable<EntityMover> Mover;

    protected override Status OnStart()
    {
        Mover.Value.StopImmediately();
        return Status.Success;
    }
}

