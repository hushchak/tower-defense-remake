using UnityEngine;

[CreateAssetMenu(menuName="Tower/Data", fileName="TowerData")]
public class TowerData : ScriptableObject
{
    [field: SerializeField] public TowerAction[] Actions { get; private set; }
}
