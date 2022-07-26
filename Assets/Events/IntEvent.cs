using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Int Event")]
public class IntEvent : ScriptableObject
{
    public UnityAction<int> OnEventRaised;

    public void RaiseEvent(int a)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(a);
    }
}

