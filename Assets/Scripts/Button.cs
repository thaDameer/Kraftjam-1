using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField]
    private int _openCondition;
    [SerializeField]
    private int _numberOfObjects = 0;
    [SerializeField]
    private bool _isOpen = false;
    [SerializeField]
    private Door door;

    private void Awake()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Object")
        {
            _numberOfObjects++; 

            if(_numberOfObjects >= _openCondition)
            {
                _isOpen = true;

                door.OpenDoor(); 
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Object")
        {
            _numberOfObjects--; 

            if(_numberOfObjects < _openCondition)
            {
                _isOpen = false;

                door.CloseDoor(); 
            }
        }
    }
}
