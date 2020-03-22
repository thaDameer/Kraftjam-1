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
    private float scale = 5; 

    [SerializeField]
    private float _rotateSpeed = 30; 
    void Start()
    {
        for (int i = 0; i < orbitSpots.Length; i++)
        {
           var scalePos = new Vector3(orbitSpots[i].localPosition.x, orbitSpots[i].localPosition.y, orbitSpots[i].localPosition.z) * scale;

            orbitSpots[i].localPosition = scalePos; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, _rotateSpeed * Time.deltaTime, 0);
    }

    public void AddObjects(InteractableObjects stuff)
    {
        _objectList.Add(stuff);

        _objectList[_amountOfObjects].transform.position = orbitSpots[_amountOfObjects].transform.position;
        _objectList[_amountOfObjects].transform.parent = orbitSpots[_amountOfObjects].transform;

        _amountOfObjects++;

        Debug.Log("Added stuff supposedly");
    }

    void RemoveObjects()
    {
        _objectList.RemoveAt(_amountOfObjects);

        _amountOfObjects--; 
    }

}
