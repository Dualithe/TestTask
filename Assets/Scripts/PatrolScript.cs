using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class PatrolScript : MonoBehaviour
{
    [SerializeField] public List<Vector2> points;
    [SerializeField] float size;

    public PatrolScript(Vector2 centre)
    {
        points = new List<Vector2>();
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < points.Count; i++)
        {
            Gizmos.color = Color.cyan;
            var alpha = Gizmos.color;
            alpha.a *= 0.3f;
            Gizmos.color = alpha;
            Gizmos.DrawWireSphere(points[i], size);
        }

        for(int i = 0; i < points.Count-1; i++)
        {
            //points[i]
            //Gizmos.DrawLine();
        }
    }
}
