using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLogic : MonoBehaviour
{
    public float openDuration = 2.0f; // Time door stays open before closing
    private Collider2D doorCollider;
   // private material material
    private bool isOpen = false;
    private Material material;
    private Renderer renderer;
    private Color color;

    void Start()
    {
        doorCollider = GetComponent<Collider2D>();
         renderer = GetComponent<Renderer>();
        if(!renderer)
        {
            Debug.Log("doorMbroken");
        }
        else
        {
             material = renderer.material;
             color = material.color;
            
        }
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
        Color color = material.color;
        color.a = 0.5f;
        material.color = color;
        // Wait for the duration the door should stay open
        yield return new WaitForSeconds(openDuration);

        // Close the door
        CloseDoor();
    }

    void CloseDoor()
    {
        // Move door to closed position
        // Reset the material opacity
        color.a = 1f;
        material.color = color;
        // Enable the collider
        doorCollider.enabled = true;
        isOpen = false;
    }
}
