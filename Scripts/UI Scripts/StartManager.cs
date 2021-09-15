using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    //Finishes the loading and starts the game.
    private void Awake()
    {
        SceneManager.LoadScene("Main");
        Debug.Log("Passou pelo Loading");
    }    
}
