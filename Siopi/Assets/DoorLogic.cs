using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLogic : MonoBehaviour
{
    public float openDuration = 2.0f; // Time door stays open before closing
    private Collider2D doorCollider;
    private SpriteRenderer doorRenderer;
    private bool isOpen = false;

    void Start()
    {
        doorCollider = GetComponent<Collider2D>();
       //FIX doorRenderer = GetComponent<SpriteRenderer>();
    }

    public void ToggleDoor()
    {
        if (!isOpen)
        {
            StartCoroutine(OpenDoor());
        }
    }

    IEnumerator OpenDoor()
    {
        Debug.Log("doorworkingsupposedly");
        isOpen = true;
        
        // Halve the material opacity
     //   doorRenderer.color = new Color(doorRenderer.color.r, doorRenderer.color.g, doorRenderer.color.b, 0.5f);
        // Disable the collider
        doorCollider.enabled = false;

        // Wait for the duration the door should stay open
        yield return new WaitForSeconds(openDuration);

        // Close the door
        CloseDoor();
    }

    void CloseDoor()
    {
        // Move door to closed position
        // Reset the material opacity
        doorRenderer.color = new Color(doorRenderer.color.r, doorRenderer.color.g, doorRenderer.color.b, 1.0f);
        // Enable the collider
        doorCollider.enabled = true;
        isOpen = false;
    }
}
