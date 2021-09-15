using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCountdown : MonoBehaviour
{
    public GameObject countdownCanvas;
    public Text countdownText;
  

    // Update is called once per frame
    void Update()
    {
        if (TutorialTargetCount.TargetCount == 0)
        {
            if (TutorialTargetCount.secondsToStart >= 0)
            {


                TutorialTargetCount.secondsToStart -= Time.deltaTime;
                    countdownCanvas.SetActive(true);
                countdownText.text = TutorialTargetCount.secondsToStart.ToString("F0");

            }
        }
    }
}
