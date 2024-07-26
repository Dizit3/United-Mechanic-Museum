using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PIDGyroSimulation))]
public class PIDGyroSimulationEditor : Editor
{
    public override void OnInspectorGUI()
    {
        PIDGyroSimulation script = (PIDGyroSimulation)target;

        script.targetVertex = (GameObject)EditorGUILayout.ObjectField("Target Vertex", script.targetVertex, typeof(GameObject), true);

        // PID параметры для первой оси
        script.kp1 = EditorGUILayout.FloatField("Kp1", script.kp1);
        script.duplicateKp = EditorGUILayout.Toggle("Duplicate Kp", script.duplicateKp);
        if (!script.duplicateKp)
        {
            script.kp2 = EditorGUILayout.FloatField("Kp2", script.kp2);
        }

        script.ki1 = EditorGUILayout.FloatField("Ki1", script.ki1);
        script.duplicateKi = EditorGUILayout.Toggle("Duplicate Ki", script.duplicateKi);
        if (!script.duplicateKi)
        {
            script.ki2 = EditorGUILayout.FloatField("Ki2", script.ki2);
        }

        script.kd1 = EditorGUILayout.FloatField("Kd1", script.kd1);
        script.duplicateKd = EditorGUILayout.Toggle("Duplicate Kd", script.duplicateKd);
        if (!script.duplicateKd)
        {
            script.kd2 = EditorGUILayout.FloatField("Kd2", script.kd2);
        }

        script.normalDamping = EditorGUILayout.FloatField("Normal Damping", script.normalDamping);
        script.highDamping = EditorGUILayout.FloatField("High Damping", script.highDamping);
        script.speedThreshold = EditorGUILayout.FloatField("Speed Threshold", script.speedThreshold);

        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
    }
}
