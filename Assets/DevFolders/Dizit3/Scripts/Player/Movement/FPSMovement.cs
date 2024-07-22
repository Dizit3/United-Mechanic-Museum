using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPSMovement : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float jumpHeight = 2f;

    private const float GRAVITY = -9.18f;

    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction jumpAction;

    private Vector3 _velocity = Vector3.zero;
    private bool _isGrounded = false;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        lookAction = playerInput.actions["Look"];
        jumpAction = playerInput.actions["Jump"];
    }

    private void Update()
    {
        MovePlayer();
        //Jump();
        //ApplyGravity();
    }


    private void MovePlayer()
    {
        Vector2 movementInput = moveAction.ReadValue<Vector2>();
        Vector3 move = transform.right * movementInput.x + transform.forward * movementInput.y;
        controller.Move(move * speed * Time.deltaTime);
    }

    private void Jump()
    {
        throw new NotImplementedException();
    }

    private void ApplyGravity()
    {
        throw new NotImplementedException();
    }
}
