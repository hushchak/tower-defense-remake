using UnityEngine;

public class StartScreenRoot : SceneRoot
{
    public override void Initialize(SceneArgs args)
    {
        InputReader.OnStartPerformed += LoadLevelMenu;
    }

    private async void LoadLevelMenu()
    {
        InputReader.OnStartPerformed -= LoadLevelMenu;
        await SceneLoader.LoadScene(SceneList.Slots.Main, SceneList.Names.LevelMenu, SceneArgs.Empty);
    }
}
