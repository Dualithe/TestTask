using UnityEngine;
using System;

public class HeartManager : MonoBehaviour
{
    [SerializeField] private IntEvent OnMaxHeartValueChanged;
    [SerializeField] private IntIntEvent OnCurrHeartValueChanged;
    [SerializeField] private int maxHearts;
    public int MaxHearts => maxHearts;

    [SerializeField] private int currentHearts;
    public int CurrentHearts => currentHearts;

    [SerializeField] private float iFramesMax;
    public float iFrames;

    private void Start()
    {
        iFramesRefresh();
    }

    private void FixedUpdate()
    {
        iFrames -= Time.deltaTime;
        iFrames = Math.Max(iFrames, 0);
    }

    public void doDamage(int value)
    {
        removeHeart(value);
        iFramesRefresh();
    }

    private void iFramesRefresh()
    {
        iFrames = iFramesMax;
    }

    public void removeHeart(int value)
    {
        setCurrentHp(currentHearts - value);
        if (currentHearts == 0)
            GameManager.Get.Player.GetComponent<HeroHealth>().animateDeath();
    }

    public void addMaxHeart(int value)
    {
        setMaxHp(maxHearts + value);
    }

    public void regenHeart(int value)
    {
        setCurrentHp(currentHearts + value);
    }

    public void setMaxHp(int value)
    {
        maxHearts = Math.Max(value, 1);
        currentHearts = Math.Min(currentHearts,maxHearts);
        OnMaxHeartValueChanged.RaiseEvent(maxHearts);
    }

    public void setCurrentHp(int value)
    {
        var pastHearts = currentHearts;
        currentHearts = Math.Clamp(value, 0, maxHearts);
        OnCurrHeartValueChanged.RaiseEvent(pastHearts,currentHearts);
    }
}
