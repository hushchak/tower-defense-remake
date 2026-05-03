using TMPro;
using UnityEngine;

public class ContextMenuEntry : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    public void Setup(string name)
    {
        text.text = name;
    }
}
