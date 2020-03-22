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

    [SerializeField]
    private List<InteractableObjects> _objectList = new List<InteractableObjects>();


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

            if (objectInRadius != null)
            {
                objectInRadius.Suck(transform); 
                if (!_objectList.Contains(objectInRadius))
                {
                    _objectList.Add(objectInRadius);
                }
            }
        }
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Object")
        {
            InteractableObjects objectInRadius = other.GetComponent<InteractableObjects>();
            objectInRadius.SwitchOnGravity(true);
           // _objectList.Remove(objectInRadius);


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
       
        moonCol.enabled = true;
    }
    
    public void CallBackMoon()
    {
        var dir = moonGun.transform.position - transform.position;
        for (int i = 0; i < _objectList.Count; i++)
        {
            _objectList[i].SwitchOnGravity(true);
        }
        moonRb.AddForce(dir.normalized * 20f, ForceMode.Impulse);
        StartCoroutine("ReturnMoon");
        StartCoroutine(ReturnMoon());
    }

    IEnumerator ReturnMoon()
    {
        bool isReturning = true; 

        while(isReturning)
        {
            
            var distance = Vector3.Distance(transform.position, moonGun.transform.position);
            var maxDist = 5f;
            var dir =  moonGun.transform.position - transform.position;
            moonRb.AddForce(dir.normalized * 10, ForceMode.Impulse);
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
        Destroy(gameObject);
    }
}
