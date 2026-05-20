using TMPro;
using UnityEngine;

public class LevelEndMenu : MonoBehaviour
{
    [SerializeField] private EventChannelBool levelEndChannel;
    [SerializeField] private EventChannel levelExitInitiationChannel;
    [Space]
    [SerializeField] private GameObject menuPanel;
    [Space]
    [SerializeField] private TMP_Text menuTitle;
    [SerializeField] private string winTitle;
    [SerializeField] private string loseTitle;
    [SerializeField] private Sound winSound;
    [SerializeField] private Sound loseSound;

    private void Awake()
    {
        menuPanel.SetActive(false);
    }

    private void OnEnable()
    {
        levelEndChannel.Subscribe(HandleLevelEnd);
    }

    private void OnDisable()
    {
        levelEndChannel.Unsubscribe(HandleLevelEnd);
    }

    private async void HandleLevelEnd(bool end)
    {
        menuPanel.SetActive(true);
        menuTitle.text = end ? winTitle : loseTitle;

        Audio.Play(end ? winSound : loseSound);
        await Awaitable.WaitForSecondsAsync(end ? winSound.Clip.length : loseSound.Clip.length);
        InputReader.OnStartPerformed += InitiateLevelEnd;
    }

    private void InitiateLevelEnd()
    {
        InputReader.OnStartPerformed -= InitiateLevelEnd;
        levelExitInitiationChannel.Raise();
    }
}
