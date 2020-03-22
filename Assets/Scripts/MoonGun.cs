using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonGun : MonoBehaviour
{
    public enum GunState
    {
        CanShoot,
        MoonIsMoving,
        MoonCanBeCalledBack
    }
    public GunState gunState;
    public MoonScript moonPrefab;
    public PlayerLogic playerLogic;
  
    public float shootSpeed;
    [SerializeField]
    Transform shootPos;
    [SerializeField]
    MoonScript activeMoon;
    [SerializeField]
    bool holdingShootButton;
    [SerializeField]
    bool hasShot;
    [SerializeField]
    bool buttonUp;
    [SerializeField]
    

    private void Awake()
    {
        gunState = GunState.CanShoot;
    }

    void Update()
    {
        holdingShootButton = Input.GetButton("Fire1");
        buttonUp = Input.GetButtonUp("Fire1");
        switch (gunState)
        {
            case GunState.CanShoot:
                if (Input.GetButtonDown("Fire1") && playerLogic.playerState == PlayerLogic.PlayerState.Aiming)
                {
                    ShootMoon();
                }
                break;

            case GunState.MoonIsMoving:
                if (buttonUp)
                {
                    activeMoon.StopMoving(this);
                    gunState = GunState.MoonCanBeCalledBack;
                }
                break;
            case GunState.MoonCanBeCalledBack:
                if (Input.GetKeyDown(KeyCode.C) && activeMoon)
                {
                    activeMoon.CallBackMoon();
                }
                break;
        }
        
    }

    private void FixedUpdate()
    {
        switch (gunState)
        {
            case GunState.CanShoot:

                break;

            case GunState.MoonIsMoving:
                    //MOON CAN BE MOVING
                    if(holdingShootButton && !hasShot)
                    {
                    activeMoon.MovingForward(shootSpeed, playerLogic.transform.forward);
                    }
                break;
        }
        

    }

    void ShootMoon()
    {
        if (!activeMoon)
        {
            activeMoon = Instantiate(moonPrefab, shootPos.transform.position, transform.rotation);
            var moonRb = activeMoon.GetComponent<Rigidbody>();
            moonRb.AddForce(transform.forward * 20f, ForceMode.Impulse);
            CameraScript.instance.CameraShakeZ();
            gunState = GunState.MoonIsMoving;
        }
    }

    public void MoonHasReturned()
    {
        //moon has retured and you can now shoot again
        //MAYBE play a litte particle effect?
        gunState = GunState.CanShoot;
    }
    
}
