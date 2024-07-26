using UnityEngine;

public class AdvancedGyroSimulation : MonoBehaviour
{
    public float rotationSpeed = 500.0f;  // Скорость вращения гироскопа
    public float stabilizationStrength = 10.0f;  // Сила стабилизации
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;  // Включаем гравитацию
        rb.angularDamping = 0.1f;  // Небольшое сопротивление вращению
    }

    void FixedUpdate()
    {
        // Применяем постоянный крутящий момент для поддержания вращения
        rb.AddRelativeTorque(Vector3.up * rotationSpeed * Time.fixedDeltaTime);

        // Определяем целевую ориентацию (например, вертикально вверх)
        Vector3 targetDirection = Vector3.up;
        Vector3 currentUp = transform.up;

        // Вычисляем момент для стабилизации ориентации
        Vector3 torque = Vector3.Cross(currentUp, targetDirection);
        rb.AddTorque(torque * stabilizationStrength * Time.fixedDeltaTime);
    }
}
