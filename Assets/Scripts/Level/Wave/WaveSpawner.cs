using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private EnemyPath Path_1;
    [SerializeField] private EnemyPath Path_2;
    [SerializeField] private EnemyPath Path_3;
    [Space]
    [SerializeField] private Transform enemyParent;

    private int runningPaths = 0;

    public async Awaitable SpawnWave(WaveData data)
    {
        if (data.Actions_1 != null && Path_1 != null)
            RunLine(Path_1, data.Actions_1);

        if (data.Actions_2 != null && Path_2 != null)
            RunLine(Path_2, data.Actions_2);

        if (data.Actions_3 != null && Path_3 != null)
            RunLine(Path_3, data.Actions_3);

        while (runningPaths > 0)
            await Awaitable.NextFrameAsync();
    }

    private async void RunLine(EnemyPath path, WaveAction[] actions)
    {
        runningPaths++;

        List<Enemy> aliveEnemies = new();
        int actionIndex = 0;
        for (int i = 0; i < actions.Length; i++)
        {
            for (int j = 0; j < actions[actionIndex].Number; j++)
            {
                Enemy enemy = SpawnEnemy(path, actions[actionIndex].Enemy);
                enemy.OnDeactivated += () => aliveEnemies.Remove(enemy);
                aliveEnemies.Add(enemy);

                await Awaitable.WaitForSecondsAsync(actions[actionIndex].SpawnRate);
            }
            await Awaitable.WaitForSecondsAsync(actions[actionIndex].WaitAfter);
        }

        while (aliveEnemies.Count > 0)
            await Awaitable.NextFrameAsync();

        runningPaths--;
    }

    private Enemy SpawnEnemy(EnemyPath path, EnemyWaveData data)
    {
        Enemy enemy = Instantiate(data.Prefab, path.StartPoint.position, Quaternion.identity);
        enemy.transform.parent = enemyParent;
        enemy.Setup(path);
        return enemy;
    }
}
