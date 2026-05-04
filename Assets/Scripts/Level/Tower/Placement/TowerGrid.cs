using System;
using UnityEngine;

public class TowerGrid : MonoBehaviour
{
    [SerializeField] private EventChannelContextMenuRequest contextMenuRequestChannel;
    [SerializeField] private EventChannelContextMenuResponce contextMenuResponceChannel;
    [Space]
    [SerializeField] private Vector2Int gridSize;
    [SerializeField] private Vector2 gridOrigin;
    [SerializeField] private Vector2 gridCellSize;
    [Space]
    [SerializeField] private ClickGrid clickGrid;
    [SerializeField] private NullTower nullTowerPrefab;

    private Grid<Tower> grid;

    private void Awake()
    {
        grid = new Grid<Tower>(gridSize.x, gridSize.y, gridCellSize, gridOrigin);
        FillWithNull();
    }

    private void OnEnable()
    {
        clickGrid.OnClick += HandleClick;
        contextMenuResponceChannel.Subscribe(HandleContextMenuResponce);
    }

    private void OnDisable()
    {
        clickGrid.OnClick -= HandleClick;
        contextMenuResponceChannel.Unsubscribe(HandleContextMenuResponce);
    }

    private void HandleClick(Vector2 pointerScreenPosition)
    {
        Vector2 pointerWorldPosition = Camera.main.ScreenToWorldPoint(pointerScreenPosition);
        if (!grid.TryGetIndex(pointerWorldPosition, out Vector2Int index))
        {
            Debug.LogWarning("You are trying to handle click that is outside of tower grid");
            return;
        }

        if (grid.TryGetValue(index.x, index.y, out Tower tower))
        {
            contextMenuRequestChannel.Raise(
                new ContextMenuRequest(
                    index,
                    pointerScreenPosition,
                    tower.GetContextMenuEntries()
                )
            );
        }
        else
        {
            Debug.LogWarning($"Unable to get tower by coordinates ({index.x}, {index.y})");
            return;
        }
    }

    private void HandleContextMenuResponce(ContextMenuResponce responce)
    {
        if (grid.TryGetValue(responce.Index.x, responce.Index.y, out Tower tower))
        {
            tower.HandleResponce(responce);
        }
        else
        {
            Debug.LogWarning($"Unable to get tower by coordinates ({responce.Index.x}, {responce.Index.y})");
            return;
        }
    }

#region Tower Spawning
    private void SpawnTower(int x, int y, Tower prefab)
    {
        Tower tower = Instantiate(prefab, GetCellCenter(x,y), Quaternion.identity);
        tower.Setup(this, new Vector2Int(x, y));
        grid.SetValue(x, y, tower);
    }

    private void FillWithNull()
    {
        Vector2[] towerPositions = clickGrid.GetAllTileCenters().ToArray();
        for (int i = 0; i < towerPositions.Length; i++)
        {
            if (grid.TryGetIndex(towerPositions[i], out Vector2Int index))
            {
                SpawnTower(index.x, index.y, nullTowerPrefab);
            }
            else
            {
                Debug.LogError("Error with getting tower index to place NullTower");
            }
        }
    }

    public void PlaceTower(int x, int y, Tower prefab)
    {
        if (grid.TryGetValue(x, y, out Tower oldTower))
        {
            Destroy(oldTower.gameObject);
            Debug.Log("Old tower destroyed");
        }
        SpawnTower(x, y, prefab);
        Debug.Log("New tower spawned");
    }
#endregion

#region Helpers
    private Vector2 GetCellCenter(int x, int y)
    {
        if (grid.TryGetValue(x, y, out Tower _))
        {
            Vector2 cellOrigin = gridOrigin + new Vector2(x * gridCellSize.x, y * gridCellSize.y);
            Vector2 cellCenter = cellOrigin + gridCellSize * 0.5f;
            return cellCenter;
        }

        return Vector2.zero;
    }
#endregion

#region Debug
    private void OnDrawGizmos()
    {
        DrawGrid();
    }

    private void DrawGrid()
    {
        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                Vector2 cellOrigin = gridOrigin + new Vector2(i * gridCellSize.x, j * gridCellSize.y);
                Gizmos.DrawLine(cellOrigin, cellOrigin + new Vector2(gridCellSize.x, 0));
                Gizmos.DrawLine(cellOrigin, cellOrigin + new Vector2(0, gridCellSize.y));
            }
        }

        Gizmos.color = Color.red;
        float maxXPosition = gridOrigin.x + gridCellSize.x * gridSize.x;
        float maxYPosition = gridOrigin.y + gridCellSize.y * gridSize.y;
        Gizmos.DrawLine(new Vector2(maxXPosition, gridOrigin.y), new Vector2(maxXPosition, maxYPosition));
        Gizmos.DrawLine(new Vector2(gridOrigin.x, maxYPosition), new Vector2(maxXPosition, maxYPosition));
    }
#endregion
}
