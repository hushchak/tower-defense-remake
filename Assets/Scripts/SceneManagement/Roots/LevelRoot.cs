using System;
using UnityEngine;

public class LevelRoot : SceneRoot
{
    [SerializeField] private EventChannel LevelLoadedChannel;
    [SerializeField] private EventChannel LevelExitInitiationChannel;
    [Space]
    [SerializeField] private EventChannel allWaveEndChannel;
    [SerializeField] private EventChannel deathChannel;
    [SerializeField] private EventChannelBool levelEndChannel;

    public override void Initialize(SceneArgs args)
    {
        if (args is not LevelArgs)
        {
            Debug.LogError("You are trying to load LevelRoot with invalid args");
            return;
        }

        LevelArgs levelArgs = (LevelArgs)args;

        Debug.Log($"Loading level: {levelArgs.LevelName}");
        Level level = Instantiate(Resources.Load<Level>($"Levels/{levelArgs.LevelName}"));
        LevelLoadedChannel.Raise();
        Debug.Log($"Level {levelArgs.LevelName} is loaded");

        LevelExitInitiationChannel.Subscribe(HandleLevelExit);
        allWaveEndChannel.Subscribe(HandleAllWaveEnd);
        deathChannel.Subscribe(HandleDeath);
    }

    private void OnDestroy()
    {
        LevelExitInitiationChannel.Unsubscribe(HandleLevelExit);
        allWaveEndChannel.Unsubscribe(HandleAllWaveEnd);
        deathChannel.Unsubscribe(HandleDeath);
    }

    private async void HandleLevelExit()
    {
        await SceneLoader.LoadScene(SceneList.Slots.Main, SceneList.Names.LevelMenu, SceneArgs.Empty, true);
    }

    private void HandleAllWaveEnd()
    {
        PauseManager.Instance.SetPause(true);
        levelEndChannel.Raise(true);
    }

    private void HandleDeath()
    {
        PauseManager.Instance.SetPause(true);
        levelEndChannel.Raise(false);
    }
}
