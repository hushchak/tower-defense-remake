using System;
using UnityEngine;

public class PlayerMoney : Singleton<PlayerMoney>
{
    [SerializeField] private EventChannelInt moneyChangedChannel;
    [Space]
    [SerializeField] private int startMoney;
    //[SerializeField] private Sound moneyAddSound;

    private int money;

    protected override void Awake()
    {
        base.Awake();
        money = startMoney;
    }

    public void AddMoney(int amount)
    {
        money += amount;
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

    public int GetMoney()
    {
        return money;
    }
}
