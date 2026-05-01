public class LevelArgs : SceneArgs
{
    public string LevelName { get; private set; }

    public LevelArgs(string levelName)
    {
        LevelName = levelName;
    }
}
