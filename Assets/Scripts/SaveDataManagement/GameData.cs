using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public List<string> UnlockedLevels;

    public GameData()
    {
        UnlockedLevels = new List<string>
        {
            SceneList.Names.Levels.Plains
        };
    }
}
