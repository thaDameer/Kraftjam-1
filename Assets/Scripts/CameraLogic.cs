using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLogic : MonoBehaviour
{

    Vector3 m_cameraTarget;
    GameObject m_player;
    public Camera mainCamera;

   
    float m_distanceZ = 5f;
    const float MIN_Z = 0;
    const float MAX_Z = 15;

    float m_rotationX;
    float m_rotationY;

    const float MIN_X = -10;
    const float MAX_X = 20;

    

    private void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        Cursor.visible = false;
    }

    private void Update()
    {
        transform.position = m_player.transform.position;
        
        if (Input.GetButton("Fire2"))
        {
          //  m_rotationY += Input.GetAxis("Mouse X");
            m_rotationX -= Input.GetAxis("Mouse Y");
            m_rotationX = Mathf.Clamp(m_rotationX, MIN_X, MAX_X);
        }
        m_distanceZ -= Input.GetAxis("Mouse ScrollWheel");
        m_distanceZ = Mathf.Clamp(m_distanceZ, MIN_Z, MAX_Z);
    }

    private void LateUpdate()
    {
        Quaternion cameraRotation = Quaternion.Euler(0, m_rotationY, 0);

        Vector3 cameraOffset = new Vector3(0, 0, -m_distanceZ);
        transform.position = cameraRotation * cameraOffset;
        transform.LookAt(m_cameraTarget);
        
    }
    public Vector3 GetForwardVector()
    {
        Quaternion rotation = Quaternion.Euler(0, m_rotationY, 0);
        return rotation * Vector3.forward;
    }
}
