using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Int Int Event")]
public class IntIntEvent : ScriptableObject
{
    public UnityAction<int,int> OnEventRaised;

    public void RaiseEvent(int a, int b)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(a,b);
    }
}
