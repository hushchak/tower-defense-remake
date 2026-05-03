using UnityEngine;
using UnityEngine.EventSystems;

public class ClickGrid : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"Clicked at: {InputReader.PointerPosition}");
    }
}
