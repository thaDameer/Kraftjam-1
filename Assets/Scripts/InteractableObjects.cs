using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjects : MonoBehaviour
{


    public enum ObjectState
    {
        STATIC, 
        SUCKING, 
        ORBIT
    }
    public ObjectState objectState;

    [SerializeField]
    private float _orbitDistanceMax = 4.0f;
    [SerializeField]
    private float _orbitSpeed = 20;
    [SerializeField]
    float suckSpeed = 1f;
    Rigidbody rigidbody;
    [SerializeField]
    Transform moon;
    Vector3 moonPosition;

    private void Awake()
    {
        objectState = ObjectState.STATIC;
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = true;
    }

    private void Update()
    {

        switch (objectState)
        {
            case ObjectState.STATIC:

                break;

            case ObjectState.SUCKING:
                if (this.moon)
                {
                    CheckForOtherObjects();
                    float orbitDist = Vector3.Distance(transform.position, this.moon.position);
                    if (orbitDist <= _orbitDistanceMax)
                    {
                        if (this.moon)
                        {
                            Orbit(this.moon);
                            objectState = ObjectState.ORBIT;
                        }
                    }
                    else
                    {
                        moonPosition = this.moon.position;
                        var dir = moon.transform.position - transform.position;
                        transform.position = Vector3.MoveTowards(transform.position, moonPosition, 10 * Time.deltaTime);
                        dir.y = moon.transform.position.y;
                        //transform.position = Vector3.Lerp(transform.position, dir, suckSpeed * Time.deltaTime);
                        // transform.position = new Vector3(transform.position.x, moon.position.y, transform.position.z);
                    }
                }
                else
                {
                    SwitchOnGravity();
                    objectState = ObjectState.STATIC;
                }
                break;

            case ObjectState.ORBIT:
                 
                break; 

        }
    }


    public void Suck(Transform moon)
    {
        this.moon = moon;
        rigidbody.mass = 5f;
        rigidbody.angularDrag = 4f;
        rigidbody.useGravity = false;
        //rigidbody.isKinematic = true;
        objectState = ObjectState.SUCKING;
    }

    void Orbit(Transform moon)
    {

        rigidbody.isKinematic = true;
        transform.RotateAround(moonPosition, moon.up, _orbitSpeed * Time.deltaTime); 
    }

    public void SwitchOnGravity()
    {
        rigidbody.mass = 1f;
        rigidbody.angularDrag = 0;
        rigidbody.isKinematic = false;
        rigidbody.useGravity = true;
        rigidbody.velocity = Vector3.zero;
    }

    void CheckForOtherObjects()
    {
        float radius = transform.localScale.x * 1.5f;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        if(hitColliders.Length != 0)
        {
            for (int i = 0; i < hitColliders.Length; i++)
            {
                if(hitColliders[i].tag == "Object")
                {
                    moonPosition.y += 0.5f;
                    Debug.Log(moonPosition.y);
                }
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Object" && this.moon)
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
            Debug.Log("HIT");
            moonPosition.y += 1f ;
        }
    }


}
