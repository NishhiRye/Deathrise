using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoopReset : WeaponVariable
{
     void Awake()
    {
        RightHandEquiped = false;
        LeftHandEquiped = false;

        secondsToStart = 10;
        reloadBullets = 0;
        WeaponCanvasVariable = false;

        ThrowingKnifeCount = 0;
        TargetCount = 12;

    gun = null;
        gun1 = null;
        gun2 = null;
        WaveBehaviour.wave = 1;
        
        batteryReload = 100;


    }
}
