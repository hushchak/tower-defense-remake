using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public event Action OnDeactivated;

    [SerializeField] private EnemyData data;
    [SerializeField] private EnemyHealth health;
    [SerializeField] private EnemyMovement movement;
    [SerializeField] private EnemyEffects effects;

    public void Setup(EnemyPath path)
    {
        movement.Setup(data, effects, path);
        health.Setup(data, effects);
    }

    private void OnEnable()
    {
        health.OnDeath += Die;
        movement.OnPathEndReached += PathEndReached;
    }

    private void OnDisable()
    {
        health.OnDeath -= Die;
        movement.OnPathEndReached -= PathEndReached;
    }

    private void Update()
    {
        if (PauseManager.Instance.IsPaused)
            return;

        movement.HandleMovement(Time.deltaTime);
    }

    private void PathEndReached()
    {
        OnDeactivated?.Invoke();
        gameObject.SetActive(false);
        PlayerHealth.Instance.TakeDamage(data.Damage);
    }

    private void Die()
    {
        OnDeactivated?.Invoke();
        gameObject.SetActive(false);
        PlayerMoney.Instance.AddMoney(data.MoneyValue);
    }
}
