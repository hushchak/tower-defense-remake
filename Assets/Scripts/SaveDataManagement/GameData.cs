using System.Collections.Generic;

public class GameData
{
    public List<string> UnlockedLevels { get; private set; }

    public GameData()
    {
        UnlockedLevels = new List<string>
        {
            "plains"
        };
    }
}
