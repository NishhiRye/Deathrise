using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialOrganization : MonoBehaviour
{
    public GameObject spawn1Gun;
    public GameObject spawn2Gun;
    public GameObject spawn3Gun;
         

    public GameObject spawn1Item;
    public GameObject spawn2Item;
    public GameObject spawn3Item;

    public GameObject spawnAWP;    

    public GameObject easyGun;
    public GameObject mediumGun;
    public GameObject expertGun;

    public GameObject item1;

    public GameObject TutorialLoadingCanvas;    



    void Awake()
    {
        
            
        

        if (WorldVariableCount.WorldVariable == -1 || WorldVariableCount.WorldVariable == 1 || WorldVariableCount.WorldVariable == 11 || WorldVariableCount.WorldVariable == 111)
        {
            Instantiate(easyGun, spawn1Gun.transform.position, Quaternion.identity);
            Instantiate(easyGun, spawn2Gun.transform.position, Quaternion.identity);
            Instantiate(easyGun, spawn3Gun.transform.position, Quaternion.identity);

            Instantiate(item1, spawn1Item.transform.position, Quaternion.identity);
            Instantiate(item1, spawn2Item.transform.position, Quaternion.identity);
            Instantiate(item1, spawn3Item.transform.position, Quaternion.identity);
        }
        
        
        if (WorldVariableCount.WorldVariable == -2 || WorldVariableCount.WorldVariable == 2 || WorldVariableCount.WorldVariable == 22 || WorldVariableCount. WorldVariable == 222 )
        {
            Instantiate(mediumGun, spawn1Gun.transform.position, Quaternion.identity);
            Instantiate(mediumGun, spawn2Gun.transform.position, Quaternion.identity);
            Instantiate(mediumGun, spawn3Gun.transform.position, Quaternion.identity);

            Instantiate(item1, spawn1Item.transform.position, Quaternion.identity);
            Instantiate(item1, spawn2Item.transform.position, Quaternion.identity);
            Instantiate(item1, spawn3Item.transform.position, Quaternion.identity);
        }
        
       if (WorldVariableCount.WorldVariable == -3 || WorldVariableCount.WorldVariable == 3 || WorldVariableCount.WorldVariable == 33 || WorldVariableCount.WorldVariable == 333)
        {
            
            Instantiate(expertGun, spawnAWP.transform.position, Quaternion.identity);
            

            Instantiate(item1, spawn1Item.transform.position, Quaternion.identity);
            Instantiate(item1, spawn2Item.transform.position, Quaternion.identity);
            Instantiate(item1, spawn3Item.transform.position, Quaternion.identity);
        }
        
      
    }

    private void Update()
    {
        if (WeaponVariable.TargetCount == 0)
        {


            if (WeaponVariable.secondsToStart <= 0)
            {
                WeaponVariable.RightHandEquiped = false;
                WeaponVariable.LeftHandEquiped = false;
                

                WeaponVariable.ThrowingKnifeCount = 0;
                WeaponVariable.gun = null;
                WeaponVariable.gun1 = null;
                WeaponVariable.gun2 = null;
                WeaponVariable.batteryReload = 100;
                TutorialLoadingCanvas.SetActive(true);
                SceneManager.LoadScene("Main");
            }

        }
    }



}
