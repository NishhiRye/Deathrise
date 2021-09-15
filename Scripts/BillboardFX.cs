

//



#region Billboard FX
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardFX : MonoBehaviour
{
    public GameObject camTransform;
    

    Quaternion originalRotation;



    //
    void Start()
    {
        camTransform = GameObject.FindWithTag("MainCamera");
        originalRotation = transform.rotation;        
    }


    //
    void Update()
    {
        transform.rotation = camTransform.transform.rotation * originalRotation;
    }
}
#endregion