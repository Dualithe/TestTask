using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] heroMovement mov;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        mov.IsGrounded = true;
    }
}
