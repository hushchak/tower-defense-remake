using UnityEngine;

public class LevelRoot : SceneRoot
{
    [SerializeField] private EventChannel LevelLoadedChannel;

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

        // TODO: Intialization of level prefab
    }
}
