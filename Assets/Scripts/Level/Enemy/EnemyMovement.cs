using System;
using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public event Action OnPathEndReached;
    public event Action<Vector2> OnDirectionChanged;

    private EnemyData data;
    private EnemyEffects effects;
    private EnemyPath path;

    private int currentPoint;

    public void Setup(EnemyData data, EnemyEffects effects, EnemyPath path)
    {
        this.data = data;
        this.path = path;
        this.effects = effects;

        currentPoint = data.IsFlying ? path.PointsCount - 1 : 0;
    }

    public void HandleMovement(float delta)
    {
        if (currentPoint == path.PointsCount)
            return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            path.GetPoint(currentPoint).position,
            data.Speed * effects.SlownessCoefficient * delta
        );

        if (Vector2.Distance(transform.position, path.GetPoint(currentPoint).position) < 0.1f)
        {
            transform.position = path.GetPoint(currentPoint).position;
            currentPoint++;

            if (currentPoint >= path.PointsCount)
            {
                OnPathEndReached?.Invoke();
            }
            else
            {
                OnDirectionChanged?.Invoke((path.GetPoint(currentPoint).position - transform.position).normalized);
            }
        }
    }
}
