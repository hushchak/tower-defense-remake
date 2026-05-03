using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EventChannels/EventChannel ContextMenuRequest", fileName = "EventChannelContextMenuRequest")]
public class EventChannelContextMenuRequest : ScriptableObject
{
    private List<Action<ContextMenuRequest>> actions = new();

    public void Subscribe(Action<ContextMenuRequest> action) => actions.Add(action);
    public void Unsubscribe(Action<ContextMenuRequest> action) => actions.Remove(action);

    public void Raise(ContextMenuRequest data)
    {
        foreach (Action<ContextMenuRequest> actions in actions)
        {
            actions?.Invoke(data);
        }
    }
}
