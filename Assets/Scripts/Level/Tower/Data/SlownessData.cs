using System;
using UnityEngine;

[Serializable]
public class SlownessData
{
    [field: SerializeField, Range(0f, 1f)] public float Coefficient { get; private set; } = 0;
    [field: SerializeField] public float Duration { get; private set; } = 0;
}
