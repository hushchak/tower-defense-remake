using System;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private EventChannelBool levelEndChannel;
    [Space]
    [SerializeField] private string[] nextLevelsNames;

    private void Awake()
    {
        levelEndChannel.Subscribe(HandleLevelEnd);
    }

    private void OnDestroy()
    {
        levelEndChannel.Unsubscribe(HandleLevelEnd);
    }

    private void HandleLevelEnd(bool end)
    {
        if (end && nextLevelsNames.Length > 0)
        {
            GameData data = SaveManager.GetGameData();
            data.UnlockedLevels.AddRange(nextLevelsNames);
            SaveManager.SetGameData(data);
        }
    }
}
