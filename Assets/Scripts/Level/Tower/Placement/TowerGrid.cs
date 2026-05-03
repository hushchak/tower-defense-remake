using UnityEngine;

public class TowerGrid : MonoBehaviour
{
    [SerializeField] private Vector2Int gridSize;
    [SerializeField] private Vector2 gridOrigin;
    [SerializeField] private Vector2 gridCellSize;

    [SerializeField] private ClickGrid clickGrid;

    private Grid<bool> grid;

    private void Awake()
    {
        grid = new Grid<bool>(gridSize.x, gridSize.y, gridCellSize, gridOrigin);
    }

    private void OnEnable()
    {
        clickGrid.OnClick += HandleClick;
    }

    private void OnDisable()
    {
        clickGrid.OnClick -= HandleClick;
    }

    private void HandleClick(Vector2 clickPosition)
    {
        if (!grid.TryGetIndex(clickPosition, out Vector2Int index))
        {
            Debug.LogWarning("You are trying to handle click that is outside of tower grid");
            return;
        }
        Debug.Log($"Cell center ({index.x}, {index.y}): {GetCellCenter(index.x, index.y)}");
    }

    private Vector2 GetCellCenter(int x, int y)
    {
        if (grid.TryGetValue(x, y, out bool _))
        {
            Vector2 cellOrigin = gridOrigin + new Vector2(x * gridCellSize.x, y * gridCellSize.y);
            Vector2 cellCenter = cellOrigin + gridCellSize * 0.5f;
            return cellCenter;
        }

        return Vector2.zero;
    }

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
}
