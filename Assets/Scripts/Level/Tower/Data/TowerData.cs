using UnityEngine;

[CreateAssetMenu(menuName="Tower/Data", fileName="TowerData_")]
public class TowerData : ScriptableObject
{
    [field: SerializeField] public Sprite ProjectileSprite { get; private set; }
    [field: SerializeField] public Sound UpgradeSound { get; private set; }
    [field: SerializeField] public Sound UpgradeCancelSound { get; private set; }
    [field: SerializeField] public Sound AttackSound { get; private set; }
    [field: Space]
    [field: SerializeField] public float ProjectileRadius { get; private set; } = 0.2f;
    [field: SerializeField] public float ProjectileSpeed { get; private set; } = 15f;
    [field: SerializeField] public Projectile ProjectilePrefab { get; private set; }
    [field: Space]
    [field: SerializeField] public int Damage { get; private set; } = 10;
    [field: SerializeField] public DamageType DamageType { get; private set; } = DamageType.Regular;
    [field: SerializeField] public float AttackRate { get; private set; } = 0.833f;
    [field: SerializeField] public float AttackRange { get; private set; } = 2f;
    [field: SerializeField] public LayerMask EnemyLayer { get; private set; }
    [field: SerializeField] public LayerMask ObstacleLayer { get; private set; }
    [field: Space]
    [field: SerializeField] public SlownessData SlownessData { get; private set; }
    [field: Space]
    [field: SerializeField] public bool DontStopAfterEnemyHit { get; private set; } = false;
    [field: Space]
    [field: SerializeField] public TowerAction[] Actions { get; private set; }
}
