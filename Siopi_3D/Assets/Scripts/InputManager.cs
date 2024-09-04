using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls PlayerControls;
    PlayerLocomotion playerLocomotion;
    AnimatorManager animatorManager;

     public Vector2 movementInput;
     public Vector2 cameraInput;

     public float cameraInputX;
     public float cameraInputY;

     public float moveAmount;
     public float verticalInput;
     public float horizontalInput;

     public bool b_Input;
     public bool jump_Input;

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();   
        playerLocomotion = GetComponent<PlayerLocomotion>();
    }
    private void OnEnable() 
    {
        if (PlayerControls == null)
        {
            PlayerControls = new PlayerControls();

            PlayerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            PlayerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();

            PlayerControls.PlayerActions.B.performed += i => b_Input = true;
            PlayerControls.PlayerActions.B.canceled += i => b_Input = false;
            PlayerControls.PlayerActions.Jump.performed += i => jump_Input = true;
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
        HandleJumpingInput();
        //HandleActionInput
    }


    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        cameraInputY = cameraInput.y;
        cameraInputX = cameraInput.x;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimatorValues(0, moveAmount);
    }

    //private void HandleSprintingInput()

    private void HandleJumpingInput()
    {
        if (jump_Input)
        {
            jump_Input = false;
            playerLocomotion.HandleJumping();
        }
    }
}
