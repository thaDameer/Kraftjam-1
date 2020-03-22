using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitSpots : MonoBehaviour
{
    public Transform[] orbitSpots;

    [SerializeField]
    private List<InteractableObjects> _objectList = new List<InteractableObjects>(); 

    private int _amountOfObjects = 0; 

    [SerializeField]
    private float _rotateSpeed = 30; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, _rotateSpeed * Time.deltaTime, 0);
    }

    void AddObjects(InteractableObjects stuff)
    {
        _objectList.Add(stuff); 

        _objectList[_amountOfObjects].transform.position = orbitSpots[_amountOfObjects].transform.position;

        _amountOfObjects++; 
    }

    void RemoveObjects()
    {
        _objectList.RemoveAt(_amountOfObjects);

        _amountOfObjects--; 
    }

}
