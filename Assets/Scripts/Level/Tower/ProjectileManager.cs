using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : Singleton<ProjectileManager>
{
    [SerializeField] private Transform poolsParent;
    private Dictionary<GameObject, ObjectPool> pools = new();

    public ObjectPool GetPool(GameObject prefab)
    {
        if (pools.TryGetValue(prefab, out ObjectPool pool))
        {
            return pool;
        }

        Transform poolParent = new GameObject("Pool").transform;
        poolParent.parent = poolsParent;

        ObjectPool newPool = new ObjectPool(prefab, poolParent, 1);
        pools.TryAdd(prefab, newPool);
        return newPool;
    }
}
