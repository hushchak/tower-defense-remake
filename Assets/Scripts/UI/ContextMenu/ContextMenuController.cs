using UnityEngine;

public class ContextMenuController : MonoBehaviour
{
    [SerializeField] private EventChannelContextMenuRequest requestChannel;
    [SerializeField] private RectTransform canvasRectTransform;
    [SerializeField] private RectTransform menuRectTransform;

    private void OnEnable()
    {
        requestChannel.Subscribe(HandleRequest);
    }

    private void OnDisable()
    {
        requestChannel.Unsubscribe(HandleRequest);
    }

    private void HandleRequest(ContextMenuRequest request)
    {
        Debug.Log($"Screen Position: {request.ScreenPosition}");
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRectTransform,
            request.ScreenPosition,
            null,
            out Vector2 localPoint
        );

        menuRectTransform.anchoredPosition = localPoint;
    }
}
