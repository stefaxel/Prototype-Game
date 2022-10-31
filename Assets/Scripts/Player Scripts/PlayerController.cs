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

    
    private float horizontalInput;
    private float verticalInput;
    private float turnSmoothVelocity;

    private Rigidbody playerRb;

    [Header("Camera")]
    //[SerializeField] Transform cam;
    public Vector2 mousePos;
    
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

        //FireProjectile();
    }

    private void PlayerMovement()
    {
        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if(Input.GetKey(jumpKey) && canJump && grounded)
        {
            canJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        //mousePos.x += Input.GetAxis("Mouse X");
        //mousePos.y += Input.GetAxis("Mouse Y");
        //transform.localRotation = Quaternion.Euler(-mousePos.y, mousePos.x, 0);

        movement = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (grounded)
        {
            playerRb.AddForce(movement.normalized * speed * Time.deltaTime, ForceMode.Force);
        }
        else if (!grounded)
        {
            playerRb.AddForce(movement.normalized * speed * airMultiplier * Time.deltaTime, ForceMode.Force);
        }
        

        //movement = new Vector3(horizontalInput, 0.0f, verticalInput).normalized;
        MovementSpeedLimit();

        //if(movement.magnitude >= 0.1f)
        //{
        //    float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        //    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothing);
        //    transform.rotation = Quaternion.Euler(0f, angle, 0f);

        //    Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        //    playerRb.AddForce(moveDirection.normalized * speed * Time.deltaTime);
        //}

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

    //private void FireProjectile()
    //{
        
    //    if (Input.GetKeyDown(KeyCode.Mouse0))
    //    {
    //        Debug.Log("Left mouse button pressed");
    //        spawnBulletPoint = Instantiate(bullet, spawnBulletPoint.transform.position, Quaternion.identity);
    //    }
    //}
}
