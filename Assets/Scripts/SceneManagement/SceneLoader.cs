using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    private static Dictionary<string, string> loadedScenes = new Dictionary<string, string>();

    public static async Awaitable LoadScene(string slotName, string sceneName, SceneArgs args, bool setActive = true)
    {
        if (loadedScenes.ContainsKey(slotName)) await UnloadScene(slotName);

        await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        if (setActive) SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));

        SceneRoot root = null;
        GameObject[] rootGameObjects = SceneManager.GetSceneByName(sceneName).GetRootGameObjects();
        foreach (GameObject rootGameObject in rootGameObjects)
        {
            rootGameObject.TryGetComponent(out root);
        }
        if (!root)
        {
            Debug.LogError($"You are trying to load scene without root game object. Name: {sceneName}");
            return;
        }
        root.Initialize(args);

        loadedScenes.Add(slotName, sceneName);
    }

    public static async Awaitable UnloadScene(string slotName)
    {
        if (loadedScenes.TryGetValue(slotName, out string sceneName))
        {
            await SceneManager.UnloadSceneAsync(sceneName);
            loadedScenes.Remove(slotName);
        }
        else
        {
            Debug.LogError($"You are trying to unload scene from unexisting slot. Slot: {slotName}");
        }
    }
}
