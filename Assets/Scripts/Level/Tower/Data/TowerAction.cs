using System;
using UnityEngine;

[CreateAssetMenu(menuName="Tower/Action", fileName="TA_")]
public class TowerAction : ScriptableObject
{
    [field: SerializeField] public string EntryName { get; private set; }
    [field: SerializeField] public Tower TowerPrefab { get; private set; }
    [field: SerializeField] public int MoneyDifference { get; private set; }
}
