using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineGizmo : MonoBehaviour
{
    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        var alpha = Gizmos.color;
        alpha.a *= 0.3f;
        Gizmos.color = alpha;
        Gizmos.DrawLine(pointA.position, pointB.position);
    }
}
