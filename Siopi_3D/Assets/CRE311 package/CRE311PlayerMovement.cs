using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRE311PlayerMovement : MonoBehaviour
{

    [Header("Movement")]
    public float moveSpeed;
    public Animator anim; 

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public bool readyToJump;
    
   
    

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space; 

    [Header(" Ground Check")]

    public float playerHeight;
    public LayerMask whatISGround;
    public bool grounded; 

    public Transform orientation;

     public float horizontalInput;
     public float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    //[Header("StepSysytem")]
   


    private void Awake()
    {
       
        
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true; 
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatISGround);
      

        MyInput();


        SpeedControl();

        anim.SetBool("IsRunning", false);

        if (horizontalInput > 0f || verticalInput > 0f || horizontalInput < 0f || verticalInput < 0f)

        {
            anim.SetBool("IsRunning", true);
        }

        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            
            rb.drag = 0; 
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
        
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

       

        if(Input.GetButtonDown("Jump") && readyToJump && grounded)
        {
            Debug.Log("hey Im Jumpin here");
            anim.ResetTrigger("jump");
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        if (grounded)
        {
           
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }

        else if(!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);         }
       
    }


    private void Jump()
    {
        anim.SetTrigger("jump");
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
       


    }
    
    private void ResetJump()
    {
        readyToJump = true;
        
    }

    

}
