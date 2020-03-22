using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjects : MonoBehaviour
{
    public enum ObjectState
    {
        NORMALSTATE, 
        SUCKING, 
        ORBIT
    }
    public ObjectState objectState;

    [SerializeField]
    private float _orbitDistanceMax = 2;
    [SerializeField]
    private float _orbitSpeed = 20;
    [SerializeField]
    float suckSpeed = 1f;
    Rigidbody rigidbody;
    [SerializeField]
    Transform moon;
    Vector3 moonPosition;

    private bool added = false; 

    private void Awake()
    {
        objectState = ObjectState.NORMALSTATE;
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = true;
    }

    private void Update()
    {

        switch (objectState)
        {
            case ObjectState.NORMALSTATE:

                break;

            case ObjectState.SUCKING:
                if (this.moon)
                {
                    CheckForOtherObjects();
                    float orbitDist = Vector3.Distance(transform.position, this.moon.position);

                    moonPosition = this.moon.position;
                    var dir = moon.transform.position - transform.position;
                    transform.position = Vector3.MoveTowards(transform.position, moonPosition, 30 * Time.deltaTime);


                    if(orbitDist <= _orbitDistanceMax)
                    {

                            objectState = ObjectState.ORBIT;
                             
                       
                    }

                }
                else
                {
                    SwitchOnGravity(true);
                    objectState = ObjectState.NORMALSTATE;
                }
                break;

            case ObjectState.ORBIT:

                if (this.moon)
                {
                    if (!added)
                    {
                        OrbitSpots orbit = moon.gameObject.GetComponentInChildren<OrbitSpots>();
                        orbit.AddObjects(this);
                        SwitchOnGravity(false);
                        added = true;
                    }

                }
                break; 
        }
    }


    public void Suck(Transform moon)
    {
        this.moon = moon;
        rigidbody.mass = 5f;
        rigidbody.angularDrag = 4f;
        rigidbody.useGravity = false;
        objectState = ObjectState.SUCKING;
    }

    void Orbit(Transform moon)
    {

        rigidbody.isKinematic = true;
        transform.RotateAround(moonPosition, moon.up, _orbitSpeed * Time.deltaTime); 
    }

    public void SwitchOnGravity(bool on)
    {
        if (on)
        {
            rigidbody.mass = 1f;
            rigidbody.angularDrag = 0;
            rigidbody.isKinematic = false;
            rigidbody.useGravity = true;
            rigidbody.velocity = Vector3.zero;
        }
        if (!on)
        {
            rigidbody.mass = 0;
            rigidbody.angularDrag = 0;
            rigidbody.isKinematic = true;
            rigidbody.useGravity = false;
            rigidbody.velocity = Vector3.zero;
        }
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

                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Object" && objectState == ObjectState.SUCKING)
        {
            InteractableObjects interactebleObj = GetComponent<InteractableObjects>();
            if (interactebleObj && interactebleObj.objectState == ObjectState.ORBIT)
            {
                objectState = ObjectState.ORBIT;
            }
        }
    }
}
