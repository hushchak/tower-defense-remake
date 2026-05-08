using UnityEngine;

[System.Serializable]
public class WaveData
{
    [field: SerializeField] public WaveAction[] Actions_1 { get; private set; }
    [field: SerializeField] public WaveAction[] Actions_2 { get; private set; }
    [field: SerializeField] public WaveAction[] Actions_3 { get; private set; }
}

[System.Serializable]
public class WaveAction
{
    [field: SerializeField] public EnemyWaveData Enemy { get; private set; }
    [field: SerializeField] public float Number { get; private set; }
    [field: SerializeField] public float SpawnRate { get; private set; }
    [field: Space]
    [field: SerializeField] public float WaitAfter { get; private set; }
}
