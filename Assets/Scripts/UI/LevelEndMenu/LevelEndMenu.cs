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

    private void HandleLevelEnd(bool end)
    {
        menuPanel.SetActive(true);
        menuTitle.text = end ? winTitle : loseTitle;
        InputReader.OnStartPerformed += InitiateLevelEnd;
    }

    private void InitiateLevelEnd()
    {
        InputReader.OnStartPerformed -= InitiateLevelEnd;
        levelExitInitiationChannel.Raise();
    }
}
