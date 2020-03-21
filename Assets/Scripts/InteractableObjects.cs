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
    private float _orbitDistance = 2.0f;
    [SerializeField]
    private float _orbitSpeed = 20;

    private void Awake()
    {
        objectState = ObjectState.STATIC; 
    }

    private void Update()
    {
        switch (objectState)
        {
            case ObjectState.STATIC:

                break;

            case ObjectState.SUCKING:
                
                break;

            case ObjectState.ORBIT:
                 
                break; 

        }
    }


    public void Suck(Transform moon)
    {
        objectState = ObjectState.SUCKING;

        
        
        if (Vector3.Distance(transform.position, moon.position) <= _orbitDistance)
        {
            objectState = ObjectState.ORBIT;
            Orbit(moon);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, moon.position, 5 * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, moon.position.y, transform.position.z);
        }

    }

    void Orbit(Transform moon)
    {
        
        transform.RotateAround(moon.position, moon.up, _orbitSpeed * Time.deltaTime); 
    }


}
