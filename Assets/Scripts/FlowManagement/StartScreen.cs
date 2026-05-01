using System;
using UnityEngine;

public class StartScreen : MonoBehaviour
{
    private void OnEnable()
    {
        InputReader.OnStartPerformed += LoadLevelMenu;
    }

    private void OnDisable()
    {
        InputReader.OnStartPerformed -= LoadLevelMenu;
    }

    private void LoadLevelMenu()
    {
        Debug.Log("Load Level Menu");
    }
}
