using System;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private EventChannel waveStartChannel;
    [SerializeField] private WaveSpawner waveSpawner;
    [Space]
    [SerializeField] private WavesData data;

    private bool waveInProgress;
    private int currentWaveIndex;

    private void OnEnable()
    {
        waveStartChannel.Subscribe(TryStartWave);
    }

    private void OnDisable()
    {
        waveStartChannel.Unsubscribe(TryStartWave);
    }

    private async void TryStartWave()
    {
        Debug.Log("Wave attempting");
        if (waveInProgress || currentWaveIndex >= data.Waves.Length)
            return;

        waveInProgress = true;

        await HandleWave();

        currentWaveIndex++;
        waveInProgress = false;
    }

    private async Awaitable HandleWave()
    {
        Debug.Log("Wave started");
        await waveSpawner.SpawnWave(data.Waves[currentWaveIndex]);
    }
}
