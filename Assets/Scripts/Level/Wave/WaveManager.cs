using System;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private EventChannel waveTriggerChannel;
    [SerializeField] private EventChannel waveStartChannel;
    [SerializeField] private EventChannel waveEndChannel;
    [Space]
    [SerializeField] private WaveSpawner waveSpawner;
    [Space]
    [SerializeField] private WavesData data;

    private bool waveInProgress;
    private int currentWaveIndex;

    private void OnEnable()
    {
        waveTriggerChannel.Subscribe(TryStartWave);
    }

    private void OnDisable()
    {
        waveTriggerChannel.Unsubscribe(TryStartWave);
    }

    private async void TryStartWave()
    {
        Debug.Log("Wave attempting");
        if (waveInProgress || currentWaveIndex >= data.Waves.Length)
            return;

        waveInProgress = true;
        waveStartChannel.Raise();

        await HandleWave();

        currentWaveIndex++;
        waveInProgress = false;
        waveEndChannel.Raise();
    }

    private async Awaitable HandleWave()
    {
        Debug.Log("Wave started");
        await waveSpawner.SpawnWave(data.Waves[currentWaveIndex]);
    }
}
