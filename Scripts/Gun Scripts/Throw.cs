using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    public GameObject spawn;
    public GameObject ome;
    


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Instantiate(ome, spawn.transform.position, ome.transform.rotation);
            ome.GetComponent<Rigidbody>().AddForce(spawn.transform.forward * 500);
            ome.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
