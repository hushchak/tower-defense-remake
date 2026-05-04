using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EventChannels/EventChannel ContextMenuResponce", fileName = "EventChannelContextMenuResponce")]
public class EventChannelContextMenuResponce : ScriptableObject
{
    private List<Action<ContextMenuResponce>> actions = new();

    public void Subscribe(Action<ContextMenuResponce> action) => actions.Add(action);
    public void Unsubscribe(Action<ContextMenuResponce> action) => actions.Remove(action);

    public void Raise(ContextMenuResponce data)
    {
        foreach (Action<ContextMenuResponce> actions in actions)
        {
            actions?.Invoke(data);
        }
    }
}
