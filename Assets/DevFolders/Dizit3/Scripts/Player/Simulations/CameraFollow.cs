using UnityEngine.InputSystem; // Подключение системы ввода: https://docs.unity3d.com/ScriptReference/InputSystem.html
using UnityEngine; // Основное пространство имён Unity: https://docs.unity3d.com/ScriptReference/

public class CameraFollow : MonoBehaviour
{
    public float mouseSensitivity = 50f; // Чувствительность мыши.
    public float smoothSpeed = 0.125f; // Скорость плавного следования

    public Transform targetBody; // Трансформ объекта, который будет вращаться.
    public Vector3 offset = new Vector3(0f, 2f, -5f);

    private float xRotation = 0f; // Накопленное вращение по оси X.


    void Update()
    {

        transform.LookAt(targetBody);
        // Плавно перемещаем камеру к целевой позиции: https://docs.unity3d.com/ScriptReference/Vector3.Lerp.html
        transform.position = Vector3.Lerp(transform.position, targetBody.position + offset, smoothSpeed);
    }
}
