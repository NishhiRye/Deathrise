using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialTargetCount : WeaponVariable
{

    
    ReceiveDamage enemy;


    
    
    // Start is called before the first frame update
    void Start()
    {
       
        enemy = gameObject.GetComponent<ReceiveDamage>();
        
        if (enemy.health <= 0 )
        {
            TargetCount -= 1;             
        }
    }

    

    
    
}
