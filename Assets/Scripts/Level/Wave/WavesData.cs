using UnityEngine;

[CreateAssetMenu(menuName="Wave Data", fileName="WaveData_")]
public class WavesData : ScriptableObject
{
    [field: SerializeField] public WaveData[] Waves { get; private set; }
}
