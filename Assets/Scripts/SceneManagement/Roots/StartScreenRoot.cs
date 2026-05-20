using UnityEngine;

public class StartScreenRoot : SceneRoot
{
    [SerializeField] private Sound startGameSound;

    public override void Initialize(SceneArgs args)
    {
        InputReader.OnStartPerformed += LoadLevelMenu;
    }

    private async void LoadLevelMenu()
    {
        InputReader.OnStartPerformed -= LoadLevelMenu;
        Audio.Play(startGameSound, true);
        await SceneLoader.LoadScene(SceneList.Slots.Main, SceneList.Names.LevelMenu, SceneArgs.Empty);
    }
}
