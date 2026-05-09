using System;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    [SerializeField] protected TowerData Data;

    protected TowerGrid ParentGrid;
    protected Vector2Int Index;

    public void Setup(TowerGrid grid, Vector2Int index)
    {
        ParentGrid = grid;
        Index = index;
    }

    public ContextMenuEntryData[] GetContextMenuEntries()
    {
        ContextMenuEntryData[] entries = new ContextMenuEntryData[Data.Actions.Length];
        for (int i = 0; i < entries.Length; i++)
        {
            entries[i] = new ContextMenuEntryData(Data.Actions[i].EntryName, Data.Actions[i]);
        }

        return entries;
    }

    public void HandleResponce(ContextMenuResponce responce)
    {
        if (responce.MoneyDifference > 0)
        {
            PlayerMoney.Instance.AddMoney(Mathf.Abs(responce.MoneyDifference));
        }
        else if (!PlayerMoney.Instance.TryDecreaseMoney(Mathf.Abs(responce.MoneyDifference)))
        {
            Debug.Log("Not enough money to place/upgrade tower");
            return;
        }

        Debug.Log("Tower Hadling");
        ParentGrid.PlaceTower(Index.x, Index.y, responce.TowerPrefab);
    }
}
