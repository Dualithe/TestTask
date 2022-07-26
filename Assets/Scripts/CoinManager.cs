using UnityEngine;
using System;

public class CoinManager : MonoBehaviour
{
    [SerializeField] private VoidEvent OnCoinValueChanged;
    private int totalCoins = 0;
    public int TotalCoins => totalCoins;

    public void addCoins(int value)
    {
        totalCoins = Math.Max(totalCoins + value, 0);
        OnCoinValueChanged.RaiseEvent();

    }
}
