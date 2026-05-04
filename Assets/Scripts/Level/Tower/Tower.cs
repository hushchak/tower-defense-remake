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
        // TODO: Check if player has enough money

        Debug.Log("Tower Hadling");
        ParentGrid.PlaceTower(Index.x, Index.y, responce.TowerPrefab);
    }
}
