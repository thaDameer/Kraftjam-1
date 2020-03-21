using System.Collections;
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

    float m_horizontalInput;
    float m_verticalInput;

    Vector3 m_movementInput;
    float m_jumpHeight = 0.4f;
    [SerializeField]
    float m_gravity = 0.45f;
    [SerializeField]
    float m_movementSpeed = 5f;
    [SerializeField]
    float m_mouseSensitivity; 

    Vector3 m_heigthMovement;
    Vector3 m_verticalMovement;
    Vector3 m_horizontalMovment;
    bool m_jump = false;

    public CharacterController m_characterController;

    void Update()
    {
        m_horizontalInput = Input.GetAxis("Horizontal");
        m_verticalInput = Input.GetAxis("Vertical");



        switch (playerState)
        {
            case PlayerState.Aiming:
                Aiming();
                Shoot(); 
                break;

            case PlayerState.Walking:

                break;

            case PlayerState.Dead:

                break;
        }

        m_movementInput = new Vector3(m_horizontalInput, 0, m_verticalInput);

        if(Input.GetButtonDown("Jump") && m_characterController.isGrounded)
        {
            m_jump = true;
        }

        if (Input.GetMouseButtonDown(1))
        {
           
            SwitchToAiming(); 
        }
        
    }

    private void FixedUpdate()
    {
        if (m_jump)
        {
            m_heigthMovement.y = m_jumpHeight;
            m_jump = false;
        }

        m_heigthMovement.y -= m_gravity * Time.deltaTime;
        m_verticalMovement = transform.forward * m_verticalInput * m_movementSpeed * Time.deltaTime;
        m_horizontalMovment = transform.right * m_horizontalInput * m_movementSpeed * Time.deltaTime;
        Vector3 movementVector = m_horizontalMovment + m_verticalMovement + m_horizontalMovment;
        
        m_characterController.Move(m_horizontalMovment + m_verticalMovement + m_heigthMovement);
        if (m_characterController.isGrounded)
        {
            m_heigthMovement.y = 0f;
        }
    }

    void Aiming()
    {
        transform.Rotate(0, Input.GetAxis("Mouse X") * Time.deltaTime * m_mouseSensitivity, 0);

        if (Input.GetMouseButtonUp(1))
        {
            SwitchToWalking();
           
        }
    }

    void SwitchToAiming()
    {
        m_movementSpeed = m_movementSpeed * 0.2f;
        playerState = PlayerState.Aiming;
        Debug.Log("Aiming"); 
    }

    void SwitchToWalking()
    {
        m_movementSpeed = m_movementSpeed / 0.2f;
        playerState = PlayerState.Walking;
        Debug.Log("Walking"); 
    }

    void Shoot()
    {
        if (Input.GetMouseButtonDown(0)) //Hold to position of projectile
        {
            Debug.Log("PEW");
        }
        
        if (Input.GetMouseButtonUp(0)) // Release to stop 
        {
            Debug.Log("STOP"); 
        }
    }

}
