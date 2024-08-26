using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls PlayerControls;
    AnimatorManager animatorManager;

     public Vector2 movemmentInput;
     public Vector2 cameraInput;

     public float cameraInputX;
     public float cameraInputY;

     private float moveAmount;
     public float verticalInput;
     public float horizontalInput;

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();   
    }
    private void OnEnable() 
    {
        if (PlayerControls == null)
        {
            PlayerControls = new PlayerControls();

            PlayerControls.PlayerMovement.Movement.performed += i => movemmentInput = i.ReadValue<Vector2>();
            PlayerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
        }

        PlayerControls.Enable();
    }

    private void OnDisable() 
    {
        PlayerControls.Disable();   
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
        //HandleJumpingInput
        //HandleActionInput
    }


    private void HandleMovementInput()
    {
        verticalInput = movemmentInput.y;
        horizontalInput = movemmentInput.x;

        cameraInputY = cameraInput.y;
        cameraInputX = cameraInput.x;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimatorValues(0, moveAmount);
    }
}
