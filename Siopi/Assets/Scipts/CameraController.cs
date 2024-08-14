using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("Camera References")]
    [SerializeField] private CinemachineFreeLook freeLookCamera;

    [Header("Camera Settings")]
    [SerializeField] private float sensitivity = 1f;
    [Range(0.1f, 5f)]
    [SerializeField] private float orbitSpeed = 1f;

    [Header("Collision Settings")]
    [SerializeField] private float minDistance = 0.5f;
    [SerializeField] private float maxDistance = 10f;
    [SerializeField] private float collisionBuffer = 0.1f;
    [SerializeField] private LayerMask collisionLayers;

    private Vector2 lookInput;
    private Transform cameraTarget;

    private void Start()
    {
        if (freeLookCamera == null)
            freeLookCamera = FindObjectOfType<CinemachineFreeLook>();

        cameraTarget = freeLookCamera.Follow;

        // Ensure the cursor is locked and hidden
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Add CinemachineCollider to handle collisions
        AddCinemachineCollider();
    }

    private void Update()
    {
        HandleRotation();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    private void HandleRotation()
    {
        float mouseX = lookInput.x * sensitivity;
        float mouseY = lookInput.y * sensitivity;

        freeLookCamera.m_XAxis.Value += mouseX * orbitSpeed * Time.deltaTime;
        freeLookCamera.m_YAxis.Value = Mathf.Clamp01(freeLookCamera.m_YAxis.Value + mouseY * orbitSpeed * Time.deltaTime);
    }

    private void AddCinemachineCollider()
    {
        CinemachineCollider collider = freeLookCamera.GetComponent<CinemachineCollider>();
        if (collider == null)
        {
            collider = freeLookCamera.gameObject.AddComponent<CinemachineCollider>();
        }

        collider.m_CollideAgainst = collisionLayers;
        collider.m_MinimumDistanceFromTarget = minDistance;
        collider.m_AvoidObstacles = true;
        collider.m_DistanceLimit = maxDistance;
        collider.m_CameraRadius = collisionBuffer;
        collider.m_Strategy = CinemachineCollider.ResolutionStrategy.PullCameraForward;
    }
}