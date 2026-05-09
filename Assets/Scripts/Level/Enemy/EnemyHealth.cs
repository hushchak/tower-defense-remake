using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private EnemyData data;

    public event Action OnDeath;
    private int health;

    public int GetHealth() => health;

    private void OnEnable()
    {
        health = data.HealthPoints;
    }

    public void ApplyDamage(int damage)
    {
        health = Mathf.Clamp(health - damage, 0, data.HealthPoints);
        //Audio.Play(data.HurtSound);

        if (health == 0)
            Die();
    }

    private void Die()
    {
        OnDeath?.Invoke();
    }
}
