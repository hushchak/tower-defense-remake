using System;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private EventChannelBool pauseChannel;
    [SerializeField] private EventChannel levelExitInitiationChannel;
    [SerializeField] private EventChannelBool levelEndChannel;
    [Space]
    [SerializeField] private GameObject pausePanel;
    [Space]
    [SerializeField] private Sound openSound;
    [SerializeField] private Sound closeSound;

    private bool pauseMenuEnabled;
    private bool canPause = true;

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
        if (!canPause)
            return;

        pauseMenuEnabled = !pauseMenuEnabled;
        SetPauseMenu(pauseMenuEnabled);
    }

    private void HandleLevelEnd(bool end)
    {
        HidePausePanel();
        canPause = false;
    }

    private void SetPauseMenu(bool pause)
    {
        pauseMenuEnabled = pause;
        PauseManager.Instance.SetPause(pauseMenuEnabled);

        if (pauseMenuEnabled)
            InitiatePausePanel();
        else
            HidePausePanel();
    }

    private void InitiatePausePanel()
    {
        Audio.Play(openSound, true);
        pausePanel.SetActive(true);
    }

    private void HidePausePanel()
    {
        Audio.Play(closeSound, true);
        pausePanel.SetActive(false);
    }

    public void ResumeButtonAction() => SetPauseMenu(false);
    public void ExitButtonAction()
    {
        Audio.Play(closeSound, true);
        levelExitInitiationChannel.Raise();
    }
}
