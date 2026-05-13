using System;
using UnityEngine;

public class LevelRoot : SceneRoot
{
    [SerializeField] private EventChannel LevelLoadedChannel;
    [SerializeField] private EventChannel LevelExitInitiationChannel;

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
    }

    private void OnDestroy()
    {
        LevelExitInitiationChannel.Unsubscribe(HandleLevelExit);
    }

    private async void HandleLevelExit()
    {
        await SceneLoader.LoadScene(SceneList.Slots.Main, SceneList.Names.LevelMenu, SceneArgs.Empty, true);
    }
}
