using System;
using UnityEngine;

public class PauseManager : Singleton<PauseManager>
{
    [SerializeField] private EventChannelBool pauseChannel;
    public bool IsPaused { get; private set; } = false;

    private void OnEnable()
    {
        InputReader.OnBackPerformed += HandleBackPerformed;
    }

    private void OnDisable()
    {
        InputReader.OnBackPerformed -= HandleBackPerformed;
    }

    private void HandleBackPerformed()
    {
        IsPaused = !IsPaused;
        pauseChannel.Raise(IsPaused);
    }

    public void SetPause(bool pause)
    {
        if (pause == IsPaused)
            return;

        IsPaused = pause;
        pauseChannel.Raise(IsPaused);
    }
}
