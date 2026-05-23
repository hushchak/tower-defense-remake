using TMPro;
using UnityEngine;

public class TextBlink : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private float rate = 1f;

    private Color regularColor;
    private float nextTimeBlink;

    private void Awake()
    {
        regularColor = text.color;
        nextTimeBlink = rate;
    }

    private void Update()
    {
        if (nextTimeBlink < Time.time)
        {
            text.color = text.color.a == 0
            ? regularColor
            : new Color(regularColor.r, regularColor.g, regularColor.b, 0);

            nextTimeBlink = Time.time + rate;
        }
    }
}
