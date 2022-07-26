using UnityEngine;
using System;

public class HeartManager : MonoBehaviour
{
    [SerializeField] private VoidEvent OnMaxHeartValueChanged;
    [SerializeField] private VoidEvent OnCurrHeartValueChanged;
    [SerializeField] private int maxHearts;
    public int MaxHearts => maxHearts;

    [SerializeField] private int currentHearts;
    public int CurrentHearts => currentHearts;

    private void Start()
    {
        setMaxHp(3);
        setCurrentHp(maxHearts);
    }

    public void removeHeart(int damage)
    {
        currentHearts = Math.Max(currentHearts - damage, 0);
        OnCurrHeartValueChanged.RaiseEvent();
        if (currentHearts == 0)
            GameManager.Get.Player.GetComponent<HeroHealth>().animateDeath();
    }

    public void addMaxHeart(int value)
    {
        maxHearts += value;
        OnMaxHeartValueChanged.RaiseEvent();
        regenHeart(value);
    }

    public void regenHeart(int value)
    {
        currentHearts += Math.Clamp(value, 0, maxHearts);
        OnCurrHeartValueChanged.RaiseEvent();
    }

    public void setMaxHp(int value)
    {
        maxHearts = value;
        OnMaxHeartValueChanged.RaiseEvent();
    }

    public void setCurrentHp(int value)
    {
        currentHearts = Math.Clamp(value, 0, maxHearts);
        OnCurrHeartValueChanged.RaiseEvent();
    }
}
