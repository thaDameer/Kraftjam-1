using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProvisoryMoonScript : MonoBehaviour
{
    public MoonGun moonGun;
    public Rigidbody moonRb;
    Collider moonCol;

    [SerializeField]
    private List<InteractableObjects> _objectList = new List<InteractableObjects>(); 

    private void Start()
    {
        moonCol = GetComponent<Collider>();
        moonRb = GetComponent<Rigidbody>();
        moonCol.enabled = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Object")
        {
            InteractableObjects objectInRadius = other.GetComponent<InteractableObjects>(); 

            if(objectInRadius != null)
            {
                objectInRadius.Suck(transform);
                if (!_objectList.Contains(objectInRadius))
                {
                    _objectList.Add(objectInRadius);
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Object")
        {
            InteractableObjects objectInRadius = other.GetComponent<InteractableObjects>();

            if (objectInRadius != null)
            {
                objectInRadius.Suck(transform);

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Object")
        {
            InteractableObjects objectInRadius = other.GetComponent<InteractableObjects>();

            _objectList.Remove(objectInRadius);
            
        }
    }


    public void MovingForward(float moveSpeed, Vector3 aimDirection)
    {
        moonRb.AddForce(aimDirection.normalized * moveSpeed, ForceMode.Acceleration);
    }
    public void StopMoving()
    {
        moonRb.velocity = Vector3.zero;

        moonCol.enabled = true;
    }
}
