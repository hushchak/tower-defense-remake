using System;
using UnityEngine;

public class ContextMenuAutoHide : MonoBehaviour
{
    public event Action Hide;

    [SerializeField] private RectTransform menuRect;
    [SerializeField] private float worldPadding = 0.1f;
    [SerializeField] private float hideDelay = 0.2f;
    [SerializeField] private int framesToIgnore;

    private float timer;
    private int currentFramesToIgnore;

    private void OnEnable()
    {
        timer = hideDelay;
        currentFramesToIgnore = framesToIgnore;
    }

    void Update()
    {
        if (currentFramesToIgnore > 0)
        {
            currentFramesToIgnore--;
            return;
        }

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(InputReader.PointerPosition);

        Vector3[] corners = new Vector3[4];
        menuRect.GetWorldCorners(corners);

        Rect rect = new Rect(
            corners[0].x - worldPadding,
            corners[0].y - worldPadding,
            (corners[2].x - corners[0].x) + worldPadding * 2,
            (corners[2].y - corners[0].y) + worldPadding * 2
        );

        bool isInside = rect.Contains(mousePos);

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
}
