using UnityEngine;

[CreateAssetMenu(menuName="Enemy/Enemy Wave Data", fileName="EnemyWaveData_")]
public class EnemyWaveData : ScriptableObject
{
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public Enemy Prefab { get; private set; }
}
