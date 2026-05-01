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
        // TODO: Get level prefab from Recources folder and instantiate it
        // TODO: Intialization of level prefab
    }
}
