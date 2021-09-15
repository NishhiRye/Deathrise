using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasActivationByOutline : WeaponVariable
{
    public GameObject WeaponCanvas;
    // Start is called before the first frame update
        
    void Update()
    {        
        if(WeaponCanvasVariable == true)
        {
            GetComponent<Outline>().enabled = true;
            WeaponCanvas.SetActive(true);
        }
        
        if(WeaponCanvasVariable == false)
        {
            GetComponent<Outline>().enabled = false;
            WeaponCanvas.SetActive(false);
            GetComponent<CanvasActivationByOutline>().enabled = false;
            
        }         
        
    }
   
}
        
