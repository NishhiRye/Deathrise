using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeCanvasFeedback : MonoBehaviour
{
    public GameObject Player;
   //Image lifeFeedback;
    //public float lifeAlpha;

    public GameObject lifePlate1;
    public GameObject lifePlate2;
    public GameObject lifePlate3;
    public GameObject lifePlate4;
    public GameObject lifePlate5;
    public GameObject lifePlate6;
    public GameObject lifePlate7;
    public GameObject lifePlate8;
    public GameObject lifePlate9;
    public GameObject lifePlate10;

    private void Start()
    {
        //lifeFeedback = GetComponent<Image>();
    }
    void Update()
    {
        if(Player.GetComponent<ReceiveDamage>().health == 1)
        {
            lifePlate1.SetActive(true);
            lifePlate2.SetActive(false);
            lifePlate3.SetActive(false);
            lifePlate4.SetActive(false);
            lifePlate5.SetActive(false);
            lifePlate6.SetActive(false);
            lifePlate7.SetActive(false);
            lifePlate8.SetActive(false);
            lifePlate9.SetActive(false);
            lifePlate10.SetActive(false);
        }
        else if (Player.GetComponent<ReceiveDamage>().health == 2)
        {
            lifePlate1.SetActive(true);
            lifePlate2.SetActive(true);
            lifePlate3.SetActive(false);
            lifePlate4.SetActive(false);
            lifePlate5.SetActive(false);
            lifePlate6.SetActive(false);
            lifePlate7.SetActive(false);
            lifePlate8.SetActive(false);
            lifePlate9.SetActive(false);
            lifePlate10.SetActive(false);
        }
        else if (Player.GetComponent<ReceiveDamage>().health == 3)
        {
            lifePlate1.SetActive(true);
            lifePlate2.SetActive(true);
            lifePlate3.SetActive(true);
            lifePlate4.SetActive(false);
            lifePlate5.SetActive(false);
            lifePlate6.SetActive(false);
            lifePlate7.SetActive(false);
            lifePlate8.SetActive(false);
            lifePlate9.SetActive(false);
            lifePlate10.SetActive(false);
        }
        else if (Player.GetComponent<ReceiveDamage>().health == 4)
        {
            lifePlate1.SetActive(true);
            lifePlate2.SetActive(true);
            lifePlate3.SetActive(true);
            lifePlate4.SetActive(true);
            lifePlate5.SetActive(false);
            lifePlate6.SetActive(false);
            lifePlate7.SetActive(false);
            lifePlate8.SetActive(false);
            lifePlate9.SetActive(false);
            lifePlate10.SetActive(false);
        }
        else if (Player.GetComponent<ReceiveDamage>().health == 5)
        {
            lifePlate1.SetActive(true);
            lifePlate2.SetActive(true);
            lifePlate3.SetActive(true);
            lifePlate4.SetActive(true);
            lifePlate5.SetActive(true);
            lifePlate6.SetActive(false);
            lifePlate7.SetActive(false);
            lifePlate8.SetActive(false);
            lifePlate9.SetActive(false);
            lifePlate10.SetActive(false);
        }
        else if (Player.GetComponent<ReceiveDamage>().health == 6)
        {
            lifePlate1.SetActive(true);
            lifePlate2.SetActive(true);
            lifePlate3.SetActive(true);
            lifePlate4.SetActive(true);
            lifePlate5.SetActive(true);
            lifePlate6.SetActive(true);
            lifePlate7.SetActive(false);
            lifePlate8.SetActive(false);
            lifePlate9.SetActive(false);
            lifePlate10.SetActive(false);
        }
        else if (Player.GetComponent<ReceiveDamage>().health == 7)
        {
            lifePlate1.SetActive(true);
            lifePlate2.SetActive(true);
            lifePlate3.SetActive(true);
            lifePlate4.SetActive(true);
            lifePlate5.SetActive(true);
            lifePlate6.SetActive(true);
            lifePlate7.SetActive(true);
            lifePlate8.SetActive(false);
            lifePlate9.SetActive(false);
            lifePlate10.SetActive(false);
        }
        else if (Player.GetComponent<ReceiveDamage>().health == 8)
        {
            lifePlate1.SetActive(true);
            lifePlate2.SetActive(true);
            lifePlate3.SetActive(true);
            lifePlate4.SetActive(true);
            lifePlate5.SetActive(true);
            lifePlate6.SetActive(true);
            lifePlate7.SetActive(true);
            lifePlate8.SetActive(true);
            lifePlate9.SetActive(false);
            lifePlate10.SetActive(false);
        }
        else if (Player.GetComponent<ReceiveDamage>().health == 9)
        {
            lifePlate1.SetActive(true);
            lifePlate2.SetActive(true);
            lifePlate3.SetActive(true);
            lifePlate4.SetActive(true);
            lifePlate5.SetActive(true);
            lifePlate6.SetActive(true);
            lifePlate7.SetActive(true);
            lifePlate8.SetActive(true);
            lifePlate9.SetActive(true);
            lifePlate10.SetActive(false);
        }
        else if (Player.GetComponent<ReceiveDamage>().health == 10)
        {
            lifePlate1.SetActive(true);
            lifePlate2.SetActive(true);
            lifePlate3.SetActive(true);
            lifePlate4.SetActive(true);
            lifePlate5.SetActive(true);
            lifePlate6.SetActive(true);
            lifePlate7.SetActive(true);
            lifePlate8.SetActive(true);
            lifePlate9.SetActive(true);
            lifePlate10.SetActive(true);
        }
        //lifeAlpha = 1/Player.GetComponent<ReceiveDamage>().health;
        //lifeFeedback.color = new Color(lifeFeedback.color.r, lifeFeedback.color.g, lifeFeedback.color.b, lifeAlpha);

    }
}
