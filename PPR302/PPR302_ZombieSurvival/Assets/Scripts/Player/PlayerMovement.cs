using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerStats playerStats;

    [Header("Movement Values")]
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;



    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump = true;

    public float staminaUsagePerJump = 15f;
    public float staminaUsagePerSecondSprinting = 1f;

    [Header("Movement Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Player References")]
    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    [Header("Anomaly References")]
    public GameObject whirlpool;
    public GameObject normalGameobjectParent;

    Vector3 moveDirection;

    Rigidbody rb;

    public MovementState state;
    public enum MovementState
    {
        walking,
        sprinting, 
        air
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        // stops the player from falling over
        rb.freezeRotation = true;
    }

    private void Update()
    {
        // ground Check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput();
        SpeedLimiter();
        StateHandler();

        //handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput() 
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //when to jump
        if(Input.GetKey(jumpKey) && 
            readyToJump && 
            grounded && 
            playerStats.currentStamina - staminaUsagePerJump >= 0)
        {
            readyToJump = false;

            Jump();

            //will wait the time of cooldown to allow for another jump
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void StateHandler()
    {

        // Mode - Sprinting
        if (grounded && Input.GetKey(sprintKey) && playerStats.currentStamina >= 0)
        {
            playerStats.UseStamina_Jumping(staminaUsagePerSecondSprinting);
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }

        // Mode - Walking
        else if (grounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }

        // Mode - Air
        else
        {
            state = MovementState.air;

        }
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //while on the ground
        if(grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if(!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedLimiter()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
    private void Jump()
    {
        playerStats.UseStamina_Jumping(staminaUsagePerJump);

        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Whirlpool"))
        {
            whirlpool = collision.gameObject;
            //set the parent of the player to the crusher Anomaly
            transform.parent = whirlpool.transform.parent;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Whirlpool"))
        {
            playerStats.PlayerTakingDamage(25f);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        whirlpool = null;
        //set the parent of the player to the crusher Anomaly
        transform.parent = normalGameobjectParent.transform.parent;
    }
}
