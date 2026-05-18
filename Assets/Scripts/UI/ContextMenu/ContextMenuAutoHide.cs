using System;
using UnityEngine;

public class ContextMenuAutoHide : MonoBehaviour
{
    public event Action Hide;

    [SerializeField] private RectTransform menuRect;
    [SerializeField] private float hideDelay = 0.2f;

    private float timer;

    void Update()
    {
        Vector2 mousePos = InputReader.PointerPosition;

        bool isInside = RectTransformUtility.RectangleContainsScreenPoint(
            menuRect,
            mousePos,
            Camera.main
        );

        if (isInside)
        {
            timer = hideDelay;
        }
        else
        {
            timer -= Time.unscaledDeltaTime;

            if (timer <= 0f)
            {
                Hide?.Invoke();
            }
        }
    }

    private void OnEnable()
    {
        timer = hideDelay;
    }
}
