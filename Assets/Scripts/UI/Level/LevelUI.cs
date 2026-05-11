using TMPro;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private EventChannel LevelLoadedChannel;
    [Space]
    [SerializeField] private EventChannelInt HealthChangedChannel;
    [SerializeField] private EventChannelInt MoneyChangedChannel;
    [SerializeField] private EventChannel WaveEndChannel;
    [Space]
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text waveText;

    private int currentWave = 0;

    public void OnEnable()
    {
        LevelLoadedChannel.Subscribe(SetStartValues);

        HealthChangedChannel.Subscribe(UpdateHealthText);
        MoneyChangedChannel.Subscribe(UpdateMoneyText);
        WaveEndChannel.Subscribe(UpdateWaveText);
    }

    public void OnDisable()
    {
        LevelLoadedChannel.Unsubscribe(SetStartValues);

        HealthChangedChannel.Unsubscribe(UpdateHealthText);
        MoneyChangedChannel.Unsubscribe(UpdateMoneyText);
        WaveEndChannel.Unsubscribe(UpdateWaveText);
    }

    private void UpdateHealthText(int health)
    {
        healthText.text = health.ToString();
    }

    private void UpdateMoneyText(int money)
    {
        moneyText.text = money.ToString();
    }

    private void UpdateWaveText()
    {
        currentWave++;
        waveText.text = currentWave.ToString();
    }

    private void SetStartValues()
    {
        UpdateHealthText(PlayerHealth.Instance.Health);
        UpdateMoneyText(PlayerMoney.Instance.Money);
        UpdateWaveText();
    }
}
