using UnityEngine;

public class ContextMenuRequest
{
    public Vector2Int Tile;
    public Vector2 ScreenPosition;
    public ContextMenuEntryData[] Entries;

    public ContextMenuRequest(Vector2Int tile, Vector2 screenPosition, ContextMenuEntryData[] entries)
    {
        Tile = tile;
        ScreenPosition = screenPosition;
        Entries = entries;
    }
}
