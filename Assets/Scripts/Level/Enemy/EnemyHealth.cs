using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public event Action OnDeath;

    private EnemyData data;
    private EnemyEffects effects;
    private int health;

    public int GetHealth() => health;

    public void Setup(EnemyData data, EnemyEffects effects)
    {
        this.data = data;
        this.effects = effects;

        health = data.HealthPoints;
    }

    public bool CanBeDamaged(DamageType type)
    {
        return !(data.IsFlying && type == DamageType.Ground);
    }

    public void ApplyDamage(int rawDamage, DamageType type, SlownessData slownessData)
    {
        int damage = type switch
        {
            DamageType.Fire => GetFireDamage(rawDamage),
            _ => GetRegularDamage(rawDamage)
        };

        health = Mathf.Clamp(health - damage, 0, data.HealthPoints);

        if (health == 0)
        {
            Die();
            return;
        }

        if (slownessData.Duration > 0f && slownessData.Coefficient > 0f)
        {
            effects.StartSlownessEffect(slownessData);

            if (type == DamageType.Tar)
                effects.StartTarredEffect(slownessData.Duration);
        }
    }

    private void Die()
    {
        OnDeath?.Invoke();
    }

    public int GetRegularDamage(int rawDamage)
    {
        return Mathf.RoundToInt(rawDamage * Math.Abs(data.DefenceCoefficient - 1));
    }

    public int GetFireDamage(int rawDamage)
    {
        int tarredBonus = effects.Tarred ? Mathf.RoundToInt(rawDamage * 0.2f) : 0;
        return Mathf.RoundToInt(rawDamage * Math.Abs(data.FireDefenceCoefficient - 1)) + tarredBonus;
    }
}
