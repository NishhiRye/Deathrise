using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockCanvas : UnlockBehaviour
{
    public GameObject MagnumUnlockCanvas;
    public GameObject AWPUnlockCanvas;

    private static bool AWPCanvasDisabler = true;
        private static bool MagnumCanvasDisabler = true;
    void Awake()
    {
        if (MagnumUnlock && MagnumCanvasDisabler)
        {
            MagnumUnlockCanvas.SetActive(true);
            MagnumCanvasDisabler = false;
        }

        if (AWPVar && AWPCanvasDisabler)
        {
            AWPUnlockCanvas.SetActive(true);
            AWPCanvasDisabler = false;
        }
    }

    public void MagnumReady()
    {
        MagnumUnlockCanvas.SetActive(false);

    }  

    public void AWPReady()
    {
        AWPUnlockCanvas.SetActive(false);
    }

    
}
