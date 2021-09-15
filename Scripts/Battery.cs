using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : WeaponVariable
{
            
    void Update()
    {
        
        if (gameObject.GetComponent<FlashLight>().lightIsEnabled == true && batteryReload >= 0 )
        {
            batteryReload -= Time.deltaTime / 2;
        }
        else if(gameObject.GetComponent<FlashLight>().lightIsEnabled == false && batteryReload <= 100)
        {
            batteryReload += Time.deltaTime / 10;
        }

           // Debug.Log(batteryReload);

        batteryReload = Mathf.Clamp(batteryReload, 0f, 100f);
            
    }
}
