using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] HeroBehavior mov;
    public float timeSinceLeavingGround;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        mov.jumped = false;
        mov.IsGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        timeSinceLeavingGround = Time.time;
    }
}
