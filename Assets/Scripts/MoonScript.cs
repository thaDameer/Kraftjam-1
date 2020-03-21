using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonScript : MonoBehaviour
{

    public MoonGun moonGun;
    public Rigidbody moonRb;
    Collider moonCol;
    Transform player;
    float maxShootDist = 20f;

    private void Start()
    {
        moonCol = GetComponent<Collider>();
        moonRb = GetComponent<Rigidbody>();
        moonCol.enabled = false;
        player = PlayerLogic.instance.transform;
    }

    public void MovingForward(float moveSpeed, Vector3 aimDirection)
    {
        moonRb.AddForce(aimDirection.normalized * moveSpeed,ForceMode.Acceleration);
    }
    public void StopMoving(MoonGun moonGun)
    {
        this.moonGun = moonGun;
        moonRb.velocity = Vector3.zero;
       
        moonCol.enabled = true;
    }
    
    public void CallBackMoon()
    {
        StartCoroutine("ReturnMoon");
        StartCoroutine(ReturnMoon());
    }

    IEnumerator ReturnMoon()
    {
        bool isReturning = true; 

        while(isReturning)
        {
            
            var distance = Vector3.Distance(transform.position, moonGun.transform.position);
            var maxDist = 3f;
            var dir =  moonGun.transform.position - transform.position;
            Debug.Log(distance);
            moonRb.AddForce(dir * 5, ForceMode.Acceleration);
            if(maxDist > distance)
            {
                isReturning = false;
            }
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("STOP MOON");
        moonRb.velocity = Vector3.zero;
        moonRb.angularVelocity = Vector3.zero;

        //PLAY A PARTICLE EFFECT
        moonGun.MoonHasReturned();
        Destroy(gameObject);
    }
}
