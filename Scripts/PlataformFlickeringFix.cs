using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformFlickeringFix : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(gameObject.transform);
        }
    }


    public void OnCollisionExit(Collision collision)
    {
        collision.gameObject.transform.parent = null;
    }
}
