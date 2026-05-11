using UnityEngine;
using UnityEngine.UI;

public class WaveButton : MonoBehaviour
{
    [SerializeField] private EventChannel waveStartChannel;
    [SerializeField] private EventChannel waveEndChannel;
    [Space]
    [SerializeField] private Button button;

    private void OnEnable()
    {
        waveStartChannel.Subscribe(TurnOff);
        waveEndChannel.Subscribe(TurnOn);
    }

    private void OnDisable()
    {
        waveStartChannel.Unsubscribe(TurnOff);
        waveEndChannel.Unsubscribe(TurnOn);
    }

    private void TurnOn() => button.interactable = true;
    private void TurnOff() => button.interactable = false;
}
