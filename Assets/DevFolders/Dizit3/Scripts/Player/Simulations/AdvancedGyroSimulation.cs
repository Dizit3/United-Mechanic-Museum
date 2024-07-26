using UnityEngine;

public class AdvancedGyroSimulation : MonoBehaviour
{
    public GameObject targetVertex;  // Указатель на трансформ угла, который должен оказаться вверху
    public float stabilizationStrength = 10.0f;
    public float correctionStrength = 20.0f;  // Уменьшенная сила коррекции
    public float normalDamping = 0.1f;  // Нормальное демпфирование
    public float highDamping = 0.5f;  // Высокое демпфирование при быстром вращении
    public float speedThreshold = 1.0f;  // Порог скорости для переключения режимов демпфирования

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component is missing on the object!");
        }
        else
        {
            rb.angularDamping = normalDamping;  // Инициализация демпфирования
        }
        PlaceVertexObject(transform, targetVertex);
    }

    void FixedUpdate()
    {
        // Целевой вектор от центра объекта к вершине
        Vector3 targetDirection = (targetVertex.transform.position - transform.position).normalized;
        // Текущий вектор "вверх" для объекта
        Vector3 currentUp = transform.up;
        // Вычисление крутящего момента для коррекции ориентации
        Vector3 correctionTorque = Vector3.Cross(currentUp, targetDirection) * stabilizationStrength;
        // Применение крутящего момента для стабилизации
        rb.AddTorque(correctionTorque);
        // Вычисление угла между текущим вектором "вверх" и целевым вектором
        float angle = Vector3.Angle(currentUp, targetDirection);
        // Плавное увеличение коррекции, если объект находится в неправильном положении
        if (angle > 80)
        {
            rb.AddTorque(correctionTorque * Mathf.Min(correctionStrength, angle - 80) / 30.0f);
        }
        // Адаптивное демпфирование
        rb.angularDamping = CalculateDamping(rb.angularVelocity.magnitude);
    }

    float CalculateDamping(float currentAngularSpeed)
    {
        return currentAngularSpeed > speedThreshold ? highDamping : normalDamping;
    }

    void PlaceVertexObject(Transform cubeTransform, GameObject vertexObject)
    {
        float size = cubeTransform.localScale.x;
        float halfSize = size / 2;
        vertexObject.transform.position = cubeTransform.position + new Vector3(halfSize, halfSize, halfSize);
        vertexObject.transform.parent = cubeTransform;
    }
}
