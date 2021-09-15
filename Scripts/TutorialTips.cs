using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTips : MonoBehaviour
{
    public GameObject Commands;
    public GameObject Tip;
    public GameObject TipCanvas;
    private bool nextCanvas = false;

    private Animator transition_;

    void Start()
    {
        transition_ = GameObject.FindWithTag("Transition").GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && nextCanvas == false)
        {
            NextTip();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && nextCanvas == true)
        {
            EndTip();

        }
    }
    
    public void NextTip()
    {
        
            Commands.SetActive(false);
            Tip.SetActive(true);
        nextCanvas = true;
        
    }

    public void EndTip()
    {
        
            TipCanvas.SetActive(false);
        transition_.SetTrigger("isTransitioning");
           
        
    }
}
