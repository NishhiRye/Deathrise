using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityLights : MonoBehaviour
{
    public GameObject lightGroup1;
    public GameObject lightGroup2;
    public GameObject lightGroup3;

    int lightValue;

    private void Start()
    {
        lightValue = Random.Range(1, 4);
        //Debug.Log(lightValue);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if(lightValue == 1)
                {
                    lightGroup1.SetActive(true);
                }
                else if (lightValue == 2)
                {
                    lightGroup2.SetActive(true);
                }
                else if (lightValue == 3)
                {
                    lightGroup3.SetActive(true);
                }
                else
                {
                    return;
                }
            }
        }
    }


}
