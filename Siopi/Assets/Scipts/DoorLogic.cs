using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLogic : MonoBehaviour
{
    public float openDuration = 2.0f; // Time door stays open before closing
    private Collider2D doorCollider;
//    private Renderer renderer;
//    private Material material;
   // private Color color;
    private bool isOpen = false;

    void Start()
    {
        doorCollider = GetComponent<Collider2D>();
      //  renderer = GetComponent<Renderer>();
       // if (!renderer)
      //  {
      //      Debug.Log("Door material is missing.");
      //  }
     //   else
     //   {
    //        material = renderer.material;
      //      color = material.color;
     //   }
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
        Debug.Log("Door opening.");
        isOpen = true;

        // Disable the collider and children
        doorCollider.enabled = false;
        SetChildrenActive(false);

        // Halve the material opacity
       // Color color = material.color;
     //   color.a = 0.5f;
     //   material.color = color;

        // Wait for the duration the door should stay open
        yield return new WaitForSeconds(openDuration);

        // Close the door
        CloseDoor();
    }

    void CloseDoor()
    {
        // Reset the material opacity
       // color.a = 1f;
       // material.color = color;

        // Enable the collider and children
        doorCollider.enabled = true;
        SetChildrenActive(true);

        isOpen = false;
    }

    void SetChildrenActive(bool isActive)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(isActive);
        }
    }
}
