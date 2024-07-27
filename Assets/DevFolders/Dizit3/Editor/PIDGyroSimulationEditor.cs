using UnityEditor;
using UnityEngine;

// Объявление кастомного редактора для скрипта PIDGyroSimulation
[CustomEditor(typeof(PIDGyroSimulation))]
public class PIDGyroSimulationEditor : Editor
{
    // Переопределение метода для отображения пользовательского интерфейса в инспекторе
    public override void OnInspectorGUI()
    {
        // Получение ссылки на скрипт, который в данный момент редактируется
        PIDGyroSimulation script = (PIDGyroSimulation)target;

        // Создание поля для выбора объекта в сцене (https://docs.unity3d.com/ScriptReference/EditorGUILayout.ObjectField.html)
        script.targetVertex = (GameObject)EditorGUILayout.ObjectField("Target Vertex", script.targetVertex, typeof(GameObject), true);

        // Поля для редактирования PID параметров первой оси
        script.kp1 = EditorGUILayout.FloatField("Kp1", script.kp1);  // Пропорциональный коэффициент
        script.duplicateKp = EditorGUILayout.Toggle("Duplicate Kp", script.duplicateKp);  // Чекбокс для дублирования значения
        if (!script.duplicateKp)
        {
            script.kp2 = EditorGUILayout.FloatField("Kp2", script.kp2);  // Поле для Kp2, если не дублируется
        }

        script.ki1 = EditorGUILayout.FloatField("Ki1", script.ki1);  // Интегральный коэффициент
        script.duplicateKi = EditorGUILayout.Toggle("Duplicate Ki", script.duplicateKi);  // Чекбокс для дублирования значения
        if (!script.duplicateKi)
        {
            script.ki2 = EditorGUILayout.FloatField("Ki2", script.ki2);  // Поле для Ki2, если не дублируется
        }

        script.kd1 = EditorGUILayout.FloatField("Kd1", script.kd1);  // Дифференциальный коэффициент
        script.duplicateKd = EditorGUILayout.Toggle("Duplicate Kd", script.duplicateKd);  // Чекбокс для дублирования значения
        if (!script.duplicateKd)
        {
            script.kd2 = EditorGUILayout.FloatField("Kd2", script.kd2);  // Поле для Kd2, если не дублируется
        }

        // Дополнительные параметры для управления поведением
        script.normalDamping = EditorGUILayout.FloatField("Normal Damping", script.normalDamping);  // Нормальное демпфирование
        script.highDamping = EditorGUILayout.FloatField("High Damping", script.highDamping);  // Высокое демпфирование
        script.speedThreshold = EditorGUILayout.FloatField("Speed Threshold", script.speedThreshold);  // Порог скорости

        // Проверка на изменения в инспекторе и пометка объекта как измененного
        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);  // Отметить объект как измененный (https://docs.unity3d.com/ScriptReference/EditorUtility.SetDirty.html)
        }
    }
}
