﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
   
    public enum PlayerState
    {
        Walking,
        Aiming, 
        Dead
    }
    public PlayerState playerState;
    public static PlayerLogic instance;
    public MoonGun moonGun;
    float m_horizontalInput;
    float m_verticalInput;

    Vector3 m_movementInput;
    [SerializeField]
    float m_jumpHeight = 0.4f;
    [SerializeField]
    float m_gravity = 0.45f;
    float storedGravity;
    [SerializeField]
    float fallGravity = 1.4f;
    [SerializeField]
    float m_movementSpeed = 5f;
    [SerializeField]
    float m_mouseSensitivity = 500; 

    Vector3 m_heigthMovement;
    Vector3 m_verticalMovement;
    Vector3 m_horizontalMovment;

    bool m_jump = false;
    [SerializeField]
    float jumpTime = 0.5f;
    float jumpTimeCounter;
    bool jumpButtonHeld;
    bool aimButtonUp;
    bool aimButtonHeld;
    bool hasShot = false;

    public CharacterController m_characterController;
    private Rigidbody playerRb;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        storedGravity = m_gravity;
        playerRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        m_horizontalInput = Input.GetAxisRaw("Horizontal");
        m_verticalInput = Input.GetAxisRaw("Vertical");
        jumpButtonHeld = Input.GetButton("Jump");
        aimButtonUp = Input.GetMouseButtonUp(1);
        aimButtonHeld = Input.GetMouseButton(1);
        if (m_characterController.isGrounded)
        {
            m_gravity = storedGravity;
        }

        switch (playerState)
        {
            case PlayerState.Aiming:
                if (Input.GetMouseButtonDown(0))
                {
                    hasShot = true;
                }
                Aiming();
                break;
            case PlayerState.Walking:
                if(m_horizontalInput != 0 || m_verticalInput != 0)
                {
                    TurnToWalkingDirection();
                }
                break;
            case PlayerState.Dead:

                break;
        }

        m_movementInput = new Vector3(m_horizontalInput, 0, m_verticalInput);

        if(Input.GetButtonDown("Jump") && m_characterController.isGrounded)
        {
            jumpTimeCounter = jumpTime;
            m_jump = true;
        }

        if (Input.GetMouseButtonDown(1))
        {
            SwitchToAiming(); 
        }
        
    }

    private void FixedUpdate()
    {
        Vector3 movementVector = new Vector3(m_horizontalInput, 0, m_verticalInput).normalized * m_movementSpeed * Time.deltaTime;

        playerRb.MovePosition(transform.position + movementVector );
        
    }
    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    void Aiming()
    {
        if (aimButtonUp)
        {
            SwitchToWalking();
            return;
        }
        if (!hasShot)
        {
            Ray ray = CameraScript.instance.camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit,Mathf.Infinity))
            {
                var hitPos = hit.point;
                var targetRotation = Quaternion.LookRotation(hitPos - transform.position);
                targetRotation.x = 0;
                targetRotation.z = 0;

                transform.rotation = targetRotation;// Quaternion.Slerp(transform.rotation, targetRotation,10);
            }

        }
        //else
        //{
        //    transform.rotation = Quaternion.Euler(transform.eulerAngles);
        //}

        //OLD ROTATION MOVEMENT

        //transform.Rotate(0, Input.GetAxis("Mouse X") * Time.deltaTime * m_mouseSensitivity, 0);
        //if (Input.GetMouseButtonUp(1))
        //{
        //    SwitchToWalking();
           
        //}
  

    }
    public void MoonHasReturned()
    {
        hasShot = false;
    }

    void SwitchToAiming()
    {
        m_movementSpeed = m_movementSpeed * 0.2f;
        playerState = PlayerState.Aiming;
    }

    void SwitchToWalking()
    {
        m_movementSpeed = m_movementSpeed / 0.2f;
        playerState = PlayerState.Walking;
    }

    void TurnToWalkingDirection()
    {
        if (!aimButtonHeld)
        {
            var rot = Quaternion.LookRotation(new Vector3(m_horizontalInput, 0, m_verticalInput));
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, 15 * Time.deltaTime);
        }

    }
}
