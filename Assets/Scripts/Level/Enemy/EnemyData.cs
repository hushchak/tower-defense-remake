using UnityEngine;

[CreateAssetMenu(menuName="Enemy/Enemy Data", fileName="EnemyData_")]
public class EnemyData : ScriptableObject
{
    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public int HealthPoints { get; private set; }
}
