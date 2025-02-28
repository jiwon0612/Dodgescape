using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/GolemStateChangeEvent")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "GolemStateChangeEvent", message: "golem state chnage to [newState]", category: "Events", id: "9b302d66bf7119d28147cdc1a6c50ce4")]
public partial class GolemStateChangeEvent : EventChannelBase
{
    public delegate void GolemStateChangeEventEventHandler(GolemState newState);
    public event GolemStateChangeEventEventHandler Event; 

    public void SendEventMessage(GolemState newState)
    {
        Event?.Invoke(newState);
    }

    public override void SendEventMessage(BlackboardVariable[] messageData)
    {
        BlackboardVariable<GolemState> newStateBlackboardVariable = messageData[0] as BlackboardVariable<GolemState>;
        var newState = newStateBlackboardVariable != null ? newStateBlackboardVariable.Value : default(GolemState);

        Event?.Invoke(newState);
    }

    public override Delegate CreateEventHandler(BlackboardVariable[] vars, System.Action callback)
    {
        GolemStateChangeEventEventHandler del = (newState) =>
        {
            BlackboardVariable<GolemState> var0 = vars[0] as BlackboardVariable<GolemState>;
            if(var0 != null)
                var0.Value = newState;

            callback();
        };
        return del;
    }

    public override void RegisterListener(Delegate del)
    {
        Event += del as GolemStateChangeEventEventHandler;
    }

    public override void UnregisterListener(Delegate del)
    {
        Event -= del as GolemStateChangeEventEventHandler;
    }
}

