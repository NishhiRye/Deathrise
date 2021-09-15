

//



#region End Alpha Script
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndAlphaScript : MonoBehaviour
{
    //Canvas to set active or deactivate.
    public GameObject LoadingCanvas;
    public GameObject EndAlphaCanvas;


    //
    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    //Send you back to menu. 
    public void MenuEnd()
    {
        EndAlphaCanvas.SetActive(false);
        LoadingCanvas.SetActive(true);
        SceneManager.LoadScene("Menu Scene");
        Debug.Log("Voltou para o menu");
    }


    //Quit the game. 
    public void QuitEnd()
    {
        Application.Quit();
        Debug.Log("Quitou");
    }
}
#endregion