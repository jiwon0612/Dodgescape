using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "WaitForAnimationTrigger", story: "wait for [trigger] end", category: "Action", id: "56043dd224ca11cc1d111b58163e5af8")]
public partial class WaitForAnimationTriggerAction : Action
{
    [SerializeReference] public BlackboardVariable<EntityAnimationTrigger> Trigger;

    private bool _animationEndTrigger;

    protected override Status OnStart()
    {
        _animationEndTrigger = false;
        Trigger.Value.OnAnimationEndTrigger += HandleAnimationEnd;
        
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return _animationEndTrigger ? Status.Success : Status.Running;
    }

    protected override void OnEnd()
    {
        Trigger.Value.OnAnimationEndTrigger -= HandleAnimationEnd;
    }
    
    private void HandleAnimationEnd()
    {
        _animationEndTrigger = true;
    }
}

