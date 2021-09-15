using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainBehaviour : MonoBehaviour
{
    public GameObject rain;    
    int rainChance;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Raining());
    }

    IEnumerator Raining()
    {
        yield return new WaitForSeconds(200);
        rainChance = Random.Range(1, 101);
        
        if(rainChance <= 20)
        {
            rain.SetActive(true);
        }
        else
        {
            rain.SetActive(false);
        }

        StartCoroutine(Raining());
    }
}
