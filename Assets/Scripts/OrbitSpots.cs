﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitSpots : MonoBehaviour
{
    public Transform[] orbitSpots;

    [SerializeField]
    private List<InteractableObjects> _objectList = new List<InteractableObjects>(); 
    [SerializeField]
    private int _amountOfObjects = 0;

    [SerializeField]
    private float scale = 5; 

    [SerializeField]
    private float _rotateSpeed = 30;

    public bool haveDroppedAll = false; 
    
    void Start()
    {
        for (int i = 0; i < orbitSpots.Length; i++)
        {
           var scalePos = new Vector3(orbitSpots[i].localPosition.x, orbitSpots[i].localPosition.y, orbitSpots[i].localPosition.z) * scale;

            orbitSpots[i].localPosition = scalePos; 
        }
    }

    void Update()
    {
        transform.Rotate(0, _rotateSpeed * Time.deltaTime, 0);
    }

    public void AddObjects(InteractableObjects stuff)
    {
        _objectList.Add(stuff);
        _objectList[_amountOfObjects].transform.position = orbitSpots[_amountOfObjects].transform.position;
        orbitSpots[_amountOfObjects].GetComponentInChildren<ParticleSystem>().Emit(10);
        _objectList[_amountOfObjects].transform.parent = orbitSpots[_amountOfObjects].transform;

        _amountOfObjects++;

        
    }

    public void ReleaseAllObjects()
    {
        transform.gameObject.SetActive(true);

        // TROR ATT VI KAN TA BORT 
        //StopCoroutine("ReleaseAllObjects_CR");  
        //StartCoroutine(ReleaseAllObjects_CR());
        foreach (InteractableObjects obj in _objectList)
        {
            obj.transform.parent = null;
            obj.SetNormalState();
            _amountOfObjects--;
            Debug.Log("Removed stuff");
            
        }


    }
    // TROR ATT VI KAN TA BORT 
    //IEnumerator ReleaseAllObjects_CR()  
    //{
    //    Debug.Log("START CR");
    //    for (int i = 0; i < orbitSpots.Length; i++)
    //    {
    //        InteractableObjects obj = orbitSpots[i].GetComponentInChildren<InteractableObjects>();
    //        if (obj)
    //        {
    //            obj.transform.parent = null;
    //        }
    //    } 
    //    yield return new WaitForSeconds(.1f);
    //}

}
