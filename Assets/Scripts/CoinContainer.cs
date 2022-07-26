using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class CoinContainer : MonoBehaviour
{
    [SerializeField] private VoidEvent OnCoinValueChanged;
    [SerializeField] private TextMeshProUGUI coinsText;
    private UnityAction handleCoinValChanged;
    private void OnEnable()
    {
        handleCoinValChanged = () => setCoins(GameManager.Get.CoinManager.TotalCoins);
        OnCoinValueChanged.OnEventRaised += handleCoinValChanged;
    }

    public void setCoins(int value)
    {
        coinsText.text = $"{value}";
    }

    public void OnDisable()
    {
        OnCoinValueChanged.OnEventRaised -= handleCoinValChanged;
    }
}
