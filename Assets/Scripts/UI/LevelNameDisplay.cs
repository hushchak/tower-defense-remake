using System;
using TMPro;
using UnityEngine;

public class LevelNameDisplay : MonoBehaviour
{
    [SerializeField] private EventChannelString levelHoverChannel;
    [SerializeField] private TMP_Text displayText;

    private void Awake() => displayText.text = "";

    private void OnEnable()
    {
        levelHoverChannel.Subscribe(HandleDisplayText);
    }

    private void OnDisable()
    {
        levelHoverChannel.Unsubscribe(HandleDisplayText);
    }

    private void HandleDisplayText(string text)
    {
        displayText.text = text;
    }
}
