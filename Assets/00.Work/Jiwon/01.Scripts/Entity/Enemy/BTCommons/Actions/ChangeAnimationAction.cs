using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ChangeAnimation", story: "[animator] change [current] to [next]", category: "Action", id: "a76bbbe7378f41a8dcc7f27f4e510aea")]
public partial class ChangeAnimationAction : Action
{
    [SerializeReference] public BlackboardVariable<Animator> Animator;
    [SerializeReference] public BlackboardVariable<string> Current;
    [SerializeReference] public BlackboardVariable<string> Next;

    protected override Status OnStart()
    {
        Animator.Value.SetBool(Current.Value, false);
        Current.Value = Next.Value;
        Animator.Value.SetBool(Current.Value, true);
        return Status.Success;
    }
}

