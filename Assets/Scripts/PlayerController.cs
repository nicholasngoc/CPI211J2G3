using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that controls the Players movement and look controls.
/// Jumping is included
/// </summary>
public class PlayerController : MonoBehaviour
{
    public Rigidbody Rb
    {
        get
        {
            return GetComponent<Rigidbody>();
        }
    }

    public GameObject playerCam;

    [Header("Movement and Looking")]
    public float lookSensitivity = 1f;
    public float maxVerticalAngle = 60f;
    public float movementSpeed = 180f;

    [Header("Jumping")]
    public int totalJumps;   //How many times the player can jump before resetting
    private int _jumpCount;
    private int _addJump;
    public float jumpForce;

    private void Awake()
    {
        //Locks mouse cursor on screen so player does not see it
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        _jumpCount = 0;
        _addJump = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _jumpCount < totalJumps)
        {
            _addJump++;
            _jumpCount++;
        }

        MouseControl();
        MovementControl();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            _jumpCount = 0;
    }

    private void FixedUpdate()
    {
        JumpControl();
    }

    /// <summary>
    /// Method that handles mouse input and rotating the
    /// players view based on that output
    /// </summary>
    private void MouseControl()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        //This moves the PLAYER gameobject's rotation
        if (mouseX != 0)
        {
            Vector3 newRotation = transform.eulerAngles;
            newRotation.y += mouseX * lookSensitivity * Time.deltaTime;
            transform.eulerAngles = newRotation;
        }
        //This moves the CAMERA's rotation. This was done to avoid collision issues if the player obj were to rotate
        if (mouseY != 0)
        {
            Vector3 newRotation = playerCam.transform.localEulerAngles;
            newRotation.x += mouseY * lookSensitivity * -1 * Time.deltaTime;

            /**
             * This prevents the player from looking beyond our set _maxVerticalAngle;
             * 
             * Note: .localEulerAngles does not return a negative number as the inspector shows.
             * For example, -5 in the inspector is 355. This is the reason why I have to do two differnt
             * checks
             */
            if (newRotation.x < maxVerticalAngle || newRotation.x > 360 - maxVerticalAngle)
                playerCam.transform.localEulerAngles = newRotation;
        }
    }

    /// <summary>
    /// Method that controls input for player movement
    /// </summary>
    private void MovementControl()
    {
        float hInput = Input.GetAxisRaw("Horizontal");
        float vInput = Input.GetAxisRaw("Vertical");

        if (hInput != 0)
        {
            transform.position += transform.right * Mathf.Sign(hInput) * movementSpeed * Time.deltaTime;
        }

        if (vInput != 0)
        {
            transform.position += transform.forward * Mathf.Sign(vInput) * movementSpeed * Time.deltaTime;
        }
    }

    private void JumpControl()
    {
        if(_addJump > 0)
        {
            /*
             * This forces all jumps to be the same no matter the current velocity. If you've played
             * Destingy it gives a Hunter jump effect. This is a design choice so remove if desired
             */
            Rb.velocity = Vector3.zero;

            Rb.AddForce(transform.up * jumpForce);
            _addJump--;
        }
    }
}
