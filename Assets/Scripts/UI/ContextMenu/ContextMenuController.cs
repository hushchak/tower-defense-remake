using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContextMenuController : MonoBehaviour
{
    [SerializeField] private EventChannelContextMenuRequest requestChannel;
    [SerializeField] private EventChannelContextMenuResponce responceChannel;
    [SerializeField] private EventChannelBool pauseChannel;
    [Space]
    [SerializeField] private RectTransform canvasRectTransform;
    [SerializeField] private RectTransform pivotRectTransform;
    [SerializeField] private RectTransform contextMenuRectTransform;
    [Space]
    [SerializeField] private GameObject contextMenu;
    [SerializeField] private ContextMenuAutoHide autoHide;
    [SerializeField] private ContextMenuEntry entryPrefab;
    [Space]
    [SerializeField] private Sound contextMenuSound;

    private bool contextMenuEnabled = false;
    private List<ContextMenuEntry> currentEntries = new();

    private void OnEnable()
    {
        requestChannel.Subscribe(HandleRequest);
        pauseChannel.Subscribe(HandlePause);
    }

    private void OnDisable()
    {
        requestChannel.Unsubscribe(HandleRequest);
        pauseChannel.Unsubscribe(HandlePause);
    }

    private void HandleRequest(ContextMenuRequest request)
    {
        if (contextMenuEnabled)
        {
            ResetContextMenu();
        }

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRectTransform,
            request.ScreenPosition,
            Camera.main,
            out Vector2 localPoint
        );

        pivotRectTransform.anchoredPosition = localPoint;
        SpawnEntries(request.Entries, request);
        contextMenu.SetActive(true);
        LayoutRebuilder.ForceRebuildLayoutImmediate(contextMenuRectTransform);
        SetContextMenuAlign(localPoint);

        autoHide.Hide += HideContextMenu;

        contextMenuEnabled = true;
        Audio.Play(contextMenuSound);
    }

    private void SetContextMenuAlign(Vector2 menuPoint)
    {
        Vector2 menuSize = contextMenuRectTransform.rect.size;
        Vector2 canvasBorderPoint = canvasRectTransform.rect.size * new Vector2(0.5f, -0.5f);

        Vector2 newPivot;
        newPivot.x = menuPoint.x + menuSize.x > canvasBorderPoint.x ? 1 : 0;
        newPivot.y = menuPoint.y - menuSize.y < canvasBorderPoint.y ? 0 : 1;

        contextMenuRectTransform.pivot = newPivot;
    }

    private void HideContextMenu()
    {
        contextMenu.SetActive(false);
        ResetContextMenu();
        contextMenuEnabled = false;
    }

    private void ResetContextMenu()
    {
        autoHide.Hide -= HideContextMenu;
        ClearEntries();
    }

    private void SpawnEntries(ContextMenuEntryData[] entriesData, ContextMenuRequest request)
    {
        for (int i = 0; i < entriesData.Length; i++)
        {
            AddEntry(entriesData[i], request);
        }
    }

    private void AddEntry(ContextMenuEntryData data, ContextMenuRequest request)
    {
        ContextMenuEntry entry = Instantiate(entryPrefab, contextMenu.transform);
        Action entryAction = () => responceChannel.Raise(
            new ContextMenuResponce(
                request.Tile,
                data.Action.TowerPrefab,
                data.Action.MoneyDifference
            )
        );
        entryAction += HideContextMenu;

        entry.Setup(data.Name, entryAction);
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

    private void HandlePause(bool isPaused)
    {
        if (isPaused)
        {
            HideContextMenu();
        }
    }
}
