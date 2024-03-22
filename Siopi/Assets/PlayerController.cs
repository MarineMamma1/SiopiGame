using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 10f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;
    public Transform cameraTransform;
    //public float rotationSpeed = 1.0f;

    private float basespeed;
    private bool running;
    private Vector3 velocity;
    private bool isGrounded;
    private Vector2 moveInput;
    private float runspeed;
    //private Vector2 mouseMovement;
    
        void Start()
    {
        basespeed = speed;
        runspeed = speed * 1.5f;
        Debug.Log(runspeed);
    }

    void Update()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // A small force to keep the character grounded
        }

        // Calculate move direction to be relative to character's orientation and optionally the camera's orientation
        Vector3 moveDirection = (cameraTransform.forward * moveInput.y + cameraTransform.right * moveInput.x).normalized;

        // Apply the movement
        Vector3 move = moveDirection * speed;
        controller.Move(move * Time.deltaTime);

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Reset vertical velocity when grounded
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {   Debug.Log("jump");
        if (context.performed && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
    public void OnRun(InputAction.CallbackContext context)
    {
        Debug.Log("SAIKSDFLIWAGILG");
        if (context.performed)
        {
          
            speed = runspeed;

        } else if (context.canceled)
        {
            speed = basespeed;
        }
    }
  
}
