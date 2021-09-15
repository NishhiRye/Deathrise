

//




using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class WeaponThrow : MonoBehaviour
{

    //
    public float ThrowForce = 600;

    PhotonView PV;

    [HideInInspector]
    public bool RightHandToThrow = false;
    public bool LeftHandToThrow = false;

    void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.E) && RightHandToThrow) 
        {
            RightHandToThrow = false;
            PrimaryHandThrow();
        }

        if (Input.GetKeyDown(KeyCode.Q) && LeftHandToThrow)
        {
            LeftHandToThrow = false;
            SecondaryHandThrow();
        }
    }

    void PrimaryHandThrow()
    {
        
            if (GetComponent<FlashLight>() != null)
            {
                GetComponent<FlashLight>().enabled = false;
            }
            if (GetComponent<WeaponShoot>() != null)
            {
                GetComponent<WeaponShoot>().enabled = false;
            }
            GetComponent<WeaponThrow>().enabled = false;
            GetComponent<WeaponSway>().enabled = false;

            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().AddForce(-transform.right * ThrowForce);
            transform.parent = null;         
            
            
            
        

    }

    void SecondaryHandThrow()
    {                    
            if (GetComponent<FlashLight>() != null)
            {
                GetComponent<FlashLight>().enabled = false;
            }
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().AddForce(-transform.right * ThrowForce);
            transform.parent = null;

            if (GetComponent<WeaponShoot>() != null)
            {
                GetComponent<WeaponShoot>().enabled = false;
            }

            GetComponent<WeaponThrow>().enabled = false;
            GetComponent<WeaponSway>().enabled = false;                        
            
        
    }
}
