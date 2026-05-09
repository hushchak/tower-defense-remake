using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public event Action OnDeactivated;

    [SerializeField] private EnemyData data;
    [SerializeField] private EnemyHealth health;

    private EnemyPath path;
    private int currentPoint;

    public void Setup(EnemyPath path)
    {
        this.path = path;
    }

    private void OnEnable()
    {
        currentPoint = 0;
        health.OnDeath += Die;
    }

    private void OnDisable()
    {
        health.OnDeath -= Die;
    }

    private void Update()
    {
        //if (SessionStateManager.Instance.IsPaused)
        //    return;

        HandleMovement(Time.deltaTime);
    }

    private void HandleMovement(float delta)
    {
        if (currentPoint == path.PointsCount)
            return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            path.GetPoint(currentPoint).position,
            data.Speed * delta
        );

        if (Vector2.Distance(transform.position, path.GetPoint(currentPoint).position) < 0.1f)
        {
            transform.position = path.GetPoint(currentPoint).position;
            currentPoint++;
            if (currentPoint >= path.PointsCount)
            {
                PathEndReached();
            }
        }
    }

    private void PathEndReached()
    {
        OnDeactivated?.Invoke();
        gameObject.SetActive(false);
        //PlayerHealth.Instance.TakeDamage(data.Damage);
    }

    private void Die()
    {
        OnDeactivated?.Invoke();
        gameObject.SetActive(false);
        //PlayerMoney.Instance.AddMoney(data.MoneyValue);
    }
}
