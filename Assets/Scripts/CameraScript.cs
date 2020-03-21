using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraScript : MonoBehaviour
{
    public static CameraScript instance;
    public Camera camera;
    public Transform playerTarget;
    public Vector3 playerOffset;
    private Vector3 followVector;

    [Header("Cam Shake Settings")]
    [SerializeField]
    Vector3 strenght;
    [SerializeField]
    int shakeVibrato;
    [SerializeField]
    float duration;

    float rotationY;
    float mouseXInput;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }


    void LateUpdate()
    {
        // followVector = new )
        if (Input.GetKeyDown(KeyCode.P))
        {
            CameraShakeXY();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            CameraShakeZ();
        }
        transform.position = Vector3.Lerp(transform.position, playerTarget.transform.position, 2 * Time.deltaTime);
    }

    public void CameraShakeXY()
    {
        camera.transform.DOShakePosition(duration, strength: strenght, vibrato: shakeVibrato, fadeOut: true);
    }
    public void CameraShakeZ()
    {
        Vector3 zShake = new Vector3(0, 0, -2);
        float shakeTime = 0.5f;
        camera.transform.DOShakePosition(shakeTime, strength: zShake, vibrato: shakeVibrato, fadeOut: true);
    }
    
}
