using System;
using System.Threading;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private EventChannel waveTriggerChannel;
    [SerializeField] private EventChannel waveStartChannel;
    [SerializeField] private EventChannel waveEndChannel;
    [SerializeField] private EventChannel allWaveEndChannel;
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

        using CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(
            destroyCancellationToken
        );

        try
        {
            await HandleWave(linkedCts.Token);

            currentWaveIndex++;
            waveEndChannel.Raise();

            if (currentWaveIndex >= data.Waves.Length)
                allWaveEndChannel.Raise();
        }
        catch (OperationCanceledException)
        {
        }
        finally
        {
            waveInProgress = false;
        }
}

    private async Awaitable HandleWave(CancellationToken cancellationToken)
    {
        Debug.Log("Wave started");
        await waveSpawner.SpawnWave(data.Waves[currentWaveIndex], cancellationToken);
    }
}
