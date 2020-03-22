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
    public bool inMoonRange = false; 

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
                    float orbitDist = Vector3.Distance(transform.position, this.moon.position);

                    if(orbitDist <= _orbitDistanceMax)
                    {
                        objectState = ObjectState.ORBIT;
                    }
                    else
                    {
                        moonPosition = this.moon.position;
                        var dir = moon.transform.position - transform.position;
                        transform.position = Vector3.MoveTowards(transform.position, moonPosition, 30 * Time.deltaTime);
                    }
                }
                else
                {
                    
                    objectState = ObjectState.NORMALSTATE;
                    inMoonRange = false; 
                }
                break;
            case ObjectState.ORBIT:

                if (this.moon)
                {
                    if (!added)
                    {
                        SwitchOnGravity(false);

                        OrbitSpots orbit = moon.gameObject.GetComponentInChildren<OrbitSpots>();
                        orbit.AddObjects(this);
                        
                        added = true;
                    }
                }
                break; 
        }
    }


    public void Suck(Transform moon)
    {
        this.moon = moon;
        objectState = ObjectState.SUCKING;

        inMoonRange = true; 

        Debug.Log("Sucking"); 
    }

    private void Sucking()
    {
        // Jag vill ha allt som händer i respektive state i sina egna funktioner. Blir så rörigt annars
    }


    public void SwitchOnGravity(bool on)
    {
        if (on)
        {
            Debug.Log("Turning ON Gravity");
            rigidbody.mass = 1.0f;
            rigidbody.angularDrag = 0;
            rigidbody.isKinematic = false;
            rigidbody.useGravity = true;
            rigidbody.velocity = Vector3.zero;
        }
        if (!on)
        {
            Debug.Log("Turning Off Gravity");
            rigidbody.mass = 0;
            rigidbody.angularDrag = 0;
            rigidbody.isKinematic = true;
            rigidbody.useGravity = false;
            rigidbody.velocity = Vector3.zero;
        }
    }

    public void SetNormalState()
    {
        objectState = ObjectState.NORMALSTATE;
        inMoonRange = false;
        added = false;
        SwitchOnGravity(true);
    }

}
