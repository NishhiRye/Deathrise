using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombiePointsCount : MonoBehaviour
{
    public GameObject player;

    Text pointsText;
    void Start()
    {
        pointsText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //pointsText.text = player.GetComponent<InteractionCanvas>().zombieMoneyPoints.ToString();
    }
}
