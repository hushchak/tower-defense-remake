using UnityEngine;

public class ProjectileData
{
    public Sprite Sprite { get; private set; }
    public float Radius { get; private set; }
    public int Damage { get; private set; }
    public float Speed { get; private set; }
    public float Distance { get; private set; }
    public Vector2 Direction { get; private set; }
    public LayerMask EnemyLayer { get; private set; }
    public float SlownessCoefficient { get; private set; }
    public bool AttackOnlyGround { get; private set; }
    public bool StopAfterEnemyHit { get; private set; }

    public ProjectileData(Sprite sprite, float radius, int damage, float speed, float distance, Vector2 direction,
        LayerMask enemyLayer, float slownessCoefficient, bool attackOnlyGround, bool stopAfterEnemyHit)
    {
        Sprite = sprite;
        Radius = radius;
        Damage = damage;
        Speed = speed;
        Distance = distance;
        Direction = direction;
        EnemyLayer = enemyLayer;
        SlownessCoefficient = slownessCoefficient;
        AttackOnlyGround = attackOnlyGround;
        StopAfterEnemyHit = stopAfterEnemyHit;
    }
}
