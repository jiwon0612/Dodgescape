using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/AnimationChangeEvent")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "AnimationChangeEvent", message: "change animation to [newanimation]", category: "Events", id: "d8743a0437eaff496ab66feaf0734131")]
public partial class AnimationChangeEvent : EventChannelBase
{
    public delegate void AnimationChangeEventEventHandler(string newanimation);
    public event AnimationChangeEventEventHandler Event; 

    public void SendEventMessage(string newanimation)
    {
        Event?.Invoke(newanimation);
    }

    public override void SendEventMessage(BlackboardVariable[] messageData)
    {
        BlackboardVariable<string> newanimationBlackboardVariable = messageData[0] as BlackboardVariable<string>;
        var newanimation = newanimationBlackboardVariable != null ? newanimationBlackboardVariable.Value : default(string);

        Event?.Invoke(newanimation);
    }

    public override Delegate CreateEventHandler(BlackboardVariable[] vars, System.Action callback)
    {
        AnimationChangeEventEventHandler del = (newanimation) =>
        {
            BlackboardVariable<string> var0 = vars[0] as BlackboardVariable<string>;
            if(var0 != null)
                var0.Value = newanimation;

            callback();
        };
        return del;
    }

    public override void RegisterListener(Delegate del)
    {
        Event += del as AnimationChangeEventEventHandler;
    }

    public override void UnregisterListener(Delegate del)
    {
        Event -= del as AnimationChangeEventEventHandler;
    }
}

