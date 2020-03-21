using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonScript : MonoBehaviour
{

    public MoonGun moonGun;
    public Rigidbody moonRb;
    Collider moonCol;

    private void Start()
    {
        moonCol = GetComponent<Collider>();
        moonRb = GetComponent<Rigidbody>();
        moonCol.enabled = false;
       
    }

    public void MovingForward(float moveSpeed, Vector3 aimDirection)
    {
        moonRb.AddForce(aimDirection.normalized * moveSpeed,ForceMode.Acceleration);
    }
    public void StopMoving()
    {
        moonRb.velocity = Vector3.zero;
       
        moonCol.enabled = true;
    }
}
