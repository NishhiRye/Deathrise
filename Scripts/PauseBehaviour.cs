using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseBehaviour : MonoBehaviour
{
    public GameObject pauseCanvas;

    bool isPaused = false;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                PauseGame();
            }
            else if (isPaused)
            {
                ResumeGame();
            }
        }
    }

    void PauseGame()
    {
        Cursor.lockState = CursorLockMode.None; 
        Cursor.visible = true;
        if (!MainManager.IsMultiplayer)
        {
            Time.timeScale = 0F;
        }
        isPaused = true;
        pauseCanvas.SetActive(true);
    }

    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1F;
        isPaused = false;
        pauseCanvas.SetActive(false);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu Scene");
    }

    public void ExitPause()
    {
        Application.Quit();
    }

    public void OptionsPause()
    {

    }
}