using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    private int value = 0;

    private void Start()
    {
        value = Random.Range(1, 5);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.Get.CoinManager.addCoins(value);
            Destroy(gameObject);
        }
    }
}
