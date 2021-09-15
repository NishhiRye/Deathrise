using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustBehaviour : MonoBehaviour
{
    public GameObject dust;
    bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O) && !isActive)
        {
            dust.SetActive(true);
            isActive = true;
        }
        else if (Input.GetKeyDown(KeyCode.O) && isActive)
        {
            dust.SetActive(false);
            isActive = false;
        }
    }
}
