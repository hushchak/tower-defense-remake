using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private string levelName;

    private void Awake()
    {
        button.interactable = LevelIsUnlocked(SaveManager.GetGameData().UnlockedLevels, levelName);
        button.onClick.AddListener(LoadLevel);
    }

    private void LoadLevel()
    {
        button.onClick.RemoveListener(LoadLevel);
        LevelLoader.LoadLevel(levelName);
    }

    private bool LevelIsUnlocked(List<string> unlockedLevels, string levelName)
    {
        for (int i = 0; i < unlockedLevels.Count; i++)
        {
            if (unlockedLevels[i] == levelName)
                return true;
        }
        return false;
    }
}
