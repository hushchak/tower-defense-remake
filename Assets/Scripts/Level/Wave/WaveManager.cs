using System;
using System.Threading;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private EventChannelWaveData waveAnnounceChannel;
    [SerializeField] private EventChannel waveTriggerChannel;
    [SerializeField] private EventChannel waveStartChannel;
    [SerializeField] private EventChannel waveEndChannel;
    [SerializeField] private EventChannel allWaveEndChannel;
    [Space]
    [SerializeField] private WaveSpawner waveSpawner;
    [Space]
    [SerializeField] private WavesData data;
    [Space]
    [SerializeField] private Sound waveStartSound;
    [SerializeField] private Sound waveEndSound;

    private bool waveInProgress;
    private int currentWaveIndex;

    private void Awake()
    {
        waveAnnounceChannel.Raise(data.Waves[currentWaveIndex]);
    }

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
            else
                waveAnnounceChannel.Raise(data.Waves[currentWaveIndex]);
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
        Audio.Play(waveStartSound);
        await waveSpawner.SpawnWave(data.Waves[currentWaveIndex], cancellationToken);
        Audio.Play(waveEndSound);
    }
}
