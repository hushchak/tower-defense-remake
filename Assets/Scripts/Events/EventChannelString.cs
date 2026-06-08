using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EventChannels/EventChannel String", fileName = "EventChannelString")]
public class EventChannelString : ScriptableObject
{
    private List<Action<string>> actions = new();

    public void Subscribe(Action<string> action) => actions.Add(action);
    public void Unsubscribe(Action<string> action) => actions.Remove(action);

    public void Raise(string data)
    {
        foreach (Action<string> actions in actions)
        {
            actions?.Invoke(data);
        }
    }
}
