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
    private Vector2 humDir;
    public float targetPitch; // Target pitch based on input
    public float pitchValue;
    private CircleCollider2D circleCollider2D;

    void Start()
    {
           
        isHumming = false;
        targetPitch = audioSource.pitch; // Initialize with the AudioSource's initial pitch
        circleCollider2D = GetComponent<CircleCollider2D>();

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
        }
        else
        {
            StopAudio();
            isHumming = false;
            circleCollider2D.enabled = false;

        }
    }

    public void OnHumSelection(InputAction.CallbackContext context)
    {
        if (context.performed && isHumming)
        {
            humDir = context.ReadValue<Vector2>();
            if (humDir != Vector2.zero ) // Validate the input is not zero
            {
                AdjustPitch(humDir);
            }
        }
    }

    private void StopAudio()
    {
        audioSource.Stop();
    }

    private void AdjustPitch(Vector2 rotateInput)
    {
        float angleRadians = Mathf.Atan2(rotateInput.y, rotateInput.x);
        float angleDegrees = angleRadians * Mathf.Rad2Deg;
        if (angleDegrees < 0)
        {
            angleDegrees += 360;
        }

        targetPitch = Mathf.Lerp(minPitch, maxPitch, angleDegrees / 360.0f);
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            PlatformMove platformMove = other.GetComponent<PlatformMove>();
            if (platformMove != null && pitchValue >= 1.1f ) // N#
            {
                platformMove.PerformAction();
            }
            else
            {
                Debug.LogError("error component not found on the object with tag 'platform'");
            }
        }
        else
        {
            if(other.gameObject.CompareTag("Door"))
            {
                DoorLogic doorLogic = other.GetComponent<DoorLogic>();
                if (doorLogic != null && pitchValue <= 0.8f) // N#
                {
                    doorLogic.ToggleDoor();
                }
                else
                {
                    Debug.LogError("error component not found on the object with tag 'door'");
                }
            }
        }
    }
    
    
   
}
