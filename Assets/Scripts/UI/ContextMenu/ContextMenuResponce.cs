using UnityEngine;

public class ContextMenuResponce
{
    public Vector2Int Index;
    public Tower TowerPrefab;
    public int MoneyDifference;

    public ContextMenuResponce(Vector2Int index, Tower towerPrefab, int moneyDifference)
    {
        Index = index;
        TowerPrefab = towerPrefab;
        MoneyDifference = moneyDifference;
    }
}
