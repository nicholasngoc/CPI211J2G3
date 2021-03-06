﻿using System.Collections;
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
    private float _movementSpeed;
    public float baseMovementSpeed;
    public float sprintSpeed;
    public bool isSprinting;

    [Header("Head Bobbing")]
    public float bobSpeed;
    public float sprintBobSpeed;
    public float bobHeight; //This modifies how far the player dips his head during the bob
    private Vector3 _originalHeight;
    private IEnumerator _bobRoutine;

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

        _originalHeight = playerCam.transform.localPosition;
    }

    private void Update()
    {
        //Queues a jump to occur for FixedUpdate
        if (Input.GetKeyDown(KeyCode.Space) && _jumpCount < totalJumps)
        {
            _addJump++;
            _jumpCount++;
        }

        MouseControl();
        MovementControl();
    }

    /// <summary>
    /// Resets the jump counter when touching the ground
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            _jumpCount = 0;
    }

    private void FixedUpdate()
    {
        //I thought I heard Physics was better in fixed update so I put it here instead of normal update
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

        if(Input.GetKey(KeyCode.LeftShift))
        {
            _movementSpeed = baseMovementSpeed + sprintSpeed;
            isSprinting = true;
        }
        else
        {
            _movementSpeed = baseMovementSpeed;
            isSprinting = false;
        }

        if (hInput != 0)
        {
            transform.position += transform.right * Mathf.Sign(hInput) * _movementSpeed * Time.deltaTime;
        }

        if (vInput != 0)
        {
            transform.position += transform.forward * Mathf.Sign(vInput) * _movementSpeed * Time.deltaTime;

            if (_bobRoutine == null)
            {
                _bobRoutine = HeadBobControl();
                StartCoroutine(_bobRoutine);
            }
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

    /// <summary>
    /// Coroutine that handles the player's head bobbing up and down
    /// </summary>
    /// <returns></returns>
    private IEnumerator HeadBobControl()
    {
        //This lowers the players head
        while(playerCam.transform.localPosition.y > -bobHeight)
        {
            float useSpeed = bobSpeed;

            if(isSprinting)
            {
                useSpeed = sprintBobSpeed;
            }

            playerCam.transform.localPosition -= new Vector3(0, useSpeed * Time.deltaTime, 0);

            yield return new WaitForEndOfFrame();
        }

        //This raises the head back up
        while(playerCam.transform.localPosition.y < _originalHeight.y)
        {
            float useSpeed = bobSpeed;

            if (isSprinting)
            {
                useSpeed = sprintBobSpeed;
            }

            playerCam.transform.localPosition += new Vector3(0, useSpeed * Time.deltaTime, 0);

            yield return new WaitForEndOfFrame();
        }

        playerCam.transform.localPosition = _originalHeight;

        _bobRoutine = null;

        yield return null;
    }
}
