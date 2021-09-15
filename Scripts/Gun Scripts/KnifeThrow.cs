using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeThrow : WeaponVariable
{
    public GameObject knifeToThrow;
    public GameObject knifeSpawn;
    public float ThrowForce;
    public float torqueForce;

    public GameObject ThrowingKnifeCountCanvas1;
    public GameObject ThrowingKnifeCountCanvas2;
    public GameObject ThrowingKnifeCountCanvas3;


    

    // Update is called once per frame
    void FixedUpdate()
    {        
        if(WeaponVariable.LeftHandEquiped == false)
        {
        StartCoroutine(ThrowingKnife());
        }        

        if(ThrowingKnifeCount == 3)
        {
            ThrowingKnifeCountCanvas1.SetActive(true);
            ThrowingKnifeCountCanvas2.SetActive(true);
            ThrowingKnifeCountCanvas3.SetActive(true);
        }
        else if(ThrowingKnifeCount == 2)
        {
            ThrowingKnifeCountCanvas1.SetActive(true);
            ThrowingKnifeCountCanvas2.SetActive(true);
            ThrowingKnifeCountCanvas3.SetActive(false);
        }
        else if (ThrowingKnifeCount == 1)
        {
            ThrowingKnifeCountCanvas1.SetActive(true);
            ThrowingKnifeCountCanvas2.SetActive(false);
            ThrowingKnifeCountCanvas3.SetActive(false);
        }
        else if (ThrowingKnifeCount == 0)
        {
            ThrowingKnifeCountCanvas1.SetActive(false);
            ThrowingKnifeCountCanvas2.SetActive(false);
            ThrowingKnifeCountCanvas3.SetActive(false);
        }
    }


    IEnumerator ThrowingKnife()
    {
        yield return new WaitForSeconds(0.000001f);

        if (Input.GetKeyDown(KeyCode.Q) && ThrowingKnifeCount > 0 && WeaponVariable.LeftHandEquiped == false)
        {
            GameObject knifeInstance = Instantiate(knifeToThrow, knifeSpawn.transform.position, Quaternion.LookRotation(knifeToThrow.transform.forward));            
            knifeInstance.GetComponent<Rigidbody>().isKinematic = false;
            knifeInstance.GetComponent<Rigidbody>().AddForce(knifeSpawn.transform.forward * ThrowForce, ForceMode.Impulse);
            knifeInstance.GetComponent<Rigidbody>().AddTorque(knifeSpawn.transform.right  * torqueForce, ForceMode.Impulse);
            knifeInstance.transform.parent = null;            
            ThrowingKnifeCount--;
            Debug.Log("Right hand é boa" + gun1);
        }                
    }
       
}
