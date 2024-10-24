using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputManager inputManager;
        public static PlayerManager Instance
    {
        get
        {
            return s_Instance;
        }
    }
    Animator animator;
    CameraManager cameraManager;
    PlayerLocomotion playerLocomotion;
    private static PlayerManager s_Instance;

    public bool isInteracting;
    public int  KeyAmount;
    public int RecordAmount;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        inputManager = GetComponent<InputManager>();
        cameraManager = FindObjectOfType<CameraManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
    }

    // I commented These out as They were not being used anywhere
    private void Update() 
    {
        //inputManager.HandleAllInputs();
    }

    private void FixedUpdate() 
    {
        //playerLocomotion.HandleAllMovement();
    }
    private void LateUpdate() 
    {
        //cameraManager.HandleAllCameraMovement();    

        //isInteracting = animator.GetBool("isInteracting");
        //playerLocomotion.isJumping = animator.GetBool("isJumping");
        //animator.SetBool("isGrounded", playerLocomotion.isGrounded);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Key")
        {
            KeyAmount += 1;
            Destroy(other.gameObject);
        }    
    }

}
