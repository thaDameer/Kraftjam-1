using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonGun : MonoBehaviour
{
    public MoonScript moonPrefab;
    public PlayerLogic playerLogic;
  
    public float shootSpeed;
    [SerializeField]
    Transform shootPos;
    MoonScript activeMoon;
    bool holdingShootButton;
    bool hasShot;

    // Update is called once per frame
    void Update()
    {
        holdingShootButton = Input.GetButton("Fire1");
        if (Input.GetButtonDown("Fire1"))
        {
            ShootMoon();
        }
    }

    private void FixedUpdate()
    {
        if (hasShot && holdingShootButton)
        {
            activeMoon.MovingForward();
        }
    }

    void ShootMoon()
    {
        if (!activeMoon)
        {
            hasShot = true;
            activeMoon = Instantiate(moonPrefab, shootPos.transform.position, transform.rotation);
        }
    }
}
