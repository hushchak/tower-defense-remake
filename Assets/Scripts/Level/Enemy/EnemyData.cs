using UnityEngine;

[CreateAssetMenu(menuName="Enemy/Enemy Data", fileName="EnemyData_")]
public class EnemyData : ScriptableObject
{
    [field: SerializeField] public float Speed { get; private set; } = 2f;
    [field: SerializeField] public bool IsFlying { get; private set; } = false;
    [field: Space]
    [field: SerializeField] public int Damage { get; private set; } = 1;
    [field: SerializeField] public int MoneyValue { get; private set; } = 1;
    [field: Space]
    [field: SerializeField] public int HealthPoints { get; private set; } = 50;
    [field: SerializeField, Range(0f, 1f)] public float DefenceCoefficient { get; private set; } = 0;
    [field: SerializeField, Range(0f, 1f)] public float FireDefenceCoefficient { get; private set; } = 0;
    [field: SerializeField] public Sound DeathSound { get; private set; }
}
