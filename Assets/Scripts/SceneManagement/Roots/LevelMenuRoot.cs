using UnityEngine;

public class LevelMenuRoot : SceneRoot
{
    [SerializeField] private Sound QuitSoundSound;

    public override void Initialize(SceneArgs args)
    {
        InputReader.OnBackPerformed += LoadStartScreen;
    }

    private void OnDestroy()
    {
        InputReader.OnBackPerformed -= LoadStartScreen;
    }

    private async void LoadStartScreen()
    {
        InputReader.OnBackPerformed -= LoadStartScreen;
        Audio.Play(QuitSoundSound, true);
        await SceneLoader.LoadScene(SceneList.Slots.Main, SceneList.Names.StartScreen, SceneArgs.Empty);
    }
}
