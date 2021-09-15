
/*
//This Script sets a canvas text component existent in the Player game object with the data of the bullets and the reaload bullets in each gun.


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    
public class AmmoCount : WeaponVariable
{

    private Text uiText_;    
      


    //
    void Start()        
    {

        //Gets the text component and stores it in the variable uitext.
        uiText_ = GetComponent<Text>();             
    }


    //
    void Update()    
    {

        if (uiText_ != null)
        {
            //Enable the ammo text component for the primary gun (gun1) if the gun1 is in hand and then sets the text to be the number of bullets and the reload bullets.
            if (CompareTag("AmmoPrimary") && gun1 !=null && gun1.CompareTag("WeaponToPick"))
            {
                uiText_.enabled = true;
                uiText_.text = WeaponShoot.bulletsInCanvasGun1.ToString() +" / " + reloadBullets.ToString();                

            }
            

            else if (CompareTag("Battery Right") && gun1 != null )
            {
                if (gun1.CompareTag("Lantern"))
                {
                    uiText_.enabled = true;
                    uiText_.text = WeaponVariable.batteryReload.ToString("F0");
                }
                else if(gun1.CompareTag("WeaponToPick") && gun1.GetComponent<FlashLight>() != null)                        
                {
                    uiText_.enabled = true;
                    uiText_.text = WeaponVariable.batteryReload.ToString("F0");
                }

            }

            //Enable the ammo text component for the secondary gun (gun2) if the gun2 is in hand and then sets the text to be the number of bullets and the reload bullets.
            else if (CompareTag("AmmoSecondary") && gun2 != null && gun2.CompareTag("WeaponToPick"))
            {
                uiText_.enabled = true;
                uiText_.text = WeaponShoot.bulletsInCanvasGun2.ToString() + " / " + reloadBullets.ToString();
            }
            else if (CompareTag("BatteryLeft") && gun2 != null )
            {
                if (gun2.CompareTag("Lantern"))
                {
                    uiText_.enabled = true;
                    uiText_.text = WeaponVariable.batteryReload.ToString("F0");
                }
                else if (gun2.CompareTag("WeaponToPick") && gun2.GetComponent<FlashLight>() != null)
                {
                    uiText_.enabled = true;
                    uiText_.text = WeaponVariable.batteryReload.ToString("F0");
                }
            }
            else if (CompareTag("AmmoPrimary") && gun1 != null && gun1.CompareTag("AWP"))
            {
                uiText_.enabled = true;
                uiText_.text = WeaponShoot.bulletsInCanvasGun1.ToString() + " / " + reloadBullets.ToString();
            }

            //If there is no gun equiped there will be no text enabled. 
            else
            {
                uiText_.enabled = false;
            }
        }
    }    
}*/
