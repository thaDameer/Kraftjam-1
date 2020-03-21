using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonScript : MonoBehaviour
{
    float moveSpeed = 10f;
    public MoonGun moonGun;
    Rigidbody moonRb;

    private void Start()
    {
        moveSpeed = moonGun.shootSpeed;
        moonRb = GetComponent<Rigidbody>();
    }

    public void MovingForward()
    {
        moonRb.velocity = transform.forward * moveSpeed * Time.fixedTime;
    }
}
