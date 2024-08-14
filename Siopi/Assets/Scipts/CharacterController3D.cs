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
    public float rotationSpeed = 720f;

    [Header("Jumping")]
    public float jumpForce = 10f;
    public float gravity = -9.81f;
    public float glideGravity = -2f;
    public float maxFallSpeed = -20f;
    public float coyoteTime = 0.2f;
    public float jumpBufferTime = 0.2f;

    [Header("Gliding")]
    public float minVerticalSpeedForGlide = -2f; // Minimum falling speed to start gliding
    public float glideTransitionSpeed = 2f; // Speed at which gravity is reduced to glide gravity
    public float glideSlope = -0.5f; // The rate at which the character descends while gliding

    [Header("Air Control")]
    public float airControl = 0.3f;

    [Header("Ground Detection")]
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public float slopeLimit = 45f;

    [Header("Debug")]
    public bool showDebug = false;
    public float CurrentSpeed { get; private set; }

    [Header("Character Model")]
    public Transform characterModel;

    [Header("Camera")]
    public Transform cameraTransform;

    private CharacterController controller;

    private Vector3 horizontalVelocity;
    private float verticalVelocity;
    private Vector2 moveInput;
    private bool isRunning;
    private bool isGrounded;
    private bool isGliding;
    private float lastGroundedTime;
    private float lastJumpTime;

    private void Start()
    {
        controller = GetComponent<CharacterController>();

        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }

        if (characterModel == null)
        {
            characterModel = transform;
            Debug.LogWarning("Character model not assigned. Using this GameObject's transform.");
        }
    }

    private void UpdateCurrentSpeed()
    {
        CurrentSpeed = new Vector3(horizontalVelocity.x, verticalVelocity, horizontalVelocity.z).magnitude;
    }

    private void Update()
    {
        GroundCheck();
        HandleMovement();
        HandleJump();
        HandleGravity();
        ApplyMovement();
        RotateCharacter();
        UpdateCurrentSpeed();
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
            if (!isGrounded)
            {
                targetVelocity = Vector3.Lerp(horizontalVelocity, targetVelocity, airControl * Time.deltaTime);
            }

            horizontalVelocity = Vector3.MoveTowards(horizontalVelocity, targetVelocity, accelerationRate * Time.deltaTime);
        }
        else
        {
            float decelerationRate = isGrounded ? deceleration : airDeceleration;
            horizontalVelocity = Vector3.MoveTowards(horizontalVelocity, Vector3.zero, decelerationRate * Time.deltaTime);
        }
    }

    private void HandleJump()
    {
        if (Time.time - lastJumpTime <= jumpBufferTime)
        {
            if (isGrounded || Time.time - lastGroundedTime <= coyoteTime)
            {
                verticalVelocity = jumpForce;
                lastJumpTime = 0f;
                Debug.Log("Jump executed");
            }
        }
    }

    private void HandleGravity()
    {
        if (!isGrounded)
        {
            float gravityToApply = isGliding ? Mathf.Lerp(gravity, glideGravity, Time.deltaTime * glideTransitionSpeed) : gravity;

            // Prevent gliding unless falling
            if (verticalVelocity <= minVerticalSpeedForGlide && isGliding)
            {
                gravityToApply = glideGravity;
                verticalVelocity += glideSlope * Time.deltaTime; // Apply glide slope for realistic descent
            }

            verticalVelocity += gravityToApply * Time.deltaTime;
            verticalVelocity = Mathf.Max(verticalVelocity, maxFallSpeed);
        }
        else if (verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }
    }

    private void ApplyMovement()
    {
        Vector3 movement = horizontalVelocity + new Vector3(0, verticalVelocity, 0);
        controller.Move(movement * Time.deltaTime);

        if (isGrounded && movement.magnitude > 0.01f)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, controller.height / 2f + 0.1f, groundLayer))
            {
                Vector3 groundNormal = hit.normal;
                float slopeAngle = Vector3.Angle(Vector3.up, groundNormal);
                if (slopeAngle <= slopeLimit)
                {
                    horizontalVelocity = Vector3.ProjectOnPlane(horizontalVelocity, groundNormal);
                }
            }
        }
    }

    private void RotateCharacter()
    {
        if (horizontalVelocity.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(horizontalVelocity, Vector3.up);
            characterModel.rotation = Quaternion.RotateTowards(characterModel.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnDrawGizmos()
    {
        if (showDebug)
        {
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - (controller.height / 2f) + controller.center.y + groundCheckRadius, transform.position.z);
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(spherePosition, groundCheckRadius);
            Vector3 totalVelocity = horizontalVelocity + new Vector3(0, verticalVelocity, 0);
            Debug.DrawRay(transform.position, totalVelocity, Color.blue);
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
