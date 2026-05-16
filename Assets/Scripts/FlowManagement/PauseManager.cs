using System;
using UnityEngine;

public class PauseManager : Singleton<PauseManager>
{
    [SerializeField] private EventChannelBool pauseChannel;
    public bool IsPaused { get; private set; } = false;

    public void SetPause(bool pause)
    {
        if (pause == IsPaused)
            return;

        IsPaused = pause;
        pauseChannel.Raise(IsPaused);
    }
}
