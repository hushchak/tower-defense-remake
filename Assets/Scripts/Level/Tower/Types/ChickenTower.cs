using System;
using UnityEngine;

public class ChickenTower : Tower
{
    public event Action OnHalfCooked, OnFullyCooked;

    public enum CookedState
    {
        Raw,
        HalfCooked,
        FullyCooked
    }

    [SerializeField] private TowerData data1;
    [SerializeField] private TowerData data2;
    [Space]
    [SerializeField] private int waveEndReward = 5;
    [Space]
    [SerializeField] private EventChannel waveEndChannel;

    private CookedState state;

    private void OnEnable()
    {
        waveEndChannel.Subscribe(HandleWaveEnd);
    }

    private void OnDisable()
    {
        waveEndChannel.Unsubscribe(HandleWaveEnd);
    }

    private void HandleWaveEnd()
    {
        if (state == CookedState.FullyCooked)
            return;

        HandleWaveEndReward();
        HandleCookedState();
    }

    private void HandleWaveEndReward()
    {
        if (state == CookedState.FullyCooked)
            return;

        PlayerMoney.Instance.AddMoney(waveEndReward);
    }

    private void HandleCookedState()
    {
        int fireplaceAmount = GetFireplaceAmount();
        if (fireplaceAmount == 0 || state == CookedState.FullyCooked)
            return;

        if (fireplaceAmount == 1 && state == CookedState.Raw)
        {
            state = CookedState.HalfCooked;
            Data = data1;
            OnHalfCooked?.Invoke();
            return;
        }

        state = CookedState.FullyCooked;
        Data = data2;
        OnFullyCooked?.Invoke();
    }

    private int GetFireplaceAmount()
    {
        int amount = 0;
        Tower[] towers = new Tower[4];

        towers[0] = ParentGrid.GetTowerAt(Index.x, Index.y + 1);
        towers[1] = ParentGrid.GetTowerAt(Index.x + 1, Index.y);
        towers[2] = ParentGrid.GetTowerAt(Index.x, Index.y - 1);
        towers[3] = ParentGrid.GetTowerAt(Index.x - 1, Index.y);

        foreach (Tower tower in towers)
        {
            if (tower is FireplaceTower)
                amount++;
        }
        return amount;
    }
}
