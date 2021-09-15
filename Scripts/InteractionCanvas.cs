

//This script was made with the purpose of enabling and disabling the Canvas with his components and the outline of the game object to be interacted.

//This script require a canvas to be enabled and disabled, an outline script, a game object to be interacted and a collider to do the magic.



#region Interaction Canvas
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionCanvas : WeaponVariable
{

    //
    public GameObject PlayerCam;


    public float CleitinsRange = 3.5f;

    public int gunBulletsToPick;
    //
    public GameObject PrimaryHandSpawner;

    public GameObject RightLanternSpawner;

    public GameObject SecondaryHandSpawner;

    public GameObject LeftLanternSpawner;

    public GameObject AWPSpawner;

    public int zombieMoneyPoints;


    //Compare if the game object is with the "Player" tag and if is inside the Collider, and then enable and disable the canvas and the outline.

    
    
    //
    void Update()
    {
        //Debug.Log(gun1);
        GunPick();
    }

    void GunPick()
    {
        RaycastHit cleitin;
        if (Physics.Raycast(PlayerCam.transform.position, PlayerCam.transform.forward, out cleitin, CleitinsRange))
        {

            /*if (cleitin.collider.CompareTag("WeaponToPick") || cleitin.collider.CompareTag("AWP") || cleitin.collider.CompareTag("Lantern"))
            {
                gun = cleitin.collider.gameObject;
                gun.GetComponent<CanvasActivationByOutline>().enabled = true;

                WeaponCanvasVariable = true;



                if (RightHandEquiped == false)
                {
                    PrimaryHandEquip();

                }

                if (LeftHandEquiped == false)
                {
                    SecondaryHandEquip();
                }

            }*/
            if (cleitin.collider.CompareTag("AmmoToPick") || cleitin.collider.CompareTag("Battery"))
            {
                ammoBox = cleitin.collider.gameObject;
                ammoBox.GetComponent<CanvasActivationByOutline>().enabled = true;


                WeaponCanvasVariable = true;
                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (cleitin.collider.CompareTag("AmmoToPick"))
                    {
                        reloadBullets += Random.Range(1, 21);
                        Destroy(ammoBox);
                    }
                    if (cleitin.collider.CompareTag("Battery"))
                    {
                        batteryReload += Random.Range(10, 31);
                        Destroy(ammoBox);
                    }
                }
            }

            else if (cleitin.collider.CompareTag("ThrowingKnifeToPick"))
            {
                ThrowingKnifeGO = cleitin.collider.gameObject;
                //ThrowingKnifeGO.GetComponent<CanvasActivationByOutline>().enabled = true;

                //WeaponCanvasVariable = true;
                if (Input.GetKeyDown(KeyCode.F))
                {
                    ThrowingKnifeCount++;
                    Destroy(ThrowingKnifeGO);
                }
            }

            else if (cleitin.collider.CompareTag("Interactable"))
            {
                interactableObj = cleitin.collider.gameObject;
                interactableObj.GetComponent<CanvasActivationByOutline>().enabled = true;
                WeaponCanvasVariable = true;

                if (Input.GetKeyDown(KeyCode.F))
                {
                    interactableObj = GameObject.FindGameObjectWithTag("DoorElevatorAnim");
                    interactableObj.GetComponent<Animator>().SetTrigger("IsOpening");
                }
            }
            else if (cleitin.collider.CompareTag("LootCrate"))
            {
                loot = cleitin.collider.gameObject;

                if (Input.GetKey(KeyCode.F) && loot.GetComponent<LootCrateActivated>().looted == false)
                {
                    loot.GetComponent<LootCrateActivated>().WarmUpCrate();
                    //loot.GetComponent<LootCrateActivated>().enabled = false;

                }
                else if (Input.GetKeyUp(KeyCode.F) && loot.GetComponent<LootCrateActivated>().looted == false)
                {
                    loot.GetComponent<LootCrateActivated>().WarmRedo();

                }
                
            }

            else if (cleitin.collider.CompareTag("LifePlate"))
            {
                plate = cleitin.collider.gameObject;
                if (Input.GetKeyDown(KeyCode.F))
                {
                    GetComponent<ReceiveDamage>().health += 1;
                    Destroy(plate);
                }
            }

            else if (cleitin.collider.CompareTag("GamePoints"))
            {
                points = cleitin.collider.gameObject;
                if (Input.GetKeyDown(KeyCode.F))
                {
                    zombieMoneyPoints += Random.Range(200, 801);
                    Destroy(points);
                }
            }
            else
            {
                plate = null;
                loot = null;
                interactableObj = null;
                ThrowingKnifeGO = null;
                ammoBox = null;
                item = null;
                gun = null;
                WeaponCanvasVariable = false;

            }

        }

    }

    void PrimaryHandEquip()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (gun.CompareTag("AWP"))
            {

                gun.GetComponent<Rigidbody>().isKinematic = true;

                gun.transform.position = AWPSpawner.transform.position;
                gun.transform.rotation = AWPSpawner.transform.rotation;
                gun.transform.parent = PlayerCam.transform;

                gun.GetComponent<WeaponShoot>().enabled = true;
                gun.GetComponent<WeaponThrow>().enabled = true;
                gun.GetComponent<WeaponSway>().enabled = true;

                gun1 = gun;
                gun = null;
                RightHandEquiped = true;                
            }
            else if (gun.CompareTag("Lantern"))
            {
                gun.GetComponent<FlashLight>().enabled = true;
                gun.GetComponent<Rigidbody>().isKinematic = true;

                gun.transform.position =  RightLanternSpawner.transform.position;
                gun.transform.rotation =  RightLanternSpawner.transform.rotation;
                gun.transform.parent = PlayerCam.transform;

                gun.GetComponent<WeaponThrow>().enabled = true;
                gun.GetComponent<WeaponSway>().enabled = true;

                gun1 = gun;
                gun = null;
                RightHandEquiped = true;
            }
            else
            {
                if (gun.GetComponent<FlashLight>() != null)
                {
                    gun.GetComponent<FlashLight>().enabled = true;
                }

                gun.GetComponent<Rigidbody>().isKinematic = true;

                gun.transform.position = PrimaryHandSpawner.transform.position;
                gun.transform.rotation = PrimaryHandSpawner.transform.rotation;
                gun.transform.parent = PlayerCam.transform;

                
                gun.GetComponent<WeaponShoot>().enabled = true;
                
                gun.GetComponent<WeaponThrow>().enabled = true;
                gun.GetComponent<WeaponSway>().enabled = true;

                gun1 = gun;
                gun = null;
                RightHandEquiped = true;
            }
        }
    }

    void SecondaryHandEquip()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (gun.CompareTag("WeaponToPick"))
            {
                if (gun.GetComponent<FlashLight>() != null)
                {
                    gun.GetComponent<FlashLight>().enabled = true;
                }

                gun.GetComponent<Rigidbody>().isKinematic = true;

                gun.transform.position = SecondaryHandSpawner.transform.position;
                gun.transform.rotation = SecondaryHandSpawner.transform.rotation;
                gun.transform.parent = PlayerCam.transform;

                
                    gun.GetComponent<WeaponShoot>().enabled = true;
                
                gun.GetComponent<WeaponThrow>().enabled = true;
                gun.GetComponent<WeaponSway>().enabled = true;

                gun2 = gun;
                gun = null;
                LeftHandEquiped = true;
            }

            else if (gun.CompareTag("Lantern"))
            {
                gun.GetComponent<FlashLight>().enabled = true;
                gun.GetComponent<Rigidbody>().isKinematic = true;

                gun.transform.position = LeftLanternSpawner.transform.position;
                gun.transform.rotation = LeftLanternSpawner.transform.rotation;
                gun.transform.parent = PlayerCam.transform;

                gun.GetComponent<WeaponThrow>().enabled = true;
                gun.GetComponent<WeaponSway>().enabled = true;

                gun2 = gun;
                gun = null;
                LeftHandEquiped = true;
            }
        }
    }
}
#endregion
