using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepManager : MonoBehaviour
{
    public AudioClip[] walkingFootstepSounds;
    public AudioClip[] runningFootstepSounds;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayFootsteps(bool isRunning)
    {
        // Determine which set of footstep sounds to use
        AudioClip[] selectedSounds = isRunning ? runningFootstepSounds : walkingFootstepSounds;
        // Select a random clip to play
        AudioClip clipToPlay = selectedSounds[Random.Range(0, selectedSounds.Length)];
        if (!audioSource.isPlaying)
        {
            audioSource.clip = clipToPlay;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void StopFootsteps()
    {
        audioSource.Stop();
        audioSource.loop = false;
    }
}
