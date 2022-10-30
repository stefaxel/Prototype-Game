using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Controls")]
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float turnSmoothing;

    [Header("Projectile")]
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject spawnBulletPoint;

    private bool isJumping;
    private float horizontalInput;
    private float verticalInput;
    private float turnSmoothVelocity;

    private Rigidbody playerRb;

    [Header("Camera")]
    [SerializeField] Transform cam;
    public Vector2 mousePos;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerMovement();

        FireProjectile();
    }

    private void PlayerMovement()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        mousePos.x += Input.GetAxis("Mouse X");
        mousePos.y += Input.GetAxis("Mouse Y");
        transform.localRotation = Quaternion.Euler(-mousePos.y, mousePos.x, 0);

        Vector3 movement = new Vector3(horizontalInput, 0.0f, verticalInput).normalized;

        if(movement.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothing);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            playerRb.AddForce(moveDirection.normalized * speed * Time.deltaTime);
        }

    }

    private void FireProjectile()
    {
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("Left mouse button pressed");
            spawnBulletPoint = Instantiate(bullet, spawnBulletPoint.transform.position, Quaternion.identity);
        }
    }
}
