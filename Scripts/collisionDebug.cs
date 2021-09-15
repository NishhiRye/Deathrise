using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionDebug : MonoBehaviour
{
    
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
        Destroy(other.gameObject);
        }
        
    }

    void Update()
    {
        //Debug.Log(this.transform.position);
    }
}
