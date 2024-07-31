using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class CharacterController3D : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float acceleration = 60f;
    public float deceleration = 60f;
    public float airAcceleration = 30f;
    public float airDeceleration = 10f;

    [Header("Jumping")]
    public float jumpForce = 10f;
    public float gravity = -9.81f;
    public float glideGravity = -2f;
    public float maxFallSpeed = -20f;
    public float coyoteTime = 0.2f;
    public float jumpBufferTime = 0.2f;

    [Header("Air Control")]
    public float airControl = 0.3f;

    [Header("Ground Detection")]
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public float slopeLimit = 45f;

    [Header("Debug")]
    public bool showDebug = false;

    [Header("Character Model")]
    public Transform characterModel;

    public CharacterController controller;
    private Transform cameraTransform;
    private Vector3 velocity;
    private Vector2 moveInput;
    private bool isRunning;
    private bool isGrounded;
    private bool isGliding;
    private float lastGroundedTime;
    private float lastJumpTime;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;

        if (characterModel == null)
        {
            Debug.LogError("Character model not assigned. Please assign the character model in the inspector.");
        }
    }

    private void Update()
    {
        GroundCheck();
        HandleMovement();
        HandleJump();
        HandleGravity();
        ApplyMovement();
        RotateCharacterModel();
    }

    private void GroundCheck()
    {
        isGrounded = IsGrounded();
        if (isGrounded)
        {
            lastGroundedTime = Time.time;
        }
    }

    private bool IsGrounded()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - (controller.height / 2f) + controller.center.y + groundCheckRadius, transform.position.z);
        return Physics.CheckSphere(spherePosition, groundCheckRadius, groundLayer, QueryTriggerInteraction.Ignore);
    }

    private void HandleMovement()
    {
        Vector3 desiredMove = new Vector3(moveInput.x, 0, moveInput.y).normalized;
        if (desiredMove != Vector3.zero)
        {
            desiredMove = cameraTransform.TransformDirection(desiredMove);
            desiredMove.y = 0f;
            desiredMove.Normalize();

            float targetSpeed = isRunning ? runSpeed : walkSpeed;
            Vector3 targetVelocity = desiredMove * targetSpeed;
            float accelerationRate = isGrounded ? acceleration : airAcceleration;
            velocity = Vector3.MoveTowards(velocity, targetVelocity, accelerationRate * Time.deltaTime);
        }
        else
        {
            float decelerationRate = isGrounded ? deceleration : airDeceleration;
            velocity = Vector3.MoveTowards(velocity, Vector3.zero, decelerationRate * Time.deltaTime);
        }
    }

    private void HandleJump()
    {
        // Check if the jump was initiated within the buffer time
        if (Time.time - lastJumpTime <= jumpBufferTime)
        {
            // Check if the character is grounded
            if (isGrounded)
            {
                Debug.Log("Jump executed while grounded");
                velocity.y = jumpForce;
                lastJumpTime = 0f; // Reset to prevent multiple jumps within the same buffer period
            }
            // Check if the character is within coyote time
            else if (Time.time - lastGroundedTime <= coyoteTime)
            {
                Debug.Log("Jump executed during coyote time");
                velocity.y = jumpForce;
                lastJumpTime = 0f; // Reset to prevent multiple jumps within the same buffer period
            }
        }
    }

    private void HandleGravity()
    {
        if (!isGrounded)
        {
            float gravityToApply = isGliding ? glideGravity : gravity;
            velocity.y += gravityToApply * Time.deltaTime;
            velocity.y = Mathf.Max(velocity.y, maxFallSpeed);
        }
        else
        {
            // Ensure the character sticks to the ground when grounded
            if (velocity.y < 0)
            {
                velocity.y = -2f;
            }
        }
    }

    private void ApplyMovement()
    {
        controller.Move(velocity * Time.deltaTime);

        if (isGrounded && velocity.magnitude > 0.01f)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, controller.height / 2f + 0.1f, groundLayer))
            {
                Vector3 groundNormal = hit.normal;
                float slopeAngle = Vector3.Angle(Vector3.up, groundNormal);
                if (slopeAngle <= slopeLimit)
                {
                    velocity = Vector3.ProjectOnPlane(velocity, groundNormal);
                }
            }
        }
    }

    private void RotateCharacterModel()
    {
        if (moveInput != Vector2.zero)
        {
            Vector3 direction = new Vector3(moveInput.x, 0, moveInput.y);
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            characterModel.rotation = Quaternion.Slerp(characterModel.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }

    private void OnDrawGizmos()
    {
        if (showDebug)
        {
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - (controller.height / 2f) + controller.center.y + groundCheckRadius, transform.position.z);
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(spherePosition, groundCheckRadius);

            Debug.DrawRay(transform.position, velocity, Color.blue);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        isRunning = context.ReadValueAsButton();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Jump button pressed");
            lastJumpTime = Time.time;
        }
    }

    public void OnGlide(InputAction.CallbackContext context)
    {
        isGliding = context.ReadValueAsButton();
    }
}
