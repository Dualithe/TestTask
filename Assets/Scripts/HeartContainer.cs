using UnityEngine;
using UnityEngine.Events;
using System;

public class HeartContainer : MonoBehaviour
{
    [SerializeField] private VoidEvent OnMaxHeartValueChanged;
    [SerializeField] private VoidEvent OnCurrHeartValueChanged;
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private Sprite heartDisabledSprite;
    [SerializeField] private Sprite heartEnabledSprite;
    private UnityAction handleMaxHeartValChanged;
    private UnityAction handleCurrHeartValChanged;
    private int currentHearts;

    private void Awake()
    {
        currentHearts = transform.childCount;
        handleMaxHeartValChanged = () => setMaxHearts(GameManager.Get.HeartManager.MaxHearts);
        handleCurrHeartValChanged = () => setCurrentHearts(GameManager.Get.HeartManager.CurrentHearts);
    }

    private void OnEnable()
    {
        OnMaxHeartValueChanged.OnEventRaised += handleMaxHeartValChanged;
        OnCurrHeartValueChanged.OnEventRaised += handleCurrHeartValChanged;
    }

    public void OnDisable()
    {
        OnMaxHeartValueChanged.OnEventRaised -= handleMaxHeartValChanged;
        OnCurrHeartValueChanged.OnEventRaised -= handleCurrHeartValChanged;
    }

    public void setMaxHearts(int hearts)
    {
        hearts = Math.Max(hearts, 0);
        var heartDelta = hearts - transform.childCount;
        if (heartDelta > 0)
        {
            for (int i = 0; i < heartDelta; i++)
            {
                var heart = Instantiate(heartPrefab, transform);
                heart.transform.GetChild(0).GetComponent<Animator>().Play("heart_regen");
                heart.transform.GetChild(0).GetComponent<Animator>().Rebind();
            }
        }
        else
        {
            for (int i = 0; i < -heartDelta; i++)
            {
                Destroy(gameObject.transform.GetChild(transform.childCount - 1));
            }
        }
    }

    public void setCurrentHearts(int hearts)
    {
        hearts = Math.Clamp(hearts, 0, transform.childCount);
        var start = Math.Min(hearts, currentHearts);
        var end = Math.Max(hearts, currentHearts);
        var heartAnimName = hearts < currentHearts ? "heart_lose" : "heart_regen";
        for (int i = start; i < end; i++)
        {
            transform.GetChild(i).transform.GetChild(0).GetComponent<Animator>().Play(heartAnimName);
        }
        currentHearts = hearts;
    }
}
