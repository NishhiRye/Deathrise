/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPick : WeaponVariable
{

    //Canvas with his components to be enabled, or disabled, if the player is close enough.
    public GameObject InteractionCanvasOnOff;

    //Place the game object with the script "Outline". 
    public GameObject ItemToBeOutlined;

    //
    public GameObject WeaponSpawner;

    //
    public GameObject Weapon;

    
    //
    public GameObject PlayerCam;

    public LayerMask RaycastWeapon;
    //
    public GameObject InteractionMechanic;



    // Update is called once per frame
    void Update()
    {
        if (RightHandEquiped == false)
        {

            RaycastHit cleitin;
            if (Physics.Raycast(PlayerCam.transform.position, PlayerCam.transform.forward, out cleitin, 100, RaycastWeapon))
            {
                if (cleitin.collider.tag == "WeaponToPick")
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {

                        Debug.Log("cleitin é " + gameObject.name);
                        //Fazer o sistema por raycast. 
                        Weapon.GetComponent<Rigidbody>().isKinematic = true;

                        Weapon.transform.position = WeaponSpawner.transform.position;
                        Weapon.transform.rotation = WeaponSpawner.transform.rotation;
                        Weapon.transform.parent = PlayerCam.transform;

                        InteractionCanvasOnOff.SetActive(false);
                        ItemToBeOutlined.GetComponent<Outline>().enabled = false;

                        Weapon.GetComponent<WeaponShoot>().enabled = true;
                        Weapon.GetComponent<WeaponThrow>().enabled = true;
                        Weapon.GetComponent<WeaponSway>().enabled = true;
                        InteractionMechanic.SetActive(false);


                        RightHandEquiped = true;

                    }
                }
            }

        }
    }
}
*/
