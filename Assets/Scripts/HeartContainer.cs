using UnityEngine;
using UnityEngine.Events;
using System;

public class HeartContainer : MonoBehaviour
{
    [SerializeField] private IntEvent OnMaxHeartValueChanged;
    [SerializeField] private IntIntEvent OnCurrHeartValueChanged;
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private Sprite heartDisabledSprite;
    [SerializeField] private Sprite heartEnabledSprite;
    private UnityAction<int> handleMaxHeartValChanged;
    private UnityAction<int, int> handleCurrHeartValChanged;

    private void Awake()
    {
        handleMaxHeartValChanged = (value) => setMaxHearts(value);
        handleCurrHeartValChanged = (pastHearts, currentHearts) => setCurrentHearts(pastHearts, currentHearts);
    }

    private void Start()
    {
        setMaxHearts(GameManager.Get.HeartManager.MaxHearts);
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
                Instantiate(heartPrefab, transform);
                //heart.transform.GetChild(0).GetComponent<Animator>().Play("heart_regen");
            }
        }
        else
        {
            for (int i = 0; i < -heartDelta; i++)
            {
                Destroy(transform.GetChild(transform.childCount - 1 - i).gameObject);
            }
        }
        int currentHearts = GameManager.Get.HeartManager.CurrentHearts;
        for (int i = 0; i < currentHearts; i++)
        {
            transform.GetChild(i).transform.GetChild(0).GetComponent<Animator>().Play("heart_full");
        }
        for (int i = currentHearts; i < hearts; i++)
        {
            transform.GetChild(i).transform.GetChild(0).GetComponent<Animator>().Play("heart_empty");
        }
    }

    public void setCurrentHearts(int pastHearts, int currentHearts)
    {
        currentHearts = Math.Clamp(currentHearts, 0, transform.childCount);
        var start = Math.Min(currentHearts, pastHearts);
        var end = Math.Max(currentHearts, pastHearts);
        var heartAnimName = currentHearts < pastHearts ? "heart_lose" : "heart_regen";
        for (int i = start; i < end; i++)
        {
            transform.GetChild(i).transform.GetChild(0).GetComponent<Animator>().Play(heartAnimName);
        }
    }
}
