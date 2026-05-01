using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    private async void Start()
    {
        await SceneLoader.LoadScene(SceneList.Slots.Main, SceneList.Names.StartScreen, SceneArgs.Empty);
    }
}
