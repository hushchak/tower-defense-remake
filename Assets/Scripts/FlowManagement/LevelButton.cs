using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private string levelName;
    [SerializeField] private Image image;
    [SerializeField] private EventChannelString levelHoverChannel;
    [SerializeField] private Sound levelOpenSound;
    [Space]
    [SerializeField] private string levelDisplayName;
    [SerializeField] private Sprite lockedSprite;
    [SerializeField] private Sprite unlockedSprite;
    [SerializeField] private Sprite activeSprite;

    private void Awake()
    {
        image.sprite = LevelIsUnlocked(SaveManager.GetGameData().UnlockedLevels, levelName)
            ? unlockedSprite
            : lockedSprite;
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

    public void OnPointerClick(PointerEventData eventData)
    {
        Audio.Play(levelOpenSound, true);
        LevelLoader.LoadLevel(levelName);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.sprite = image.sprite == unlockedSprite
            ? activeSprite
            : image.sprite;

        levelHoverChannel.Raise(levelDisplayName);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.sprite = image.sprite == activeSprite
            ? unlockedSprite
            : image.sprite;

        levelHoverChannel.Raise("");
    }
}
