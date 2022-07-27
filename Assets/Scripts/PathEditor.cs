using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PathCreator))]
public class PathEditor : Editor
{
    PathCreator creator;
    Path path;

    private void OnSceneGUI()
    {
        Draw();
    }

    //public override void OnInspectorGUI()
    //{
    //    Label();
    //}
    //void Label()
    //{
    //    GUILayout.Label("Tutorial: Tworzenie nowego punktu patrolu:       Shift+LMB \n" +
    //                    "                 Usuwanie punktu patrolu:                        Shift+RMB\n" +
    //                    "                 Przesuwanie punktu patrolu:                   LMB\n");
    //}

    //void Input()
    //{
    //    Event guiEvent = Event.current;
    //    Vector2 mousePos = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition).origin;

    //    if (guiEvent.type == EventType.MouseDown && guiEvent.button == 0 && guiEvent.shift)
    //    {
    //        Undo.RecordObject(creator, "Add segment");
    //        path.AddPoint(mousePos);
    //    }

    //    if (guiEvent.type == EventType.MouseDown && guiEvent.button == 1 && guiEvent.shift)
    //    {
    //        float minDstToAnchor = .15f;
    //        int closestAnchorIndex = -1;

    //        for (int i = 0; i < path.getPathLength(); i++)
    //        {
    //            float dst = Vector2.Distance(mousePos, path[i]);
    //            if (dst < minDstToAnchor)
    //            {
    //                minDstToAnchor = dst;
    //                closestAnchorIndex = i;
    //            }
    //        }
    //        if (closestAnchorIndex != -1)
    //        {
    //            Undo.RecordObject(creator, "Remove segment");
    //            path.RemovePoint(closestAnchorIndex);
    //        }
    //    }
    //}

    void Draw()
    {
        Handles.color = Color.magenta;
        for (int i = 0; i < path.getPathLength(); i++)
        {
            Handles.DrawDottedLine(path[i] + Vector2.up * 5, path[i] + Vector2.down * 5, 1.5f);
            Vector2 newPos = Handles.FreeMoveHandle(path[i], Quaternion.identity, .3f, Vector2.zero, Handles.CylinderHandleCap);
            if (path[i] != newPos)
            {
                Undo.RecordObject(creator, "Move point");
                path.MovePoint(i, newPos);
            }
        }
        Handles.DrawLine(path[0], path[1], 3.5f);
    }

    private void OnEnable()
    {
        creator = (PathCreator)target;
        if (creator.path == null)
        {
            creator.CreatePath();
        }
        path = creator.path;
    }
}
