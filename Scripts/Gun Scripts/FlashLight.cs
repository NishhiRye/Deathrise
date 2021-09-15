using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    public GameObject flashLight;
    public bool lightIsEnabled = false; 
    

    // Update is called once per frame
    void Update()
    {
        if (WeaponVariable.batteryReload > 0)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                if (lightIsEnabled)
                {
                    FlashLightOff();
                }
                else
                {
                    FlashLightOn();
                }

            }
        }
        else
        {
            flashLight.SetActive(false);
            lightIsEnabled = false;
        }
    }
    void FlashLightOn()
    {
        flashLight.SetActive(true);
        lightIsEnabled = true;
    }

    void FlashLightOff()
    {
        flashLight.SetActive(false);
        lightIsEnabled = false;
    }

}
