using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] HeroBehavior mov;
    public float timeSinceLeavingGround;
    private Collider2D col;
    public Collider2D gdCol => col;
    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

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
