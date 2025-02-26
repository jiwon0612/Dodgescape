using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "FindTarget", story: "[self] set [target] from finder", category: "Action", id: "7cc9d897f156b18a2f7c3d4d52bb46ce")]
public partial class FindTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<BTEnemy> Self;
    [SerializeReference] public BlackboardVariable<Transform> Target;

    protected override Status OnStart()
    {
        Target.Value = Self.Value.PlayerFinder.target.transform;
        Debug.Assert(Target.Value != null, $"Target is null {Self.Value.gameObject.name}");
        return Status.Success;
    }
}

