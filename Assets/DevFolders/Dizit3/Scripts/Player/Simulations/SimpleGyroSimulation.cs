using UnityEngine;

public class SimpleGyroSimulation : MonoBehaviour
{
    public float rotationSpeed = 100.0f;  // Скорость вращения объекта.
    public Vector3 targetAngularDirection = Vector3.up;  // Целевое направление вращения (по умолчанию вверх).
    private Rigidbody rb;  // Ссылка на компонент Rigidbody.

    void Start()
    {
        rb = GetComponent<Rigidbody>();  // Получаем компонент Rigidbody.
        // Опционально: устанавливаем нулевое демпфирование, чтобы физика не замедляла вращение.
        rb.angularDamping = 0;
    }

    void FixedUpdate()
    {
        // Применяем вращение вокруг оси вверх с заданной скоростью.
        rb.AddTorque(transform.up * rotationSpeed * Time.deltaTime);

        // Стабилизация: Пытаемся минимизировать угол между текущим вектором вверх и целевым.
        Vector3 currentUp = transform.up;  // Текущее направление вверх объекта.
        Vector3 torqueVector = Vector3.Cross(currentUp, targetAngularDirection);  // Вычисляем вектор крутящего момента.
        rb.AddTorque(torqueVector * rotationSpeed * 0.1f);  // Применяем крутящий момент для коррекции положения.
    }
}
