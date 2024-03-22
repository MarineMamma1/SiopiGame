using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class rotate : MonoBehaviour
{
    public float rotationSpeed = 1.0f;

    private Vector2 lookMovement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.right, -lookMovement.y * rotationSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up, lookMovement.x * rotationSpeed * Time.deltaTime);

    }
    
    public void OnLook(InputAction.CallbackContext context)
    {
        Debug.Log("bruh");
        lookMovement = context.ReadValue<Vector2>();
    }
}
