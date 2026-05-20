using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EventChannels/EventChannel WaveData", fileName = "EventChannelContextMenuRequest")]
public class EventChannelWaveData : ScriptableObject
{
    private List<Action<WaveData>> actions = new();

    public void Subscribe(Action<WaveData> action) => actions.Add(action);
    public void Unsubscribe(Action<WaveData> action) => actions.Remove(action);

    public void Raise(WaveData data)
    {
        foreach (Action<WaveData> actions in actions)
        {
            actions?.Invoke(data);
        }
    }
}
