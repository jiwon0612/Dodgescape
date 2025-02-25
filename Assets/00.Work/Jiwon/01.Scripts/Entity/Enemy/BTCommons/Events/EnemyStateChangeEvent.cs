using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/EnemyStateChangeEvent")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "EnemyStateChangeEvent", message: "enemy state change to [newState]", category: "Events", id: "1b4b07845466c25fbd4600210cc821e8")]
public partial class EnemyStateChangeEvent : EventChannelBase
{
    public delegate void EnemyStateChangeEventEventHandler(EnemyState newState);
    public event EnemyStateChangeEventEventHandler Event; 

    public void SendEventMessage(EnemyState newState)
    {
        Event?.Invoke(newState);
    }

    public override void SendEventMessage(BlackboardVariable[] messageData)
    {
        BlackboardVariable<EnemyState> newStateBlackboardVariable = messageData[0] as BlackboardVariable<EnemyState>;
        var newState = newStateBlackboardVariable != null ? newStateBlackboardVariable.Value : default(EnemyState);

        Event?.Invoke(newState);
    }

    public override Delegate CreateEventHandler(BlackboardVariable[] vars, System.Action callback)
    {
        EnemyStateChangeEventEventHandler del = (newState) =>
        {
            BlackboardVariable<EnemyState> var0 = vars[0] as BlackboardVariable<EnemyState>;
            if(var0 != null)
                var0.Value = newState;

            callback();
        };
        return del;
    }

    public override void RegisterListener(Delegate del)
    {
        Event += del as EnemyStateChangeEventEventHandler;
    }

    public override void UnregisterListener(Delegate del)
    {
        Event -= del as EnemyStateChangeEventEventHandler;
    }
}

