using System;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private EventChannelBool pauseChannel;
    [SerializeField] private EventChannel levelExitInitiationChannel;
    [SerializeField] private EventChannelBool levelEndChannel;
    [Space]
    [SerializeField] private GameObject pausePanel;

    private bool pauseMenuEnabled;

    private void Awake()
    {
        pausePanel.SetActive(false);
    }

    private void OnEnable()
    {
        InputReader.OnBackPerformed += HandleBackPerformed;
        levelEndChannel.Subscribe(HandleLevelEnd);
    }

    private void OnDisable()
    {
        InputReader.OnBackPerformed -= HandleBackPerformed;
        levelEndChannel.Unsubscribe(HandleLevelEnd);
    }

    private void HandleBackPerformed()
    {
        pauseMenuEnabled = !pauseMenuEnabled;
        SetPauseMenu(pauseMenuEnabled);
    }

    private void HandleLevelEnd(bool end) => HidePausePanel();

    private void SetPauseMenu(bool pause)
    {
        pauseMenuEnabled = pause;
        PauseManager.Instance.SetPause(pauseMenuEnabled);

        if (pauseMenuEnabled)
            InitiatePausePanel();
        else
            HidePausePanel();
    }

    private void InitiatePausePanel() => pausePanel.SetActive(true);
    private void HidePausePanel() => pausePanel.SetActive(false);

    public void ResumeButtonAction() => SetPauseMenu(false);
    public void ExitButtonAction() => levelExitInitiationChannel.Raise();
}
