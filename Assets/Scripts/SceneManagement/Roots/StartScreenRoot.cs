using System;
using UnityEngine;

public class StartScreenRoot : SceneRoot
{
    [SerializeField] private Sound startGameSound;

    public override void Initialize(SceneArgs args)
    {
        InputReader.OnStartPerformed += LoadLevelMenu;
        InputReader.OnBackPerformed += ExitGame;
    }

    private void OnDestroy()
    {
        InputReader.OnStartPerformed -= LoadLevelMenu;
        InputReader.OnBackPerformed -= ExitGame;
    }

    private async void LoadLevelMenu()
    {
        InputReader.OnStartPerformed -= LoadLevelMenu;
        Audio.Play(startGameSound, true);
        await SceneLoader.LoadScene(SceneList.Slots.Main, SceneList.Names.LevelMenu, SceneArgs.Empty);
    }

    private void ExitGame() => Application.Quit();
}
