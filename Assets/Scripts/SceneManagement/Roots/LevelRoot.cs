using UnityEngine;

public class LevelRoot : SceneRoot
{
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
        Debug.Log($"Level {levelArgs.LevelName} is loaded");

        // TODO: Intialization of level prefab
    }
}
