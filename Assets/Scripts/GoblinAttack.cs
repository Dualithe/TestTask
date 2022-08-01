using UnityEngine;

public class GoblinAttack : MonoBehaviour
{
    [SerializeField] private GoblinBehavior gob;
    public Collider2D lastPlayerHit;
    public bool isInCollider = false;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" && gob.enemyAttackCooldown == 0)
        {
            isInCollider = true;
            gob.animator.Play("goblin_attack");
            gob.body.velocity = Vector2.zero;
            gob.isMoving = false;
            gob.enemyAttackCooldown = gob.enemyStartingAttackCooldown;
            lastPlayerHit = collision;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isInCollider = false;
    }
}
