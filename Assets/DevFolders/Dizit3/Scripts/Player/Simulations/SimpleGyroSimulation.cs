using UnityEngine;

public class SimpleGyroSimulation : MonoBehaviour
{
    public float rotationSpeed = 100.0f;
    public Vector3 targetAngularDirection = Vector3.up;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Опционально: устанавливаем, чтобы физика не замедляла вращение
        rb.angularDamping = 0;
    }

    void FixedUpdate()
    {
        // Применяем вращение
        rb.AddTorque(transform.up * rotationSpeed * Time.deltaTime);

        // Стабилизация: Пытаемся минимизировать угол между текущим вектором вверх и целевым
        Vector3 currentUp = transform.up;
        Vector3 torqueVector = Vector3.Cross(currentUp, targetAngularDirection);
        rb.AddTorque(torqueVector * rotationSpeed * 0.1f);
    }
}
