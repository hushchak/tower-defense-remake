using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickGrid : MonoBehaviour, IPointerClickHandler
{
    public event Action<Vector2> OnClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick?.Invoke(InputReader.PointerPosition);
    }
}
