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
  
}
