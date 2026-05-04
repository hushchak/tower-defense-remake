using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContextMenuEntry : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private Button button;

    public void Setup(string name, Action action)
    {
        text.text = name;
        button.onClick.AddListener(() => action?.Invoke());
    }
}
