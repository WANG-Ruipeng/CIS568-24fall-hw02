using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SubCubeDetector))]
public class SubCubeDetectorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SubCubeDetector detector = (SubCubeDetector)target;
        if (GUILayout.Button("Move Detection Center to Current Position"))
        {
            detector.MoveDetectionCenterToPosition();
        }
    }
}
