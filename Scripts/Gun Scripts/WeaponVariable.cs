using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class WeaponVariable : MonoBehaviourPunCallbacks
{

    public static bool RightHandEquiped = false;

    public static bool LeftHandEquiped = false;


    public static int TargetCount = 12;

    public static int reloadBullets;
    public static int ThrowingKnifeCount = 0;

    public static float secondsToStart = 10;

    public static bool WeaponCanvasVariable = false;

    public static float batteryReload = 100;  

    

    public static GameObject gun;

    public static GameObject gun1;

    public static GameObject gun2;

    public static GameObject ammoBox;
    public static GameObject item;
    public static GameObject loot;
    public static GameObject points;
    public static GameObject plate;


    public static GameObject ThrowingKnifeGO;

    public static GameObject interactableObj;





   

    //
    void Update()
    {
        
        // Debug.Log("A mao direita ta com arma? " + RightHandEquiped);
        // Debug.Log("A mao esquerda ta com arma? " + LeftHandEquiped);
        /*Debug.Log("A arma direita é " + gun1);
        Debug.Log("A arma esquerda é " + gun2);
        Debug.Log("Gun é " + gun);*/

    }
}
