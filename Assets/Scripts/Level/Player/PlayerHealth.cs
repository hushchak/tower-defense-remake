using UnityEngine;

public class PlayerHealth : Singleton<PlayerHealth>
{
    [SerializeField] private EventChannel deathEventChannel;
    [SerializeField] private EventChannelInt healthChangedChannel;
    [Space]
    [SerializeField] private int maxHealth;

    private int health;
    public int Health => health;

    protected override void Awake()
    {
        base.Awake();
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        health = health < 0
            ? 0
            : health;

        healthChangedChannel.Raise(health);

        if (health == 0)
        {
            Death();
        }
    }

    private void Death()
    {
        deathEventChannel.Raise();
    }
}
