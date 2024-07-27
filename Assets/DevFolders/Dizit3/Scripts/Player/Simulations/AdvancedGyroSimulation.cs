using UnityEngine;

public class AdvancedGyroSimulation : MonoBehaviour
{
    public GameObject targetVertex;  // Указатель на трансформ угла, который должен оказаться вверху.
    public float stabilizationStrength = 10.0f;  // Сила стабилизации.
    public float correctionStrength = 20.0f;  // Сила коррекции при сильном отклонении.
    public float normalDamping = 0.1f;  // Обычное демпфирование при нормальной скорости вращения.
    public float highDamping = 0.5f;  // Повышенное демпфирование при высокой скорости вращения.
    public float speedThreshold = 1.0f;  // Порог скорости, при котором переключается режим демпфирования.

    private Rigidbody rb;  // Компонент Rigidbody объекта.

    void Start()
    {
        rb = GetComponent<Rigidbody>();  // Получение компонента Rigidbody.
        if (rb == null)
        {
            Debug.LogError("Rigidbody component is missing on the object!");  // Ошибка, если Rigidbody отсутствует.
        }
        else
        {
            rb.angularDamping = normalDamping;  // Установка начального демпфирования.
        }
        PlaceVertexObject(transform, targetVertex);  // Размещение целевой вершины.
    }

    void FixedUpdate()
    {
        // Определение вектора направления от объекта к целевой вершине.
        Vector3 targetDirection = (targetVertex.transform.position - transform.position).normalized;
        // Текущее направление "вверх" объекта.
        Vector3 currentUp = transform.up;
        // Вычисление вектора крутящего момента для коррекции ориентации объекта.
        Vector3 correctionTorque = Vector3.Cross(currentUp, targetDirection) * stabilizationStrength;
        // Применение крутящего момента к Rigidbody.
        rb.AddTorque(correctionTorque);
        // Вычисление угла между текущим направлением "вверх" и целевым направлением.
        float angle = Vector3.Angle(currentUp, targetDirection);
        // Коррекция при значительном отклонении.
        if (angle > 80)
        {
            rb.AddTorque(correctionTorque * Mathf.Min(correctionStrength, angle - 80) / 30.0f);
        }
        // Адаптация демпфирования в зависимости от скорости вращения.
        rb.angularDamping = CalculateDamping(rb.angularVelocity.magnitude);
    }

    // Функция для расчета значения демпфирования в зависимости от скорости вращения.
    float CalculateDamping(float currentAngularSpeed)
    {
        return currentAngularSpeed > speedThreshold ? highDamping : normalDamping;
    }

    // Функция для размещения объекта-вершины в нужной позиции относительно куба.
    void PlaceVertexObject(Transform cubeTransform, GameObject vertexObject)
    {
        float size = cubeTransform.localScale.x;
        float halfSize = size / 2;
        vertexObject.transform.position = cubeTransform.position + new Vector3(halfSize, halfSize, halfSize);
        vertexObject.transform.parent = cubeTransform;  // Сделать вершину дочерним объектом куба.
    }
}
