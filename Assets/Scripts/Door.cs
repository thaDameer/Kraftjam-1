using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour
{

    public void OpenDoor()
    {
        transform.DORotate(new Vector3(0, 120, 0), 2); 
    }

    public void CloseDoor()
    {
        transform.DORotate(new Vector3(0, 0, 0), 2);
    }

}
