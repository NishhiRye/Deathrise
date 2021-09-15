using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootCrateActivated : MonoBehaviour
{
    public GameObject lootParticle;

    public bool looted = false;

    public GameObject lootInstantiator;

    public GameObject AWP;
    int AWPChance;
    public GameObject glock;
    int glockChance;
    public GameObject magnum;
    int magnumChance;

    public GameObject ammo;    
    int ammoChance;

    public GameObject battery;    
    int batteryChance;

    public GameObject throwingKnife;
    int throwingKnifeChance;

    public GameObject zombiePoints;
    int zombiePointsChance;

    public GameObject plate;    
    int plateChance;


    public GameObject sliderToWarm;
    public Slider lootSlider;
    float startSliderValue = 1f;
    


    private void Start()
    {
        AWPChance = Random.Range(1,101);        

        glockChance = Random.Range(1,101);        

        magnumChance = Random.Range(1,101);        

        ammoChance = Random.Range(1,101);        

        batteryChance = Random.Range(1,101);

        throwingKnifeChance = Random.Range(1, 101);

        zombiePointsChance = Random.Range(1, 101);

        plateChance = Random.Range(1,101);


        lootSlider.value = startSliderValue;
    }

    public void Update()
    {
        sliderToWarm.SetActive(false);
    }
    public void WarmRedo()
    {
        lootSlider.value = 1f;
        sliderToWarm.SetActive(false);
    }
    public void WarmUpCrate()
    {
        sliderToWarm.SetActive(true);
        lootSlider.value -= 0.2f * Time.deltaTime;
        if (lootSlider.value <= 0)
        {
            StartCoroutine(LootTime());
        }
    }

    IEnumerator LootTime()
    {
        yield return new WaitForEndOfFrame();
            if(looted == false)
        {
            looted = true;
            LootInstantiate();
        }
            
    }
    public void LootInstantiate()
    {
        if(gameObject.GetComponent<LootCrateActivated>().enabled == true){
            if (AWPChance <= 5)
            {
                Instantiate(AWP, lootInstantiator.transform.position, Quaternion.identity);
            }



            if (glockChance <= 5)
            {
                Instantiate(glock, lootInstantiator.transform.position, Quaternion.identity);
            }



            if (magnumChance <= 5)
            {
                Instantiate(magnum, lootInstantiator.transform.position, Quaternion.identity);
            }

            if(throwingKnifeChance <= 25)
            {
                Instantiate(throwingKnife, lootInstantiator.transform.position, Quaternion.identity);
                if (throwingKnifeChance <= 15)
                {
                    Instantiate(throwingKnife, lootInstantiator.transform.position, Quaternion.identity);
                    if (throwingKnifeChance <= 5)
                    {
                        Instantiate(throwingKnife, lootInstantiator.transform.position, Quaternion.identity);
                    }
                }
            }

            if (ammoChance >= 1 && ammoChance <= 40)
            {
                Instantiate(ammo, lootInstantiator.transform.position, Quaternion.identity);
                if (ammoChance >= 1 && ammoChance <= 20)
                {
                    Instantiate(ammo, lootInstantiator.transform.position, Quaternion.identity);

                    if (ammoChance >= 1 && ammoChance <= 10)
                    {
                        Instantiate(ammo, lootInstantiator.transform.position, Quaternion.identity);
                    }
                }
            }



            if (batteryChance >= 1 && batteryChance <= 50)
            {
                Instantiate(battery, lootInstantiator.transform.position, Quaternion.identity);
                if (batteryChance >= 1 && batteryChance <= 25)
                {
                    Instantiate(battery, lootInstantiator.transform.position, Quaternion.identity);

                    if (batteryChance >= 1 && batteryChance <= 15)
                    {
                        Instantiate(battery, lootInstantiator.transform.position, Quaternion.identity);
                    }
                }
            }



            if (plateChance >= 1 && plateChance <= 30)
            {
                Instantiate(plate, lootInstantiator.transform.position, Quaternion.identity);
                if (plateChance >= 1 && plateChance <= 20)
                {
                    Instantiate(plate, lootInstantiator.transform.position, Quaternion.identity);

                    if (plateChance >= 1 && plateChance <= 10)
                    {
                        Instantiate(plate, lootInstantiator.transform.position, Quaternion.identity);
                    }
                }
            }

            if (zombiePointsChance >= 1 && zombiePointsChance <= 60)
            {
                Instantiate(zombiePoints, lootInstantiator.transform.position, Quaternion.identity);
                if (zombiePointsChance >= 1 && zombiePointsChance <= 40)
                {
                    Instantiate(zombiePoints, lootInstantiator.transform.position, Quaternion.identity);

                    if (zombiePointsChance >= 1 && zombiePointsChance <= 20)
                    {
                        Instantiate(zombiePoints, lootInstantiator.transform.position, Quaternion.identity);
                    }
                }
            }
            lootParticle.SetActive(false);
        }
        
    }
}
