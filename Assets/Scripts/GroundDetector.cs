using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] HeroBehavior mov;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        mov.IsGrounded = true;
    }
}
