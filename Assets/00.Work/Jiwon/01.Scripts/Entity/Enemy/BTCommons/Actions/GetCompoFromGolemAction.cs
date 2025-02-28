using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "GetCompoFromGolem", story: "get compo from [self]", category: "Action", id: "d007161bad15676a10e0d064e903307f")]
public partial class GetCompoFromGolemAction : Action
{
    [SerializeReference] public BlackboardVariable<BTEnemy> Self;

    protected override Status OnStart()
    {
        BTEnemy enemy = Self.Value;
        
        SetVariableToBT(enemy, "MainAnimator", enemy.GetComponentInChildren<Animator>());
        SetVariableToBT(enemy, "Mover", enemy.GetComp<EntityNavMeshMover>());
        SetVariableToBT(enemy, "AnimationTrigger", enemy.GetComp<GolemAnimationTrigger>());
        return Status.Success;
    }
    
    private void SetVariableToBT<T>(BTEnemy enemy, string variableName, T component)
    {
        Debug.Assert(component != null, $"Check {variableName} component exist on {enemy.gameObject.name}");
        BlackboardVariable<T> variable = enemy.GetBlackboardVariable<T>(variableName);
        variable.Value = component;
    }
}

