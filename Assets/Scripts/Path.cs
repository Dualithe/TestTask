using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Path
{

    [SerializeField, HideInInspector] List<Vector2> points;

    public Path(Vector2 centre)
    {
        points = new List<Vector2>
        {
            centre+Vector2.left,
            centre+Vector2.right
        };
    }
    public Vector2 this[int i]
    {
        get
        {
            return points[i];
        }
    }

    public void AddPoint(Vector2 point)
    {
        points.Add(point);
    }
    public void RemovePoint(int i)
    {
        points.RemoveAt(i);
    }

    public int getPathLength()
    {
        return points.Count;
    }

    public void MovePoint(int i, Vector2 pos)
    {
        points[i] = pos;
    }
}
