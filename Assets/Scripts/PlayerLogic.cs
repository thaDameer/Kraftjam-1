using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
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

    CharacterController m_characterController;
    Animator m_animator;
    [SerializeField]
    List<AudioClip> m_footstepSounds = new List<AudioClip>();
    AudioSource m_audioSource;
    GameObject m_camera;
    CameraLogic m_cameraLogic;

    void Start()
    {
        m_camera = Camera.main.gameObject;
        if (m_camera)
        {
            m_cameraLogic = m_camera.GetComponent<CameraLogic>();
        }
        m_audioSource = GetComponent<AudioSource>();
        m_characterController = GetComponent<CharacterController>();
        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        m_horizontalInput = Input.GetAxis("Horizontal");
        m_verticalInput = Input.GetAxis("Vertical");

        m_movementInput = new Vector3(m_horizontalInput, 0, m_verticalInput);

        if(Input.GetButtonDown("Jump") && m_characterController.isGrounded)
        {
            m_jump = true;
        }
        if (m_animator)
        {
            m_animator.SetFloat("HorizontalInput", m_horizontalInput);
            m_animator.SetFloat("VerticalInput", m_verticalInput);
        }
    }

    private void FixedUpdate()
    {
        if (m_jump)
        {
            m_heigthMovement.y = m_jumpHeight;
            m_jump = false;
        }
        if (m_cameraLogic && Mathf.Abs(m_horizontalInput) > 0.1f || Mathf.Abs(m_verticalInput) > 0.1f)
        {
            transform.forward = m_cameraLogic.GetForwardVector();
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
    public void PlayFootstepSound(int footIndex)
    {
        if(m_footstepSounds.Count > 0 && m_audioSource)
        {
            int randomNum = Random.Range(0, m_footstepSounds.Count -1);
            m_audioSource.PlayOneShot(m_footstepSounds[randomNum]);
            Debug.Log("footstep " + footIndex);
        }
    }
}
