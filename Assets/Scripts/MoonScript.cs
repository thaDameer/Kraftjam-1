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

    public OrbitSpots orbitSpots; 

    private void Start()
    {
        moonCol = GetComponent<Collider>();
        moonRb = GetComponent<Rigidbody>();
       
        player = PlayerLogic.instance.transform;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Object")
        {
            InteractableObjects objectInRadius = other.GetComponent<InteractableObjects>();

            if (objectInRadius != null && !objectInRadius.inMoonRange)
            {
                objectInRadius.Suck(transform);
            }
        }
    }


    public void MovingForward(float moveSpeed, Vector3 aimDirection)
    {
        moonRb.AddForce(aimDirection.normalized * moveSpeed,ForceMode.Acceleration);
    }
    public void StopMoving(MoonGun moonGun)
    {
        this.moonGun = moonGun;
        moonRb.velocity = Vector3.zero;
       
        moonCol.enabled = false;
    }
    
    public void CallBackMoon()
    {
        //RELEASE ALL THE OBJECTS IN ORBIT
        GetComponent<SphereCollider>().enabled = false;
        orbitSpots.ReleaseAllObjects();

            //var dir = moonGun.transform.position - transform.position;
            //moonRb.AddForce(dir.normalized * 20f, ForceMode.Impulse);
            StartCoroutine("ReturnMoon");
            StartCoroutine(ReturnMoon());
        
        
    }

    IEnumerator ReturnMoon()
    {
        bool isReturning = true;
        moonRb.constraints = RigidbodyConstraints.None;
        while (isReturning)
        {
            


            var distance = Vector3.Distance(transform.position, moonGun.transform.position);
            var maxDist = 2f;
            var dir =  moonGun.transform.position - transform.position;
            moonRb.velocity += dir.normalized * 5 * Time.fixedDeltaTime;
            //moonRb.AddForce(dir.normalized * 15, ForceMode.Acceleration);
            yield return new WaitForEndOfFrame();
            if(maxDist > distance)
            {
                CameraScript.instance.CameraShakeZ();
                moonRb.velocity = Vector3.zero;
                moonRb.angularVelocity = Vector3.zero;
                isReturning = false;
            }
        }

        

        //PLAY A PARTICLE EFFECT
        moonGun.MoonHasReturned();
        player.GetComponent<PlayerLogic>().MoonHasReturned();
       
            Destroy(gameObject);

        
    }
}
