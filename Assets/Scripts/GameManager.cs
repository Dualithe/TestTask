using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Get => instance;

    [SerializeField] private CoinManager cm;
    public CoinManager CoinManager => cm;

    [SerializeField] private HeartManager hm;
    public HeartManager HeartManager => hm;

    [SerializeField] private GameObject player;
    public GameObject Player => player;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
