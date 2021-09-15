using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBarBehaviour : MonoBehaviour
{
    Slider staminaSlider;
    float maxStamina;
    public float currentStamina;
    //bool staminaRestore = true;
    //bool staminaReady = true;
    void Start()
    {
        staminaSlider = GameObject.FindWithTag("Stamina Slider").GetComponent<Slider>();
        //staminaSlider.maxValue = maxStamina;
        staminaSlider.value = currentStamina;
        currentStamina = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.LeftShift)) //&& staminaReady == true)
        {
            currentStamina -= 0.1f * Time.deltaTime;
            staminaSlider.value = currentStamina;
            //Debug.Log(currentStamina);
        }
        else //if(staminaRestore == true)
        {
            currentStamina += 0.03f * Time.deltaTime;
            staminaSlider.value = currentStamina;
        }
        /*else if(currentStamina <= 0)
        {
            staminaReady = false;
            staminaRestore = false;
            StartCoroutine(staminaDelayToRestore());
            StartCoroutine(staminaDelayToBeReady());
        }*/
        currentStamina = Mathf.Clamp(currentStamina, 0f, 1f);
    }/*
    IEnumerator staminaDelayToBeReady()
    {
        yield return new WaitForSeconds(3f);
        staminaReady = true;
    }
    IEnumerator staminaDelayToRestore()
    {
        yield return new WaitForSeconds(1f);
        staminaRestore = true;
    }*/
    
}
