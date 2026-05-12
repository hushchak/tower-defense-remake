using System;
using System.Collections.Generic;
using UnityEngine;

public class AttackTower : Tower
{
    private float nextAttackTime = 0;

    private void Update()
    {
        HandleAttack();
    }

    private void HandleAttack()
    {
        if (nextAttackTime > Time.time)
            return;

        if (TryAttack())
        {
            nextAttackTime = Time.time + Data.AttackRate;
        }
    }

    private bool TryAttack()
    {
        Collider2D[] potentialTargets = Physics2D.OverlapCircleAll(transform.position, Data.AttackRange, Data.EnemyLayer);
        GameObject[] targets = GetTargets(potentialTargets);

        if (targets.Length == 0)
            return false;

        GameObject closestTarget = GetClosest(targets, transform.position);
        SpawnProjectile(new ProjectileData(
                Data.ProjectileSprite,
                Data.ProjectileRadius,
                Data.Damage,
                Data.DamageType,
                Data.ProjectileSpeed,
                Data.AttackRange,
                (closestTarget.transform.position - this.transform.position).normalized,
                Data.EnemyLayer,
                Data.SlownessCoefficient,
                Data.DontStopAfterEnemyHit
            )
        );
        return true;
    }

    private GameObject[] GetTargets (Collider2D[] potentialTargets)
    {
        List<GameObject> targets = new();
        foreach (Collider2D potentialTarget in potentialTargets)
        {
            if (potentialTarget.gameObject.TryGetComponent(out Enemy enemy))
            {
                Vector2 direction = (enemy.transform.position - this.transform.position).normalized;
                RaycastHit2D hit = Physics2D.CircleCast(
                    transform.position,
                    Data.ProjectileRadius,
                    direction,
                    Data.AttackRange,
                    Data.ObstacleLayer
                );

                if (!hit)
                    targets.Add(enemy.gameObject);
            }
            else
            {
                continue;
            }
        }

        return targets.ToArray();
    }

    GameObject GetClosest(GameObject[] objects, Vector2 position)
    {
        GameObject closest = null;
        float closestDistance = float.MaxValue;

        foreach (GameObject @object in objects)
        {
            if (@object == null)
                continue;

            float distance = Vector2.Distance(@object.transform.position, position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = @object;
            }
        }

        return closest;
    }

    private void SpawnProjectile(ProjectileData data)
    {
        Projectile projectile = ProjectileManager.Instance.GetPool(Data.ProjectilePrefab.gameObject)
            .GetObject()
            .GetComponent<Projectile>();

        projectile.transform.position = this.transform.position;
        projectile.Setup(data);
        projectile.gameObject.SetActive(true);
    }
}
