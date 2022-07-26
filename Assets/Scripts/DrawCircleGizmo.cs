using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DrawCircleGizmo : MonoBehaviour
{
    [SerializeField] Transform pointOrigin;
    [SerializeField] float size;

    [SerializeField] PatrolScript ps;

    private void Update()
    {
        ps = GetComponent<PatrolScript>();
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < ps.points.Count; i++)
        {
            Gizmos.color = Color.cyan;
            var alpha = Gizmos.color;
            alpha.a *= 0.3f;
            Gizmos.color = alpha;
            Gizmos.DrawWireSphere(ps.points[i], size);
        }
    }
}
