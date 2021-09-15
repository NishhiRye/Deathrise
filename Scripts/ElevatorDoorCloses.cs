using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorDoorCloses : MonoBehaviour
{
    public GameObject ElevatorLights;
    public GameObject ElevatorAnim;
    Animator anim;
    

     void Start()
    {
        anim = ElevatorAnim.GetComponent<Animator>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Invoke("LightsOut", 2.5f);
            anim.SetTrigger("DoorClosing");
        }
        
    }
    void LightsOut()
    {
        ElevatorLights.SetActive(false);
        Destroy(this);
    }
}
