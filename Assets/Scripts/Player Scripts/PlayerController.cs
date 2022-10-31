using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Controls")]
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float turnSmoothing;
    [SerializeField] float groundDrag;
    [SerializeField] float jumpCooldown;
    [SerializeField] float airMultiplier;
    private Vector3 movement;

    [Header("Jumping/Ground Checks")]
    [SerializeField] float playerHeight;
    [SerializeField] LayerMask whatIsGround;
    private bool grounded;
    private bool canJump;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;

    [Header("Projectile")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform orientation;
    [SerializeField] Transform spawnBulletPoint;

    private float horizontalInput;
    private float verticalInput;
    private float turnSmoothVelocity;

    private Rigidbody playerRb;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.freezeRotation = true;
        canJump = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GroundCheck();

        PlayerMovement();

        FireProjectile();
    }

    private void PlayerMovement()
    {
        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");
        movement = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (Input.GetKey(jumpKey) && canJump && grounded)
        {
            canJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        if (grounded)
        {
            playerRb.AddForce(movement.normalized * speed * Time.deltaTime, ForceMode.Force);
        }
        else if (!grounded)
        {
            playerRb.AddForce(movement.normalized * speed * airMultiplier * Time.deltaTime, ForceMode.Force);
        }
        
        MovementSpeedLimit();

    }

    private void GroundCheck()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        if (grounded)
        {
            playerRb.drag = groundDrag;
        }
        else
        {
            playerRb.drag = 0;
        }
    }

    private void MovementSpeedLimit()
    {
        Vector3 flatVelocity = new Vector3(playerRb.velocity.x, 0f, playerRb.velocity.z);

        if(flatVelocity.magnitude > speed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * speed * Time.deltaTime;
            playerRb.velocity = new Vector3(limitedVelocity.x, playerRb.velocity.y, limitedVelocity.z);
        }
    }

    private void Jump()
    {
        playerRb.velocity = new Vector3(playerRb.velocity.x, 0f, playerRb.velocity.z);

        playerRb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        canJump = true;
    }

    private void FireProjectile()
    {
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("Left mouse button pressed");
            GameObject currentBullet = Instantiate(bullet, spawnBulletPoint.position, Quaternion.identity);
        }
    }
}
