using UnityEngine.InputSystem; // Подключение системы ввода: https://docs.unity3d.com/ScriptReference/InputSystem.html
using UnityEngine; // Основное пространство имён Unity: https://docs.unity3d.com/ScriptReference/

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 50f; // Чувствительность мыши.
    public Transform playerBody; // Трансформ игрока, который будет вращаться.

    private float xRotation = 0f; // Накопленное вращение по оси X.

    void Update()
    {
        Vector2 lookInput = Mouse.current.delta.ReadValue(); // Получение вектора движения мыши: https://docs.unity3d.com/ScriptReference/InputSystem.Mouse-delta.html
        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime; // Горизонтальное вращение.
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime; // Вертикальное вращение.

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Ограничение вращения камеры: https://docs.unity3d.com/ScriptReference/Mathf.Clamp.html

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Применение вращения камеры: https://docs.unity3d.com/ScriptReference/Quaternion.Euler.html
        playerBody.Rotate(Vector3.up * mouseX); // Вращение тела игрока: https://docs.unity3d.com/ScriptReference/Transform.Rotate.html
    }
}
