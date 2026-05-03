using UnityEngine;

public class ContextMenuRequest
{
    public Vector2Int Tile;
    public Vector2 ScreenPosition;
    public ContextMenuEntry[] Entries;

    public ContextMenuRequest(Vector2Int tile, Vector2 screenPosition, ContextMenuEntry[] entries)
    {
        Tile = tile;
        ScreenPosition = screenPosition;
        Entries = entries;
    }
}
