using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseVisible : MonoBehaviour
{
   void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
