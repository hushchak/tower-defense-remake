using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class ClickGrid : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Tilemap tilemap;
    public event Action<Vector2> OnClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick?.Invoke(InputReader.PointerPosition);
    }

    public List<Vector2> GetAllTileCenters()
    {
        List<Vector2> centers = new List<Vector2>();
        tilemap.CompressBounds();

        BoundsInt bounds = tilemap.cellBounds;
        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {

            if (!tilemap.HasTile(pos)) continue;

            centers.Add(tilemap.GetCellCenterWorld(pos));

        }
        return centers;
    }
}
