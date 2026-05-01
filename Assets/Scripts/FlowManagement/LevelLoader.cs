public static class LevelLoader
{
    public async static void LoadLevel(string levelName)
    {
        await SceneLoader.LoadScene(SceneList.Slots.Main, SceneList.Names.Level, new LevelArgs(levelName), true);
    }
}
