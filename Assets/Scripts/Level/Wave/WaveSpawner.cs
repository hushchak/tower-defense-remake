using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private EnemyPath Path_1;
    [SerializeField] private EnemyPath Path_2;
    [SerializeField] private EnemyPath Path_3;
    [Space]
    [SerializeField] private Transform poolsParent;

    private Dictionary<string, ObjectPool> pools = new();
    private int runningPaths = 0;

    public async Awaitable SpawnWave(WaveData data, CancellationToken cancellationToken)
    {
        if (data.Actions_1 != null && Path_1 != null)
            RunLine(Path_1, data.Actions_1, cancellationToken);

        if (data.Actions_2 != null && Path_2 != null)
            RunLine(Path_2, data.Actions_2, cancellationToken);

        if (data.Actions_3 != null && Path_3 != null)
            RunLine(Path_3, data.Actions_3, cancellationToken);

        while (runningPaths > 0)
            await Awaitable.NextFrameAsync();
    }

    private async void RunLine(EnemyPath path, WaveAction[] actions, CancellationToken cancellationToken)
    {
        runningPaths++;

        List<Enemy> aliveEnemies = new();
        int actionIndex = 0;
        try
        {
            for (int i = 0; i < actions.Length; i++)
            {
                for (int j = 0; j < actions[actionIndex].Number; j++)
                {
                    Enemy enemy = SpawnEnemy(path, actions[actionIndex].Enemy);
                    enemy.OnDeactivated += () => aliveEnemies.Remove(enemy);
                    aliveEnemies.Add(enemy);
                    enemy.gameObject.SetActive(true);

                    await WaitWithPause(actions[actionIndex].SpawnRate, cancellationToken);
                }
                await WaitWithPause(actions[actionIndex].WaitAfter, cancellationToken);
                actionIndex++;
            }
        }
        catch (OperationCanceledException)
        {
        }
        finally
        {
            while (aliveEnemies.Count > 0)
                await Awaitable.NextFrameAsync();

            runningPaths--;
        }
    }

    private Enemy SpawnEnemy(EnemyPath path, EnemyWaveData data)
    {
        Enemy enemy = GetPool(data.Prefab.gameObject).GetObject().GetComponent<Enemy>();

        enemy.transform.position = path.StartPoint.position;
        enemy.Setup(path);

        return enemy;
    }

    private ObjectPool GetPool(GameObject prefab)
    {
        if (pools.TryGetValue(prefab.name, out ObjectPool pool))
        {
            return pool;
        }

        Transform poolParent = new GameObject("Pool").transform;
        poolParent.parent = poolsParent;

        ObjectPool newPool = new ObjectPool(prefab, poolParent, 1);
        pools.TryAdd(prefab.name, newPool);
        return newPool;
    }

    private async Awaitable WaitWithPause(float seconds, CancellationToken cancellationToken)
    {
        float elapsed = 0f;

        while (elapsed < seconds)
        {
            while (PauseManager.Instance.IsPaused)
            {
                await Awaitable.NextFrameAsync(cancellationToken);
            }

            await Awaitable.NextFrameAsync(cancellationToken);
            elapsed += Time.deltaTime;
        }
    }
}
