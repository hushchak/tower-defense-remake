using UnityEngine;

public class ProjectileData
{
    public Sprite Sprite { get; private set; }
    public float Radius { get; private set; }
    public int Damage { get; private set; }
    public DamageType DamageType { get; private set; }
    public float Speed { get; private set; }
    public float Distance { get; private set; }
    public Vector2 Direction { get; private set; }
    public LayerMask EnemyLayer { get; private set; }
    public float SlownessCoefficient { get; private set; }
    public bool DontStopAfterEnemyHit { get; private set; }

    public ProjectileData(Sprite sprite, float radius, int damage, DamageType damageType, float speed, float distance,
        Vector2 direction, LayerMask enemyLayer, float slownessCoefficient, bool dontStopAfterEnemyHit)
    {
        Sprite = sprite;
        Radius = radius;
        Damage = damage;
        DamageType = damageType;
        Speed = speed;
        Distance = distance;
        Direction = direction;
        EnemyLayer = enemyLayer;
        SlownessCoefficient = slownessCoefficient;
        DontStopAfterEnemyHit = dontStopAfterEnemyHit;
    }
}
