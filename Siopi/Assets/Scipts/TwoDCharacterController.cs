using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TwoDCharacterController : MonoBehaviour
{
    public FootstepManager footstepManager;
    private bool isMoving;
    private bool isRunning = false;
    public Transform ModelTransform;
    public bool isBDoorOpen;

    public float speed = 5.0f; // Speed of character movement
    public float jumpForce = 10.0f; // Force of the jump
    public float glideFallSpeed = 2.0f; // Speed at which the character falls while gliding
    private Rigidbody2D rb;
    private bool isGrounded = true; // Check if the character is grounded
    public bool isOnPlatform = false; // To check if the character is on a moving platform
    private Transform currentPlatform; // To keep track of the current platform the character is on
    private Vector2 moveInput;
    private bool isGliding = false; // Track if the character is currently gliding
    public int collectibleCount = 0;


    void Start()
    {
        
        footstepManager = GetComponentInChildren<FootstepManager>();
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component attached to the character
    }

    void Update()
    {
        RotateChildObject();

        if (isGliding && rb.velocity.y < 0 && !isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, -glideFallSpeed);
        }

        if (isMoving && isGrounded)
        {
            footstepManager.PlayFootsteps(isRunning);
        }
        else
        {
            footstepManager.StopFootsteps();
        }
    }

    void FixedUpdate()
    {
        if (isOnPlatform && currentPlatform != null)
        {
            // Get the current platform's velocity
            Vector2 platformVelocity = currentPlatform.GetComponent<PlatformMove>().velocity;
            // Apply platform velocity directly to player
            rb.velocity = new Vector2(moveInput.x * speed + platformVelocity.x, rb.velocity.y);
        }
        else
        {
            // Regular movement without platform velocity
            rb.velocity = new Vector2(moveInput.x * speed, rb.velocity.y);
        }
    }

    void RotateChildObject()
    {
        if (rb.velocity.x > 0.1f) // Moving right
        {
            ModelTransform.localEulerAngles = new Vector3(0, 0, 0);
        }
        else if (rb.velocity.x < -0.1f) // Moving left
        {
            ModelTransform.localEulerAngles = new Vector3(0, 180, 0);
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isGrounded = false; // Assume not grounded until collision check
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        isMoving = moveInput.x != 0;
        isRunning = true;
    }

    public void OnStartGlide(InputAction.CallbackContext context)
    {
        if (context.performed && !isGrounded && rb.velocity.y < 0)
        {
            isGliding = true;
        }
        else if (context.canceled)
        {
            isGliding = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            isGliding = false;
        } else
        if (collision.gameObject.tag == "Platform")
        {
            isOnPlatform = true;
            currentPlatform = collision.transform;
            isGrounded = true;
            isGliding = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isOnPlatform = false;
            currentPlatform = null;
        }
    }
    public void AddCollectible()
    {
        collectibleCount++;
        if(collectibleCount>=4)
        {
            isBDoorOpen = true;
            //openDoor
        }
    }
}
