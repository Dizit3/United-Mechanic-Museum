using UnityEngine;
using UnityEngine.InputSystem; // Подключение системы ввода: https://docs.unity3d.com/ScriptReference/InputSystem.html

[RequireComponent(typeof(CharacterController))] // Требование наличия компонента CharacterController: https://docs.unity3d.com/ScriptReference/RequireComponent.html
public class SimpleMovement : MonoBehaviour
{
    public float speed = 5f; // Скорость перемещения.
    public float jumpHeight = 1.5f; // Высота прыжка.

    private CharacterController controller; // Контроллер персонажа.
    private PlayerInput playerInput; // Ввод игрока.
    private Vector3 velocity; // Вектор скорости.
    private bool isGrounded; // Статус нахождения на земле.

    void Awake()
    {
        controller = GetComponent<CharacterController>(); // Получение компонента контроллера персонажа: https://docs.unity3d.com/ScriptReference/CharacterController.html
        playerInput = GetComponent<PlayerInput>(); // Получение компонента ввода игрока: https://docs.unity3d.com/ScriptReference/InputSystem.PlayerInput.html
    }

    void Update()
    {
        isGrounded = controller.isGrounded; // Проверка на земле ли персонаж: https://docs.unity3d.com/ScriptReference/CharacterController-isGrounded.html
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Сброс скорости падения.
        }

        Vector2 moveInput = playerInput.actions["Move"].ReadValue<Vector2>(); // Чтение ввода перемещения: https://docs.unity3d.com/ScriptReference/InputSystem.InputAction.ReadValue.html
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y; // Расчет вектора перемещения.
        controller.Move(move * speed * Time.deltaTime); // Перемещение персонажа: https://docs.unity3d.com/ScriptReference/CharacterController.Move.html

        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded) // Обработка прыжка: https://docs.unity3d.com/ScriptReference/InputSystem.Keyboard.html
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y); // Расчет начальной скорости прыжка: https://docs.unity3d.com/ScriptReference/Mathf.Sqrt.html
        }

        velocity.y += Physics.gravity.y * Time.deltaTime; // Применение гравитации: https://docs.unity3d.com/ScriptReference/Physics-gravity.html
        controller.Move(velocity * Time.deltaTime); // Перемещение с учетом вертикальной скорости.
    }
}
