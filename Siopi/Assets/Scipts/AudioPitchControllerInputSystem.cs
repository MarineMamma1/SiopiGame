using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AudioPitchControllerInputSystem : MonoBehaviour
{
    public AudioSource audioSource; // Assign in the inspector
    public float maxPitch = 2.0f; // Maximum pitch
    public float minPitch = 0.5f; // Minimum pitch
    public float pitchSmoothing = 0.05f; // Smoothing speed

    private bool isHumming;
    private float humInput; // Hum input as float
    public float targetPitch; // Target pitch based on input
    public float pitchValue;
    private CircleCollider2D circleCollider2D;
    private GameObject childObject;
    private SpriteRenderer childSpriteRenderer;

    private float initialColliderRadius; // Store initial radius

    void Start()
    {
        isHumming = false;
        targetPitch = audioSource.pitch; // Initialize with the AudioSource's initial pitch
        circleCollider2D = GetComponent<CircleCollider2D>();
        circleCollider2D.enabled = false;
        childObject = transform.GetChild(0).gameObject; // Assuming the child object is the first child
        childSpriteRenderer = childObject.GetComponent<SpriteRenderer>();
        childSpriteRenderer.enabled = false;

        initialColliderRadius = circleCollider2D.radius; // Store initial radius
    }

    private void StartAudio()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void OnHum(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            StartAudio();
            isHumming = true;
            circleCollider2D.enabled = true;
            childSpriteRenderer.enabled = true; // Turn on the child object's sprite renderer
            StartCoroutine(ChangeColliderRadius());
        }
        else
        {
            StopAudio();
            isHumming = false;
            circleCollider2D.enabled = false;
            childSpriteRenderer.enabled = false; // Turn off the child object's sprite renderer
            StopCoroutine(ChangeColliderRadius()); // Stop changing the radius
            circleCollider2D.radius = initialColliderRadius; // Reset radius to initial value
            targetPitch = pitchValue; // Set target pitch to current pitch
        }
    }

    public void OnHumSelection(InputAction.CallbackContext context)
    {
        if (context.performed && isHumming)
        {
            humInput = context.ReadValue<float>();
            AdjustPitch(humInput);
        }
    }

    private void StopAudio()
    {
        audioSource.Stop();
    }

    private void AdjustPitch(float input)
    {
        targetPitch = Mathf.Lerp(minPitch, maxPitch, Mathf.Clamp01((input + 1) / 2)); // Assuming input range is -1 to 1
    }

    void Update()
    {
        if (isHumming)
        {
            // Smoothly transition to the target pitch only if the difference is significant
            if (Mathf.Abs(audioSource.pitch - targetPitch) > 0.01f)
            {
                audioSource.pitch = Mathf.Lerp(audioSource.pitch, targetPitch, pitchSmoothing * Time.deltaTime);
                pitchValue = audioSource.pitch;
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            Debug.Log(other);
            PlatformMove platformMove = other.GetComponent<PlatformMove>();
            if (platformMove != null)
            {
                Debug.Log("Platform detected with PlatformMove component"); // Debug log
                if (pitchValue >= 1.1f)
                {
                    platformMove.PerformAction();
                }
            }
            else
            {
                Debug.LogError("Error: PlatformMove component not found on the platform object"); // Error log
            }
        }
        else if (other.gameObject.CompareTag("Door"))
        {
            if (pitchValue <= 0.8f && pitchValue != 0)
            {
                DoorLogic doorLogic = other.GetComponent<DoorLogic>();
                if (doorLogic != null) // Added null check
                {
                    doorLogic.ToggleDoor();
                }
                else
                {
                    Debug.LogError("Error: DoorLogic component not found on the door object"); // Error log
                }
            }
        }
    }

    private IEnumerator ChangeColliderRadius()
    {
        while (true)
        {
            // Increase radius
            for (float r = circleCollider2D.radius; r <= initialColliderRadius + 0.3; r += 0.1f)
            {
                circleCollider2D.radius = r;
                yield return new WaitForSeconds(0.1f);
            }

            // Decrease radius
            for (float r = circleCollider2D.radius; r >= initialColliderRadius; r -= 0.1f)
            {
                circleCollider2D.radius = r;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
