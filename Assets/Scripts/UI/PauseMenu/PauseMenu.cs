using System;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private EventChannelBool pauseChannel;
    [SerializeField] private EventChannel LevelExitInitiationChannel;
    [Space]
    [SerializeField] private GameObject pausePanel;

    private void OnEnable()
    {
        pauseChannel.Subscribe(HandlePause);
    }

    private void OnDisable()
    {
        pauseChannel.Unsubscribe(HandlePause);
    }

    private void HandlePause(bool IsPaused)
    {
        if (IsPaused)
        {
            InitiatePausePanel();
        }
        else
        {
            HidePausePanel();
        }
    }

    private void InitiatePausePanel()
    {
        pausePanel.SetActive(true);
    }

    private void HidePausePanel()
    {
        pausePanel.SetActive(false);
    }

    public void ResumeButtonAction()
    {
        PauseManager.Instance.SetPause(false);
    }

    public void ExitButtonAction()
    {
        LevelExitInitiationChannel.Raise();
    }
}
