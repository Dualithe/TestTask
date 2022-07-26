using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] HeroMovement mov;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        mov.IsGrounded = true;
    }
}
