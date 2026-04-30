using UnityEngine;

public static class SaveManager
{
    private static FileStorage<GameData> gameStorage;
    private static FileStorage<PreferencesData> preferencesStorage;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        gameStorage = new FileStorage<GameData>(
            GetPath("Data", "game.json"),
            () => new GameData()
        );

        preferencesStorage = new FileStorage<PreferencesData>(
            GetPath("Data", "preferences.json"),
            () => new PreferencesData()
        );
    }

    public static GameData GetGameData() =>
        gameStorage.Read();

    public static void SetPreferencesData(GameData data) =>
        gameStorage.Write(data);

    public static PreferencesData GetPreferencesData() =>
        preferencesStorage.Read();

    public static void SetPreferencesData(PreferencesData data) =>
        preferencesStorage.Write(data);

    private static string GetPath(string folder, string file) =>
        System.IO.Path.Combine(Application.persistentDataPath, folder, file);
}
