using System;
using UnityEngine;

public class TestStartWave : MonoBehaviour
{
    [SerializeField] private EventChannel waveStartChannel;

    private void OnEnable()
    {
        InputReader.OnStartPerformed += StartWave;
    }

    private void OnDisable()
    {
        InputReader.OnStartPerformed -= StartWave;
    }

    private void StartWave()
    {
        waveStartChannel.Raise();
    }
}
