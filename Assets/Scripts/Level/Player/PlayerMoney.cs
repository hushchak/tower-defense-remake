using System;
using UnityEngine;

public class PlayerMoney : Singleton<PlayerMoney>
{
    [SerializeField] private EventChannelInt moneyChangedChannel;
    [SerializeField] private EventChannel waveEndChannel;
    [Space]
    [SerializeField] private int startMoney = 20;
    [SerializeField] private int waveEndReward = 20;
    [SerializeField] private int moneyMaxCapacity = 99;
    //[SerializeField] private Sound moneyAddSound;

    private int money;
    public int Money => money;

    protected override void Awake()
    {
        base.Awake();
        money = startMoney;
    }

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
        AddMoney(waveEndReward);
    }

    public void AddMoney(int amount)
    {
        money = Mathf.Clamp(money + amount, 0, moneyMaxCapacity);
        moneyChangedChannel.Raise(money);
        //Audio.Play(moneyAddSound);
    }

    public bool TryDecreaseMoney(int amount)
    {
        if (money < amount)
            return false;

        money -= amount;
        moneyChangedChannel.Raise(money);
        return true;
    }

    public void DecreaseMoney(int amount)
    {
        money = Mathf.Clamp(money - amount, 0, int.MaxValue);
        moneyChangedChannel.Raise(money);
    }
}
