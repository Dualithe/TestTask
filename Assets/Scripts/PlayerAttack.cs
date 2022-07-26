using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
            //collision.GetComponent<EnemyHealth>().dealDamage()
                ;
    }
}
