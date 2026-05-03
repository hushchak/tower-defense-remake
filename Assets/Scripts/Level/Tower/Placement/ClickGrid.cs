using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickGrid : MonoBehaviour, IPointerClickHandler
{
    public event Action<Vector2> OnClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"Clicked at: {Camera.main.ScreenToWorldPoint(InputReader.PointerPosition)}");
        OnClick?.Invoke(InputReader.PointerPosition);
    }
}
