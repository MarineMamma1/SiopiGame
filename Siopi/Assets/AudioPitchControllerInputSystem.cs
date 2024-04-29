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

    void Start()
    {
   
        isHumming = false;
        targetPitch = audioSource.pitch; // Initialize with the AudioSource's initial pitch
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
        }
        else
        {
            StopAudio();
            isHumming = false;
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
}
