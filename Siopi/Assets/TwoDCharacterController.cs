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

    public float speed = 5.0f; // Speed of character movement
    public float jumpForce = 10.0f; // Force of the jump
    public float glideFallSpeed = 2.0f; // Speed at which the character falls while gliding
    private Rigidbody2D rb;
    private bool isGrounded = true; // Check if the character is grounded
    private Vector2 moveInput;
    private bool isGliding = false; // Track if the character is currently gliding

    // Start is called before the first frame update
    void Start()
    {
        footstepManager = GetComponentInChildren<FootstepManager>();
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component attached to the character
    }

    // Update is called once per frame
    void Update()
    {
        // Horizontal Movement
        rb.velocity = new Vector2(moveInput.x * speed, rb.velocity.y);
        if(rb.velocity.x == 0)
        {
            isMoving = false;
        }
        RotateChildObject();

        // Implement glide mechanic, only if falling (y velocity < 0)
        if (isGliding && rb.velocity.y < 0 && !isGrounded)
        {
            // Apply a constant falling velocity to simulate gliding
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

    void RotateChildObject()
    {
        if (rb.velocity.x > 0.1f) // Moving right
        {
            // Set the child object rotation to face right
            ModelTransform.localEulerAngles = new Vector3(0, 0, 0);
        }
        else if (rb.velocity.x < -0.1f) // Moving left
        {
            // Set the child object rotation to face left
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
   
    // This function could be called from a separate action bound to the same jump button but with a "Hold" interaction
    public void OnStartGlide(InputAction.CallbackContext context)
    {
        // Only start gliding if the player is in the air and falling
        if (context.performed && !isGrounded && rb.velocity.y < 0)
        {
            isGliding = true;
        } else if (context.canceled)
        {
            isGliding = false;
        }
    }

    // Check for collisions with the ground
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the object we collided with has a tag "Ground", we are grounded
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            isGliding = false; // Stop gliding when touching the ground
        }
    }
}
