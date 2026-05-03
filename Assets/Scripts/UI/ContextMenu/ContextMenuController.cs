using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContextMenuController : MonoBehaviour
{
    [SerializeField] private EventChannelContextMenuRequest requestChannel;
    [Space]
    [SerializeField] private RectTransform canvasRectTransform;
    [SerializeField] private RectTransform pivotRectTransform;
    [SerializeField] private RectTransform contextMenuRectTransform;
    [Space]
    [SerializeField] private GameObject contextMenu;
    [SerializeField] private ContextMenuAutoHide autoHide;
    [SerializeField] private ContextMenuEntry entryPrefab;

    private bool contextMenuEnabled = false;
    private List<ContextMenuEntry> currentEntries = new();

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
        if (contextMenuEnabled)
        {
            ClearEntries();
        }

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRectTransform,
            request.ScreenPosition,
            null,
            out Vector2 localPoint
        );

        pivotRectTransform.anchoredPosition = localPoint;
        SpawnEntries(request.Entries);
        LayoutRebuilder.ForceRebuildLayoutImmediate(contextMenuRectTransform);
        SetContextMenuAlign(localPoint);

        autoHide.Hide += HideContextMenu;

        contextMenu.SetActive(true);
        contextMenuEnabled = true;
    }

    private void SetContextMenuAlign(Vector2 menuPoint)
    {
        Vector2 menuSize = contextMenuRectTransform.rect.size;
        Vector2 canvasBorderPoint = canvasRectTransform.rect.size * new Vector2(0.5f, -0.5f);

        Debug.Log($"{canvasBorderPoint}, {menuSize}, {menuPoint}");

        Vector2 newPivot;
        newPivot.x = menuPoint.x + menuSize.x > canvasBorderPoint.x ? 1 : 0;
        newPivot.y = menuPoint.y - menuSize.y < canvasBorderPoint.y ? 0 : 1;

        contextMenuRectTransform.pivot = newPivot;
    }

    private void HideContextMenu()
    {
        autoHide.Hide -= HideContextMenu;

        contextMenu.SetActive(false);
        ClearEntries();
        contextMenuEnabled = false;
    }

    private void SpawnEntries(ContextMenuEntryData[] entriesData)
    {
        for (int i = 0; i < entriesData.Length; i++)
        {
            AddEntry(entriesData[i].Name);
        }
    }

    private void AddEntry(string name)
    {
        ContextMenuEntry entry = Instantiate(entryPrefab, contextMenu.transform);
        entry.Setup(name);
        currentEntries.Add(entry);
    }

    private void ClearEntries()
    {
        for (int i = 0; i < currentEntries.Count; i++)
        {
            Destroy(currentEntries[i].gameObject);
        }
        currentEntries.Clear();
    }
}
