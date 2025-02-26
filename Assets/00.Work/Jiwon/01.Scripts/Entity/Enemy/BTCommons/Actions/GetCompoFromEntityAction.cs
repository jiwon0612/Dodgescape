using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "GetCompoFromEntity", story: "Get components from [btEnemy]", category: "Action",
    id: "bda578f09b218f76b93e8aa748656077")]
public partial class GetCompoFromEntityAction : Action
{
    [SerializeReference] public BlackboardVariable<BTEnemy> BtEnemy;

    protected override Status OnStart()
    {
        BTEnemy enemy = BtEnemy.Value;

        SetVariableToBT(enemy, "Mover", enemy.GetComp<EntityNavMeshMover>(true));
        SetVariableToBT(enemy, "MainAnimator", enemy.GetComponentInChildren<Animator>());
        SetVariableToBT(enemy, "AnimationTrigger", enemy.GetComponentInChildren<EntityAnimationTrigger>());
        
        return Status.Success;
    }

    private void SetVariableToBT<T>(BTEnemy enemy, string variableName, T component)
    {
        Debug.Assert(component != null, $"component is null {enemy.gameObject.name}");
        BlackboardVariable<T> variable = enemy.GetBlackboardVariable<T>(variableName);
        variable.Value = component;
    }
}

