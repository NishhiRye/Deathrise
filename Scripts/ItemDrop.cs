using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{

    public GameObject AmmoDrop;
    public GameObject BatteryDrop;

    int dropPercentage;
    // Start is called before the first frame update
    void Start()
    {
        dropPercentage = Random.Range(1, 5);
    }

    // Update is called once per frame
    private void OnDisable()
    {
        if (dropPercentage == 1)
        {
            Instantiate(AmmoDrop, gameObject.transform.position, gameObject.transform.rotation);
        }
        if (dropPercentage == 2)
        {
            Instantiate(BatteryDrop, gameObject.transform.position, gameObject.transform.rotation);
        }
    }
}
