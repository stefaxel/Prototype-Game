using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform orientation;
    [SerializeField] Transform player;
    [SerializeField] Transform playerObject;
    [SerializeField] Rigidbody playerRb;

    [Header("Other Params")]
    [SerializeField] float rotationSpeed;
    [SerializeField] Transform combatLookAt;
    [SerializeField] GameObject thirdPersonCamera;
    [SerializeField] GameObject combatCamera;

    [SerializeField] CamraStyle currentStyle;
    private enum CamraStyle
    {
        Basic,
        Combat
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SwitchCameraStyle(CamraStyle.Basic);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SwitchCameraStyle(CamraStyle.Combat);

        Vector3 viewDirection = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDirection.normalized;

        if(currentStyle == CamraStyle.Basic)
        {
            float horizonatlInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 inputDirection = orientation.forward * verticalInput + orientation.right * horizonatlInput;

            if (inputDirection != Vector3.zero)
            {
                playerObject.forward = Vector3.Slerp(playerObject.forward, inputDirection.normalized, Time.deltaTime * rotationSpeed);
            }
        }
        else if (currentStyle == CamraStyle.Combat)
        {
            Vector3 combatDirection = combatLookAt.position - new Vector3(transform.position.x, combatLookAt.position.y, transform.position.z);
            orientation.forward = combatDirection.normalized;

            playerObject.forward = combatDirection.normalized;
        }
    }

    private void SwitchCameraStyle(CamraStyle newStyle)
    {
        combatCamera.SetActive(false);
        thirdPersonCamera.SetActive(false);

        if (newStyle == CamraStyle.Basic) thirdPersonCamera.SetActive(true);
        if (newStyle == CamraStyle.Combat) combatCamera.SetActive(true);

        currentStyle = newStyle;
    }
}
