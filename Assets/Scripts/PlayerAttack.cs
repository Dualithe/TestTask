using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private int playerDamage = 1;
    private bool entered = false;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && !entered)
        {
            collision.GetComponent<MushroomBehavior>()?.takeDamage(playerDamage);
            collision.GetComponent<GoblinBehavior>()?.takeDamage(playerDamage);
            entered = true;
        }
    }
}
